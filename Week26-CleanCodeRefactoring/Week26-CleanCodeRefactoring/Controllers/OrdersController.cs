using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Week26_CleanCodeRefactoring.Models;

using DaveCoBusinessObjects;
using System.Text;

namespace Week26_CleanCodeRefactoring.Controllers
{
    public class OrdersController : Controller
    {
        private ShopContext db = new ShopContext();
        
        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.PaymentCard);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.PaymentCardId = new SelectList(db.PaymentCards, "PaymentCardId", "CardNumber");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,DateCreated,DateDispatched,PaymentCardId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PaymentCardId = new SelectList(db.PaymentCards, "PaymentCardId", "CardNumber", order.PaymentCardId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Where(o => o.OrderId == id).Include(o => o.OrderItems).First();
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.PaymentCardId = new SelectList(db.PaymentCards, "PaymentCardId", "CardNumber", order.PaymentCardId);
            return View(order);
        }


        // Note - this method has been written REALLY BADLY on purpose, so that we can take a look at some of the refactoring tools
        // ... and learn a little bit about how to go about cleaning dirty code up.

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,CustomerId,DateCreated,DateDispatched,PaymentCardId")] Order order)
        {

            OrderConfirmation orderConfirmation = new OrderConfirmation();
            orderConfirmation.OrderId = order.OrderId;

            if (ModelState.IsValid)
            {
                
                order.OrderItems = db.OrderItems.Where(oi => oi.OrderId == order.OrderId).ToList<OrderItem>();
                order.PaymentCard = db.PaymentCards.Where(pc => pc.PaymentCardId == order.PaymentCardId).First<PaymentCard>();
                order.Customer = db.Customers.Where(c => c.CustomerId == order.CustomerId).First<Customer>();

                orderConfirmation.OrderTotal = order.getTotalValue();
                
                if (ProcessPayment(order).Equals("PAYMENT FAILURE"))
                {
                    orderConfirmation.PaymentOutcome = "Payment failed";
                    return RedirectToAction("Confirm", new { id = order.OrderId, outcome = "failure" });
                }

                orderConfirmation.PaymentOutcome = "Payment successful";
                    
                orderConfirmation.WarehouseNotificationOutcome = SendWarehouseMessage(order);

                orderConfirmation.CustomerEmailNotificationOutcome = SendCustomerOrderMessage(order);

                try
                {
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                    orderConfirmation.DatabaseUpdateOutcome = "Our database has been updated to confirm your order";
                }
                catch(Exception ex)
                {
                    orderConfirmation.DatabaseUpdateOutcome = "Unfortunately there was an error saving your order in our database. Our customer service representatives know that this error occurred and will process your order. Please call 01112 223344 and they will confirm your order was successful.";
                    // you would log ex.Message here, of course.                    
                }

                orderConfirmation.Outcome = "success";
                return RedirectToAction("Confirm", orderConfirmation);
 
            }
            ViewBag.PaymentCardId = new SelectList(db.PaymentCards, "PaymentCardId", "CardNumber", order.PaymentCardId);
            return View(order);
        }


        private string SendWarehouseMessage(Order order)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("A new order has arrived" + Environment.NewLine);
            sb.Append(order.printOrderItems());

            Message warehouseMessage = new Message
            {
                To = "brian.mcbrian@daveco.co.uk",
                From = "weborders@daveco.co.uk",
                Subject = "New Order",
                Body = sb.ToString()
            };

            if (SendMessage(warehouseMessage))
            {
                return "Our warehouse has been notified and will commence packing and sending your order";
            }
            else
            {
                return "An error occurred when notifying the warehouse of your order. Please call customer services on 01112 223344.";
            };
        }
        
        private string SendCustomerOrderMessage(Order order)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Thankyou for placing an order with us!" + Environment.NewLine);
            sb.Append(order.printOrderItems());

            Message customerConfirmationMessage = new Message
            {
                To = order.Customer.Email,
                From = "customer.services@daveco.co.uk",
                Subject = "Thanks for placing an order with us",
                Body = sb.ToString()
            };

            if (SendMessage(customerConfirmationMessage))
            {
                return string.Format("An email confirming your order has been sent to {0}", order.Customer.Email);
            }
            else
            {
                return "We were unable to send you a confirmation email. Please call Please call customer services on 01112 223344 and a customer services representative will send you confirmation personally.";
            };
        }

        private bool SendMessage(Message message)
        {
            MailSender mailSender = new MailSender();

            return mailSender.SendMail(message.To, message.From, message.Subject, message.Body);
        }

        private static string ProcessPayment(Order order)
        {
            PaymentGateway gateway = new PaymentGateway();
            string customerName = string.Format("{0} {1}", order.Customer.FirstName, order.Customer.LastName);
            string response = gateway.Authorise(order.getTotalValue(), customerName, order.PaymentCard.CardNumber, order.PaymentCard.CVV, order.PaymentCard.ExpiryDate);
            return response;
        }


        public ActionResult Confirm(OrderConfirmation confirmation)
        {
            if (confirmation.OrderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(confirmation.OrderId);
            if (order == null)
            {
                return HttpNotFound();
            }

            ViewBag.Confirmation = confirmation;

            return View(order);
        }


        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

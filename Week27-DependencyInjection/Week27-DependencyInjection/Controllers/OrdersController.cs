using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Text;

using Week27_DependencyInjection.Models;
using Week27_DependencyInjection.Interfaces;
using Week27_DependencyInjection.Services.PaymentProcessors;
using Week27_DependencyInjection.Services.MessagingServices;


namespace Week27_DependencyInjection.Controllers
{
    public class OrdersController : Controller
    {
        private IMessageService _messageService;
        private IPaymentProcessor _paymentProcessor;
        
        private IShopContext _db;
        
        public OrdersController(IMessageService messagingService, IPaymentProcessor paymentProcessor, IShopContext db)
        {

            _messageService = messagingService;
            _paymentProcessor = paymentProcessor;
            _db = db;
        }


        // GET: Orders
        public ActionResult Index()
        {
            var orders = _db.Orders.Include(o => o.Customer).Include(o => o.PaymentCard);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(_db.Customers, "CustomerId", "FirstName");
            ViewBag.PaymentCardId = new SelectList(_db.PaymentCards, "PaymentCardId", "CardNumber");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,CustomerId,DateCreated,DateDispatched,PaymentCardId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _db.Orders.Add(order);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(_db.Customers, "CustomerId", "FirstName", order.CustomerId);
            ViewBag.PaymentCardId = new SelectList(_db.PaymentCards, "PaymentCardId", "CardNumber", order.PaymentCardId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(_db.Customers, "CustomerId", "FirstName", order.CustomerId);
            ViewBag.PaymentCardId = new SelectList(_db.PaymentCards, "PaymentCardId", "CardNumber", order.PaymentCardId);
            return View(order);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,CustomerId,DateCreated,DateDispatched,PaymentCardId")] Order order)
        {

            OrderConfirmation orderConfirmation = new OrderConfirmation();
            orderConfirmation.OrderId = order.OrderId;

            if (ModelState.IsValid)
            {

                order.OrderItems = _db.OrderItems.Where(oi => oi.OrderId == order.OrderId).ToList<OrderItem>();
                order.PaymentCard = _db.PaymentCards.Where(pc => pc.PaymentCardId == order.PaymentCardId).First<PaymentCard>();
                order.Customer = _db.Customers.Where(c => c.CustomerId == order.CustomerId).First<Customer>();

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
                    _db.SetModified(order);
                    _db.SaveChanges();
                    orderConfirmation.DatabaseUpdateOutcome = "Our database has been updated to confirm your order";
                }
                catch (Exception ex)
                {
                    orderConfirmation.DatabaseUpdateOutcome = "Unfortunately there was an error saving your order in our database. Our customer service representatives know that this error occurred and will process your order. Please call 01112 223344 and they will confirm your order was successful.";
                    // you would log ex.Message here, of course.                    
                }

                orderConfirmation.Outcome = "success";
                return RedirectToAction("Confirm", orderConfirmation);

            }
            ViewBag.PaymentCardId = new SelectList(_db.PaymentCards, "PaymentCardId", "CardNumber", order.PaymentCardId);
            return View(order);
        }


        /// <summary>
        /// These methods are NOT correct as they contain all sorts of "magic data" such as people's email addresses etc.
        /// In reality, you would keep such data in a configuration file (or possibly even in a configuration table in the database, 
        /// so it could be changed without having to rebuild and redeploy the website).
        /// 
        /// Also - you'd never do something as dopey as sending an important email just to one person in the warehouse (called Brian), would you?
        /// </summary>

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
            return _messageService.SendMessage(message);
            
        }

        private string ProcessPayment(Order order)
        {
            
            _paymentProcessor.CardNumber = order.PaymentCard.CardNumber;
            _paymentProcessor.CardHolderName = string.Format("{0} {1}", order.Customer.FirstName, order.Customer.LastName);
            _paymentProcessor.CardExpiryDate = order.PaymentCard.ExpiryDate;
            _paymentProcessor.CVV = order.PaymentCard.CVV;
            _paymentProcessor.PaymentAmount = order.getTotalValue();

            _paymentProcessor.processPayment();
            return _paymentProcessor.ResponseCode;

       }

        public ActionResult Confirm(OrderConfirmation confirmation)
        {
            if (confirmation.OrderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _db.Orders.Where(o => o.OrderId == confirmation.OrderId).First();
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
            Order order = _db.Orders.Find(id);
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
            Order order = _db.Orders.Find(id);
            _db.Orders.Remove(order);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

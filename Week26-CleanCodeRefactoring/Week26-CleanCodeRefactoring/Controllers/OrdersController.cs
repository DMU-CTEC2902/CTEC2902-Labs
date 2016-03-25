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
            
            if (ModelState.IsValid)
            {
                // User the OrderId to repopulate order data from the database
                order.OrderItems = db.OrderItems.Where(oi => oi.OrderId == order.OrderId).ToList<OrderItem>();
                order.PaymentCard = db.PaymentCards.Where(pc => pc.PaymentCardId == order.PaymentCardId).First<PaymentCard>();
                order.Customer = db.Customers.Where(c => c.CustomerId == order.CustomerId).First<Customer>();

                // Work out the amount for the order

                decimal orderTotal = 0.00m;

                foreach(OrderItem item in order.OrderItems) {
                    orderTotal += item.getPrice();
                }
                
                // Authorise the payment for the order

                PaymentGateway gateway = new PaymentGateway();
                string customerName = string.Format("{0} {1}", order.Customer.FirstName, order.Customer.LastName);

                string response = gateway.Authorise(orderTotal, customerName, order.PaymentCard.CardNumber, order.PaymentCard.CVV, order.PaymentCard.ExpiryDate);

                // If the payment fails, forward the user to confirmation with an error message

                if(response.Equals("PAYMENT FAILURE"))
                {
                    return RedirectToAction("Confirm", new { id = order.OrderId, outcome = "failure" });
                }
                else 
                {
                    // The order was successful so email the warehouse

                    MailSender mailSender = new MailSender();
                    
                    string toEmailWarehouse = "brian.mcbrian@daveco.co.uk";
                    string fromEmailWarehouse = "weborders@daveco.co.uk";
                    string emailSubjectWarehouse = "New Order";
                    
                    StringBuilder sb = new StringBuilder();
                    
                    sb.Append("A new order has been received:" + Environment.NewLine);

                    foreach(OrderItem item in order.OrderItems)
                    {
                        sb.Append(string.Format("Item name: {0} - Price: {1} - Quantity: {2} - Total Cost: {3}{4}",
                            item.Product.Name,
                            item.Product.Price,
                            item.Quantity,
                            item.getPrice(),
                            Environment.NewLine));
                    }
                    
                    string emailBodyWarehouse = sb.ToString();

                    if(!mailSender.SendMail(toEmailWarehouse, fromEmailWarehouse, emailSubjectWarehouse, emailBodyWarehouse))
                    {
                        return RedirectToAction("Confirm", new { id = order.OrderId, outcome = "failure" });
                    }

                    // Then email the customer (note - going to have to add a customer class...)

                    string toEmailCustomer = order.Customer.Email;
                    string fromEmailCustomer = "customer.services@daveco.co.uk";
                    string emailSubjectCustomer = "Thanks for placing an order with us";

                    sb.Clear();

                    sb.Append("Thankyou for placing an order with DaveCo. Your order details are:" + Environment.NewLine);

                    foreach (OrderItem item in order.OrderItems)
                    {
                        sb.Append(string.Format("Item name: {0} - Price: {1} - Quantity: {2} - Total Cost: {3}{4}",
                            item.Product.Name,
                            item.Product.Price,
                            item.Quantity,
                            item.getPrice(),
                            Environment.NewLine));
                    }

                    string emailBodyCustomer = sb.ToString();

                    if(!mailSender.SendMail(toEmailCustomer, fromEmailCustomer, emailSubjectCustomer, emailBodyCustomer))
                    {
                        return RedirectToAction("Confirm", new { id = order.OrderId, outcome = "failure" });
                    }

                    // Then last but not least, update the order details in the database

                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Confirm", new { id = order.OrderId, outcome = "success" });
                }

            }
            ViewBag.PaymentCardId = new SelectList(db.PaymentCards, "PaymentCardId", "CardNumber", order.PaymentCardId);
            return View(order);
        }


        // GET: Orders/Details/5
        public ActionResult Confirm(int? id, string outcome)
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
            if(outcome.Equals("success"))
            {
                ViewBag.ConfirmationMessage = "The following order has been confirmed";
            }
            else if(outcome.Equals("failure"))
            {
                ViewBag.ConfirmationMessage = "The attempt to take your order failed";
            }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Week25_Bootstrap.Models;

namespace Week25_Bootstrap.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View(CreateOrder());
        }


        /// <summary>
        /// A private method to create one dummy order with a customer and a list of products.
        /// This is then returned by the Index method to give us a (vaguely) complicated model object
        /// that we can style in the view with some Bootstrap.
        /// </summary>
        /// <returns>A dummy order object</returns>

        private Order CreateOrder()
        {
            Customer customer = new Customer();
            customer.CustomerId = 1;
            customer.Title = "Mr.";
            customer.FirstName = "Tony";
            customer.MiddleName = "T";
            customer.LastName = "Inchpractice";
            customer.HomeAddress = new Address()
            {
                AddressId = 1,
                Line1 = "9 Acacia Avenue",
                Line2 = "Glenfield",
                Town = "Leicester",
                County = "Leicestershire",
                PostCode = "LE8, 1QR"
            };
            customer.DeliveryAddress = new Address()
            {
                AddressId = 2,
                Line1 = "Room 27 - Ranieri Enterprises Ltd, Vardy Towers",
                Line2 = "Oxford Road",
                Town = "Leicester",
                County = "Leicestershire",
                PostCode = "LE1 8TT"
            };

            Order order = new Order();
            order.OrderId = 1;
            order.Customer = customer;

            order.Items = new List<Product>
            {
                new Product { ProductId = 1, Name = "Tea Strainer", Description = "A stainless steel seive for straining your tea with", Price = 9.99m},
                new Product { ProductId = 2, Name = "Electric Kettle", Description = "For use in the boiling of water", Price = 24.99m},
                new Product { ProductId = 3, Name = "Cup and saucer set", Description = "Four plain white china cups and accompanying saucers", Price = 39.99m},
                new Product { ProductId = 4, Name = "Sugar tongs", Description = "Fancy silver sugar tongs for picking up sugar cubes with", Price = 75.49m}
            };

            order.DateReceived = new DateTime(2016, 2, 17);
            order.DateDispatched = new DateTime(2016, 2, 21);

            return order;
        }

    }
}
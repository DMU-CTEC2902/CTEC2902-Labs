using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Web.Mvc; 
using Week26_CleanCodeRefactoring.Controllers;
using Week26_CleanCodeRefactoring.Models;

namespace Shop.Tests.Unit
{
    [TestClass]
    public class OrdersControllerTests
    {
        [TestMethod]
        public void TestThatAnOrderIsTakenSuccessfully()
        {
            
            Order order = new Order { 
                OrderId = 1,
                CustomerId = 1,
                DateCreated = new DateTime(2016, 03, 11),
                PaymentCardId = 1
            };

            OrdersController ordersController = new OrdersController();

            RedirectToRouteResult result = ordersController.Edit(order) as RedirectToRouteResult;

            Assert.AreEqual("success", result.RouteValues["outcome"]);

        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Web.Mvc;
using Week27_DependencyInjection.Controllers;
using Week27_DependencyInjection.Models;

namespace Shop.Tests.Unit.Controllers
{
    [TestClass]
    public class OrdersControllerTests
    {

        private Order _order;
        private OrdersController _ordersController;

        [TestInitialize]
        public void SetUp()
        {
            _order = new Order
            {
                OrderId = 1,
                CustomerId = 1,
                DateCreated = new DateTime(2016, 03, 11),
                PaymentCardId = 1
            };

            _ordersController = new OrdersController();
        }

        
        [TestMethod]
        public void TestThatAnOrderIsTakenSuccessfully()
        {
            
            RedirectToRouteResult result = _ordersController.Edit(_order) as RedirectToRouteResult;

            Assert.AreEqual("success", result.RouteValues["Outcome"]);

        }

        [TestMethod]
        public void TestThatTheOrderTotalIsCalculatedProperly()
        {

            RedirectToRouteResult result = _ordersController.Edit(_order) as RedirectToRouteResult;

            Assert.AreEqual(1259.97m, result.RouteValues["OrderTotal"]);

        }

        [TestMethod]
        public void TestThatSuccessfulPaymentFeedbackIsProvided()
        {

            RedirectToRouteResult result = _ordersController.Edit(_order) as RedirectToRouteResult;

            Assert.AreEqual("Payment successful", result.RouteValues["PaymentOutcome"]);

        }

        [TestMethod]
        public void TestThatWarehouseNotificationFeedbackIsProvided()
        {

            RedirectToRouteResult result = _ordersController.Edit(_order) as RedirectToRouteResult;

            Assert.AreEqual("Our warehouse has been notified and will commence packing and sending your order", result.RouteValues["WarehouseNotificationOutcome"]);

        }

        [TestMethod]
        public void TestThatCustomerEmailSent()
        {

            RedirectToRouteResult result = _ordersController.Edit(_order) as RedirectToRouteResult;

            Assert.AreEqual("An email confirming your order has been sent to tony@madeupemail.com", result.RouteValues["CustomerEmailNotificationOutcome"]);

        }

        [TestMethod]
        public void TestThatDatabaseUpdateConfirmed()
        {

            RedirectToRouteResult result = _ordersController.Edit(_order) as RedirectToRouteResult;

            Assert.AreEqual("Our database has been updated to confirm your order", result.RouteValues["DatabaseUpdateOutcome"]);

        }
    }
}

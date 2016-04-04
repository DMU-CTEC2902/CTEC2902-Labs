using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using Moq;

using Week27_DependencyInjection.Controllers;
using Week27_DependencyInjection.Models;
using Week27_DependencyInjection.Interfaces;
using Week27_DependencyInjection.Services.MessagingServices;
using Week27_DependencyInjection.Services.PaymentProcessors;
using System.Data.Entity.Infrastructure;

namespace Shop.Tests.Unit.Controllers
{
    [TestClass]
    public class OrdersControllerTests
    {

        private Order _order;
        private OrdersController _ordersController;
        private Mock<IShopContext> _mockShopContext;

        [TestInitialize]
        public void SetUp()
        {
            List<OrderItem> orderItems = new List<OrderItem>
            {
                    new OrderItem { OrderItemId = 1, OrderId = 1, ProductId = 1, Product = new Product { Name = "IPhone 6", Price = 619.99m }, Quantity = 1},
                    new OrderItem { OrderItemId = 2, OrderId = 1, ProductId = 1, Product = new Product { Name = "Galaxy S5", Price = 319.99m}, Quantity = 2}
            };

            Mock<DbSet<OrderItem>> mockOrderItemSet = GetMockDbSet(orderItems.AsQueryable());

            List<PaymentCard> paymentCards = new List<PaymentCard>
            {
                new PaymentCard { PaymentCardId = 1, CardNumber = "1111222233334444", ExpiryDate = "07/19", CVV = "123" }
            };

            Mock<DbSet<PaymentCard>> mockPaymentCardSet = GetMockDbSet(paymentCards.AsQueryable());

            List<Customer> customers = new List<Customer>
            {
                new Customer { CustomerId = 1, Email = "tony@madeupemail.com" }
            };

            Mock<DbSet<Customer>> mockCustomerSet = GetMockDbSet(customers.AsQueryable());

            _mockShopContext = new Mock<IShopContext>();
            _mockShopContext.Setup(m => m.OrderItems).Returns(mockOrderItemSet.Object);
            _mockShopContext.Setup(m => m.PaymentCards).Returns(mockPaymentCardSet.Object);
            _mockShopContext.Setup(m => m.Customers).Returns(mockCustomerSet.Object);
            
            
            _order = new Order
            {
                OrderId = 1,
                CustomerId = 1,
                DateCreated = new DateTime(2016, 03, 11),
                PaymentCardId = 1
            };

            IPaymentProcessor paymentProcessor = new AcmePaymentProcessorAdaptor();
            IMessageService messagingService = new AcmeMessagingServiceAdaptor();

            _ordersController = new OrdersController(messagingService, paymentProcessor, _mockShopContext.Object);
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
        public void TestCallToSaveChangesMade()
        {
            RedirectToRouteResult result = _ordersController.Edit(_order) as RedirectToRouteResult;
            _mockShopContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void TestCallToSetModifiedMade()
        {
            RedirectToRouteResult result = _ordersController.Edit(_order) as RedirectToRouteResult;
            _mockShopContext.Verify(m => m.SetModified(_order), Times.Once);
        }
        
        [TestMethod]
        public void TestThatDatabaseUpdateConfirmed()
        {

            RedirectToRouteResult result = _ordersController.Edit(_order) as RedirectToRouteResult;

            Assert.AreEqual("Our database has been updated to confirm your order", result.RouteValues["DatabaseUpdateOutcome"]);

        }


        private Mock<DbSet<T>> GetMockDbSet<T>(IQueryable<T> entities) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();

            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(entities.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entities.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entities.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(entities.GetEnumerator());

            return mockSet;

        }

    }
}

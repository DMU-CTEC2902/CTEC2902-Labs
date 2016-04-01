using System;
using Microsoft.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

using Week27_DependencyInjection.Controllers;
using Week27_DependencyInjection.Models;

using Week27_DependencyInjection.Interfaces;
using Week27_DependencyInjection.Services.MessagingServices;
using Week27_DependencyInjection.Services.PaymentProcessors;

namespace Shop.Tests.Unit.Controllers
{
    [TestClass]
    public class OrdersControllerConfirmTests
    {
        [TestMethod]
        public void TestOrderTotalDisplayed()
        {

            IPaymentProcessor paymentProcessor = new AcmePaymentProcessorAdaptor();
            IMessageService messagingService = new AcmeMessagingServiceAdaptor();

            OrdersController _ordersController = new OrdersController(messagingService, paymentProcessor);

            OrderConfirmation _confirmaton = new OrderConfirmation
            {
                OrderId = 1,
                Outcome = "success",
                OrderTotal = 299.99m
            };

            ViewResult confirmationResult = _ordersController.Confirm(_confirmaton) as ViewResult;
            OrderConfirmation result = confirmationResult.ViewBag.Confirmation as OrderConfirmation;

            Assert.AreEqual(299.99m, result.OrderTotal);

        }

        // We could add a whole series of similar confirmation tests here, especially if we wanted to tidy
        // up any of the more business-jargon type language about AuthCodes etc into more customer-friendly language
        // if we did that, or added any display language to the View, we could test it here.
    }
}

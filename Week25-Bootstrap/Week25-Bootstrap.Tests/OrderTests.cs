using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Week25_Bootstrap.Models;

namespace Week25_Bootstrap.Tests
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void AnOrderWithItemsTotalling30ReturnsAGetTotalOf30()
        {

            Order order = new Order();
            order.Items = new List<Product> {

                new Product { ProductId = 1, Name="Test Product 1", Description = "The first test product", Price= 12.00m},
                new Product { ProductId = 2, Name="Test Product 2", Description = "The second test product", Price= 10.00m},
                new Product { ProductId = 3, Name="Test Product 3", Description = "The third test product", Price= 8.00m}
            };

            Assert.AreEqual(30.00m, order.GetTotal());

        }
    }
}

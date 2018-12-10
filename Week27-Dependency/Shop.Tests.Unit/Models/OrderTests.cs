using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;

using Week27_DependencyInjection.Models;

namespace Shop.Tests.Unit.Models
{
    [TestClass]
    public class OrderTests
    {

        private Order _testOrder;

        [TestInitialize]
        public void SetUp()
        {
            _testOrder = new Order
            {
                OrderId = 1,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem {
                        OrderItemId = 1,
                        ProductId = 1,
                        Product = new Product {Name = "Test Item 1", ProductId = 1, Price = 15.99m},
                        Quantity = 1
                    },
                    new OrderItem {
                        OrderItemId = 2,
                        ProductId = 2,
                        Product = new Product {Name = "Test Item 2", ProductId = 2, Price = 7.99m},
                        Quantity = 2
                    },
                    new OrderItem {
                        OrderItemId = 3,
                        ProductId = 3,
                        Product = new Product {Name = "Test Item 3", ProductId = 3, Price = 1.50m},
                        Quantity = 5
                    }
                }
            };


        }

        [TestMethod]
        public void TestOrderTotalForThreeProductsInVariousQuantitiesIs39_47()
        {
            Assert.AreEqual(39.47m, _testOrder.getTotalValue());
        }

        [TestMethod]
        public void TesPrintOrderItemsForThreeOrderItems()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Item name: Test Item 1 - Price: 15.99 - Quantity: 1 - Total Cost: 15.99" + Environment.NewLine);
            sb.Append("Item name: Test Item 2 - Price: 7.99 - Quantity: 2 - Total Cost: 15.98" + Environment.NewLine);
            sb.Append("Item name: Test Item 3 - Price: 1.50 - Quantity: 5 - Total Cost: 7.50" + Environment.NewLine);

            string result = sb.ToString();

            Assert.AreEqual(result, _testOrder.printOrderItems());
        }

    }
}

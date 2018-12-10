namespace Week27_DependencyInjection.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using Week27_DependencyInjection.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Week27_DependencyInjection.Models.ShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Week27_DependencyInjection.Models.ShopContext";
        }

        protected override void Seed(Week27_DependencyInjection.Models.ShopContext context)
        {
            
            var products = new List<Product>
            {
                new Product {ProductId = 1, Name = "IPhone 6", Price = 619.99m},
                new Product {ProductId = 2, Name = "Galaxy S5", Price = 319.99m},
                new Product {ProductId = 3, Name = "Lumia 720", Price = 119.99m}
            };

            products.ForEach(product => context.Products.AddOrUpdate(p => p.ProductId, product));
            context.SaveChanges();

            Customer customer = new Customer
            {
                CustomerId = 1,
                FirstName = "Tony",
                LastName = "Inchpractice",
                Email = "tony@madeupemail.com", 
                MobileNumber = "077882223344",
                TelephoneNumber = "011223334455"
            };

            context.Customers.AddOrUpdate(c => c.CustomerId, customer);

            Order order = new Order
            {
                OrderId = 1,
                CustomerId = 1,
                DateCreated = new DateTime(2016, 3, 11),
                OrderItems = new List<OrderItem> {
                    new OrderItem { OrderItemId = 1, OrderId = 1, ProductId = 1, Quantity = 1},
                    new OrderItem { OrderItemId = 2, OrderId = 1, ProductId = 2, Quantity = 2}
                },
                PaymentCardId = 1,
                PaymentCard = new PaymentCard { PaymentCardId= 1, CardNumber = "1111222233334444", ExpiryDate="07/19", CVV="123" }
            };
            context.Orders.AddOrUpdate(o => o.OrderId, order);
            context.SaveChanges();
        }

    }
}

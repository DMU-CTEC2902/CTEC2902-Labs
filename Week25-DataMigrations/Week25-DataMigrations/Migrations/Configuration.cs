namespace Week25_DataMigrations.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using System.Collections.Generic;
    using Week25_DataMigrations.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Week25_DataMigrations.Models.ShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Week25_DataMigrations.Models.ShopContext";
        }

        protected override void Seed(Week25_DataMigrations.Models.ShopContext context)
        {

            var products = new List<Product>
            {
                new Product {ProductId = 1, Name = "IPhone 6", Colour = "Silver", Price = 619.99m},
                new Product {ProductId = 2, Name = "Galaxy S5", Colour = "Black", Price = 319.99m},
                new Product {ProductId = 3, Name = "Lumia 720", Colour = "Orange", Price = 119.99m}
            };

            products.ForEach(product => context.Products.AddOrUpdate(p => p.ProductId, product));
            context.SaveChanges();

            Order order = new Order
            {
                OrderId = 1,
                DateCreated = new DateTime(2016, 3, 11),
                DateDispatched = new DateTime(2016, 3, 12),
                OrderItems = new List<OrderItem> {
                    new OrderItem { OrderItemId = 1, OrderId = 1, ProductId = 1, Quantity = 1},
                    new OrderItem { OrderItemId = 2, OrderId = 1, ProductId = 2, Quantity = 2}
                }

            };
            context.Orders.AddOrUpdate(o => o.OrderId, order);
            context.SaveChanges();

        }
    }
}

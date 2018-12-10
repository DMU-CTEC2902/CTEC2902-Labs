using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Week27_DependencyInjection.Models
{
    public class ShopContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ShopContext() : base("name=ShopContext")
        {
        }

        public System.Data.Entity.DbSet<Order> Orders { get; set; }

        public System.Data.Entity.DbSet<Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<OrderItem> OrderItems { get; set; }

        public System.Data.Entity.DbSet<PaymentCard> PaymentCards { get; set; }

        public System.Data.Entity.DbSet<Product> Products { get; set; }

    }
}

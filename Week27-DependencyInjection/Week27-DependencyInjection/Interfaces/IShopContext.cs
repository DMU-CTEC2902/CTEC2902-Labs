using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using Week27_DependencyInjection.Models;

namespace Week27_DependencyInjection.Interfaces
{
    public interface IShopContext : IDisposable
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<PaymentCard> PaymentCards { get; set; }
        DbSet<Product> Products { get; set; }

        int SaveChanges();
        void SetModified(object entity);
        

    }
}

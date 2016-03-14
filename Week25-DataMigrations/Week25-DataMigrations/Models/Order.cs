using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week25_DataMigrations.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateDispatched { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week25_DataMigrations.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
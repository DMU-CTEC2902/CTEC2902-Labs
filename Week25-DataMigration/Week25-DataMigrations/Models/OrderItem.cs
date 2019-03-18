using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week25_DataMigrations.Models
{
    public class OrderItem
    {
        public virtual int OrderItemId { get; set; }
        public virtual int OrderId { get; set; }
        public virtual int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual int Quantity { get; set; }
    }

}
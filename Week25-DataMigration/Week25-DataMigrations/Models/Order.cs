using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week25_DataMigrations.Models
{
    public class Order
    {
        public virtual int OrderId { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime DateDispatched { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week27_DependencyInjection.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }

        public decimal getPrice()
        {
            return Product.Price * Quantity;
        }

    }
}
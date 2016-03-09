using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week25_Bootstrap.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public List<Product> Items { get; set; }
        public Customer Customer { get; set; }
        public DateTime DateReceived { get; set; }
        public DateTime DateDispatched { get; set; }

        public decimal GetTotal()
        {
            if(this.Items != null)
            {
                return Items.Sum(p => p.Price);
            }
            else
            {
                return 0;
            }
        }
    }
}
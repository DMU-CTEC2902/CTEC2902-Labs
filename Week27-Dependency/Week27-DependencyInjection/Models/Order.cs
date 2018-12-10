using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Week27_DependencyInjection.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateDispatched { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public int PaymentCardId { get; set; }
        public virtual PaymentCard PaymentCard { get; set; }

        public decimal getTotalValue()
        {
            // Work out the amount for the order

            decimal orderTotal = 0.00m;

            foreach (OrderItem item in this.OrderItems)
            {
                orderTotal += item.getPrice();
            }

            return orderTotal;

        }

        public string printOrderItems()
        {
            StringBuilder sb = new StringBuilder();

            foreach (OrderItem item in this.OrderItems)
            {
                sb.Append(string.Format("Item name: {0} - Price: {1} - Quantity: {2} - Total Cost: {3}{4}",
                    item.Product.Name,
                    item.Product.Price,
                    item.Quantity,
                    item.getPrice(),
                    Environment.NewLine));
            }

            return sb.ToString();

        }

    }
}
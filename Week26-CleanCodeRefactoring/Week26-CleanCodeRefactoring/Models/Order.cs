using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week26_CleanCodeRefactoring.Models
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
    }
}
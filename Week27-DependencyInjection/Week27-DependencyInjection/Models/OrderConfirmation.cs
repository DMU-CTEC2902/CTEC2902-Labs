using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week27_DependencyInjection.Models
{
    public class OrderConfirmation
    {
        public int? OrderId { get; set; }
        public string Outcome { get; set; }
        public decimal OrderTotal { get; set; }
        public string PaymentOutcome { get; set; }
        public string WarehouseNotificationOutcome { get; set; }
        public string CustomerEmailNotificationOutcome { get; set; }
        public string DatabaseUpdateOutcome { get; set; }

    }
}
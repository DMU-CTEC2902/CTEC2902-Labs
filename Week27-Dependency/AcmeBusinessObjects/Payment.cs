using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeBusinessObjects
{
    public class Payment
    {
        public decimal PaymentAmount { get; set; }
        public string PayerName { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CVV { get; set; }

    }
}

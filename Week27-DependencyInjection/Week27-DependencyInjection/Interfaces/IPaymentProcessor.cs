using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week27_DependencyInjection.Interfaces
{
    interface IPaymentProcessor
    {
        string CardNumber { get; set; }
        string CardHolderName { get; set; }
        string CardExpiryDate { get; set; }
        string CVV { get; set; }
        decimal PaymentAmount { get; set; }
        string ResponseCode { get; set; }

        bool processPayment();

    }
}

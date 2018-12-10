using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeBusinessObjects
{
    public class PaymentProvider
    {

        public string ResponseCode { get; set; }

        public bool Authorise(Payment payment)
        {
            if (payment.CardNumber.Equals("1111222233334444"))
            {
                ResponseCode = "AUTHCODE: 57GHL79";
                return true;
            }

            ResponseCode = "FAILED!";

            return false;
        }

    }
}

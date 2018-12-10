using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaveCoBusinessObjects
{
    public class PaymentGateway
    {

        public string Authorise(decimal amount, string CustomerName, string cardNo, string cvv, string expiry)
        {

            if (cardNo.Equals("1111222233334444"))
            {
                return "AUTHCODE: 57GHL79";
            }
            else
            {
                return "PAYMENT FAILURE";
            }

        }

    }
}

using DaveCoBusinessObjects;
using Week27_DependencyInjection.Interfaces;

namespace Week27_DependencyInjection.Services.PaymentProcessors
{
    public class DaveCoPaymentProcessorAdaptor : IPaymentProcessor
    {

        public string CardNumber { get; set;}
        public string CardHolderName { get; set; }
        public string CardExpiryDate { get; set; }
        public string CVV { get; set; }
        public decimal PaymentAmount { get; set; }
        public string ResponseCode { get; set; }

        public bool processPayment()
        {

            PaymentGateway gateway = new PaymentGateway();

            ResponseCode = gateway.Authorise(PaymentAmount, CardHolderName, CardNumber, CVV, CardExpiryDate);

            if (ResponseCode.Equals("PAYMENT FAILURE")) return false;

            return true;

        }
    }
}
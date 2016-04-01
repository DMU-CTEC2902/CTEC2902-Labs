using Week27_DependencyInjection.Interfaces;
using AcmeBusinessObjects;

namespace Week27_DependencyInjection.Services.PaymentProcessors
{
    public class AcmePaymentProcessorAdaptor : IPaymentProcessor
    {

        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string CardExpiryDate {get; set;}
        public string CVV {get; set;}
        public decimal PaymentAmount {get; set;}
        public string ResponseCode {get; set;}
        
        
        public bool processPayment()
        {
            Payment payment = new Payment();
            payment.CardNumber = CardNumber;
            payment.PayerName = CardHolderName;
            payment.ExpiryDate = CardExpiryDate;
            payment.CVV = CVV;
            payment.PaymentAmount = PaymentAmount;

            PaymentProvider provider = new PaymentProvider();

            bool paymentSuccessful = provider.Authorise(payment);
            if(paymentSuccessful)
            {
                ResponseCode = provider.ResponseCode;
            }
            else
            {
                ResponseCode = "PAYMENT FAILURE";
            }

            return paymentSuccessful;
        }



    }
}
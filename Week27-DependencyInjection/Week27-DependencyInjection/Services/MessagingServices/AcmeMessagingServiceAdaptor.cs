using Week27_DependencyInjection.Models;
using Week27_DependencyInjection.Interfaces;

using AcmeBusinessObjects;

namespace Week27_DependencyInjection.Services.MessagingServices
{
    public class AcmeMessagingServiceAdaptor : IMessageService
    {

        public bool SendMessage(Message messageToSend)
        {
            SendAMail sender = new SendAMail();
            sender.SendTo = messageToSend.To;
            sender.SentFrom = messageToSend.From;
            sender.MailSubject = messageToSend.Subject;
            sender.MailBody = messageToSend.Body;

            return sender.Send();
        }
    }
}
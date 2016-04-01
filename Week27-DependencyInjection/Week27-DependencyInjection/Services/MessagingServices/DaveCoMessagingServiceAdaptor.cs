using Week27_DependencyInjection.Models;
using Week27_DependencyInjection.Interfaces;

using DaveCoBusinessObjects;

namespace Week27_DependencyInjection.Services.MessagingServices
{
    public class DaveCoMessagingServiceAdaptor : IMessageService
    {
        public bool SendMessage(Message messageToSend)
        {
            MailSender sender = new MailSender();

            return sender.SendMail(messageToSend.To, messageToSend.From, messageToSend.Subject, messageToSend.Body);

        }
    }
}
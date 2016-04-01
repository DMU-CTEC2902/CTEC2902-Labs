using Week27_DependencyInjection.Models;

namespace Week27_DependencyInjection.Interfaces
{
    interface IMessageService
    {
        bool SendMessage(Message messageToSend);

    }
}

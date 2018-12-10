using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaveCoBusinessObjects
{
    public class MailSender
    {
        public bool SendMail(string to, string from, string subject, string body)
        {
            if (subject.Equals("New Order") || subject.Equals("Thanks for placing an order with us")) return true;

            return false;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeBusinessObjects
{
    public class SendAMail
    {
        public string SendTo { get; set; }
        public string SentFrom { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }

        public bool Send()
        {
            if (MailSubject.Equals("New Order") || MailSubject.Equals("Thanks for placing an order with us")) return true;

            return false;

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week26_CleanCodeRefactoring.Models
{
    public class Message
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week25_Bootstrap.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
    }
}
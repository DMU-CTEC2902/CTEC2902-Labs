using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week25_Bootstrap.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Address HomeAddress { get; set; }
        public Address DeliveryAddress { get; set; }

    }
}
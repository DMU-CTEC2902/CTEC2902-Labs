using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week27_DependencyInjection.Models
{
    public class Customer
    {

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
        public string MobileNumber { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}
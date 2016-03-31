using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week27_DependencyInjection.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

    }
}
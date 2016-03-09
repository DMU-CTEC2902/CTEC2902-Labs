using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week25_Bootstrap.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
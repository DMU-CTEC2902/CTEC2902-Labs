using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week24_Views.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Colour { get; set; }
        public decimal Price { get; set; }
    }
}
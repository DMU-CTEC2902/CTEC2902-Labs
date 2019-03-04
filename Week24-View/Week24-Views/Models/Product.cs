using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Week24_Views.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        public string Colour { get; set; }
        [Range(typeof(decimal), "30.00", "1000.00")]
        public decimal Price { get; set; }

    }

}
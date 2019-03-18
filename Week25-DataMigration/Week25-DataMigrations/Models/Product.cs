using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week25_DataMigrations.Models
{
    public class Product
    {
        public virtual int ProductId { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal Price { get; set; }
    }
}
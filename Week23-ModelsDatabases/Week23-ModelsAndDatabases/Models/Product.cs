using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week23_ModelsAndDatabases.Models
{
    public class Product
    {
        public virtual int ProductId { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual decimal Price { get; set; }
        public virtual Category Category { get; set; }
    }

}
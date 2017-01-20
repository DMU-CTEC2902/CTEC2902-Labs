using System;

namespace Week23_ModelsAndDatabases.Models
{
    public class Category
    {

        public virtual int CategoryId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}
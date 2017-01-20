using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;

namespace Week23_ModelsAndDatabases.Models
{
    public class ProductDataInitialiser : DropCreateDatabaseAlways<ProductContext>
    {

        protected override void Seed(ProductContext context)
        {
            Category cat1 = new Category(); 
            cat1.CategoryId = 1; 
            cat1.Name = "CDs"; 
            cat1.Description ="Music compact discs" ;
            context.Categories.Add(cat1);

            Category cat2 = new Category();
            cat2.CategoryId = 2; 
            cat2.Name = "DVDs"; 
            cat2.Description = "Film DVDs"; 
            context.Categories.Add(cat2);

            Product prod1 = new Product();
            prod1.ProductId = 1;
            prod1.CategoryId = 1;
            prod1.Name = "Now That's What I Call Music 261";
            prod1.Description = "More terrible hits";
            prod1.Category = cat1;
            context.Products.Add(prod1);

            Product prod2 = new Product();
            prod2.ProductId = 2;
            prod2.CategoryId = 2;
            prod2.Name = "Fast and Furious 261";
            prod2.Description = "More terrible car wrecks";
            prod2.Category = cat2;
            context.Products.Add(prod2);

            base.Seed(context);
        }

    }
}
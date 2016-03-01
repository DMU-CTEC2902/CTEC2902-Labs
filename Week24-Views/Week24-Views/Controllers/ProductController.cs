using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Collections.Generic;
using System.Linq;

using Week24_Views.Models;

namespace Week24_Views.Controllers
{
    public class ProductController : Controller
    {

        private List<Product> _products = new List<Product>() {
            new Product { ProductId = 1, 
                            Name = "IPhone 6", 
                            Colour = "White", 
                            Description = "The most expensive smart phone money can buy?", 
                            Price = 619.99m},
            new Product { ProductId = 2, 
                            Name = "Lumia 720", 
                            Colour = "Black", 
                            Description = "It's kind of plastic but works.", 
                            Price = 119.99m},
            new Product { ProductId = 3, 
                            Name = "Samsung Galaxy S5", 
                            Colour = "Silver", 
                            Description = "It's a smartphone. Who cares?", 
                            Price = 319.99m}
        };
        
        // GET: Product
        public ActionResult Index()
        {
            return View(_products);
        }

        // GET: Details/id
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpNotFoundResult();

            Product selectedProduct = _products.First(p => p.ProductId == id);
    
            if(selectedProduct == null) return new HttpNotFoundResult();

            return View(selectedProduct);
        }

        // GET: Edit/id 

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpNotFoundResult();

            Product selectedProduct = _products.First(p => p.ProductId == id);

            if (selectedProduct == null) return new HttpNotFoundResult();

            return View(selectedProduct);
        }

    }
}
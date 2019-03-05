using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Week24_Views.Models;

namespace Week24_Views.Controllers
{
    public class ProductController : Controller
    {

        private List<Product> _products = new List<Product>() {
        new Product { ProductId = 1,
               Name = "IPhone XS",
                    Colour = "White",
                    Description = "The most expensive smart phone money can buy?",
                    Price = 819.99m},
        new Product { ProductId = 2,
               Name = "Lumia 720",
                    Colour = "Black",
                    Description = "A what?",
                    Price = 119.99m},
        new Product { ProductId = 3,
               Name = "Samsung S10",
                    Colour = "Black",
                    Description = "It's an Android. Beep Boop",
                    Price = 819.99m}
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
            if (selectedProduct == null) return new HttpNotFoundResult();

            Customer customer = new Customer
            {
                CustomerId = 1,
                FirstName = "Bob",
                LastName = "Fossil"
            };

            CustomerProductViewModel viewModel = new CustomerProductViewModel
            {
                Customer = customer,
                Product = selectedProduct
            };
            return View(viewModel);
        }


        // GET: Edit/id 
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpNotFoundResult();
            Product selectedProduct = _products.First(p => p.ProductId == id);
            if (selectedProduct == null) return new HttpNotFoundResult();
            return View(selectedProduct);

        }

        // POST: Edit
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                Debug.WriteLine(product.Name);
                Debug.WriteLine(product.Description);
                Debug.WriteLine(product.Price);

                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

    }
}
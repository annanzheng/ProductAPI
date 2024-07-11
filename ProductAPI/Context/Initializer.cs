using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ProductAPI.Models;

namespace ProductAPI.Context
{
    public class Initializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ProductContext>
    {
        protected override void Seed(ProductContext context)
        {
            base.Seed(context);
            var products = new List<Product>
            {
                new Product{id = 1, name = "Coke", price = 3, quantity = 100, description = "Beverage"},
                new Product{id = 2, name = "Red Bull", price = 3, quantity = 100, description = "Beverage"},
                new Product{id = 3, name = "Vodka", price = 10, quantity = 100, description = "Beverage"},
            };

            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();
        }

    }
}
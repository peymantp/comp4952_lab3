using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace comp4956.lab3.Models
{
    public class ProductDatabaseInitializer : DropCreateDatabaseIfModelChanges<ProductContext>
    {
        protected override void Seed(ProductContext context)
        {
            ProductDatabaseInitializer.GetCategories().ForEach(c => context.Categories.Add(c));
            GetProducts().ForEach(p => context.Products.Add(p));
        }

        private static List<Category> GetCategories()
        {
            var categories = new List<Category>
            {
                new Category{CategoryID = 1, CategoryName = "Media"},
                new Category{CategoryID = 2, CategoryName = "Food", Description = "For when the stomach is grumbly "},
                new Category{CategoryID = 3, CategoryName = "Entertainment", Description = "For when you're bored"}
            };
            return categories;
        }

        private static List<Product> GetProducts()
        {
            var products = new List<Product>
            {
            };
            return products;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace comp4956.lab3.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext()
        : base("ArtStore") { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
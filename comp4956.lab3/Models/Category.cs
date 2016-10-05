using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace comp4956.lab3.Models
{
    public class Category
    {
        [ScaffoldColumn(false)]
        public int CategoryID  { get; set; }

        [Required, StringLength(200), Display(Name = "Name",Description = "Name the category")]
        public string CategoryName { get; set; }

        [Display(Name = "Product Description", Prompt = "Type")]
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
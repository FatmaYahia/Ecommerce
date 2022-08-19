using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.ViewModel
{
    public class ProductVM:Product
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="This field is required!")]
        public string Name { get; set; }
        [Display(Name ="Category")]
        public Nullable<int> CategoryFK { get; set; }
        [Display(Name = "Brand")]
        public Nullable<int> BrandFK { get; set; }
        
        [AllowHtml]
        public string Description { get; set; }
        [Display(Name ="Short Description")]
        public string ShortDescription { get; set; }
        public Nullable<double> Price { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
    }
}
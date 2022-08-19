using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.ViewModel
{
    public class BrandVM:Brand
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="This Field is required")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
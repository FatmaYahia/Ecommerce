using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.ViewModel;
using Ecommerce.Models;
namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        ECommerceEntities DB = new ECommerceEntities();
        public ActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                Category = DB.Categories.Count(),
                Brand = DB.Brands.Count(),
                Product = DB.Products.Count(),
                ProductImage = DB.ProductImages.Count()
            };
            return View(homeViewModel);
        }

        
    }
}
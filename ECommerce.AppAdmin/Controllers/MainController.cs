using Microsoft.AspNetCore.Mvc;

namespace ECommerce.AppAdmin.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

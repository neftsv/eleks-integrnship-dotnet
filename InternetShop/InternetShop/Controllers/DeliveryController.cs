using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Controllers
{
    public class DeliveryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

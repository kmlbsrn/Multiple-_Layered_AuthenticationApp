using Microsoft.AspNetCore.Mvc;

namespace Authentication.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

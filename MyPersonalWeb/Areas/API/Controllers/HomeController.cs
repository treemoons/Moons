using Microsoft.AspNetCore.Mvc;

namespace MyPersonalWeb.Areas.API.Controllers
{
    [Area("API")]
    public class HomeController : Controller
    {
        public string Index() => "sssss";
        public IActionResult About()
        {
            return View();
        }
    }
}
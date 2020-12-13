using Microsoft.AspNetCore.Mvc;

namespace MyPersonalWeb.Areas.Admin.Controllers
{
    // [Area("Admin")]
    public class HomeController : BaseAdminController
    {
       public IActionResult Index() => View();
    }
}
using Microsoft.AspNetCore.Mvc;

namespace MyPersonalWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
       public string Index() => "test!";

    }
}
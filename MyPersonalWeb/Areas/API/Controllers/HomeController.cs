using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyPersonalWeb.Areas.API.Controllers
{
    [Area("API")]
    public class HomeController : Controller
    {
        public async Task<string> Index() =>
            await Task.Run( () =>
            {
                return "";
            });

    }
}
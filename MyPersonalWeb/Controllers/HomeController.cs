using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MyPersonalWeb.Models;
using ModelsLibrary.User;

namespace MyPersonalWeb.Controllers
{
    public class PromissAttribute : ActionFilterAttribute
    {
    }
    public class NoPromissionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
    }

   public class PermissionController:Controller
   {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Session.TryGetValue("CurrentUser", out byte[] result))
            {
                TempData["userprofile"] = "showLogin()";
            }else{
                TempData["userprofile"] = "showUserOptions()";
            }
            base.OnActionExecuting(filterContext);
        }
   }
    public class HomeController : PermissionController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<string> Login(UserSignIn users) =>
            await Task.Run(() =>
            {
                if (!ModelState.IsValid || users.UserName == "default")
                {
                    string[] user = { users.UserName, users.Password, users.LastLoginTime };
                    // ShowLogin();
                    return "F";

                }
                else
                {
                    string[] user = { users.UserName, users.Password, users.LastLoginTime };
                    HttpContext.Session.SetString("CurrentUser", user[0]);
                    // ViewBag.tip = "pass";
                    byte[] session = HttpContext.Session.Get("CurrentUser");
                    return "T";
                }
                // return View("Privacy",users);
            });
        public IActionResult Privacy()
        {
            return View();
        }

        string ShowLogin() =>
                ViewBag.ShowLogin = "<script>setTimeout(showLogin,401);</script>";

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

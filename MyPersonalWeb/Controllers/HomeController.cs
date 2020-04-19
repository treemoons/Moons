using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyPersonalWeb.Models;
using ModelsLibrary.User;

namespace MyPersonalWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("username") == null)
                TempData["userprofile"] = "showLogin();";
            else
                TempData["userprofile"] = "showUserOptions();";
            return View();
        }
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<string> Login(UserSignIn users) =>
            await Task.Run(() =>
            {
                if(ModelState.IsValid&& users.UserName=="default"){
                    string[] user = { users.UserName, users.Password, users.LastLoginTime };
                    // ShowLogin();
                    return "F";
                    
                }
                else{
                    string[] user = { users.UserName, users.Password, users.LastLoginTime };
                    // HttpContext.Session.SetString("username", user[0]);
                    // ViewBag.tip = "pass";
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

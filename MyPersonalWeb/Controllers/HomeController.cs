using System.Net;
using System.ComponentModel;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MyPersonalWeb.Models;
using ModelsLibrary.User;
using ModelsLibrary;
using CommonUtils;

namespace MyPersonalWeb.Controllers
{
    public class HomeController : PermissionController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public IActionResult Post(UserSignIn users) => Redirect(HttpContext.Request.GetDisplayUrl());
        // [Area("en")]
        // [Route("en/[Controller]/[Action]")]
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<string> Login(UserSignIn users) =>
            await Task.Run(() =>
            {
                if (!ModelState.IsValid)
                {
                    return "F";
                }
                else
                {
                    HttpContext.Session.SetString("CurrentUser", users.UserName);
                    HttpContext.Response.Cookies.Append(LoginCookieBase64.GetCookieUserNameBase64,
                        RSAData.RSAEncryption(users.UserName, "username"),
                        new CookieOptions { Expires = DateTimeOffset.Now.AddDays(7d) });
                    HttpContext.Response.Cookies.Append(LoginCookieBase64.GetCookiePasswordBase64,
                        RSAData.RSAEncryption(users.Password, "password"),
                        new CookieOptions { Expires = DateTimeOffset.Now.AddDays(7d) });
                    var IsRemembered = users.IsRemembered;
                    if (IsRemembered == "true")
                    {
                        HttpContext.Response.Cookies.Append(LoginCookieBase64.GetCookieRememberBase64, "1",
                            new CookieOptions { Expires = DateTimeOffset.Now.AddYears(1) });
                    }
                    else
                    {
                        HttpContext.Response.Cookies.Delete(LoginCookieBase64.GetCookieRememberBase64);
                    }
                    return "T";
                }
            });

        [HttpPost]
        public async Task<string> Logout(UserSignIn users) =>
                   await Task.Run(() =>
                   {
                       try{
                       HttpContext.Session.Remove("CurrentUser");
                        return "T";
                       }catch{return "F";}
                       
                   });
        string ShowLogin() =>
                ViewBag.ShowLogin = "<script>setTimeout(showLogin,401);</script>";

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

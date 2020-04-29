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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MyPersonalWeb.Models;
using ModelsLibrary.User;
using CommonUtils;

namespace MyPersonalWeb.Controllers
{
    public class NoPromissionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
    }
    /// <summary>
    /// filter logged on
    /// </summary>
    public class PermissionController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Session.TryGetValue("CurrentUser", out byte[] result))
            {
                TempData["userprofile"] = "showLogin()";
                var isremembered = filterContext.HttpContext.Request.Cookies.TryGetValue(LoginCookieBase64.GetCookieRememberBase64, out string IsRemembered);
                if (isremembered)
                {
                    ViewBag.isremembered = "checked";
                    filterContext.HttpContext.Request.Cookies.TryGetValue(LoginCookieBase64.GetCookieUserNameBase64, out string username);
                    filterContext.HttpContext.Request.Cookies.TryGetValue(LoginCookieBase64.GetCookiePasswordBase64, out string password);
                    ViewBag.username = Utils.RSAData.RSADecrypt(username, "username");
                    ViewBag.password = Utils.RSAData.RSADecrypt(password, "password");
                }
            }
            else
            {
                TempData["userprofile"] = "showUserOptions()";
            }
            base.OnActionExecuting(filterContext);
        }
    }
    /// <summary>
    /// encrypt cookie with Base64
    /// </summary>
    public struct LoginCookieBase64
    {
        static public string GetCookieUserNameBase64 => Convert.ToBase64String(Encoding.ASCII.GetBytes("username"));
        static public string GetCookiePasswordBase64 => Convert.ToBase64String(Encoding.ASCII.GetBytes("password"));
        static public string GetCookieRememberBase64 => Convert.ToBase64String(Encoding.ASCII.GetBytes("isremembered"));
    }
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
        public async Task<IActionResult> Index() =>await Task.Run(() => View());
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
                    HttpContext.Response.Cookies.Append(LoginCookieBase64.GetCookieUserNameBase64,
                        Utils.RSAData.RSAEncryption(users.UserName, "username"),
                        new CookieOptions { Expires = DateTimeOffset.Now.AddDays(7d) });
                    HttpContext.Response.Cookies.Append(LoginCookieBase64.GetCookiePasswordBase64,
                        Utils.RSAData.RSAEncryption(users.Password, "password"),
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
                    byte[] session = HttpContext.Session.Get("CurrentUser");
                    return "T";
                }
                // return View("Privacy",users);
            });


        public IActionResult Privacy()
        {
            //var isremember = HttpContext.Request.Form["isremembered"];
            return View();
        }
        public IActionResult LangChanged(string Id){
            string id = Id;

            return Redirect(HttpContext.Request.GetDisplayUrl());
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

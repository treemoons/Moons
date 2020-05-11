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
    /// <summary>
    /// filter logged on
    /// </summary>
    public class PermissionController : Controller
    {

        public override ViewResult View(object Models) => base.View((new Language(LanguageJsonElementDictionary[ViewBag.lang]), Models));
        public static Dictionary<string, JsonElement> LanguageJsonElementDictionary { get; set; } = new Dictionary<string, JsonElement>();
        [NonAction]
        public static void ReadAllLanguageJson()
        {
            var langDictionary = new DirectoryInfo("./wwwroot/src/language");
            var fileInfos = langDictionary.GetFileSystemInfos();
            foreach (var file in fileInfos)
            {
                if (file.Extension == ".json")
                {
                    using var stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
                    LanguageJsonElementDictionary.TryAdd(file.Name.Replace(file.Extension, ""), JsonDocument.Parse(stream).RootElement);
                }
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            #region get route
            string parameterValue = default, action = default, controller = default, route = default;
            action = filterContext.RouteData.Values["Action"].ToString();
            controller = filterContext.RouteData.Values["controller"].ToString();
            route = $"{controller}/{action}";
            try
            {
                parameterValue = filterContext.RouteData.Values["id"].ToString();
            }
            catch
            {
                if (filterContext.HttpContext.Request.Cookies.TryGetValue("lang", out string lang))
                {
                    parameterValue = lang;
                    // do language translation
                }
                else
                {
                    parameterValue = "en";
                }
            }
            ViewBag.route = route;
            ViewBag.lang = parameterValue;
            ViewBag.langTranslation = parameterValue;// 暂时测试使用 parameterValue
            #endregion

            #region about login
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
            #endregion
            base.OnActionExecuting(filterContext);
        }

        public class NoPromissionAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                base.OnActionExecuting(filterContext);
            }
        }

        public class CheckAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                base.OnActionExecuting(filterContext);
            }
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

}
using System.Collections;
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
using ModelsLibrary;
using CommonUtils;
using Languages= ModelsLibrary.Languages;
namespace MyPersonalWeb.Controllers
{
    /// <summary>
    /// filter logged on
    /// </summary>
    public class PermissionController : Controller
    {

        [NonAction]
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            #region get route
            string parameterValue = default, action = default, controller = default;
            action = filterContext.RouteData.Values["Action"].ToString();
            controller = filterContext.RouteData.Values["controller"].ToString();
            parameterValue = filterContext.RouteData.Values["language"]?.ToString();
            parameterValue= parameterValue??
            (HttpContext.Request.Cookies.TryGetValue("lang", out string lang) ? lang : "en");
            ViewBag.controller = controller.ToLowerInvariant();
            ViewBag.action = action.ToLowerInvariant();
            ViewBag.lang = parameterValue.ToLowerInvariant();
            ViewBag.langTranslation = parameterValue.ToLowerInvariant();// 暂时测试使用 parameterValue
            #endregion

            #region about login
            if (filterContext.HttpContext.Session.GetString("CurrentUser")==null)
            {
                ViewBag.userprofile = "showLogin()";
                var isremembered = filterContext.HttpContext.Request.Cookies.TryGetValue(LoginCookieBase64.GetCookieRememberBase64, out string IsRemembered);
                if (isremembered)
                {
                    ViewBag.isremembered = "checked";
                    filterContext.HttpContext.Request.Cookies.TryGetValue(LoginCookieBase64.GetCookieUserNameBase64, out string encryptUserName);
                    filterContext.HttpContext.Request.Cookies.TryGetValue(LoginCookieBase64.GetCookiePasswordBase64, out string encrptyPassword);
                    ViewBag.username = RSAData.RSADecrypt(encryptUserName, "username");
                    ViewBag.password = RSAData.RSADecrypt(encrptyPassword, "password");
                }
                ViewBag.logged=false;
            }
            else
            {
                ViewBag. userprofile = "showUserOptions()";
                ViewBag.logged = true;
            }
            #endregion
            base.OnActionExecuting(filterContext);
        }
        public class TestAttribute:Attribute{
           public TestAttribute(){
                Test("");

            }
            [NonAction]
                public void Test(string a)
                {
                    var aaa = a;
                }
        }

        public class NoPromissionAttribute : ActionFilterAttribute
        {

            [NonAction]
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                base.OnActionExecuting(filterContext);
            }
        }

        public class CheckAttribute : ActionFilterAttribute
        {
            [NonAction]
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
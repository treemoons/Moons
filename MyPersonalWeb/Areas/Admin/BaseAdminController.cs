
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using CommonUtils;
namespace MyPersonalWeb.Areas
{
    [Area("Admin")]
    public class BaseAdminController : Controller
    {

        [NonAction]
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            #region get route
            string parameterValue = default, action = default, controller = default;
            action = filterContext.RouteData.Values["action"].ToString();
            controller = filterContext.RouteData.Values["controller"].ToString();
            parameterValue = filterContext.RouteData.Values["language"]?.ToString();
            parameterValue = parameterValue ??
            (HttpContext.Request.Cookies.TryGetValue("adminlang", out string lang) ? lang : "en");
            ViewBag.controller = controller.ToLowerInvariant();
            ViewBag.action = action.ToLowerInvariant();
            ViewBag.lang = parameterValue.ToLowerInvariant();
            // ViewBag.langTranslation = parameterValue.ToLowerInvariant();// 暂时测试使用 parameterValue
            #endregion

            #region about login
            if (filterContext.HttpContext.Session.GetString("AdminCurrentUser") == null)
            {
                var isremembered = filterContext.HttpContext.Request.Cookies.TryGetValue(LoginCookieBase64.GetCookieRememberBase64, out string IsRemembered);
                if (isremembered)
                {
                    ViewBag.isremembered = "checked";
                    filterContext.HttpContext.Request.Cookies.TryGetValue(LoginCookieBase64.GetCookieUserNameBase64, out string encryptUserName);
                    filterContext.HttpContext.Request.Cookies.TryGetValue(LoginCookieBase64.GetCookiePasswordBase64, out string encrptyPassword);
                    if (null != encryptUserName)
                        ViewBag.username = RSAData.RSADecrypt(encryptUserName, "username");
                    if (null != encrptyPassword)
                        ViewBag.password = RSAData.RSADecrypt(encrptyPassword, "password");
                }
                ViewBag.logged = false;
            }
            else
            {
                ViewBag.logged = true;
            }
            #endregion
            base.OnActionExecuting(filterContext);
        }
    }
}
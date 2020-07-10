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
using CommonUtils;
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
            JudgeRouteLang(ref parameterValue, filterContext);
            ViewBag.controller = controller.ToLowerInvariant();
            ViewBag.action = action?.ToLowerInvariant();
            ViewBag.lang = parameterValue?.ToLowerInvariant();
            ViewBag.langTranslation = parameterValue?.ToLowerInvariant();// 暂时测试使用 parameterValue
            #endregion
            FilterLogin(filterContext);
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// judge and set route lang
        /// </summary>
        /// <param name="parameterValue">lang of route</param>
        /// <param name="filterContext">filterContext parameter</param>
        [NonAction]
        private void JudgeRouteLang(ref string parameterValue, ActionExecutingContext filterContext)
        {
            //parameter is null or empty or excoluded with parterrn
            if (string.IsNullOrEmpty(parameterValue) || !ModelsLibrary.Languages.Utils.LanguagesParterrn.ToString().Contains(parameterValue))
            {
                if (HttpContext.Request.Cookies.TryGetValue("lang", out string lang)
                    && !string.IsNullOrEmpty(lang)
                    && ModelsLibrary.Languages.Utils.LanguagesParterrn.ToString().Contains(lang))
                {
                    parameterValue = lang;
                }
                else
                {
                    SetLocalLanguage(ref parameterValue, filterContext);
                }
            }
        }

        /// <summary>
        /// Set Local language culture ,default is 'en'
        /// </summary>
        /// <param name="param">parameter of route</param>
        /// <param name="localLanguage">computer default language of culture</param>
        /// <returns></returns>
        [NonAction]
        private void SetLocalLanguage(ref string param, ActionExecutingContext filterContext)
        {
            var localLanguage = System.Globalization.CultureInfo.InstalledUICulture.Name.ToLower();
            param = ModelsLibrary.Languages.Utils.LanguagesParterrn.ToString().Contains(localLanguage) ?
            localLanguage : "en";
            filterContext.HttpContext.Response.Cookies.Append("lang", param, new CookieOptions { Expires = DateTimeOffset.Now.AddYears(1) });
        }


        /// <summary>
        /// judge  Login (remember password)
        /// </summary>
        /// <param name="filterContext"></param>
        void FilterLogin(ActionExecutingContext filterContext)
        {

            #region about login
            if (string.IsNullOrEmpty(filterContext.HttpContext.Session.GetString("CurrentUser")))
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
                ViewBag.logged = false;
            }
            else
            {
                ViewBag.userprofile = "showUserOptions()";
                ViewBag.logged = true;
            }
            #endregion

        }
    }
}

using System.Text;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyPersonalWeb.Areas.Admin
{



    public class PromissionAttribute : ActionFilterAttribute
    {

        [NonAction]
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            #region do the filter
                
                
            #endregion
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
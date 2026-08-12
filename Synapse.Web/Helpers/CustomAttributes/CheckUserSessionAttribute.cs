using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Web;

namespace Synapse.Web.Helpers.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CheckUserSessionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ISession session = filterContext.HttpContext.Session;
            var user = session.GetString("Core.Models.Extensions.CustomeUser");

            if (user == null) // Or string.IsNullOrEmpty(userString) depending on your type
            {
                // 1. Generate the absolute or relative URL using the modern LinkGenerator or IUrlHelper
                var urlHelperFactory = filterContext.HttpContext.RequestServices.GetRequiredService<IUrlHelperFactory>();
                var urlHelper = urlHelperFactory.GetUrlHelper(filterContext);
                var loginUrl = urlHelper.Content("~/Account/Login");

                // 2. Clear the modern session context (replaces RemoveAll, Clear, and Abandon)
                filterContext.HttpContext.Session.Clear();

                // 3. Perform the redirect by setting the Result property (Short-circuits the pipeline)
                filterContext.Result = new RedirectResult(loginUrl);
                return;
            }
        }
    }
}
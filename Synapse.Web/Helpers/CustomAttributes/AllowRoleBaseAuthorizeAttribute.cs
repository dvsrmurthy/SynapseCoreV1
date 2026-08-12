using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Synapse.Web.Helpers.SecureAccess;

namespace Synapse.Web.Helpers.CustomAttributes
{
    
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AllowRoleBaseAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        // Modern equivalent to MasterName is Layout in .NET Core Razor Views
        public virtual string? MasterName { get; set; }
        public virtual string ViewName { get; set; } = "Error";

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var httpContext = context.HttpContext;

            // 1. Execute authorization logic
            if (AuthorizeCore(httpContext))
            {
                SetCachePolicy(httpContext);
            }
            // 2. Auth failed because user is not logged in
            else if (httpContext.User.Identity?.IsAuthenticated != true)
            {
                context.Result = new ChallengeResult(); // Modern equivalent to HttpUnauthorizedResult()
            }
            // 3. Authenticated but lacks permissions (Forbidden)
            else
            {
                // Recreating the legacy ViewData message mapping for .NET Core 8
                var modelMetadataProvider = context.HttpContext.RequestServices.GetService(typeof(IModelMetadataProvider)) as IModelMetadataProvider;
                var viewData = new ViewDataDictionary(modelMetadataProvider!, context.ModelState)
                {
                    ["Message"] = "You do not have sufficient privileges for this operation."
                };

                context.Result = new ViewResult
                {
                    ViewName = this.ViewName,
                    ViewData = viewData
                };

                // Note: Modern MVC renders Layout specified in the view, 
                // but if you dynamically override it, you pass it via ViewData or a View Model.
            }

            await Task.CompletedTask;
        }

        private bool AuthorizeCore(HttpContext httpContext)
        {
            // .NET Core 8 requires manual serialization/deserialization or complex object parsing for Session.
            //// For dynamic typing to match your legacy approach, we fetch the complex objects from Session.
            //dynamic userActions = httpContext.Session.GetString("Synapse.Web.Models.LayoutMenu");
            //dynamic extUser = httpContext.Session.GetString("Core.Models.Extensions.CustomeUser");

            //if (userActions != null)
            //{
            //    // Extract the route data for 'controller' in .NET Core 8
            //    var controllerName = httpContext.Session.GetString("controller")?.ToString();

            //    // Cast dynamic underlying collection to its structured list type
            //    var actionsList = userActions.UserActions as List<UserActions>;
            //    var isHavingPermission = actionsList?.FirstOrDefault(x =>
            //        x.ControllerName.Equals(controllerName, StringComparison.OrdinalIgnoreCase));

            //    if (isHavingPermission == null)
            //    {
            //        httpContext.Session.SetString("internalmessage", "Your session has been expired.");
            //    }

            //    if (extUser?.LogOnRespons?.uIsFirstLogin == 1)
            //    {
            //        httpContext.Session.SetString("internalmessage", "User Doesn't have to Execute this Operation.");
            //    }

            //    bool isFirstLoginCheck = extUser?.LogOnRespons?.uIsFirstLogin == null || extUser?.LogOnRespons?.uIsFirstLogin == 0;
            //    return isHavingPermission != null && isFirstLoginCheck;
            //}

            return false;
        }

        private void SetCachePolicy(HttpContext httpContext)
        {
            // Modern approach to prevent downstream proxy and browser caching
            var response = httpContext.Response;
            response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate, max-age=0";
            response.Headers["Pragma"] = "no-cache";
            response.Headers["Expires"] = "-1";
        }
    }
}
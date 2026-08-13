using Core.Models;
using Core.Models.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Synapse.Web.CampaignPlugin.Helpers.SecureAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SessionExtensions = Synapse.Web.CampaignPlugin.Helpers.SecureAccess.SessionExtensions;

namespace Synapse.Web.CampaignPlugin.Helpers.CustomAttributes
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
            // For dynamic typing to match your legacy approach, we fetch the complex objects from Session.
            dynamic userActions = SessionExtensions.GetItem<List<UserActions>>(httpContext.Session);
            dynamic extUser = SessionExtensions.GetItem<Core.Models.Extensions.CustomeUser>(httpContext.Session);

            if (userActions != null)
            {
                // Extract the route data for 'controller' in .NET Core 8
                var controllerName = httpContext.GetRouteValue("controller")?.ToString();

                // Cast dynamic underlying collection to its structured list type
                var actionsList = userActions as List<UserActions>;
                var isHavingPermission = actionsList?.FirstOrDefault(x =>x.ControllerName.Equals(controllerName, StringComparison.OrdinalIgnoreCase));

                if (isHavingPermission == null)
                {
                    httpContext.Session.SetString("internalmessage", "Your session has been expired.");
                }

                if (extUser?.LogOnRespons?.uIsFirstLogin == 1)
                {
                    httpContext.Session.SetString("internalmessage", "User Doesn't have to Execute this Operation.");
                }

                bool isFirstLoginCheck = extUser?.LogOnRespons?.uIsFirstLogin == null || extUser?.LogOnRespons?.uIsFirstLogin == 0;
                return isHavingPermission != null && isFirstLoginCheck;
            }

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
    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    //public class AllowRoleBaseAuthorizeAttribute : AuthorizeAttribute
    //{        
    //    /// <summary>
    //    /// The name of the master page or view to use when rendering the view on authorization failure.  Default
    //    /// is null, indicating to use the master page of the specified view.
    //    /// </summary>
    //    public virtual string MasterName { get; set; }

    //    /// <summary>
    //    /// The name of the view to render on authorization failure.  Default is "Error".
    //    /// </summary>
    //    public virtual string ViewName { get; set; }

    //    public AllowRoleBaseAuthorizeAttribute()
    //        : base()
    //    {
    //        this.ViewName = "Error";
    //    }

    //    protected void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
    //    {
    //        validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
    //    }

    //    public override void OnAuthorization(AuthorizationContext filterContext)
    //    {
    //        if (AuthorizeCore(filterContext.HttpContext))
    //        {
    //            SetCachePolicy(filterContext);
    //        }
    //        else if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
    //        {
    //            // auth failed, redirect to login page
    //            filterContext.Result = new HttpUnauthorizedResult();
    //        }
    //        else
    //        {
    //            ViewDataDictionary viewData = new ViewDataDictionary
    //            {
    //                {"Message", "You do not have sufficient privileges for this operation."}
    //            };
    //            filterContext.Result = new ViewResult
    //            {
    //                MasterName = this.MasterName,
    //                ViewName = this.ViewName,
    //                ViewData = viewData
    //            };
    //        }
    //    }

    //    protected void SetCachePolicy(AuthorizationContext filterContext)
    //    {
    //        HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
    //        cachePolicy.SetProxyMaxAge(new TimeSpan(0));
    //        cachePolicy.AddValidationCallback(CacheValidateHandler, null /* data */);
    //    }

    //    protected override bool AuthorizeCore(HttpContextBase httpContext)
    //    {
    //        dynamic UserActions = HttpContext.Current.Session["Synapse.Web.Models.LayoutMenu"];
    //        if (UserActions != null)
    //        {
    //            var controllerName = HttpContext.Current.Request.RequestContext.RouteData.GetRequiredString("controller");
    //            var isHavingPermission =
    //                (UserActions.UserActions as List<UserActions>).FirstOrDefault(
    //                    x => x.ControllerName.Equals(controllerName));
    //            return isHavingPermission != null;
    //        }
    //        return false;
    //    }
    //}
}
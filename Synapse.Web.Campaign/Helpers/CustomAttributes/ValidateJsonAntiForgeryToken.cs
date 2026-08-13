//using System;
//using System.Net;
//using System.Web;
//using System.Web.Helpers;
//using System.Web.Mvc;


//namespace Synapse.Web.CampaignPlugin.Helpers.CustomAttributes
//{
//    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
//    public class ValidateJsonAntiForgeryToken : AuthorizeAttribute
//    {
//        public override void OnAuthorization(AuthorizationContext filterContext)
//        {
//            var request = filterContext.HttpContext.Request;

//            if (request.HttpMethod == WebRequestMethods.Http.Post)
//            {
//                if (request.IsAjaxRequest())
//                    AntiForgery.Validate(CookieValue(request), request.Headers["__RequestVerificationToken"]);
//                else
//                    new ValidateAntiForgeryTokenAttribute().OnAuthorization(filterContext);
//            }
//        }

//        private string CookieValue(HttpRequestBase request)
//        {
//            var cookie = request.Cookies[AntiForgeryConfig.CookieName];
//            return cookie != null ? cookie.Value : null;
//        }
//    }
//}
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Synapse.Web.CampaignPlugin.Helpers.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ValidateJsonAntiForgeryTokenAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var httpContext = context.HttpContext;
            var request = httpContext.Request;

            // Only validate state-changing POST requests matching your original logic
            if (HttpMethods.IsPost(request.Method))
            {
                // Resolve modern Antiforgery services via Dependency Injection container
                var antiforgery = httpContext.RequestServices.GetRequiredService<IAntiforgery>();

                if (IsAjaxRequest(request))
                {
                    try
                    {
                        // Modern .NET Core automatically reads the cookie and matches it against 
                        // either form data OR a header token (typically named "RequestVerificationToken" or custom configured)
                        await antiforgery.ValidateRequestAsync(httpContext);
                    }
                    catch (AntiforgeryValidationException)
                    {
                        // Return a clean 400 Bad Request if the token is invalid or missing
                        context.Result = new BadRequestObjectResult("Invalid anti-forgery token.");
                    }
                }
                else
                {
                    try
                    {
                        // Standard full postback validation equivalent to old ValidateAntiForgeryTokenAttribute
                        await antiforgery.ValidateRequestAsync(httpContext);
                    }
                    catch (AntiforgeryValidationException)
                    {
                        context.Result = new BadRequestObjectResult("Anti-forgery token validation failed.");
                    }
                }
            }
        }

        /// <summary>
        /// .NET Core alternative to verify if incoming traffic is an AJAX call
        /// </summary>
        private bool IsAjaxRequest(HttpRequest request)
        {
            if (request == null) return false;

            return request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }
}
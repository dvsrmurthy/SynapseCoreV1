//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Cryptography;
//using System.Text;
//using System.Web.Caching;

//namespace Synapse.Web.Helpers.CustomAttributes
//{
//    public class PreventSpamAttribute : ActionFilterAttribute
//    {
//        public int DelayRequest = 10;
//        // The Error Message that will be displayed in case of 
//        // excessive Requests
//        public string? ErrorMessage = "Excessive Request Attempts Detected.";
//        // This will store the URL to Redirect errors to
//        public string? RedirectURL;

//        public override void OnActionExecuting(ActionExecutingContext filterContext)
//        {
//            // Store our HttpContext (for easier reference and code brevity)
//            var request = filterContext.HttpContext.Request;
//            // Store our HttpContext.Cache (for easier reference and code brevity)
//            var cache = filterContext.HttpContext.Cache;

//            // Grab the IP Address from the originating Request (example)
//            var originationInfo = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress;

//            // Append the User Agent
//            originationInfo += request.UserAgent;

//            // Now we just need the target URL Information
//            var targetInfo = request.RawUrl + request.QueryString;

//            // Generate a hash for your strings (appends each of the bytes of
//            // the value into a single hashed string
//            var hashValue = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(originationInfo + targetInfo)).Select(s => s.ToString("x2")));

//            // Checks if the hashed value is contained in the Cache (indicating a repeat request)
//            if (cache[hashValue] != null)
//            {
//                // Adds the Error Message to the Model and Redirect
//                filterContext.Controller.ViewData.ModelState.AddModelError("ExcessiveRequests", ErrorMessage);
//            }
//            else
//            {
//                // Adds an empty object to the cache using the hashValue
//                // to a key (This sets the expiration that will determine
//                // if the Request is valid or not)
//                //cache.Insert(hashValue, null, null, DateTime.Now.AddSeconds(DelayRequest), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
//                cache.Insert(hashValue, DBNull.Value, null, DateTime.Now.AddMinutes(DelayRequest), Cache.NoSlidingExpiration);
//            }
//            base.OnActionExecuting(filterContext);
//        }
//    }
//}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.Web.Helpers.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class PreventSpamAttribute : Attribute, IAsyncActionFilter
    {
        public int DelayRequest { get; set; } = 10;

        // The Error Message that will be displayed in case of excessive Requests
        public string? ErrorMessage { get; set; } = "Excessive Request Attempts Detected.";

        // This will store the URL to Redirect errors to
        public string? RedirectURL { get; set; }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var httpContext = context.HttpContext;
            var request = httpContext.Request;

            // Resolve modern IMemoryCache from the request services (DI)
            var cache = httpContext.RequestServices.GetRequiredService<IMemoryCache>();

            // Grab the IP Address safely factoring in proxies (Forwarded headers)
            string originationInfo = request.Headers["X-Forwarded-For"].FirstOrDefault()
                                     ?? httpContext.Connection.RemoteIpAddress?.ToString()
                                     ?? "UnknownIP";

            // Append the User Agent
            originationInfo += request.Headers["User-Agent"].ToString();

            // Fetch the modern full Request URL string with its query details
            string targetInfo = request.GetDisplayUrl();

            // Generate an MD5 hash securely using high-performance modern static methods
            byte[] inputBytes = Encoding.ASCII.GetBytes(originationInfo + targetInfo);
            byte[] hashBytes = MD5.HashData(inputBytes);
            string hashValue = string.Concat(hashBytes.Select(b => b.ToString("x2")));

            // Checks if the hashed value is contained in the modern MemoryCache
            if (cache.TryGetValue(hashValue, out _))
            {
                // Adds the Error Message to the ModelState
                context.ModelState.AddModelError("ExcessiveRequests", ErrorMessage);

                // Optional: If a Redirect URL was specified, skip action execution and route away immediately
                if (!string.IsNullOrEmpty(RedirectURL))
                {
                    context.Result = new RedirectResult(RedirectURL);
                    return; // Short-circuits the pipeline
                }
            }
            else
            {
                // Store a token object in the cache with an absolute expiration 
                // Equivalent to your legacy Cache.NoSlidingExpiration configuration
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(DelayRequest));

                cache.Set(hashValue, true, cacheEntryOptions);
            }

            // Continue down the MVC execution pipeline if valid
            await next();
        }
    }
}
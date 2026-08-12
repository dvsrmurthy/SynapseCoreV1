using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using log4net;

namespace APIServices.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ValidateBasicAuthrioze : ActionFilterAttribute
    {
        ILog Logger = LogManager.GetLogger(typeof(ValidateBasicAuthrioze));
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                if (actionContext.Request.Headers.Authorization == null)
                {
                    //actionContext.Response =
                       // new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                    return;
                }
                else
                {
                    var httpRequestHeader = actionContext.Request.Headers.GetValues("Authorization").FirstOrDefault();
                    if (string.IsNullOrWhiteSpace(httpRequestHeader))
                    {
                        return;
                    }
                    if (!httpRequestHeader.Equals(System.Configuration.ConfigurationManager.AppSettings["accsToken"]))
                    {
                        return;
                    }
                    //actionContext.Response = !string.IsNullOrWhiteSpace(httpRequestHeader)
                    //    ? (httpRequestHeader.Equals(System.Configuration.ConfigurationManager.AppSettings["accsToken"])
                    //        ? new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK)
                    //        : new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized))
                    //    : new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("ValidateBasicAuthrioze :: Internal server error :: {0}", ex.StackTrace);
                //actionContext.Response =
                //    new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
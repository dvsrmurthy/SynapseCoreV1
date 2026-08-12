using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

namespace Synapse.Web.Helpers
{
    public static class HtmlExtensions
    {
        public static IHtmlContent Translate(this IHtmlHelper htmlHelper, string key)
        {
            var httpContext = htmlHelper.ViewContext.HttpContext;
            var localizerFactory = httpContext.RequestServices.GetService<IStringLocalizerFactory>();

            if (localizerFactory == null)
            {
                throw new InvalidOperationException("Localization services are not registered.");
            }
            string viewPath = htmlHelper.ViewContext.ExecutingFilePath ?? string.Empty;
            // (Culture is handled automatically by the request culture middleware)
            var localizer = localizerFactory.Create(viewPath, string.Empty);
            var localizedString = localizer[key];
            return new HtmlString(localizedString.Value);
        }
    }
}
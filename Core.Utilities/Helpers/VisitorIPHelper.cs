using System;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Helpers
{
    public class VisitorIPHelperFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public VisitorIPHelperFactory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string? RequesterBrowserInfo { get; set; }
        public VisitorIPHelperFactory()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            string browserInfo =
             "RemoteUser=" + httpContext.GetServerVariable("REMOTE_USER") + ";"
            + "RemoteHost=" + httpContext.GetServerVariable("REMOTE_HOST") + ";";
            //+ "Type=" + httpContext.Request..Browser.Type + ";"
            //+ "Name=" + HttpContext.Current.Request.Browser.Browser + ";"
            //+ "Version=" + HttpContext.Current.Request.Browser.Version + ";"
            //+ "MajorVersion=" + HttpContext.Current.Request.Browser.MajorVersion + ";"
            //+ "MinorVersion=" + HttpContext.Current.Request.Browser.MinorVersion + ";"
            //+ "SupportsCookies=" + HttpContext.Current.Request.Browser.Cookies + ";"
            //+ "SupportsJavaScript=" + HttpContext.Current.Request.Browser.EcmaScriptVersion.ToString() + ";"
            //+ "SupportsActiveXControls=" + HttpContext.Current.Request.Browser.ActiveXControls + ";"
            //+ "SupportsJavaScriptVersion=" + HttpContext.Current.Request.Browser["JavaScriptVersion"] + "||"
            //+ "Platform=" + HttpContext.Current.Request.UserAgent;
            this.RequesterBrowserInfo = BuildXMLDocument(browserInfo);
            #region Commented - working
            //Dictionary<string, string> osList = new Dictionary<string, string>
            //{
            //    {"Windows NT 6.3", "Windows 8.1"},
            //    {"Windows NT 6.2", "Windows 8"},
            //    {"Windows NT 6.1", "Windows 7"},
            //    {"Windows NT 6.0", "Windows Vista"},
            //    {"Windows NT 5.2", "Windows Server 2003"},
            //    {"Windows NT 5.1", "Windows XP"},
            //    {"Windows NT 5.0", "Windows 2000"}
            //};

            //string userAgentText = HttpContext.Current.Request.UserAgent;
            //if (userAgentText != null)
            //{
            //    var userAgentString = GetSubstringByString("(", ")", userAgentText).Split(';').Select(x=> x.TrimStart()).ToArray();
            //    var result = osList.FirstOrDefault(x => userAgentString.Contains(x.Key));
            //}
            #endregion
        }
        public string? GetBrowserType()
        {
            // 1. Get the current HttpContext safely
            var context = _httpContextAccessor.HttpContext;
            if (context == null) return "No active HTTP request";

            // 2. Fetch the User-Agent header from the request
            string userAgent = context.Request.Headers["User-Agent"].ToString();
            
            // 3. Fallback check if header is empty
            if (string.IsNullOrEmpty(userAgent)) return "Unknown";

            // 4. Implement basic detection or map to your logic
            if (userAgent.Contains("Edg")) return "Microsoft Edge";
            if (userAgent.Contains("Chrome")) return "Google Chrome";
            if (userAgent.Contains("Safari") && !userAgent.Contains("Chrome")) return "Safari";
            if (userAgent.Contains("Firefox")) return "Mozilla Firefox";

            return userAgent; // Or return a truncated version/type
        }

        public string? GetSubstringByString(string a, string b, string c)
        {
            return c.Substring((c.IndexOf(a) + a.Length), (c.IndexOf(b) - c.IndexOf(a) - a.Length));
        }

        public string? BuildXMLDocument(string browserInfo)
        {
            try
            {
                var result = new XElement("root",
                     from s in browserInfo.Split(new string[] { "||" }, StringSplitOptions.None)[0].Split(';')
                     select new XElement(s.Split('=')[0].ToString(), s.Split('=')[1].ToString()));
                result.Add(new XElement("Platform", browserInfo.Split(new string[] { "||" }, StringSplitOptions.None)[1].ToString()));
                return result.ToString();
            }
            catch (Exception ex)
            {

            }
            return string.Empty;
        }
    }
}

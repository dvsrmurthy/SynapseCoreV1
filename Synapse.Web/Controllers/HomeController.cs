using Apache.NMS.ActiveMQ;
using Core.Models;
using Core.Models.Dtos.CommonDtos;
using Core.Models.Dtos.Requests.Synapse.Analytics;
using Core.Models.Dtos.Requests.Synapse.UserCampaigns;
using Core.Models.Dtos.Responses.Synapse.Analytics;
using Core.Models.Dtos.Responses.Synapse.UserCampaigns;
using Core.Models.Extensions;
using Core.Utilities.Helpers;
using Elmah;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Synapse.Web.Helpers;
using Synapse.Web.Helpers.SecureAccess;
using Synapse.Web.Models;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using CustomeUser = Core.Models.Extensions.CustomeUser;
using SessionExtensions = Synapse.Web.Helpers.SecureAccess.SessionExtensions;

namespace Synapse.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        internal CustomeUser ExtendedUser = null; 
        internal List<UserActions> UserActions = null;
        ILog Logger = LogManager.GetLogger(typeof(HomeController));
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, 
            IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
            ExtendedUser = _httpContextAccessor.HttpContext.Session.GetItem<CustomeUser>();
            UserActions = _httpContextAccessor.HttpContext.Session.GetItem<List<UserActions>>();
            _configuration = configuration;
        }

        public IActionResult Index()
        {            
            return RedirectToAction("About", "Home");            
        }
        public IActionResult About()
        {            
            return View();         
        }
        public IActionResult Contact()
        {
            return View();            
        }
        public IActionResult services()
        {
            return View();
        }
        public IActionResult complaince()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        
    }
   
}

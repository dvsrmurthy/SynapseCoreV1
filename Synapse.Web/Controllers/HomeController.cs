using Apache.NMS.ActiveMQ;
using Core.Models;
using Core.Models.Extensions;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Synapse.Web.Models;
using System.Diagnostics;
using Synapse.Web.Helpers.SecureAccess;
using CustomeUser = Core.Models.Extensions.CustomeUser;

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

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
            ExtendedUser = _httpContextAccessor.HttpContext.Session.GetItem<CustomeUser>();
            UserActions = _httpContextAccessor.HttpContext.Session.GetItem<List<UserActions>>();
        }

        public IActionResult Index()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

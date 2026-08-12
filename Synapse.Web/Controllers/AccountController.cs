using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Core.Models;
using Core.Models.Dtos.CommonDtos;
using Core.Models.Dtos.Requests.Synapse.UserCampaigns;
using Core.Models.Dtos.Responses.Synapse.Account;
using Core.Models.Dtos.Responses.Synapse.UserCampaigns;
using Core.Models.Enums;
using Core.Models.Extensions;
using Core.Utilities.Helpers;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using log4net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Synapse.Web.Helpers;
using Synapse.Web.Helpers.SecureAccess;
using Synapse.Web.Models;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;
using static RasterEdge.Imaging.MSOffice.Spreadsheet.XlsFileFormat.Records.Chart3DBarShape;
using SessionExtensions = Synapse.Web.Helpers.SecureAccess.SessionExtensions;

namespace Synapse.Web.Controllers
{
    public class AccountController : Controller
    {
        ILog Logger = LogManager.GetLogger(typeof(AccountController));
        internal List<UserActions> UserActions = null;
        internal Core.Models.Extensions.CustomeUser ExtendedUser = null;
        internal LocalizationResponse lz = null;

        private IHttpContextAccessor _httpContextAccessor;
        private IConfiguration _configuration;

        public AccountController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            try
            {
                ExtendedUser = _httpContextAccessor.HttpContext?.Session?.GetItem<Core.Models.Extensions.CustomeUser>();
                UserActions = _httpContextAccessor.HttpContext?.Session?.GetItem<List<UserActions>>();
                lz = _httpContextAccessor.HttpContext?.Session?.GetItem<LocalizationResponse>();
            }
            catch
            {
                ExtendedUser = null;
                UserActions = null;
            }
        }
        public IActionResult Index()
        {
            ViewBag.Message = "0";
            HttpContext.Session.SetString("nooftimes", "0");
            ViewBag.msg = "";
            HttpContext.Session.SetString("nooftimesinvalid", "0");
            HttpContext.Session.SetString("rancatpch", string.Empty);
            if (ExtendedUser != null && ExtendedUser.LogOnRespons != null && !string.IsNullOrWhiteSpace(ExtendedUser.LogOnRespons.UserName))
            {
                return null;
            }
            var randomCaptcha = GetRandomCaptcha(); //Forgot password captcha
            ViewBag.Captch = randomCaptcha;
            ViewBag.LoginCaptcha = randomCaptcha;
            HttpContext.Session.SetString("CaptchaCode", randomCaptcha);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LChange(string lng)
        {
            switch (lng)
            {
                case "ar-lan":
                    HttpContext.Session.SetString("ln", "ab");
                    SessionExtensions.UpdateItem<LocalizationResponse>(HttpContext.Session, IEnumerableExtension.BuildLocalizations("ab"));
                    break;
                default:
                    HttpContext.Session.SetString("ln", "en");
                    SessionExtensions.UpdateItem<LocalizationResponse>(HttpContext.Session, IEnumerableExtension.BuildLocalizations("en"));
                    break;
            }
            return Json("true");
        }
        [AllowAnonymous]


        private string GetRandomCaptcha()
        {
            StringBuilder randomText = new StringBuilder();
            string alphabets = "012345679ACEFGHKLMNPRSWXZabcdefghijkhlmnopqrstuvwxyz";
            Random r = new Random();

            for (int j = 0; j <= 5; j++)
            {
                randomText.Append(alphabets[r.Next(alphabets.Length)]);
            }

            HttpContext.Session.SetString("CaptchaCode", randomText.ToString());
            return randomText.ToString();
        }

        public string? ipWhitelistOTPSCREEN(int userid, int customerId)
        {
            LogOnRespons result = new LogOnRespons();
            if (HttpContext.Session.GetString("result") != null)
            {
                result = HttpContext.Session.GetItem<LogOnRespons>();
            }
            if (result.IPWhiteListCount >= 50)
            {
                Logger.Info("IP whitelist limit exceeded. Please contact the administrator.");
                return "IP whitelist limit exceeded. Please contact the administrator.";
            }
            if (result.OtpCount >= 50)
            {
                Logger.Info("OTP limit exceeded. Please contact the administrator.");
                return "OTP Request limit exceeded. Please contact the administrator.";
            }
            using (var ClientAccess = new AuthenticateSecurityClient())
            {
                Logger.Info("Quick SMS XML File Generation and OTP SMS DB Insertion");
                var fileContent = new StringBuilder();
                int ValidMobNosCnt = 0;
                string validMobileNos = string.Empty;
                string ReturnVal = string.Empty;
                var LogPath = _configuration["tempPath"] + "\\QuickSMS" + "QuickSMS.txt";
                int SenderID = Convert.ToInt32(_configuration["OTPintSenderId"]);
                fileContent.AppendLine("---------------------- LOG :: SenderID : " + SenderID + " :: Time : " + DateTime.Now + " ----------------------");
                try
                {
                    var sender_countrycodes = ValidateMobileNumbers(Convert.ToInt32(SenderID));
                    if (sender_countrycodes.Any())
                    {
                        if (!string.IsNullOrWhiteSpace(result.MobileNo))
                        {
                            var totLengthValid = sender_countrycodes.Where(w => w.TotalLength.Equals(result.MobileNo.Length));
                            if (!totLengthValid.Any())
                            {
                                Logger.Info("InValid series for " + result.MobileNo + ", Sender ID " + SenderID);
                                ViewBag.msg = "InValid mobile number series, Please contact admin !!";
                                ReturnVal = "Invalid mobile number";
                                return ReturnVal;
                            }
                            var validcountrycode = from n in sender_countrycodes
                                                   let countrycode = n.CountryCode
                                                   let countrycodelength = n.CountryCode.ToString().Length
                                                   where result.MobileNo.Substring(0, countrycodelength) == n.CountryCode.ToString()
                                                   select result.MobileNo;
                            if (!validcountrycode.Any())
                            {
                                Logger.Info("InValid country for " + result.MobileNo + ", Sender ID " + SenderID);
                                ReturnVal = "Invalid mobile number";
                                return ReturnVal;
                            }
                            if (totLengthValid.Any() && validcountrycode.Any())
                            {
                                validMobileNos = result.MobileNo;
                            }
                        }
                    }
                    string otp = GetRandomOTP();
                    if (validMobileNos != null && validMobileNos != "")
                    {
                        string oResult = "";
                        string msg = "Your OTP for Whitelist IP Address : " + otp;
                        int noofTimes = HttpContext.Session.GetInt32("nooftimes") != null ? Convert.ToInt32(HttpContext.Session.GetInt32("nooftimes")) : 0;
                        noofTimes = noofTimes + 1;
                        HttpContext.Session.SetString("nooftimes", noofTimes.ToString());
                        List<string> Stage = new List<string>();
                        int SrvcStage = 0; string LangID = "1";
                        ViewBag.Message = "100";
                        ViewBag.isIpWhitelistScreen = "1";

                        if (string.IsNullOrEmpty(result.MobileNo) || result.MobileNo.Length <= 5)
                            ViewBag.MobileNo = result.MobileNo;

                        ViewBag.MobileNo = string.Concat(result.MobileNo.Select((c, i) => (i < 2 || i >= result.MobileNo.Length - 3) ? c : 'X'));
                        var response = ClientAccess.OTPQSMS(new InsertQSMSOnRequest
                        {
                            QSMSID = Convert.ToInt32(0),
                            SenderID = Convert.ToInt32(SenderID),
                            LangID = Convert.ToInt32(1),
                            Message = msg,
                            CharCount = msg.Length,
                            CreditsUsed = Convert.ToInt32(1),
                            CreditsCount = ValidMobNosCnt * 1,
                            DLR = 1,
                            CreatedBy = userid,
                            CustomerID = customerId,
                            Sender = _configuration["OTPSenderName"].ToString(),
                            Module = "QSMS",
                            MobileNos = validMobileNos,
                            Stage = string.Join(",", Stage.Distinct()),
                            Status = 0,
                            CurrentStatus = 1,
                            UserName = userid.ToString(),
                            UserIp = AppInternalEncKey.Encrypt(Request.Headers["X-Forwarded-For"].FirstOrDefault()?.Split(',')[0].Trim()
                                    ?? HttpContext.Connection.RemoteIpAddress?.ToString(), false),
                            Sendtime = "0",
                            OTPValue = otp
                        });

                        var result2 = response.Result.Split('$')[0];
                        var nId = response.Result.Split('$')[1];
                        ViewBag.Uname = HttpContext.Session.GetInt32("Username") != null ? HttpContext.Session.GetInt32("Username").ToString() : string.Empty;
                        Logger.Info("Mobile No Validated, DB Insertion done");

                        if (nId != "0")
                        {
                            var tCounts = validMobileNos.Split(',').Length;
                            if (tCounts == 0)
                            {
                                tCounts = 1;
                            }

                            if (LangID == "2")
                                LangID = "8";
                            else
                            {
                                if (msg.Contains('@') || msg.Contains('{') || msg.Contains('}'))
                                    LangID = "1";
                                else
                                    LangID = "1";
                            }

                            var fileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + nId;

                            string uname = result.UserName;

                            var xmlContent = "<root iscustome='false' priority='5'><sendsms userid='" + 1 + "'  username='" + uname + "' campainid='" + 0 + "' sender='" + WebUtility.HtmlEncode(_configuration["OTPSenderName"].ToString()) +
                                "' language='" + ((LangID)) + "' message='" + WebUtility.HtmlEncode(msg).Replace("\n", "&#10;") +
                                    "' mobile=''><mobile>" + validMobileNos + "</mobile></sendsms></root>";

                            var fPath = Path.Combine((_configuration["tempPath"] + "QuickSMS"), fileName);
                            if (!Directory.Exists(fPath))
                            {
                                Directory.CreateDirectory(fPath);
                            }

                            System.IO.File.WriteAllText(Path.Combine(fPath, fileName + ".xml"), xmlContent);

                            var QMsg = "action=start&camp_id=" + nId + "&camp_type=0&dir_name=" + fileName + "&count=" + tCounts;
                            var Qresult = new CampaignQLog().PushMessageToQ(QMsg);
                            ViewBag.Qresult = "OTPSCREEN";
                            Logger.Info("OTP SMS :: notification QMsg:: " + QMsg);
                        }

                        var nID = response.Result.Split('$')[1];
                        if (result2 == "7")
                        {
                            ReturnVal = "MsgSubmitSuccess";
                        }
                        else if (result2 == "4")
                        {
                            Logger.Info("Insufficent Credits");
                            ReturnVal = "Insufficent Credits";
                        }
                        else if (result2 == "8")
                        {
                            Logger.Info("Sender Inactive");
                            ReturnVal = "Sender Inactive";
                        }
                        else if (result2 == "0")
                        {
                            Logger.Info("Database Connection failed");
                            ReturnVal = "Database Connection failed";
                        }
                        else if (result2 == "10")
                        {
                            Logger.Info("Maximum OTP requests reached. A new OTP can be generated after " + result.OTPTime + " minutes.");
                            ReturnVal = "Maximum OTP requests reached. A new OTP can be generated after " + result.OTPTime + " minutes.";
                        }
                    }
                    else
                    {
                        ReturnVal = "InValid mobile number series!!";
                    }
                    return ReturnVal;
                }
                catch (Exception exp)
                {
                    string oResult = "";
                    oResult = exp.Message;
                    Logger.InfoFormat("Error - While OTP Send in IP Whitelist -{0}", exp.Message);
                    return exp.Message;
                }
            }
        }
        private string GetRandomOTP()
        {
            StringBuilder randomText = new StringBuilder();
            string digits = "0123456789";
            Random r = new Random();

            for (int j = 0; j <= 5; j++)
            {
                randomText.Append(digits[r.Next(digits.Length)]);
            }

            HttpContext.Session.SetString("OTPCode", randomText.ToString());
            return randomText.ToString();
        }
        private List<MobileLengthValidationResponse> ValidateMobileNumbers(int senderid)
        {
            try
            {
                using (var ClientAccess = new AuthenticateSecurityClient())
                {
                    var result = ClientAccess.ValidateMobileNums(senderid);
                    return result.Result;
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        private int ValidateCredentialsForIPWhiteList(string username, string email)
        {
            return 1;
        }
        public ActionResult Login(string returnUrl)
        {
            ViewBag.Message = "0";
            HttpContext.Session.SetString("nooftimes", "0");
            ViewBag.msg = "";
            HttpContext.Session.SetString("nooftimesinvalid", "0");
            HttpContext.Session.SetString("rancatpch", string.Empty);
            if (ExtendedUser != null && ExtendedUser.LogOnRespons != null && !string.IsNullOrWhiteSpace(ExtendedUser.LogOnRespons.UserName))
            {
                return null;
            }
            var randomCaptcha = GetRandomCaptcha(); //Forgot password captcha
            ViewBag.Captch = randomCaptcha;
            ViewBag.LoginCaptcha = randomCaptcha;
            HttpContext.Session.SetString("CaptchaCode", randomCaptcha);
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model)
        {
            switch (model.forgotForm)
            {
                case "0": { return LoginValidationLogic(model); }
                case "1": { return ForgotPasswordLogic(model); }
                case "2": { return IpWhitelistLogic(model); }
            }
            return View(model);
        }
        private ActionResult LoginValidationLogic(LoginViewModel model)
        {
            HttpContext.Session.SetString("FogotPassword", "false");
            HttpContext.Session.SetString("Username", model.Email);
            var Attempt = _configuration["AttemptMessage"];
            var Unsuccess = _configuration["Unsuccessful"];
            var Freeze = _configuration["Freeze"];
            string Attval = string.Empty;
            string Freeval = string.Empty;
            Logger.Info("Login Action :: Started");
            if (!string.IsNullOrWhiteSpace(model.ActionVerificationId) && !string.IsNullOrWhiteSpace(model.VerifiedToken))
            {
                model.Email = AESEncrytDecry.DecryptStringAES(model.ActionVerificationId);
                model.Password = AESEncrytDecry.DecryptStringAES(model.VerifiedToken);
            }

            if (model.randomcapt != null)
            {
                if (model.randomcapt != AESEncrytDecry.DecryptStringAES(model.LoginCaptchaValue))
                {
                    Logger.Info("Invalid Captcha, user entered Captcha : " + model.forgotPasswordrandomcapt + ", original captcha : " + HttpContext.Session.GetString("CaptchaCode"));
                    ModelState.AddModelError("", "Invalid Captcha");
                    var randomCaptcha2 = GetRandomCaptcha();
                    ViewBag.Captch = randomCaptcha2;
                    ViewBag.LoginCaptcha = randomCaptcha2;
                    ViewBag.Message = "0";
                    return View(model);
                }
            }

            if (model.Otp != null)
            {
                model.usercheck = "1";
            }
            else
            {
                model.usercheck = "0";
            }
            //if (!ModelState.IsValid)
            //{
            //    Logger.InfoFormat("modelstate isvalid returned ::{0}, {1} ", model.Email, model.Password);
            //    return View(model);
            //}
            if (model.loginCaptcha != null && model.usercheck == "0" && model.LoginCaptchaValue != "0")
            {
                if (model.loginCaptcha != AESEncrytDecry.DecryptStringAES(model.LoginCaptchaValue))
                {
                    Logger.Info("Invalid Captcha, user entered Captcha : " + model.loginCaptcha + ", original captcha : " + model.LoginCaptchaValue);
                    ModelState.AddModelError("", "Invalid Captcha");
                    ViewBag.Message = "0";
                    ViewBag.msg = "Invalid Captcha";
                    var rancatpch = GetRandomCaptcha();
                    ViewBag.LoginCaptcha = rancatpch;
                    HttpContext.Session.SetString("CaptchaCode", rancatpch);
                    return View(model);
                }
            }
            using (var clientAcces = new AuthenticateSecurityClient())
            {
                Logger.Info("API Services Execution Started");
                var networkIDetails = NetworkInterface.GetAllNetworkInterfaces();
                var result =
                    clientAcces.AuthenticateUser(new LogOnRequest
                    {
                        UserName = AppInternalEncKey.Encrypt(model.Email, false),
                        Password = AppInternalEncKey.Encrypt(model.Password, false),
                        otp = AppInternalEncKey.Encrypt(model.Otp == null ? "0" : model.Otp, false),
                        MacAddress =
                            AppInternalEncKey.Encrypt(networkIDetails[0].GetPhysicalAddress().ToString(), false),
                        IpAddress =
                            AppInternalEncKey.Encrypt(Request.Headers["X-Forwarded-For"].FirstOrDefault()?.Split(',')[0].Trim()
                                    ?? HttpContext.Connection.RemoteIpAddress?.ToString(), false),
                        UserSessionId =
                            AppInternalEncKey.Encrypt(HttpContext.Session.Id, false),
                        IsWebRequest = true,
                        useremail = model.Email,
                        UserId = 0
                    });
                result.UserName = model.Email;
                HttpContext.Session.AddItem(result);
                Logger.Info("API Services Execution Done");
                if (result != null)
                {
                    if (result.ActionResult == 0 && result.ReturnValue == 11)
                    {
                        result.ActionResult = ActionStatus.OTP;
                        Attval = result.RemainAttempts.ToString();
                    }
                    if (result.ActionResult == 0 && result.ReturnValue == 15)
                    {
                        result.ActionResult = ActionStatus.Freeze;
                        Freeval = result.FreezeTimeMinutes.ToString();
                    }
                    if (Attval == "0")
                    {
                        result.ActionResult = ActionStatus.Unsuccess;
                    }
                    var LogOnRespons = result;
                    HttpContext.Session.SetString("ln", "en");
                    string strcid = result.CustomerId.ToString();
                    string strctype = result.CustomerId.ToString();
                    string strrid = result.CustomerId.ToString();
                    HttpContext.Session.SetString("category", string.Empty);

                    GetCredits(AppInternalEncKey.Encrypt(strcid, false), AppInternalEncKey.Encrypt(strctype, false), AppInternalEncKey.Encrypt(strrid, false), "yes");
                    Logger.Info("API Services Execution Started ::" + result.ActionResult);
                    switch (result.ActionResult)
                    {
                        case ActionStatus.Success:
                            if (model.usercheck == "0" && result.IsTwoFactor)
                            {
                                model.usercheck = "1";
                                ViewBag.Message = model.usercheck;
                                ViewBag.Mobile = result.MobileNo;
                                TempData["ReMobile"] = result.MobileNo;
                                ViewBag.Email = result.Mail;
                                ViewBag.Uname = result.UserName;
                                ViewBag.Pword = model.Password;
                                var rancatpch1 = GetRandomCaptcha();
                                ViewBag.Captch = rancatpch1;
                                ViewBag.Otptime = result.OTPTime;
                                LogOnRespons.GetIPAddress = Request.Headers["X-Forwarded-For"].FirstOrDefault()?.Split(',')[0].Trim()
                                    ?? HttpContext.Connection.RemoteIpAddress?.ToString();

                                SessionExtensions.AddItem<Core.Models.Extensions.CustomeUser>(HttpContext.Session, new Core.Models.Extensions.CustomeUser
                                {
                                    LogOnRespons = LogOnRespons
                                });
                                return View(model);
                            }
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, LogOnRespons.UserName),
                                new Claim(ClaimTypes.NameIdentifier, LogOnRespons.Id.ToString())
                            };
                            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var principal = new ClaimsPrincipal(identity);
                            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                            SessionExtensions.AddItem<LocalizationResponse>(HttpContext.Session,
                               IEnumerableExtension.BuildLocalizations("en"));
                            var preferences = clientAcces.GetAllPreferences(result.CustomerId);
                            if (preferences != null && preferences.ActionStatus == ActionStatus.Success &&
                                preferences.Preferences.Any())
                            {
                                SessionExtensions.AddItem<PreferencesResponse>(HttpContext.Session, preferences);
                            }
                            LogOnRespons.GetIPAddress = Request.Headers["X-Forwarded-For"].FirstOrDefault()?.Split(',')[0].Trim()
                                    ?? HttpContext.Connection.RemoteIpAddress?.ToString();
                            var CustomerPreferences = clientAcces.GetCustomerAppPreferencesAsync(new ReUsableRequest { CustomerId = result.CustomerId });
                            if (CustomerPreferences.Result != null)
                            {

                                LogOnRespons.ProductLogoReplacedWithCLogo = CustomerPreferences.Result.ProductLogoReplacedWithCLogo;

                            }
                            SessionExtensions.AddItem<Core.Models.Extensions.CustomeUser>(HttpContext.Session, new Core.Models.Extensions.CustomeUser
                            {
                                LogOnRespons = LogOnRespons
                            });
                            ExtendedUser = _httpContextAccessor.HttpContext?.Session?.GetItem<Core.Models.Extensions.CustomeUser>();
                            SessionExtensions.AddItem<GlobalUsageProperties>(HttpContext.Session,
                                new GlobalUsageProperties
                                {
                                    DivisionTable = clientAcces.GetAllDivisionsColomnsAsync(ExtendedUser.LogOnRespons.CustomerId).Result.DivisionTable,
                                    UsersList = clientAcces.GetAllUsersAsync().Result.UsersList,
                                    PreferedList = clientAcces.GetAllPreferredCountry().Result.PreferedList,
                                    CustomerViewlist = clientAcces.GetReportcustomer(ExtendedUser.LogOnRespons.CustomerId, ExtendedUser.LogOnRespons.Id, ExtendedUser.LogOnRespons.RoleId).Result.CustomerViewlist,
                                    VendorViewlist = clientAcces.GetVendor().Result.VendorViewlist,
                                    SendersList = clientAcces.GetAllSendersAsync().Result.SendersList,
                                    UserType = clientAcces.GetDashBoardTypeAsync(ExtendedUser.LogOnRespons.Id).Result,
                                });

                            var preventPages =
                                    _configuration["PrePgs"].Split(',')
                                        .Where(w => !string.IsNullOrWhiteSpace(w))
                                        .Select(s => s)
                                        .ToList();
                            var MenuItems = new LayoutMenu(clientAcces, result.Id, (ExtendedUser.LogOnRespons.RoleId == 1), preventPages);
                            SessionExtensions.AddItem<List<MenuItem>>(HttpContext.Session, MenuItems.MenuItems);
                            if (result.CustomerType == (int)CustomerTypes.Enterpriser || result.CustomerType == (int)CustomerTypes.HmsEnterpriser)
                            {
                                var preventCust =
                                   _configuration["PrePgsRsl"].Split(',')
                                       .Where(w => !string.IsNullOrWhiteSpace(w))
                                       .Select(s => s)
                                       .ToList();
                                foreach (var item in preventCust)
                                {
                                    var pitem = MenuItems.MenuItems.FirstOrDefault(w => w.Name == item);
                                    if (pitem != null)
                                    {
                                        MenuItems.MenuItems.Remove(pitem);
                                    }
                                    foreach (var keypair in MenuItems.MenuKeyValuPaires)
                                    {
                                        var keyValue = keypair.Value.FirstOrDefault(f => f.Name == item);
                                        if (keyValue != null)
                                        {
                                            keypair.Value.Remove(keyValue);
                                        }

                                    }
                                }
                            }
                            else
                            {
                                var preventDepartmentsForReseller =
                                   _configuration["PrevDepartmentForResellers"].Split(',')
                                       .Where(w => !string.IsNullOrWhiteSpace(w))
                                       .Select(s => s)
                                       .ToList();
                                foreach (var w_item in preventDepartmentsForReseller)
                                {
                                    foreach (var key_item in MenuItems.MenuKeyValuPaires)
                                    {
                                        var s_items = key_item.Value.Select(s => s.ChildMenuItems);
                                        foreach (var _item in s_items)
                                        {
                                            var key_value = _item.FirstOrDefault(f => f.Name.Equals(w_item, StringComparison.OrdinalIgnoreCase));
                                            if (key_value != null)
                                            {
                                                _item.Remove(key_value);
                                            }
                                        }
                                    }
                                }
                            }

                            SessionExtensions.AddItem<List<KeyValuePair<string, List<MenuItem>>>>(HttpContext.Session,
                                MenuItems.MenuKeyValuPaires);

                            var Eticloud = _configuration["onPremiseCloud"];
                            if (Eticloud != "false" || Eticloud == "")
                            {
                                //starts here
                                /* this logic only for Ethi direct cloud version - start  */
                                var allowedModules = _configuration["CloudRestrictions"] != null
                                    ? _configuration["CloudRestrictions"].ToString()
                                        .Split(',')
                                        .Where(w => !string.IsNullOrWhiteSpace(w))
                                        .Select(s => s.Trim())
                                        .ToArray()
                                    : new string[0];
                                var modifiedMenuItems = new List<MenuItem>();
                                if (allowedModules.Any())
                                {
                                    modifiedMenuItems = (from o in MenuItems.MenuItems
                                                         join r in allowedModules on o.Name equals r.Split('-')[0]
                                                         select o).ToList();
                                    foreach (var a_item in allowedModules)
                                    {
                                        var menuArray = a_item.Split('-');
                                        var mItem = modifiedMenuItems.FirstOrDefault(w => w.Name.Equals(menuArray[0]));
                                        if (mItem != null)
                                        {
                                            var menuPages = menuArray[1];
                                            if (!string.IsNullOrWhiteSpace(menuPages))
                                            {
                                                var p = menuPages.Split('|');
                                                if (p.Length > 0)
                                                {
                                                    var pages = (from m in mItem.ChildMenuItems
                                                                 join n in p on m.Name equals n
                                                                 select m).ToList();
                                                    foreach (var pp in pages)
                                                    {
                                                        mItem.ChildMenuItems.Remove(pp);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    modifiedMenuItems = MenuItems.MenuItems;
                                }

                                SessionExtensions.AddItem<List<MenuItem>>(HttpContext.Session, modifiedMenuItems);
                                SessionExtensions.AddItem<List<UserActions>>(HttpContext.Session, MenuItems.UserActions);
                                SessionExtensions.AddItem<LayoutMenu>(HttpContext.Session, MenuItems);
                                /* this logic only for Ethi direct cloud version - end  */
                                //ends here
                            }
                            else
                            {
                                SessionExtensions.AddItem<List<MenuItem>>(HttpContext.Session, MenuItems.MenuItems);
                                SessionExtensions.AddItem<List<UserActions>>(HttpContext.Session, MenuItems.UserActions);
                                SessionExtensions.AddItem<LayoutMenu>(HttpContext.Session, MenuItems);
                            }
                            Logger.Info("Login Action :: End");
                            if (preferences.Preferences.Any())
                            {
                                var pref = preferences.Preferences.FirstOrDefault();
                                if (result.IsFirstLogin == 1)
                                {
                                    if ((pref.ChangeOnFirstLogin) && (result.uIsFirstLogin == 1))
                                    {
                                        return RedirectToAction("Index", "AdminOperationsPlugin", new { Area = "AdminOperationsPlugin" });
                                    }
                                }
                            }
                            return RedirectToAction("Index", "DashBoard");
                        case ActionStatus.Locked:
                            Logger.Info("Login Action :: End");
                            ModelState.AddModelError("", "Account has been locked please try after some time.");
                            ViewBag.msg = "Account has been locked please try after some time.";
                            return View("Lockout");
                        case ActionStatus.NetworkInstenceError:
                            ModelState.AddModelError("",
                                "Network issue, please contact system admin.");
                            ViewBag.msg = "Network issue, please contact system admin.";
                            return View(model);
                        case ActionStatus.IncompleteConfiguration:
                            ModelState.AddModelError("",
                                "Incomplete configuration, Please contact admin.");
                            ViewBag.msg = "Incomplete configuration, Please contact admin.";
                            return View(model);
                        case ActionStatus.CustomerExpired:
                            ModelState.AddModelError("", "Customer expired, please contact admin.");
                            ViewBag.msg = "Customer expired, please contact admin.";
                            break;
                        case ActionStatus.Fail:
                            ModelState.AddModelError("",
                                "due to other your account locked, please contact system admin.");
                            ViewBag.msg = "due to other your account locked, please contact system admin.";
                            Logger.Info("Login Action :: End");
                            return View(model);
                        case ActionStatus.UserSessionsExceeded:
                            ModelState.AddModelError("", "User Session Exceeded.");
                            ViewBag.msg = "User Session Exceeded.";
                            Logger.Info("Login Action :: End");
                            return View(model);
                        case ActionStatus.CustomerPendingOrRejectStatus:
                            ModelState.AddModelError("", "Please Contact Administrator.");
                            ViewBag.msg = "Please Contact Administrator.";
                            Logger.Info("Login Action :: End");
                            return View(model);
                        case ActionStatus.InValidRequest:
                            ModelState.AddModelError("", "User Not Authorized to Access Web App.");
                            ViewBag.msg = "User Not Authorized to Access Web App.";
                            Logger.Info("Login Action :: End");
                            return View(model);
                        case ActionStatus.OTP:
                            ModelState.AddModelError("", Attempt.Replace("x", Attval));
                            Logger.Info("Login Action :: End");
                            ViewBag.verifymessage = "Failed";
                            ViewBag.Pword = model.Password;
                            var rancatpch = GetRandomCaptcha();
                            ViewBag.Captch = rancatpch;
                            ViewBag.msg = Attempt.Replace("x", Attval);
                            //Session["CaptchaCodeNew"] = ViewBag.Captch.ToString();
                            ViewBag.Otptime = result.OTPTime;
                            return View(model);
                        case ActionStatus.Freeze:
                            ModelState.AddModelError("", Freeze.Replace("x", Freeval));
                            Logger.Info("Login Action :: End");
                            ViewBag.msg = Freeze.Replace("x", Freeval);
                            var rancatpch61 = GetRandomCaptcha();
                            ViewBag.Captch = rancatpch61;
                            return View(model);
                        case ActionStatus.Unsuccess:
                            ModelState.AddModelError("", Unsuccess);
                            ViewBag.msg = Unsuccess;
                            Logger.Info("Login Action :: End"); //User failed(In multifactor Authentication) with invalid OTP
                            var randomCaptcha3 = GetRandomCaptcha();
                            ViewBag.Captch = randomCaptcha3;
                            ViewBag.LoginCaptcha = randomCaptcha3;
                            //Session["CaptchaCode"] = randomCaptcha;
                            return View(model);
                        case ActionStatus.OtpExpire:
                            ModelState.AddModelError("", "Otp expired");
                            ViewBag.msg = "Otp expired";
                            Logger.Info("Login Action :: End");
                            return View(model);
                        case ActionStatus.InvalidIpAddress:
                            ModelState.AddModelError("", "Your IP Address is unregistered.");
                            ModelState.AddModelError("", "Please whitelist your IP before login.");
                            Logger.Info("Reason :: Not Whitelisted Ip address tried to attempt Login");
                            ViewBag.Uname = model.Email;
                            rancatpch = GetRandomCaptcha();
                            ViewBag.Captch = rancatpch;
                            //Session["CaptchaCode"] = rancatpch;
                            ViewBag.Message = "100";
                            ViewBag.isIpWhitelistScreen = "1";
                            ViewBag.Otptime = result.OTPTime;
                            HttpContext.Session.SetString("mobilenooriginal", result.MobileNo);
                            LogOnRespons.GetIPAddress = Request.Headers["X-Forwarded-For"].FirstOrDefault()?.Split(',')[0].Trim()
                                    ?? HttpContext.Connection.RemoteIpAddress?.ToString();
                            SessionExtensions.AddItem<Core.Models.Extensions.CustomeUser>(HttpContext.Session, new Core.Models.Extensions.CustomeUser
                            {
                                LogOnRespons = LogOnRespons
                            });
                            if (string.IsNullOrEmpty(result.MobileNo) || result.MobileNo.Length <= 5)
                                ViewBag.MobileNo = result.MobileNo;
                            if (result != null)
                            {
                                if (result.MobileNo != null)
                                {
                                    ViewBag.MobileNo = string.Concat(result.MobileNo.Select((c, i) => (i < 2 || i >= result.MobileNo.Length - 3) ? c : 'X'));
                                }
                            }
                            return View(model);
                        default:
                            ModelState.AddModelError("", "Invalid Credentials.");
                            ViewBag.msg = "Invalid Credentials.";
                            Logger.Info("Reason :: Not valid credentials");
                            Logger.Info("Login Action :: End");
                            var randomCaptcha2 = GetRandomCaptcha();
                            ViewBag.Captch = randomCaptcha2;
                            ViewBag.LoginCaptcha = randomCaptcha2;
                            return View(model);
                    }
                }
                Logger.Info("Login Action :: End");
                return View(model);
            }
        }
        private ActionResult IpWhitelistLogic(LoginViewModel model)
        {
            HttpContext.Session.SetString("FogotPassword", "false");
            var mobileNo = HttpContext.Session.GetString("mobilenooriginal") != null ? HttpContext.Session.GetString("mobilenooriginal") : "";
            Logger.Info("IP Whitelist Started, User Registered Mobile No : " + mobileNo);
            //Default values            
            ViewBag.Uname = model.ipwhitelistUserName;
            ViewBag.Captch = (model.WhiteListIPCaptchaValue != null && model.WhiteListIPCaptchaValue != string.Empty) ? AESEncrytDecry.DecryptStringAES(model.WhiteListIPCaptchaValue) : GetRandomCaptcha();
            ViewBag.Message = "100";
            ViewBag.isIpWhitelistScreen = "1";
            ViewBag.Qresult = "OTPSCREEN";
            //Default values
            var Attempt = _configuration["AttemptMessage"];
            var Unsuccess = _configuration["Unsuccessful"];
            var Freeze = _configuration["Freeze"];
            string Attval = string.Empty;
            string Freeval = string.Empty;
            if (model.ipwhitelistRandomcapt != null && model.WhiteListIPCaptchaValue != "0")
            {
                if (model.ipwhitelistRandomcapt != AESEncrytDecry.DecryptStringAES(model.WhiteListIPCaptchaValue))
                {
                    Logger.Info("Invalid Captcha, user entered Captcha : " + model.ipwhitelistRandomcapt + ", original captcha : " + AESEncrytDecry.DecryptStringAES(model.WhiteListIPCaptchaValue));
                    ViewBag.Message = "100";
                    ViewBag.isIpWhitelistScreen = "1";
                    ViewBag.Qresult = "OTPSCREEN";
                    ViewBag.Uname = model.ipwhitelistUserName;
                    ViewBag.msg = "Invalid Captcha !!";
                    return View(model);
                }
            }
            if (model.ipwhitelistMobileOTP != string.Empty && model.ipwhitelistMobileOTP != null)
            {
                Logger.Info("API Services Execution Started - IP Whitelist Started");
                var networkIDetails = NetworkInterface.GetAllNetworkInterfaces();
                using (var clientAcces = new AuthenticateSecurityClient())
                {
                    var result =
                    clientAcces.IpWhitelistByUser(new IpWhiteListRequest
                    {
                        Username = HttpContext.Session.GetString("Username") != null ? HttpContext.Session.GetString("Username") : string.Empty,
                        MacAddress =
                            AppInternalEncKey.Encrypt(networkIDetails[0].GetPhysicalAddress().ToString(), false),
                        IpAddress =
                            AppInternalEncKey.Encrypt(
                                Request.Headers["X-Forwarded-For"].FirstOrDefault()?.Split(',')[0].Trim() ?? HttpContext.Connection.RemoteIpAddress?.ToString(), false),
                        UserSessionId =
                            AppInternalEncKey.Encrypt(HttpContext.Session.Id, false),
                        mobileNo = mobileNo,
                        otpvalue = model.ipwhitelistMobileOTP
                    });

                    if (result == null)
                    {
                        ViewBag.ActionResult = "Request Invalid";
                        Logger.Info("API request for IP Whitelist was failed, result value : " + result);
                        return View(model);
                    }

                    if (result.returnValue == 2 || result.returnValue == 3 || result.returnValue == 4)
                    {
                        ViewBag.Uname = HttpContext.Session.GetString("Username") != null ? HttpContext.Session.GetString("Username") : string.Empty;
                        ViewBag.Message = "100";
                        ViewBag.isIpWhitelistScreen = "1";
                        //ViewBag.invalidCaptcha = "Invalid OTP !!";
                        ViewBag.Qresult = "OTPSCREEN";
                        ViewBag.Uname = model.ipwhitelistUserName;
                        ViewBag.msg = result.ActionResult;
                        Logger.Info("API request executed for OTP Validation : " + result.returnValue);
                        HttpContext.Session.SetString("mobilenooriginal", mobileNo);
                        Logger.Info("Captha Generated, OTP Sent to registered Mobile no, Captcha value : " + ViewBag.Captch);
                        if (string.IsNullOrEmpty(mobileNo) || mobileNo.Length <= 5)
                            ViewBag.MobileNo = mobileNo;

                        ViewBag.MobileNo = string.Concat(mobileNo.Select((c, i) => (i < 2 || i >= mobileNo.Length - 3) ? c : 'X'));
                    }
                    if (result.returnValue == 5)    //IP Validated and inserted in DB - SUCCESS
                    {
                        ViewBag.Message = "0";
                        ViewBag.isIpWhitelistScreen = "0";
                        ViewBag.Qresult = "";
                        ViewBag.Uname = model.ipwhitelistUserName;
                        ViewBag.msg = result.ActionResult;
                        ViewBag.ActionResult = result.ActionResult;

                        var randomCaptcha = GetRandomCaptcha();
                        ViewBag.Captch = randomCaptcha;
                        ViewBag.LoginCaptcha = randomCaptcha;
                    }
                    if (result.returnValue == 6)
                    {
                        ViewBag.Uname = HttpContext.Session.GetString("Username") != null ? HttpContext.Session.GetString("Username") : string.Empty;
                        ViewBag.Message = "100";
                        ViewBag.isIpWhitelistScreen = "1";
                        ViewBag.Qresult = "OTPSCREEN";
                        ViewBag.Uname = model.ipwhitelistUserName;
                        ViewBag.msg = result.ActionResult;
                        Logger.Info("API request executed for OTP Validation : " + result.returnValue);
                        HttpContext.Session.SetString("mobilenooriginal", mobileNo);
                        int nooftimesInvalid = HttpContext.Session.GetInt32("nooftimesinvalid") != null ? Convert.ToInt32(HttpContext.Session.GetInt32("nooftimesinvalid")) : 0;
                        nooftimesInvalid = nooftimesInvalid + 1;
                        HttpContext.Session.SetString("nooftimesinvalid", (nooftimesInvalid).ToString());
                        ViewBag.msg = "Invalid OTP attempt : " + nooftimesInvalid.ToString() + " time(s), Remaining attempts : " + (result.NoOfAttempts - nooftimesInvalid) + ", you will be redirected to home page after " + result.NoOfAttempts + " invalid attempts.";
                        if (result.NoOfAttempts <= nooftimesInvalid)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                Logger.Info("API Services Execution Done - IP Whitelist Started");

                return View(model);
            }
            if (!string.IsNullOrWhiteSpace(model.ActionVerificationId) && !string.IsNullOrWhiteSpace(model.VerifiedToken))
            {
                model.Email = AESEncrytDecry.DecryptStringAES(model.ActionVerificationId);
                model.Password = AESEncrytDecry.DecryptStringAES(model.VerifiedToken);
            }
            if (model.Otp != null)
            {
                model.usercheck = "1";
            }
            else
            {
                model.usercheck = "0";
            }
            //commented on 
            using (var clientAcces = new AuthenticateSecurityClient())
            {
                Logger.Info("OTP Sending to moble - started");

                var networkIDetails = NetworkInterface.GetAllNetworkInterfaces();
                LogOnRespons result = new LogOnRespons();
                if (HttpContext.Session.GetString("result") != null)
                {
                    result = HttpContext.Session.GetItem<LogOnRespons>();
                    //XML File generation and Send it to Queue...
                    string OTPSCREENResult = ipWhitelistOTPSCREEN(result.UserId, result.CustomerId);
                    if (OTPSCREENResult.Contains("Success"))
                    {
                        ViewBag.Qresult = "OTPSCREEN";
                        ViewBag.msg = "OTP will remain valid for 3 minutes from the time of generation. OTP resend option is allowed only one time.";
                        Logger.Info("OTP Sent to mobile no");
                        if (result.ActionResult == 0 && result.ReturnValue == 11)
                        {
                            result.ActionResult = ActionStatus.OTP;
                            Attval = result.RemainAttempts.ToString();
                        }
                        if (result.ActionResult == 0 && result.ReturnValue == 15)
                        {
                            result.ActionResult = ActionStatus.Freeze;
                            Freeval = result.FreezeTimeMinutes.ToString();
                        }
                        if (Attval == "0")
                        {
                            result.ActionResult = ActionStatus.Unsuccess;
                        }
                        var LogOnRespons = result;
                        HttpContext.Session.SetString("ln", "en");
                        string strcid = result.CustomerId.ToString();
                        string strctype = result.CustomerId.ToString();
                        string strrid = result.CustomerId.ToString();
                        ViewBag.Otptime = result.OTPTime;
                        HttpContext.Session.SetString("category", string.Empty);

                        GetCredits(AppInternalEncKey.Encrypt(strcid, false), AppInternalEncKey.Encrypt(strctype, false), AppInternalEncKey.Encrypt(strrid, false), "yes");
                        switch (result.ActionResult)
                        {
                            case ActionStatus.InvalidIpAddress:
                                {
                                    ViewBag.Uname = HttpContext.Session.GetString("Username") != null ? HttpContext.Session.GetString("Username") : string.Empty;
                                    if (string.IsNullOrEmpty(result.MobileNo) || result.MobileNo.Length <= 5)
                                        ViewBag.MobileNo = result.MobileNo;

                                    ViewBag.MobileNo = string.Concat(result.MobileNo.Select((c, i) => (i < 2 || i >= result.MobileNo.Length - 3) ? c : 'X'));
                                    return View(model);
                                }
                            default:
                                ModelState.AddModelError("", "Invalid Credentials.");
                                Logger.Info("Reason :: Not valid credentials");
                                Logger.Info("Login Action :: End");
                                return View(model);
                        }
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError("OTPSCREENResult", OTPSCREENResult);
                        ViewBag.msg = OTPSCREENResult;
                        ViewBag.Message = "0";
                        ViewBag.Qresult = "OTPFAILED";
                        ViewBag.isIpWhitelistScreen = "0";
                        var randomCaptcha = GetRandomCaptcha();
                        ViewBag.Captch = randomCaptcha;
                        ViewBag.LoginCaptcha = randomCaptcha;
                        return View(model);
                    }
                }
            }
            return View(model);
        }
        private ActionResult ForgotPasswordLogic(LoginViewModel model)
        {
            HttpContext.Session.SetString("FogotPassword", "true");
            HttpContext.Session.SetString("Username", model.Email);

            ViewBag.Captch = (model.ForgotPasswordCaptchaValue != null && model.ForgotPasswordCaptchaValue != string.Empty) ? AESEncrytDecry.DecryptStringAES(model.ForgotPasswordCaptchaValue) : GetRandomCaptcha();
            ViewBag.Message = "10";
            if (model.forgotPasswordrandomcapt != null && model.ForgotPasswordCaptchaValue != "0")
            {
                if (model.forgotPasswordrandomcapt != AESEncrytDecry.DecryptStringAES(model.ForgotPasswordCaptchaValue))
                {
                    Logger.Info("Invalid Captcha, user entered Captcha : " + model.forgotPasswordrandomcapt + ", original captcha : " + HttpContext.Session.GetString("CaptchaCode"));
                    model.forgotPasswordEmail = string.Empty;
                    model.forgotPasswordUsername = string.Empty;
                    model.forgotPasswordrandomcapt = string.Empty;
                    model.forgotReturnMessage = "Invalid Captcha !!";
                    ViewBag.Message = "10";
                    return View(model);
                }
            }
            using (var clientAcces = new AuthenticateSecurityClient())
            {
                Logger.Info("API Services Execution Started - forgot password");
                var networkIDetails = NetworkInterface.GetAllNetworkInterfaces();
                var result =
                    clientAcces.AuthenticateUserForgotPassword(new LogOnRequest
                    {
                        UserName = model.forgotPasswordUsername,
                        Password = GetRandomCaptcha(),
                        useremail = model.forgotPasswordEmail,
                        otp = string.Empty,
                        MacAddress = AppInternalEncKey.Encrypt(networkIDetails[0].GetPhysicalAddress().ToString(), false),
                        IpAddress =
                            AppInternalEncKey.Encrypt(
                                Request.Headers["X-Forwarded-For"].FirstOrDefault()?.Split(',')[0].Trim()
                                    ?? HttpContext.Connection.RemoteIpAddress?.ToString(), false),
                        UserSessionId = AppInternalEncKey.Encrypt(HttpContext.Session.Id, false),
                        IsWebRequest = true
                    });
                Logger.Info("API Services Execution Done - forgot password");
                var LogOnRespons = result;
                HttpContext.Session.AddItem(result);
                if (result != null)
                {
                    if (result.ActionResult.ToString().Contains("Success"))
                    {
                        try
                        {
                            if (result.Password == string.Empty)
                            {
                                model.forgotPasswordEmail = string.Empty;
                                model.forgotPasswordUsername = string.Empty;
                                model.forgotPasswordrandomcapt = string.Empty;
                                model.forgotReturnMessage = "Please contact admin/accounts manager !!";
                                ViewBag.Message = "10";
                                return View(model);
                            }
                        }
                        catch (Exception ex1)
                        {
                            var oResult = ex1.Message;
                            Logger.InfoFormat("Account Controller::Forgot Email ::{0} ", oResult);
                            model.forgotPasswordEmail = string.Empty;
                            model.forgotPasswordUsername = string.Empty;
                            model.forgotPasswordrandomcapt = string.Empty;
                            model.forgotReturnMessage = "Invalid username/Email, Please contact admin !!";
                            ViewBag.Message = "10";
                            return View(model);
                        }
                        try
                        {
                            sendEmail(result);
                        }
                        catch (Exception exp)
                        {
                            Logger.InfoFormat("Account Controller::Forgot Email - Mail Sent failed");
                            model.forgotPasswordEmail = string.Empty;
                            model.forgotPasswordUsername = string.Empty;
                            model.forgotPasswordrandomcapt = string.Empty;
                            model.forgotReturnMessage = "User locked, Please contact admin !!";
                            ViewBag.Message = "10";
                            return View(model);
                        }

                        var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, LogOnRespons.UserName),
                                new Claim(ClaimTypes.NameIdentifier, LogOnRespons.Id.ToString())
                            };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        model.forgotPasswordEmail = string.Empty;
                        model.forgotPasswordUsername = string.Empty;
                        model.forgotPasswordrandomcapt = string.Empty;
                        model.forgotReturnMessage = $"The user details have been successfully verified. Your password has been reset, and a reset password has been sent to your registered email address. <a href='{Url.Action("Login", "Account")}'>Login again</a>";
                        ViewBag.Message = "10";
                        ViewBag.isIpWhitelistScreen = "0";
                        return View(model);
                    }
                    else
                    {
                        model.forgotPasswordEmail = string.Empty;
                        model.forgotPasswordUsername = string.Empty;
                        model.forgotPasswordrandomcapt = string.Empty;
                        model.forgotReturnMessage = "Invalid Username or Email !!";
                        ViewBag.Message = "10";
                        return View(model);
                    }
                }
            }
            return View();
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOff()
        {
            if (HttpContext.Session.GetString("FogotPassword") != null)
            {
                if (Convert.ToBoolean(HttpContext.Session.GetString("FogotPassword")) == true)
                {
                    ViewBag.Message = "10";
                }
            }
            using (var clientAcces = new AuthenticateSecurityClient())
            {
                var networkIDetails = NetworkInterface.GetAllNetworkInterfaces();
                if (ExtendedUser != null && ExtendedUser.LogOnRespons != null)
                {
                    var result = clientAcces.AuthenticateUserLogout(new LogOnRequest
                    {
                        UserId = ExtendedUser.LogOnRespons.Id,
                        IpAddress = AppInternalEncKey.Encrypt(Request.Headers["X-Forwarded-For"].FirstOrDefault()?.Split(',')[0].Trim()
                                    ?? HttpContext.Connection.RemoteIpAddress?.ToString(), false),
                        MacAddress = AppInternalEncKey.Encrypt(networkIDetails[0].GetPhysicalAddress().ToString(), false)
                    });
                }
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Home", new { Area = string.Empty });
            }
        }

        [HttpPost]
        public ActionResult GetCredits(string cid, string cttyp, string rid, string islogin = "")
        {
            if (islogin == "yes")
            {
                cid = AppInternalEncKey.Decrypt(cid, false);
                cttyp = AppInternalEncKey.Decrypt(cttyp, false);
                rid = AppInternalEncKey.Decrypt(rid, false);
            }
            else
            {
                cid = AESEncrytDecry.DecryptStringAES(cid);
                cttyp = AESEncrytDecry.DecryptStringAES(cttyp);
                rid = AESEncrytDecry.DecryptStringAES(rid);
            }

            if (Convert.ToInt32(rid) == 1)
            {
                return Json("ADMIN");
            }
            if (Convert.ToInt32(cttyp) == 2 && Convert.ToInt32(rid) != 1)
            {
                return Json("UNLIMITED");
            }
            using (var clientAcces = new AuthenticateSecurityClient())
            {
                var availCredits = clientAcces.GetAvailableCreditsAsync(Convert.ToInt32(cid)).Result;
                if (availCredits != null)
                {
                    var result = new LogOnRespons();
                    result.AvailableCredits = Convert.ToInt32(availCredits);
                    return Json(result.AvailableCredits);
                }
            }
            return Json("");
        }

        private void sendEmail(LogOnRespons result)
        {
            string Message = string.Empty;
            //string textBody = Message + ran; ;
            string textBody = Message;
            string fromemail = _configuration["FromMailQ"];
            string password = _configuration["PassMailQ"];
            string host = _configuration["smtpHost"];
            int port = int.Parse(_configuration["smtpPort"]);
            string toemail = result.Mail;
            string subject = "Forgot Password - Recovery Email";
            textBody = "Please use your temporary password to log in : " + AppInternalEncKey.Decrypt(result.Password, false);
            System.Net.Mail.MailMessage msgobj = new System.Net.Mail.MailMessage();
            SmtpClient serverobj = new SmtpClient();
            serverobj.Credentials = new NetworkCredential(fromemail, AppInternalEncKey.Decrypt(password, false));
            serverobj.Host = host;

            serverobj.Port = port;
            msgobj.From = new MailAddress(fromemail);

            Logger.InfoFormat("ToEmail ::{0} ", toemail);
            msgobj.To.Add(toemail.Trim());
            Logger.InfoFormat("toemail ::{0} ", toemail);

            msgobj.Subject = subject;
            Logger.InfoFormat("Subject ::{0} ", subject);

            msgobj.Body = textBody.Trim();
            Logger.InfoFormat("Body ::{0} ", textBody);

            msgobj.IsBodyHtml = true;

            msgobj.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            Logger.InfoFormat("DeliveryNotificationOptions ::{0} ", DeliveryNotificationOptions.OnFailure);

            serverobj.EnableSsl = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            Logger.InfoFormat("EnableSsl ::{0} ", true);

            serverobj.Send(msgobj);
            Logger.InfoFormat("Send");

            ViewBag.otpmessage = Message;
            Logger.Info("Email :: End");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                base.Dispose(disposing);
            }
        }
    }
}

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
    public class DashBoardController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        internal CustomeUser ExtendedUser = null;
        internal List<UserActions> UserActions = null;
        ILog Logger = LogManager.GetLogger(typeof(HomeController));
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public DashBoardController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor,
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
            if (ExtendedUser == null)
            {
                return RedirectToAction("LogOff", "Account", new { Area = string.Empty });
            }
            if (User.Identity.IsAuthenticated)
            {
                using (var client = new AuthenticateSecurityClient())
                {
                    var globalprops = SessionExtensions.GetItem<GlobalUsageProperties>(_httpContextAccessor.HttpContext.Session);
                    globalprops.UserType = client.GetDashBoardTypeAsync(ExtendedUser.LogOnRespons.Id).Result;
                    SessionExtensions.UpdateItem<GlobalUsageProperties>(_httpContextAccessor.HttpContext.Session, globalprops);
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult myPasswordReset()
        {
            if (_httpContextAccessor.HttpContext.Session.GetItem<LogOnRespons>() != null)
            {
                var result = _httpContextAccessor.HttpContext.Session.GetItem<LogOnRespons>();
                if (result != null)
                {

                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult RadioData(string Medata)
        {
            string ReturnVal = string.Empty;
            try
            {
                Medata = AESEncrytDecry.DecryptStringAES(Medata);
            }
            catch (Exception ex)
            {
                return Json(new { Invalid = true, Message = "InvalidInputParameters" });
            }
            using (var ClientAccess = new AuthenticateSecurityClient())
            {
                var response = ClientAccess.GetRadioData(new InsertQSMSOnRequest
                {
                    UserId = ExtendedUser.LogOnRespons.Id,
                    Medata = Medata
                });

                var result1 = response.Result.Split('$')[0];    //mobile
                var result2 = response.Result.Split('$')[1];    //mail
                ViewBag.Mobile = result1;
                ViewBag.Email = result2;

                if (result1 != "" && result2 != "")
                {
                    ReturnVal = result1 + '&' + result2 + '$' + 1;
                }
                else if (result1 != "" && result2 == "")
                {
                    ReturnVal = result1 + '$' + 2;
                }
                else
                {
                    ReturnVal = result2;
                }
            }
            return Json(ReturnVal);
        }
        public ActionResult LogOff()
        {
            return RedirectToAction("LogOff", "Account", new { Area = string.Empty });
        }
        public ActionResult VerifyOtp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult VerifyOtp(LoginViewModel model)
        {
            if (_httpContextAccessor.HttpContext.Session.GetItem<LogOnRespons>() != null)
            {
                var response = _httpContextAccessor.HttpContext.Session.GetItem<LogOnRespons>();
                if (response != null)
                {
                    var networkIDetails = NetworkInterface.GetAllNetworkInterfaces();
                    using (var clientAcces = new AuthenticateSecurityClient())
                    {
                        var result =
                            clientAcces.VerifyOTP(new LogOnRequest
                            {
                                UserName = response.UserName,
                                Password = string.Empty,
                                otp = model.Otp,
                                MacAddress =
                                    AppInternalEncKey.Encrypt(networkIDetails[0].GetPhysicalAddress().ToString(), false),
                                IpAddress =
                                AppInternalEncKey.Encrypt(Request.Headers["X-Forwarded-For"].FirstOrDefault()?.Split(',')[0].Trim()
                                    ?? HttpContext.Connection.RemoteIpAddress?.ToString(), false),
                                UserSessionId =
                                    AppInternalEncKey.Encrypt(_httpContextAccessor.HttpContext.Session.Id, false),
                                IsWebRequest = true
                            });
                        if (result.ReturnValue == 0)
                        {
                            return RedirectToAction("changePassword", "Home");
                        }
                    }
                }
            }
            return View();
        }

        public ActionResult changePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult OTPSMS(string QSMSID, string SenderID, string LangID, string Message, string DLR, string SenderName, string Module, string MobileNos, string CharCount, string Credits, string MbcID, string ModuleID, string RADIO, string EMAIL, string Sendtime)
        {

            var OTPValue = GetRandomOTP();
            Logger.InfoFormat("OTPSMS :: start :: {0}", OTPValue);
            var extendedUser = SessionExtensions.GetItem<Core.Models.Extensions.CustomeUser>(_httpContextAccessor.HttpContext.Session);
            string ReturnVal = string.Empty;
            try
            {
                if (_httpContextAccessor.HttpContext.Session.GetItem<LogOnRespons>() != null)
                {
                    var response = _httpContextAccessor.HttpContext.Session.GetItem<LogOnRespons>();
                    if (response != null)
                    {
                        EMAIL = response.Mail;
                        MobileNos = response.MobileNo;
                    }
                }
                QSMSID = AESEncrytDecry.DecryptStringAES(QSMSID);
                SenderID = AESEncrytDecry.DecryptStringAES(SenderID);
                LangID = AESEncrytDecry.DecryptStringAES(LangID);
                Message = AESEncrytDecry.DecryptStringAES(Message);
                Message = _configuration["OTPMessage"];
                Message = Message.Replace("{#username#}", ExtendedUser.LogOnRespons.UserName);
                Message = Message.Replace("{#OTP#}", OTPValue);
                DLR = AESEncrytDecry.DecryptStringAES(DLR);
                SenderName = AESEncrytDecry.DecryptStringAES(SenderName);

                CharCount = AESEncrytDecry.DecryptStringAES(CharCount);
                Credits = AESEncrytDecry.DecryptStringAES(Credits);
                MbcID = AESEncrytDecry.DecryptStringAES(MbcID);
                Module = AESEncrytDecry.DecryptStringAES(Module);
                ModuleID = AESEncrytDecry.DecryptStringAES(ModuleID);
                RADIO = AESEncrytDecry.DecryptStringAES(RADIO);

                Sendtime = AESEncrytDecry.DecryptStringAES(Sendtime);
            }
            catch (Exception ex)
            {
                return Json(new { Invalid = true, Message = "InvalidInputParameters" });
            }

            if (RADIO == "3")
            {
                string oResult = "";
                try
                {
                    Logger.InfoFormat("EmailID ::{0}, {1} ", RADIO, EMAIL);
                    //string textBody = Message + ran; ;
                    string textBody = Message;
                    string fromemail = _configuration["FromMailQ"];
                    string password = _configuration["PassMailQ"];
                    string host = _configuration["smtpHost"];
                    int port = int.Parse(_configuration["smtpPort"]);
                    string toemail = EMAIL;
                    string subject = "OTP for Synapse";

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
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    Logger.InfoFormat("EnableSsl ::{0} ", true);

                    serverobj.Send(msgobj);
                    Logger.InfoFormat("Send");

                    ViewBag.otpmessage = Message;
                    Logger.Info("Email :: End");

                }
                catch (Exception ex1)
                {
                    oResult = ex1.Message;
                    Logger.InfoFormat("HomeController::OTPSMS-Radio-EmailID ::{0} ", oResult);
                }

                using (var ClientAccess = new AuthenticateSecurityClient())
                {
                    DataSet objDs = null; string strDup = string.Empty; string strInvalid = string.Empty; string validMobileNos = string.Empty;
                    List<string> Stage = new List<string>();
                    int SrvcStage = 0;
                    var fileContent = new StringBuilder();
                    int ValidMobNosCnt = 0;
                    var LogPath = _configuration["tempPath"] + "\\QuickSMS" + "QuickSMS.txt";
                    fileContent.AppendLine("---------------------- lOG :: SenderID : " + SenderName + " :: Time : " + DateTime.Now + " ----------------------");
                    try
                    {
                        var response = ClientAccess.OTPQSMS(new InsertQSMSOnRequest
                        {
                            QSMSID = Convert.ToInt32(QSMSID),
                            SenderID = 1,
                            LangID = Convert.ToInt32(LangID),
                            //Message = Message + ran,
                            Message = Message,
                            CharCount = Convert.ToInt32(CharCount),
                            CreditsUsed = Convert.ToInt32(Credits),
                            CreditsCount = ValidMobNosCnt * Convert.ToInt32(Credits),
                            DLR = 1,
                            CreatedBy = extendedUser.LogOnRespons.Id,
                            CustomerID = extendedUser.LogOnRespons.CustomerId,
                            Sender = "0",
                            Module = "EmailOTP",
                            MobileNos = "999999999999",
                            Stage = string.Join(",", Stage.Distinct()),
                            Status = -1,
                            CurrentStatus = 1,
                            UserName = ExtendedUser.LogOnRespons.UserName,
                            UserIp = extendedUser.LogOnRespons.GetIPAddress,
                            Sendtime = Sendtime,
                            OTPValue = OTPValue
                        });

                        var result = response.Result.Split('$')[0];

                        var nID = response.Result.Split('$')[1];
                        if (result == "7")
                        {
                            ReturnVal = "MsgSubmitSuccess";
                        }
                        else if (result == "4")
                        {
                            ReturnVal = "InsufficentCredits";
                        }

                        using (StreamWriter w = System.IO.File.AppendText(LogPath))
                        {
                            w.WriteLine(fileContent.ToString());
                        }

                        return Json(ReturnVal);
                    }
                    catch (Exception ex)
                    {
                        using (StreamWriter w = System.IO.File.AppendText(LogPath))
                        {
                            fileContent.AppendLine("Exception :: " + ex.Message);
                            w.WriteLine(fileContent.ToString());
                        }
                        Logger.InfoFormat("HomeController::OTPSMS- Radio-EmailID- Inserting OTP into DB ::{0} ", ex.Message);
                    }
                    Logger.Info("EmailID :: End");
                }
            }
            else if (RADIO == "1")
            {
                Logger.InfoFormat("Both Mobileno and EmailID ::{0} ", RADIO);
                using (var ClientAccess = new AuthenticateSecurityClient())
                {
                    DataSet objDs = null; string strDup = string.Empty; string strInvalid = string.Empty; string validMobileNos = string.Empty;
                    List<string> Stage = new List<string>();
                    int SrvcStage = 0;
                    var fileContent = new StringBuilder();
                    int ValidMobNosCnt = 0;
                    var LogPath = _configuration["tempPath"] + "\\QuickSMS" + "QuickSMS.txt";
                    fileContent.AppendLine("---------------------- lOG :: SenderID : " + SenderName + " :: Time : " + DateTime.Now + " ----------------------");
                    try
                    {
                        var sender_countrycodes = ValidateMobileNumbers(Convert.ToInt32(SenderID));

                        if (sender_countrycodes.Any())
                        {
                            foreach (var mob in MobileNos.Split(',').Select(s => s.Trim()))
                            {
                                if (!string.IsNullOrWhiteSpace(mob))
                                {
                                    var totLengthValid = sender_countrycodes.Where(w => w.TotalLength.Equals(mob.Length));
                                    if (!totLengthValid.Any())
                                    {
                                        ReturnVal = "InValidseries";
                                        return Json(ReturnVal);
                                    }
                                    var validcountrycode = from n in sender_countrycodes
                                                           let countrycode = n.CountryCode
                                                           let countrycodelength = n.CountryCode.ToString().Length
                                                           where mob.Substring(0, countrycodelength) == n.CountryCode.ToString()
                                                           select mob;
                                    if (!validcountrycode.Any())
                                    {
                                        ReturnVal = "InValidseries";
                                        return Json(ReturnVal);
                                    }
                                    if (totLengthValid.Any() && validcountrycode.Any())
                                    {
                                        validMobileNos = MobileNos;
                                    }
                                }
                            }
                        }

                        if (validMobileNos != null && validMobileNos != "")
                        {
                            var response = ClientAccess.OTPQSMS(new InsertQSMSOnRequest
                            {
                                QSMSID = Convert.ToInt32(QSMSID),
                                SenderID = Convert.ToInt32(SenderID),
                                LangID = Convert.ToInt32(LangID),
                                //Message = Message + ran,
                                Message = Message,
                                CharCount = Convert.ToInt32(CharCount),
                                CreditsUsed = Convert.ToInt32(Credits),
                                CreditsCount = ValidMobNosCnt * Convert.ToInt32(Credits),
                                DLR = 1,
                                CreatedBy = extendedUser.LogOnRespons.Id,
                                CustomerID = extendedUser.LogOnRespons.CustomerId,
                                Sender = SenderName,
                                Module = Module,
                                MobileNos = MobileNos != null ? validMobileNos.Trim(',') : MobileNos,
                                Stage = string.Join(",", Stage.Distinct()),
                                Status = 0,
                                CurrentStatus = 1,
                                UserName = ExtendedUser.LogOnRespons.UserName,
                                UserIp = extendedUser.LogOnRespons.GetIPAddress,
                                Sendtime = Sendtime,
                                OTPValue = OTPValue
                            });

                            var result = response.Result.Split('$')[0];
                            var nId = response.Result.Split('$')[1];
                            if (nId != "0")
                            {
                                var tCounts = MobileNos.Split(',').Length;
                                if (tCounts == 0)
                                {
                                    tCounts = 1;
                                }

                                if (LangID == "2")
                                    LangID = "8";
                                else
                                {
                                    if (Message.Contains('@') || Message.Contains('{') || Message.Contains('}'))
                                        LangID = "1";
                                    else
                                        LangID = "1";
                                }

                                var fileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + nId;

                                string uname = extendedUser.LogOnRespons.Id != 1 ? "admin" : extendedUser.LogOnRespons.UserName;

                                var xmlContent = "<root iscustome='false' priority='5'><sendsms userid='" + 1 + "'  username='" + uname + "' campainid='" + 0 + "' sender='" + WebUtility.HtmlEncode(SenderName) +
                                    "' language='" + ((LangID)) + "' message='" + WebUtility.HtmlEncode(Message).Replace("\n", "&#10;") +
                                        "' mobile=''><mobile>" + MobileNos + "</mobile></sendsms></root>";

                                var fPath = Path.Combine(
                                        (_configuration["tempPath"] + "QuickSMS"),
                                        fileName);
                                if (!Directory.Exists(fPath))
                                {
                                    Directory.CreateDirectory(fPath);
                                }

                                System.IO.File.WriteAllText(Path.Combine(fPath, fileName + ".xml"), xmlContent);

                                var QMsg = "action=start&camp_id=" + nId + "&camp_type=0&dir_name=" + fileName + "&count=" + tCounts;
                                var Qresult = new CampaignQLog().PushMessageToQ(QMsg);
                                Logger.Info("OTP SMS :: notification QMsg:: " + QMsg);
                                Logger.Info("OTP SMS notification sent to Queue");
                            }

                            var nID = response.Result.Split('$')[1];
                            if (result == "7")
                            {
                                ReturnVal = "MsgSubmitSuccess";
                            }
                            else if (result == "4")
                            {
                                ReturnVal = "InsufficentCredits";
                            }
                            else if (result == "8")
                            {
                                ReturnVal = "SenderInactive";
                            }
                        }
                        else
                        {
                            ReturnVal = "InValidseries";
                        }
                        using (StreamWriter w = System.IO.File.AppendText(LogPath))
                        {
                            w.WriteLine(fileContent.ToString());
                        }

                        if (ReturnVal == "MsgSubmitSuccess")
                        {
                            string oResult = "";
                            try
                            {
                                Logger.InfoFormat("Email ::{0} ", EMAIL);
                                //string textBody = Message + ran; ;
                                string textBody = Message;
                                string fromemail = _configuration["FromMailQ"];
                                string password = _configuration["PassMailQ"];
                                string host = _configuration["smtpHost"];
                                int port = int.Parse(_configuration["smtpPort"]);
                                string toemail = EMAIL;
                                string subject = "OTP for Synapse";

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
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                                Logger.InfoFormat("EnableSsl ::{0} ", true);

                                serverobj.Send(msgobj);
                                Logger.InfoFormat("Send");

                                ViewBag.otpmessage = Message;
                                Logger.Info("EmailID :: End");

                            }
                            catch (Exception ex1)
                            {
                                oResult = ex1.Message;
                                Logger.InfoFormat("Error - OTP to both Mobileno and EmailID -{0}", ex1.Message);

                            }
                        }
                        return Json(ReturnVal);
                    }
                    catch (Exception ex)
                    {
                        using (StreamWriter w = System.IO.File.AppendText(LogPath))
                        {
                            fileContent.AppendLine("Exception :: " + ex.Message);
                            w.WriteLine(fileContent.ToString());
                        }
                        Logger.ErrorFormat("OtpSMS :-{0} Error :- {1}", QSMSID, SenderID, LangID, Message, DLR, SenderName, Module, MobileNos, CharCount, Credits, MbcID, ModuleID);
                        ErrorSignal.FromCurrentContext().Raise(ex);
                    }
                    Logger.Info("OTP to both Mobileno and EmailId :: End");
                }
            }
            else
            {
                Logger.InfoFormat("Mobileno ::{0} ", RADIO);
                using (var ClientAccess = new AuthenticateSecurityClient())
                {
                    DataSet objDs = null; string strDup = string.Empty; string strInvalid = string.Empty; string validMobileNos = string.Empty;
                    List<string> Stage = new List<string>();
                    int SrvcStage = 0;
                    var fileContent = new StringBuilder();
                    int ValidMobNosCnt = 0;
                    var LogPath = _configuration["tempPath"] + "\\QuickSMS" + "QuickSMS.txt";
                    fileContent.AppendLine("---------------------- lOG :: SenderID : " + SenderName + " :: Time : " + DateTime.Now + " ----------------------");
                    try
                    {
                        var sender_countrycodes = ValidateMobileNumbers(Convert.ToInt32(SenderID));

                        if (sender_countrycodes.Any())
                        {
                            foreach (var mob in MobileNos.Split(',').Select(s => s.Trim()))
                            {
                                if (!string.IsNullOrWhiteSpace(mob))
                                {
                                    var totLengthValid = sender_countrycodes.Where(w => w.TotalLength.Equals(mob.Length));
                                    if (!totLengthValid.Any())
                                    {
                                        ReturnVal = "InValidseries";
                                        return Json(ReturnVal);
                                    }
                                    var validcountrycode = from n in sender_countrycodes
                                                           let countrycode = n.CountryCode
                                                           let countrycodelength = n.CountryCode.ToString().Length
                                                           where mob.Substring(0, countrycodelength) == n.CountryCode.ToString()
                                                           select mob;
                                    if (!validcountrycode.Any())
                                    {
                                        ReturnVal = "InValidseries";
                                        return Json(ReturnVal);
                                    }
                                    if (totLengthValid.Any() && validcountrycode.Any())
                                    {
                                        validMobileNos = MobileNos;
                                    }
                                }
                            }
                        }

                        if (validMobileNos != null && validMobileNos != "")
                        {
                            var response = ClientAccess.OTPQSMS(new InsertQSMSOnRequest
                            {
                                QSMSID = Convert.ToInt32(QSMSID),
                                SenderID = Convert.ToInt32(SenderID),
                                LangID = Convert.ToInt32(LangID),
                                //Message = Message + ran,
                                Message = Message,
                                CharCount = Convert.ToInt32(CharCount),
                                CreditsUsed = Convert.ToInt32(Credits),
                                CreditsCount = ValidMobNosCnt * Convert.ToInt32(Credits),
                                DLR = 1,
                                CreatedBy = extendedUser.LogOnRespons.Id,
                                CustomerID = extendedUser.LogOnRespons.CustomerId,
                                Sender = SenderName,
                                Module = Module,
                                MobileNos = validMobileNos.Trim(','),
                                Stage = string.Join(",", Stage.Distinct()),
                                Status = 0,
                                CurrentStatus = 1,
                                UserName = ExtendedUser.LogOnRespons.UserName,
                                UserIp = extendedUser.LogOnRespons.GetIPAddress,
                                Sendtime = Sendtime,
                                OTPValue = OTPValue
                            });

                            var result = response.Result.Split('$')[0];
                            var nId = response.Result.Split('$')[1];

                            if (nId != "0")
                            {
                                var tCounts = MobileNos.Split(',').Length;
                                if (tCounts == 0)
                                {
                                    tCounts = 1;
                                }

                                if (LangID == "2")
                                    LangID = "8";
                                else
                                {
                                    if (Message.Contains('@') || Message.Contains('{') || Message.Contains('}'))
                                        LangID = "1";
                                    else
                                        LangID = "1";
                                }

                                var fileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + nId;

                                string uname = extendedUser.LogOnRespons.Id != 1 ? "admin" : extendedUser.LogOnRespons.UserName;

                                var xmlContent = "<root iscustome='false' priority='5'><sendsms userid='" + 1 + "'  username='" + uname + "' campainid='" + 0 + "' sender='" + WebUtility.HtmlEncode(SenderName) +
                                    "' language='" + ((LangID)) + "' message='" + WebUtility.HtmlEncode(Message).Replace("\n", "&#10;") +
                                        "' mobile=''><mobile>" + MobileNos + "</mobile></sendsms></root>";

                                var fPath = Path.Combine(
                                    (_configuration["tempPath"] + "QuickSMS"),
                                    fileName);
                                if (!Directory.Exists(fPath))
                                {
                                    Directory.CreateDirectory(fPath);
                                }
                                System.IO.File.WriteAllText(Path.Combine(fPath, fileName + ".xml"), xmlContent);

                                var QMsg = "action=start&camp_id=" + nId + "&camp_type=0&dir_name=" + fileName + "&count=" + tCounts;
                                var Qresult = new CampaignQLog().PushMessageToQ(QMsg);
                                Logger.Info("OTP SMS :: notification QMsg:: " + QMsg);
                                Logger.Info("OTP SMS notification sent to Queue");

                            }

                            var nID = response.Result.Split('$')[1];
                            if (result == "7")
                            {
                                //ViewBag.RandValue = timestamp;
                                ReturnVal = "MsgSubmitSuccess";
                            }
                            else if (result == "4")
                            {
                                ReturnVal = "InsufficentCredits";
                            }
                            else if (result == "0")
                            {
                                ReturnVal = "MobileNo Changed";
                            }
                            else if (result == "10")
                            {
                                ReturnVal = "You have exceeded the maximum OTP attempts. Please try again later.";
                            }
                            else if (result == "8")
                            {
                                ReturnVal = "SenderInactive";
                            }
                        }
                        else
                        {
                            ReturnVal = "InValidseries";
                        }
                        using (StreamWriter w = System.IO.File.AppendText(LogPath))
                        {
                            w.WriteLine(fileContent.ToString());
                        }

                        ViewBag.otpmessage = Message;
                        return Json(ReturnVal);
                    }
                    catch (Exception ex)
                    {
                        using (StreamWriter w = System.IO.File.AppendText(LogPath))
                        {
                            fileContent.AppendLine("Exception :: " + ex.Message);
                            w.WriteLine(fileContent.ToString());
                        }
                        Logger.ErrorFormat("OTP SMS to MobileNo-Radio Error :- {0}", ex.Message);
                        ErrorSignal.FromCurrentContext().Raise(ex);
                    }
                    Logger.Info("Mobileno :: End");
                }
            }
            return Json("");
        }

        private List<MobileLengthValidationResponse> ValidateMobileNumbers(int senderid)
        {
            //Logger.InfoFormat("ValidateMobileNumbers :: start :: {0}", senderid);
            try
            {

                using (var ClientAccess = new AuthenticateSecurityClient())
                {
                    var result = ClientAccess.ValidateMobileNums(senderid);
                    //Logger.Info("Validity Mobile Number Result :: " + result.Result);
                    return result.Result;
                }
            }
            catch (Exception ex)
            {
                // Logger.ErrorFormat("ValidateMobileNumbers ::userId :-{0} Error :- {1}", senderid);
                // ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return null;
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

            _httpContextAccessor.HttpContext.Session.SetString("OTPCode", randomText.ToString());
            return randomText.ToString();
        }

        private string GetRandomValue()
        {
            StringBuilder randomText = new StringBuilder();
            string alphabets = "123456789ACEFGHKLMNSWXZabcdefghijkhlmnopqrstuvwxyz";
            Random r = new Random();

            for (int j = 0; j <= 6; j++)
            {
                randomText.Append(alphabets[r.Next(alphabets.Length)]);
            }

            return randomText.ToString();
        }
        // Dashboard Analytics
        [HttpPost]
        public async Task<ActionResult> DashBoard(int days = 4, int day = 4, string text = "")
        {
            using (var client = new AuthenticateSecurityClient())
            {
                var result = await client.GetDashBoardAnalytics(new DashBoardAnalyticsRequest
                {
                    Days = days,
                    Day = day,
                    Text = text,
                    UserId = ExtendedUser.LogOnRespons.Id
                });
                var respone = new AnalyMain
                {
                    AnalyticsGraph = new List<Analy>(),
                    SRation = new SRation(),
                    TpValues = new List<int>(),
                    Module = new Module(),
                    PullSmscs = new List<string>(),
                    WorldMaps = new WorldMapMock()
                };
                respone.AnalyticsGraph = BuildAnalyticsData(result.AnalyticsGraph, days);
                if (result.SucessRatio != null)
                {
                    var Arrayofitems = new List<int>();
                    Arrayofitems.Add(result.SucessRatio.Delivrd);
                    Arrayofitems.Add(result.SucessRatio.UnDeliv);
                    Arrayofitems.Add(result.SucessRatio.Submitted);
                    double a = (result.SucessRatio.Delivrd) + (result.SucessRatio.UnDeliv);
                    double b = (result.SucessRatio.Delivrd + result.SucessRatio.UnDeliv + result.SucessRatio.Submitted);
                    if (result.SucessRatio.Delivrd > 0 || result.SucessRatio.UnDeliv > 0 || result.SucessRatio.Submitted > 0)
                    {
                        respone.SRation = new SRation
                        {
                            Average = Math.Round((a * 100) / b, 2), //Convert.ToInt32(Average(Arrayofitems.ToArray())),
                            Delivrd = result.SucessRatio.Delivrd,
                            UnDeliv = result.SucessRatio.UnDeliv,
                            Submitted = result.SucessRatio.Submitted
                        };
                    }
                    else
                    {
                        respone.SRation = new SRation
                        {
                            Average = 0
                        };
                    }
                }
                return Json(respone);
            }
            return Json("");
        }

        [HttpPost]
        public async Task<ActionResult> MoAnalytics(string scode, int days = 4, int day = 4, string text = "")
        {

            using (var client = new AuthenticateSecurityClient())
            {
                var result = await client.GetSMSMOAnalytics(new SMSMOAnalyticsRequest
                {
                    Shortcode = scode,
                    Days = days,
                    Day = day,
                    Text = text,
                    UserId = ExtendedUser.LogOnRespons.Id
                });
                var respone = new AnalyMain
                {
                    SMSMoChartGraph = new List<SMSMo>(),
                    SMSMoMinutes = new List<SMSMoMinutes>()
                };
                respone.SMSMoChartGraph = BuildSMSMOAnalyticsData(result.SMSMOAnalyticsResponse, days);
                respone.SMSMoMinutes = BuildSMSMOMinuteData(result.SMSMOMinuteResponse, days);
                return Json(respone);
            }
            return Json("");
        }


        //Dashboard SMSC
        [HttpPost]
        public async Task<ActionResult> LoadSMSC(int days = 4, int day = 4, string text = "")
        {
            using (var client = new AuthenticateSecurityClient())
            {
                var result = await client.GetDashBoardSMSC(new DashBoardAnalyticsRequest
                {
                    Days = days,
                    Day = day,
                    Text = text,
                    UserId = ExtendedUser != null ? ExtendedUser.LogOnRespons.Id : 1
                });
                var respone = new AnalyMain
                {
                    Module = new Module(),
                };
                if (result == null) { return Json(""); }
                if (result.Smsc != null)
                {
                    respone.Smsc = result.Smsc.Select(s => new Smsc
                    {
                        Id = s.Id,
                        SmscName = s.SmscName,
                        Progress = s.Progress,
                        Tps = s.Tps,
                        Status = s.Status,
                        Action = s.Action
                    }).ToList();
                }
                return Json(respone);
            }

        }

        // Dashboard Campaign Activities
        [HttpPost]
        public async Task<ActionResult> LoadCampActivites(int days = 4, int day = 4, string text = "")
        {
            using (var client = new AuthenticateSecurityClient())
            {
                var result = await client.GetDashBoardCampaignActivities(new DashBoardAnalyticsRequest
                {
                    Days = days,
                    Day = day,
                    Text = text,
                    UserId = ExtendedUser != null ? ExtendedUser.LogOnRespons.Id : 1
                });
                var respone = new AnalyMain();
                if (result == null) return Json("");
                if (result.CampaignActivities != null)
                {
                    respone.CampActivity = result.CampaignActivities.Select(s => new CampActivity
                    {
                        CampName = s.CampName,
                        UserName = s.UserName,
                        ScheduleDate = s.ScheduleDate,
                        CampaignType = s.CampaignType,
                        Progress = s.Progress,
                        Percentage = s.Percentage
                    }).ToList();
                }
                return Json(respone);
            }
        }
        //Dashboard Modules
        [HttpPost]
        public async Task<ActionResult> LoadModules(int days = 4, int day = 4, string text = "")
        {
            using (var client = new AuthenticateSecurityClient())
            {
                var result = await client.GetDashBoardModules(new DashBoardAnalyticsRequest
                {
                    Days = days,
                    Day = day,
                    Text = text,
                    UserId = ExtendedUser != null ? ExtendedUser.LogOnRespons.Id : 1
                });
                var respone = new AnalyMain
                {
                    Module = new Module(),
                };
                if (result == null) return Json("");
                if (result.Modules != null)
                {
                    respone.Module = new Module
                    {
                        Camp = result.Modules.Camp,
                        Qsms = result.Modules.Qsms,
                        Alerts = result.Modules.Alerts
                    };
                }
                return Json(respone);
            }
        }

        //Dashboard SuccessRatio
        [HttpPost]
        public async Task<ActionResult> LoadSuccessRatio(int days = 4, int day = 4, string text = "")
        {
            using (var client = new AuthenticateSecurityClient())
            {
                var result = await client.GetDashBoardSucessRatio(new DashBoardAnalyticsRequest
                {
                    Days = days,
                    Day = day,
                    Text = text,
                    UserId = ExtendedUser != null ? ExtendedUser.LogOnRespons.Id : 1
                });
                var respone = new AnalyMain
                {
                    SRation = new SRation(),
                };
                if (result == null) return Json("");
                if (result.SucessRatio != null)
                {
                    var Arrayofitems = new List<int>();
                    Arrayofitems.Add(result.SucessRatio.Delivrd);
                    Arrayofitems.Add(result.SucessRatio.UnDeliv);
                    Arrayofitems.Add(result.SucessRatio.Submitted);
                    double a = (result.SucessRatio.Delivrd);
                    double b = (result.SucessRatio.Delivrd + result.SucessRatio.UnDeliv + result.SucessRatio.Submitted);
                    if (result.SucessRatio.Delivrd > 0 || result.SucessRatio.UnDeliv > 0 || result.SucessRatio.Submitted > 0)
                    {
                        respone.SRation = new SRation
                        {
                            Average = Math.Round((a * 100) / b, 2), //Convert.ToInt32(Average(Arrayofitems.ToArray())),
                            Delivrd = result.SucessRatio.Delivrd,
                            UnDeliv = result.SucessRatio.UnDeliv,
                            Submitted = result.SucessRatio.Submitted
                        };
                    }
                    else
                    {
                        respone.SRation = new SRation
                        {
                            Average = 0
                        };
                    }
                }
                return Json(respone);
            }
        }

        //Dashboard PullSMS
        [HttpPost]
        public async Task<ActionResult> LoadPullSms(int days = 4, int day = 4, string text = "")
        {
            using (var client = new AuthenticateSecurityClient())
            {
                var result = await client.GetDashBoardPullSmses(new DashBoardAnalyticsRequest
                {
                    Days = days,
                    Day = day,
                    Text = text,
                    UserId = ExtendedUser != null ? ExtendedUser.LogOnRespons.Id : 1
                });
                var respone = new AnalyMain
                {
                    PullSmscs = new List<string>(),
                };
                if (result == null) return Json("");
                if (result.PullSmses != null && result.PullSmses.Any())
                {
                    respone.PullSmscs = result.PullSmses.Select(s => Convert.ToString(s.ProcessTime)).ToList();

                }
                return Json(respone);
            }
        }

        //Dashboard ThroughPut
        [HttpPost]
        public async Task<ActionResult> LoadThroughPut(int days = 4, int day = 4, string text = "")
        {
            using (var client = new AuthenticateSecurityClient())
            {
                var result = await client.GetDashBoardThroughPut(new DashBoardAnalyticsRequest
                {
                    Days = days,
                    Day = day,
                    Text = text,
                    UserId = ExtendedUser != null ? ExtendedUser.LogOnRespons.Id : 1
                });
                var respone = new AnalyMain
                {
                    TpValues = new List<int>(),
                };
                if (result == null) return Json("");
                if (result.Tp != null && result.Tp.Any())
                {
                    respone.TpValues = result.Tp.Select(s => Convert.ToInt32(s.Tp)).ToList();
                }
                return Json(respone);
            }
        }

        //Dashboard WorldMap
        [HttpPost]
        public async Task<ActionResult> LoadDashBoardWorld(int days = 4, int day = 4, string text = "")
        {
            using (var client = new AuthenticateSecurityClient())
            {
                var result = await client.GetDashBoardWorldMap(new DashBoardAnalyticsRequest
                {
                    Days = days,
                    Day = day,
                    Text = text,
                    UserId = ExtendedUser != null ? ExtendedUser.LogOnRespons.Id : 1
                });
                var respone = new AnalyMain
                {
                    WorldMaps = new WorldMapMock()
                };
                if (result == null) return Json("");
                if (result.WorldMaps != null)
                {
                    var WorldMaps = result.WorldMaps.Select(s => new WorldMap
                    {
                        key = s.key,
                        doc_count = s.doc_count,
                        delivery_rate = s.delivery_rate,
                    }).ToList();

                    respone.WorldMaps = Newtonsoft.Json.JsonConvert.DeserializeObject<WorldMapMock>(System.IO.File.ReadAllText(_configuration["WMData"]));
                    if (respone.WorldMaps != null)
                    {
                        respone.WorldMaps.aggregations.world_map.buckets = respone.WorldMaps.aggregations.world_map.buckets.Select(s => new buckets { key = s.key, delivery_rate = "0", doc_count = 0 }).ToArray();
                        var xmlDoc = new XmlDocument();
                        xmlDoc.Load(_configuration["wmmap"]);
                        var xDoc = XDocument.Parse(xmlDoc.OuterXml);
                        if (xDoc != null && xDoc.Root != null && xDoc.Root.Elements().Any())
                        {
                            foreach (var item in WorldMaps)//mockData.aggregations.world_map.buckets)
                            {
                                var element = xDoc.Root.Elements().FirstOrDefault(w => w.Attribute(XName.Get("fullname")).Value.Equals(item.key, StringComparison.OrdinalIgnoreCase));
                                if (element != null)
                                {
                                    var buck = respone.WorldMaps.aggregations.world_map.buckets.FirstOrDefault(f => f.key.Equals(element.Attribute(XName.Get("aliasname")).Value, StringComparison.OrdinalIgnoreCase));
                                    if (buck != null)
                                    {
                                        // buck.delivery_rate = Math.Round(Convert.ToDouble(item.delivery_rate + " %"),2);
                                        buck.delivery_rate = item.delivery_rate + " %";
                                        buck.doc_count = item.doc_count;
                                    }
                                }
                            }
                        }
                    }
                }
                return Json(respone);
            }
        }
        private int Sum(params int[] customerssalary)
        {
            int result = 0;

            for (int i = 0; i < customerssalary.Length; i++)
            {
                result += customerssalary[i];
            }

            return result;
        }
        private List<Analy> BuildAnalyticsData(List<AnalyticsGraph> model, int dh)
        {
            var result = model.Select(m => new Analy
            {
                y = dh == 1 ? m.Hour : m.Day,
                a = string.IsNullOrWhiteSpace(m.Count) ? 0 : Convert.ToInt32(m.Count)
            }).ToList();
            return result ?? new List<Analy> { };
        }
        private List<SMSMo> BuildSMSMOAnalyticsData(List<SMSMOAnalyticsResponse> model, int dh)
        {
            var result = model.Select(m => new SMSMo
            {
                y = m.Day,
                a = string.IsNullOrWhiteSpace(m.Count) ? 0 : Convert.ToInt32(m.Count)
            }).ToList();
            return result ?? new List<SMSMo> { };
        }
        private List<SMSMoMinutes> BuildSMSMOMinuteData(List<SMSMOMinuteResponse> model, int dh)
        {
            var result = model.Select(m => new SMSMoMinutes
            {
                LastMinute = m.LastMinute,
                mCount = m.MOCount
            }).ToList();
            return result ?? new List<SMSMoMinutes> { };
        }

        public ActionResult BindOrUnbind(int Id, int bound)
        {
            using (var client = new AuthenticateSecurityClient())
            {
                var response = client.UpdateSmsAnalyticsAsync(new ReUsableRequest
                {
                    SmsId = Id,
                    Bound = bound
                });
                return Json(response);
            }
        }

        [HttpPost]
        public ActionResult GetShortcodeByUsers(string id)
        {
            return Json(GetShortcodesByUsers(id));
        }

        private List<ShortcodeMO> GetShortcodesByUsers(string id)
        {
            using (var client = new AuthenticateSecurityClient())
            {
                var response = client.GetShortcodeByUserid(new ReUsableRequest { UserIds = id });
                return response.Result;
            }
        }

        public ActionResult IpWhiteList()
        {
            return View();
        }
    }
    public class AnalyMain
    {
        public List<Analy> AnalyticsGraph { get; set; }
        public List<SMSMo> SMSMoChartGraph { get; set; }
        public List<SMSMoMinutes> SMSMoMinutes { get; set; }

        public List<int> TpValues { get; set; }

        public decimal TpAverage { get; set; }

        public SRation SRation { get; set; }

        public Module Module { get; set; }

        public List<string> PullSmscs { get; set; }

        public string? ProcessTime { get; set; }

        public List<CampActivity> CampActivity { get; set; }

        public List<Smsc> Smsc { get; set; }
        public WorldMapMock WorldMaps { get; set; }
    }

    public class Analy
    {
        public string? y { get; set; }

        public int a { get; set; }
    }
    public class SMSMo
    {
        public string? y { get; set; }
        public int a { get; set; }
    }
    public class SMSMoMinutes
    {
        public int LastMinute { get; set; }
        public int mCount { get; set; }
    }

    public class SRation
    {
        public int Delivrd { get; set; }

        public int UnDeliv { get; set; }

        public int Submitted { get; set; }

        public double Average { get; set; }
    }

    public class Tp
    {
        public int Max { get; set; }

        public int TpValue { get; set; }
    }

    public class Module
    {
        public int Camp { get; set; }

        public int Qsms { get; set; }

        public int Alerts { get; set; }
    }

    public class CampActivity
    {
        public string? CampName { get; set; }

        public string? UserName { get; set; }

        public string? ScheduleDate { get; set; }

        public string? CampaignType { get; set; }

        public string? Progress { get; set; }

        public string? Percentage { get; set; }
    }

    public class Smsc
    {
        public int Id { get; set; }

        public string? SmscName { get; set; }

        public string? Status { get; set; }

        public string? Progress { get; set; }

        public string? Tps { get; set; }

        public string? Action { get; set; }
    }

    public class WorldMap
    {
        public int Id { get; set; }
        public string? key { get; set; }
        public int doc_count { get; set; }
        public string? delivery_rate { get; set; }

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Claims;
using System.Threading;
using System.Threading;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.Dtos.Requests.Synapse.UserCampaigns;
using Core.Models.Dtos.Responses.Synapse.UserCampaigns;
using Core.Models.Enums;
using Core.Models.Extensions;
using Core.Models.Helpers;
using Elmah;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Synapse.Web.CampaignPlugin.Helpers;
using Synapse.Web.CampaignPlugin.Helpers.CustomAttributes;
using Synapse.Web.CampaignPlugin.Helpers.SecureAccess;
using Synapse.Web.CampaignPlugin.Models;

namespace Synapse.Web.CampaignPlugin.Controllers
{
    [CheckUserSessionAttribute]
    public class CCampaignPluginController : Controller
    {
        // GET: CCampaignPlugin
        internal CustomeUser ExtendedUser => HttpContext.Session.GetItem<CustomeUser>();
        internal List<UserActions> UserActions => HttpContext.Session.GetItem<List<UserActions>>();
        internal const string ControllerName = "CCampaignPlugin";
        public ActionResult Index()
        {
            var model = new QuickSMSOrCampaignMain
            {
                QuicksmsorCampaign = new QuickSMSOrCampaign() { TypeofCampaign = "QuickSMS", Schedule = "SendNow", Language = "English" },
                QuicksmsorCampaignCollections = new QuickSMSOrCampaign().buildmodel(0, ExtendedUser.LogOnRespons.Id, 2, 1, 0, ExtendedUser.LogOnRespons.GetIPAddress,"").Where(w => w.CurrentStatus == (Cstatus)2 && w.intStatus == 2).ToList(),
                Senders = new QuickSMSOrCampaign().buildSenders(ExtendedUser.LogOnRespons.Id),
                CampaignTypes = new CampsCampaignType().buildCampTypes(),
                //ExtGSMCharecters = string.Join(",", new MessageDetails().buildGSMChars().Where(w => w.GsmType == "E").Select(s => s.GsmChar.Trim()))
            };
            return View(model);
        }
        [HttpPost]
        [Synapse.Web.CampaignPlugin.Helpers.CustomAttributes.ValidateJsonAntiForgeryToken]
        public async Task<ActionResult> ApproveOrRejectQSMS(int QSMSID, int Status, int CurrentStatus, string Rejectreason, int MbcID, string Stage, int ModuleType)
        {
            if (QSMSID != 0)
            {
                var extendedUser = SessionExtensions.GetItem<Core.Models.Extensions.CustomeUser>(HttpContext.Session);
                using (var clientAcces = new AuthenticateSecurityClient())
                {
                    string ReturnVal = string.Empty;
                    var response = clientAcces.CheckerUpdateQuickSMS(new CheckerUpdateQSMSOnRequest
                    {
                        QSMSID = QSMSID,
                        Status = Status,
                        CurrentStatus = CurrentStatus,
                        RejectReason = Rejectreason,
                        UpdatedBy = extendedUser.LogOnRespons.Id,
                        ModuleType=ModuleType
                    });                  
                    return Json(response.Result);
                }
            }
            return Json("");
        }

        //bulk sms

        public ActionResult CBulkSms()
        {
            var model = new QuickSMSOrCampaignMain
            {
                QuicksmsorCampaign = new QuickSMSOrCampaign() { TypeofCampaign = "BulkkSMS", Schedule = "SendNow", Language = "English" },
                QuicksmsorCampaignCollections = new QuickSMSOrCampaign().buildmodel(0, ExtendedUser.LogOnRespons.Id, 2, 1, 1, ExtendedUser.LogOnRespons.GetIPAddress, "").Where(w => w.CurrentStatus == (Cstatus)2 || w.CurrentStatus == (Cstatus)4 && w.intStatus != 4).ToList(),
                Senders = new QuickSMSOrCampaign().buildSenders(ExtendedUser.LogOnRespons.Id),
                CampaignTypes = new CampsCampaignType().buildCampTypes(),
                //ExtGSMCharecters = string.Join(",", new MessageDetails().buildGSMChars().Where(w => w.GsmType == "E").Select(s => s.GsmChar.Trim())),
                Groups = new Group().buildGroups(ExtendedUser.LogOnRespons.Id).Where(w => w.CurrentStatus == (Cstatus)1).ToList()
            };
            return View(model);
        }



        //custom sms

        public ActionResult CCustomSms()
        {
            var model = new QuickSMSOrCampaignMain
            {
                QuicksmsorCampaign = new QuickSMSOrCampaign() { TypeofCampaign = "CustomkSMS", Schedule = "SendNow", Language = "English" },
                QuicksmsorCampaignCollections = new QuickSMSOrCampaign().buildmodel(0, ExtendedUser.LogOnRespons.Id, 2, 1, 2, ExtendedUser.LogOnRespons.GetIPAddress, "").Where(w => w.CurrentStatus == (Cstatus)2 || w.CurrentStatus == (Cstatus)4 && w.intStatus != 4).ToList(),
              
                Senders = new QuickSMSOrCampaign().buildSenders(ExtendedUser.LogOnRespons.Id),
                CampaignTypes = new CampsCampaignType().buildCampTypes(),
               
                Groups = new Group().buildGroups(ExtendedUser.LogOnRespons.Id).Where(w => w.CurrentStatus == (Cstatus)1).ToList()
            };
            return View(model);
        }


        [PreventSpam]
        [HttpPost]
        public ActionResult OnView(string QSMSName, string Sender, string Lang, string Message, string CreditsUsed, string TotalCredits, string Dlr, string AddedDate, string Status, string strRecipentID, string GrpName, string ValidCount, string ScheduleDate, string ActualFileName, string cmd = "")
        {
            Logger.InfoFormat("OnView :: start :: {0}", QSMSName, Sender, Lang, Message, CreditsUsed, TotalCredits, Dlr, AddedDate, Status);
            try
            {
                try
                {
                    QSMSName = AESEncrytDecry.DecryptStringAES(QSMSName);
                    Sender = AESEncrytDecry.DecryptStringAES(Sender);
                    Lang = AESEncrytDecry.DecryptStringAES(Lang);
                    Message = AESEncrytDecry.DecryptStringAES(Message);
                    Message = Message.Replace("Ø","\\").Replace("^", "\"").Replace("ÞÞ","^").Replace("&#39;", "'").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">").Replace("$nl$", "\n").Replace("$sq$", "'");
                    CreditsUsed = AESEncrytDecry.DecryptStringAES(CreditsUsed);
                    TotalCredits = AESEncrytDecry.DecryptStringAES(TotalCredits);
                    Dlr = AESEncrytDecry.DecryptStringAES(Dlr);
                    AddedDate = AESEncrytDecry.DecryptStringAES(AddedDate);
                    Status = AESEncrytDecry.DecryptStringAES(Status);
                    GrpName = AESEncrytDecry.DecryptStringAES(GrpName) == "QSMS" ? "" : AESEncrytDecry.DecryptStringAES(GrpName);
                    ValidCount = AESEncrytDecry.DecryptStringAES(ValidCount);
                    ScheduleDate = AESEncrytDecry.DecryptStringAES(ScheduleDate) == "QSMS" ? "" : AESEncrytDecry.DecryptStringAES(ScheduleDate);
                    ActualFileName = AESEncrytDecry.DecryptStringAES(ActualFileName) == "QSMS" ? "" : AESEncrytDecry.DecryptStringAES(ActualFileName);
                    strRecipentID = AESEncrytDecry.DecryptStringAES(strRecipentID);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("OnView() :: Exception Error :" + ex.Message.ToString());
                    return Json(new { Invalid = true, Message ="InvalidInputParameters" });
                }
                //  GrpName == "QSMS" ? "" : GrpName;
                QSMSName = QSMSName.Replace("^", "\\").Replace("ÞÞ","^");
                //Message = Message.Replace("~~","'").Replace("~", "^");
                int RecipentID = Convert.ToInt32(strRecipentID);
                var model = new QuickSMSOrCampaign
                {
                    CampaignNameorRecipient = QSMSName,
                    Sender = Sender,
                    Language = Lang,
                    Message = Message,
                    CreditsUsed = Convert.ToInt32(CreditsUsed),
                    TotalQSMSCredits = Convert.ToInt32(TotalCredits),
                    Dlr = 1,
                    AddedDate = AddedDate,
                    Status = Status,
                    RecipientsType = (FILETYPE)RecipentID,
                    ValidCount = Convert.ToInt32(ValidCount),
                    ScheduleDate = ScheduleDate,
                    ActualFileName = ActualFileName.Replace("^", "\\"),
                    GroupIds = GrpName.Trim(',')
                };
                if (cmd != "")
                {
                    return Json(new
                    {
                        PartialResult = RenderRazorViewToString(cmd, model)

                    });
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("OnView :-{0} Error :- {1}", QSMSName, Sender, Lang, Message, TotalCredits, CreditsUsed, Status);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return Json("");
        }

        private async Task<string> RenderRazorViewToString(string viewName, object model)
        {
            // 1. Assign model to ViewData
            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                // 2. Get the Razor view engine from HttpContext services
                var viewEngine = HttpContext.RequestServices.GetService(typeof(IRazorViewEngine)) as IRazorViewEngine;
                if (viewEngine == null) throw new InvalidOperationException("IRazorViewEngine not found.");

                // 3. Find the partial view
                var viewResult = viewEngine.FindView(ControllerContext, viewName, false);

                if (!viewResult.Success)
                {
                    // Fallback to absolute or relative path if name lookup fails
                    viewResult = viewEngine.GetView(executingFilePath: null, viewPath: viewName, isMainPage: false);
                    if (!viewResult.Success)
                    {
                        throw new FileNotFoundException($"Cannot find view: {viewName}");
                    }
                }

                // 4. Create the view context required for rendering
                var tempDataProvider = HttpContext.RequestServices.GetService(typeof(ITempDataProvider)) as ITempDataProvider;
                var viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    ViewData,
                    new TempDataDictionary(HttpContext, tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                // 5. Render the view async
                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }

        [HttpPost]
        [Synapse.Web.CampaignPlugin.Helpers.ValidateJsonAntiForgeryToken]
        public ActionResult LoadTemplates(int CampType)
        {
            Logger.InfoFormat("LoadTemplates :: start :: {0}", CampType);
            try
            {
                var extendedUser = SessionExtensions.GetItem<Core.Models.Extensions.CustomeUser>(HttpContext.Session);
                //var templates = new Template().buildTemplates(extendedUser.LogOnRespons.CustomerId, extendedUser.LogOnRespons.Id, CampType);
                var templates = new TemplateCreation().buildmodel(extendedUser.LogOnRespons.CustomerId, extendedUser.LogOnRespons.Id, ExtendedUser.LogOnRespons.GetIPAddress, "", false);

                return Json(templates.Where(x => x.TYPE == CampType && x.STATUS == true));
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("LoadTemplates :-{0} Error :- {1}", CampType);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        [HttpPost]
        public ActionResult OnSheetChange(string Sheet)
        {
            Logger.InfoFormat("OnSheetChange :: start :: {0}", Sheet);
            try
            {
                try
                {
                    Sheet = AESEncrytDecry.DecryptStringAES(Sheet);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("OnSheetChange() :: Exception Error :" + ex.Message.ToString());
                    return Json(new { Invalid = true, Message = "InvalidInputParameters" });
                }
                if (Sheet != "")
                {
                    var TotalRec = SessionExtensions.GetItem<List<FileUploadDet>>(HttpContext.Session);
                    var SelRec = TotalRec.FirstOrDefault(x => x.SheetName == Sheet);
                    return Json(SelRec);
                }
                return Json("");
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("OnSheetChange  :-{0} Error :- {1}", Sheet);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        [HttpPost]
        public ActionResult CampaignEvents(int CampID, string StageIDs, int Status,int CurrentStatus, string cmd = "")
        {     
            using (var _clientAccess = new AuthenticateSecurityClient())
            {

                var response = _clientAccess.SetCampaignEvents(new SetCampEventsOnRequest
                {
                    CampID = CampID,
                    StageIDs = StageIDs,
                    Status = Status,
                    CreatedBy = ExtendedUser.LogOnRespons.Id,
                    CurrentStatus = CurrentStatus,
                    UserIp=ExtendedUser.LogOnRespons.GetIPAddress
                });
                return Json(response.Result);
            }
            return Json(-1);
        }

        [HttpPost]
        public ActionResult GetCampaignSearchText(string SearchText, string DiffId)
        {
            Logger.InfoFormat("GetCampaignSearchText :: start :: {0}", SearchText);
            try
            {
                try
                {
                    SearchText = AESEncrytDecry.DecryptStringAES(SearchText);
                    DiffId = AESEncrytDecry.DecryptStringAES(DiffId);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat(" GetCampaignSearchText :: Error :: {0}", ex.ToString());
                    return Json(new { Invalid = true, Message = "InvalidInputParameters" });
                }

                var ischeckerrequierd = UserActions.IsCheckerRequiredVerification(x => x.ActionName.Equals("BulkSms", StringComparison.OrdinalIgnoreCase)
                             && x.ControllerName.Equals(ControllerName, StringComparison.OrdinalIgnoreCase) && x.IsCheckerRequired);


                if (DiffId == "1")
                {
                    var model = new QuickSMSOrCampaign().buildmodel(0, ExtendedUser.LogOnRespons.Id, 2, 1, 1, ExtendedUser.LogOnRespons.GetIPAddress, SearchText).Where(w => w.CurrentStatus == (Cstatus)2 && w.intStatus != 4).ToList();
                    return PartialView("_CBulkSMSTable", model);
                }
                else if (DiffId == "4")
                {
                    var model = new QuickSMSOrCampaign().buildmodel(0, ExtendedUser.LogOnRespons.Id, 2, 1, 4, ExtendedUser.LogOnRespons.GetIPAddress, SearchText);
                    return PartialView("_BulkSMSTable", model);
                }
                else
                {
                    var model = new QuickSMSOrCampaign().buildmodel(0, ExtendedUser.LogOnRespons.Id, 2, 1, 2, ExtendedUser.LogOnRespons.GetIPAddress, SearchText);
                    return PartialView("_CCustomSMSTable", model);
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("GetCampaignSearchText ::CustomerId :-{0} Error :- {1}", SearchText);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }


        [HttpPost]
        [Synapse.Web.CampaignPlugin.Helpers.CustomAttributes.ValidateJsonAntiForgeryToken]
        public ActionResult GettingColumnList(int Id)
        {
            var templates = new templatemapcolumns { }.buildmodelForGetColumns(Id);
            return Json(templates);
        }

        [HttpPost]
        public ActionResult FileChangedEventOnEdit(string filepath)
        {
            Logger.InfoFormat("FileChangedEventOnEdit :: start :: {0}", filepath);
            try
            {
                filepath = AESEncrytDecry.DecryptStringAES(filepath);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("FileChangedEventOnEdit :: Error :: {0}", ex.ToString());
                return Json(new { Invalid = true, Message = "InvalidInputParameters" });
            }
            if (filepath != null)
            {
                try
                {
                    var ValidPath = filepath.Replace(System.Environment.NewLine, "").Replace("^", "\\");
                    var isExists = false;
                    if (System.IO.File.Exists(ValidPath))
                    {
                        isExists = true;
                    }
                    if (!isExists)
                    {
                        ValidPath = ValidPath.Replace(".csv", ".xlsx");
                        if (System.IO.File.Exists(ValidPath))
                        {
                            isExists = true;
                        }

                        if (!isExists)
                        {
                            ValidPath = ValidPath.Replace(".csv", ".xls");
                            if (System.IO.File.Exists(ValidPath))
                            {
                                isExists = true;
                            }
                        }
                        if (!isExists)
                        {
                            ValidPath = ValidPath.Replace(".xlsx", ".csv");
                            if (System.IO.File.Exists(ValidPath))
                            {
                                isExists = true;
                            }
                        }
                        if (!isExists)
                        {
                            ValidPath = ValidPath.Replace(".xls", ".csv");
                            if (System.IO.File.Exists(ValidPath))
                            {
                                isExists = true;
                            }
                        }

                    }
                    var fileData = Synapse.Web.CampaignPlugin.Helpers.ExcelParser.ParseFile(ValidPath);

                    var dt = IEnumerableExtension.BuildCsvToTable(ValidPath);

                    var fileDetails = SessionExtensions.GetItem<List<FileUploadDet>>(HttpContext.Session);
                    if (fileDetails != null)
                    {
                        fileDetails = null;
                        fileDetails = fileData;
                        SessionExtensions.AddItem<List<FileUploadDet>>(HttpContext.Session, fileDetails);
                    }
                    else
                    {
                        SessionExtensions.AddItem<List<FileUploadDet>>(HttpContext.Session, fileData);
                    }

                    fileData.ForEach(X =>
                    {
                        X.FileRecords = new List<dynamic>();
                        //X.SheetName = dt.TableName;
                    });

                    return Json(fileData);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("FileChangedEventOnEdit  :-{0} Error :- {1}", filepath);
                    ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }
            return Json("");
        }
    }
}
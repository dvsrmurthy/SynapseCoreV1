using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Dynamic;
using System.IO;
//using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
//using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using ClientHTTPConsuming.Utilities;
using Core.Models;
//using Core.Models.Helpers;
using Core.Models.Dtos.CommonDtos;
using Core.Models.Dtos.Requests.Synapse.ManageMobilityCenter;
using Core.Models.Dtos.Requests.Synapse.UserCampaigns;
using Core.Models.Dtos.Responses.Synapse.SecurityManagement;
using Core.Models.Dtos.Responses.Synapse.UserCampaigns;
using Core.Models.Enums;
using Core.Models.Extensions;
using Elmah;
using ElmahCore;
using Excel;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Synapse.Web.CampaignPlugin.Helpers;
using Synapse.Web.CampaignPlugin.Helpers.CustomAttributes;
using Synapse.Web.CampaignPlugin.Helpers.SecureAccess;
using Synapse.Web.CampaignPlugin.Models;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using SessionExtensions = Synapse.Web.CampaignPlugin.Helpers.SecureAccess.SessionExtensions;
using Microsoft.Extensions.DependencyInjection;

namespace Synapse.Web.CampaignPlugin.Controllers
{
    [AllowRoleBaseAuthorize]
    [CheckUserSessionAttribute]
    [Area("CampaignPlugin")]
    [Route("CampaignPlugin")]
    public class CampaignPluginController : Controller
    {
        ILog Logger = LogManager.GetLogger(typeof(CampaignPluginController));
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        internal int? conCode;
        internal int conMobLength;
        internal CustomeUser ExtendedUser;
        internal List<UserActions> UserActions = null;
        internal const string ControllerName = "CampaignPlugin";
        internal LocalizationResponse lz = null;

        public CampaignPluginController(IConfiguration configuration, IHttpContextAccessor httpContext)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContext;
            conCode = Convert.ToInt32(_configuration["CountryCode"]); //countrycode
            conMobLength = Convert.ToInt32(_configuration["CountryMobileLength"]); //mobilelength
            ExtendedUser = SessionExtensions.GetItem<Core.Models.Extensions.CustomeUser>(httpContext.HttpContext.Session);
            UserActions = SessionExtensions.GetItem<List<UserActions>>(httpContext.HttpContext.Session);
            lz = SessionExtensions.GetItem<LocalizationResponse>(httpContext.HttpContext.Session);
        }
        [HttpGet("Index")]
        public ActionResult Index()
        {

            Logger.Info("Index :: start :: {0}");
            try
            {
                var model = new QuickSMSOrCampaignMain
                {
                    QuicksmsorCampaign = new QuickSMSOrCampaign() { TypeofCampaign = "QuickSMS", Schedule = "SendNow", Language = "English" },
                    QuicksmsorCampaignCollections = new QuickSMSOrCampaign().buildmodel(0, ExtendedUser.LogOnRespons.Id, 2, 1, 0, ExtendedUser.LogOnRespons.GetIPAddress, ""),
                    Senders = new List<Sender>(),
                    CampaignTypes = new CampsCampaignType().buildCampTypes(),
                };
                return View(model);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Index  :-{0} Error :- {1}");
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        public async Task<ActionResult> __QuickSMSTable()
        {
            Logger.Info("__QuickSMSTable :: start :: {0}");
            try
            {
                var ischeckerrequierd =
                    UserActions.IsCheckerRequiredVerification(
                        x => x.ActionName.Equals("Index", StringComparison.OrdinalIgnoreCase)
                             && x.ControllerName.Equals(ControllerName, StringComparison.OrdinalIgnoreCase) &&
                             x.IsCheckerRequired);

                var model = new QuickSMSOrCampaign().buildmodel(0, ExtendedUser.LogOnRespons.Id, 2, 1, 0, ExtendedUser.LogOnRespons.GetIPAddress, "");
                HttpContext.Session.Remove("GetRecpCon");
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("__QuickSMSTable  :-{0} Error :- {1}");
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SessionContacts(string RemoveRecp)
        {
            HttpContext.Session.Remove("GetRecpCon");
            string? sessionValue = HttpContext.Session.GetString("GetRecpCon");
            return Json(sessionValue);
        }
        [HttpGet("BulkSms")]
        public ActionResult BulkSms()
        {
            Logger.Info("BulkSms :: start :: {0}");
            try
            {
                var ischeckerrequierd =
                UserActions.IsCheckerRequiredVerification(
                    x => x.ActionName.Equals("BulkSms", StringComparison.OrdinalIgnoreCase)
                         && x.ControllerName.Equals(ControllerName, StringComparison.OrdinalIgnoreCase) &&
                         x.IsCheckerRequired);
                bool ic = ischeckerrequierd == false ? true : ischeckerrequierd;

                var model = new QuickSMSOrCampaignMain
                {
                    QuicksmsorCampaign = new QuickSMSOrCampaign() { TypeofCampaign = "BulkkSMS", Schedule = "SendNow", Language = "English" },

                    QuicksmsorCampaignCollections = new QuickSMSOrCampaign().buildmodel(0, ExtendedUser.LogOnRespons.Id, 2, 1, 1, ExtendedUser.LogOnRespons.GetIPAddress, "", ic),
                    Senders = new List<Sender>(),
                    CampaignTypes = new CampsCampaignType().buildCampTypes(),
                    Groups = new Synapse.Web.CampaignPlugin.Models.Group().buildGroups(ExtendedUser.LogOnRespons.Id).Where(w => w.CurrentStatus == (Cstatus)1).ToList()
                };
                HttpContext.Session.SetString("QuicksmsorCampaignCollections", JsonSerializer.Serialize(model.QuicksmsorCampaignCollections));
                return View(model);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("BulkSms  :-{0} Error :- {1}");
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        public async Task<ActionResult> _BulkSMSTable()
        {
            Logger.Info("_BulkSMSTable :: start :: {0}");
            try
            {
                var ischeckerrequierd =
                    UserActions.IsCheckerRequiredVerification(
                        x => x.ActionName.Equals("BulkSms", StringComparison.OrdinalIgnoreCase)
                             && x.ControllerName.Equals(ControllerName, StringComparison.OrdinalIgnoreCase) &&
                             x.IsCheckerRequired);
                bool ic = ischeckerrequierd == false ? true : ischeckerrequierd;
                var model = new QuickSMSOrCampaign().buildmodel(0, ExtendedUser.LogOnRespons.Id, 2, 1, 1, ExtendedUser.LogOnRespons.GetIPAddress, "", ic);
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("_BulkSMSTable  :-{0} Error :- {1}");
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }
        [HttpGet("CustomSms")]
        public ActionResult CustomSms()
        {
            Logger.Info("CustomSms :: start :: {0}");
            try
            {
                var ischeckerrequierd =
                UserActions.IsCheckerRequiredVerification(
                    x => x.ActionName.Equals("CustomSms", StringComparison.OrdinalIgnoreCase)
                         && x.ControllerName.Equals(ControllerName, StringComparison.OrdinalIgnoreCase) &&
                         x.IsCheckerRequired);
                bool ic = ischeckerrequierd == false ? true : ischeckerrequierd;
                var model = new QuickSMSOrCampaignMain
                {
                    QuicksmsorCampaign = new QuickSMSOrCampaign() { TypeofCampaign = "CustomkSMS", Schedule = "SendNow", Language = "English" },

                    QuicksmsorCampaignCollections = new QuickSMSOrCampaign().buildmodel(0, ExtendedUser.LogOnRespons.Id, 2, 1, 2, ExtendedUser.LogOnRespons.GetIPAddress, "", ic),
                    Senders = new List<Sender>(),
                    CampaignTypes = new CampsCampaignType().buildCampTypes(),
                    Groups = new Synapse.Web.CampaignPlugin.Models.Group().buildGroups(ExtendedUser.LogOnRespons.Id).Where(w => w.CurrentStatus == (Cstatus)1).ToList()
                };
                HttpContext.Session.SetString("QuicksmsorCampaignCollectionsC", JsonSerializer.Serialize(model.QuicksmsorCampaignCollections));
                return View(model);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("CustomSms  :-{0} Error :- {1}");
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        public async Task<ActionResult> _CustomSMSTable()
        {
            Logger.Info("_CustomSMSTable :: start :: {0}");
            try
            {
                var ischeckerrequierd =
                    UserActions.IsCheckerRequiredVerification(
                        x => x.ActionName.Equals("CustomSms", StringComparison.OrdinalIgnoreCase)
                             && x.ControllerName.Equals(ControllerName, StringComparison.OrdinalIgnoreCase) &&
                             x.IsCheckerRequired);
                bool ic = ischeckerrequierd == false ? true : ischeckerrequierd;
                var model = new QuickSMSOrCampaign().buildmodel(0, ExtendedUser.LogOnRespons.Id, 2, 1, 2, ExtendedUser.LogOnRespons.GetIPAddress, "", ic);


                return PartialView(model);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("_CustomSMSTable :-{0} Error :- {1}");
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();

        }

        public ActionResult ExternalCampaign()
        {
            Logger.Info("ExternalCampaign :: start :: {0}");
            try
            {
                var model = new QuickSMSOrCampaignMain
                {
                    QuicksmsorCampaign = new QuickSMSOrCampaign() { TypeofCampaign = "ExternalCampaign", Schedule = "SendNow", Language = "English" },
                    QuicksmsorCampaignCollections = new QuickSMSOrCampaign().buildmodel(0, ExtendedUser.LogOnRespons.Id, 2, 1, 4, ExtendedUser.LogOnRespons.GetIPAddress, ""),
                    Senders = new QuickSMSOrCampaign().buildSenders(ExtendedUser.LogOnRespons.Id),
                    Nationality = new QuickSMSOrCampaign().buildNationality(),
                    City = new QuickSMSOrCampaign().buildCity(),
                    Incomegroup = new QuickSMSOrCampaign().buildIncomegroup(),
                    CampaignTypes = new CampsCampaignType().buildCampTypes(),
                    Groups = new Synapse.Web.CampaignPlugin.Models.Group().buildGroups(ExtendedUser.LogOnRespons.Id).Where(w => w.CurrentStatus == (Cstatus)1).ToList()
                };
                return View(model);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("ExternalCampaign  :-{0} Error :- {1}");
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        public async Task<ActionResult> _ExterCampaignTable()
        {
            Logger.Info("_ExterCampaignTable :: start :: {0}");
            try
            {
                var ischeckerrequierd =
                    UserActions.IsCheckerRequiredVerification(
                        x => x.ActionName.Equals("ExternalCampaign", StringComparison.OrdinalIgnoreCase)
                             && x.ControllerName.Equals(ControllerName, StringComparison.OrdinalIgnoreCase) &&
                             x.IsCheckerRequired);

                var model = new QuickSMSOrCampaign().buildmodel(0, ExtendedUser.LogOnRespons.Id, 2, 1, 4, ExtendedUser.LogOnRespons.GetIPAddress, "");
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("_ExterCampaignTable  :-{0} Error :- {1}");
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    return Json(new { Invalid = true, Message = lz.InvalidInputParameters });
                }

                var ischeckerrequierd = UserActions.IsCheckerRequiredVerification(x => x.ActionName.Equals("BulkSms", StringComparison.OrdinalIgnoreCase)
                             && x.ControllerName.Equals(ControllerName, StringComparison.OrdinalIgnoreCase) && x.IsCheckerRequired);


                if (DiffId == "1")
                {
                    var model = new QuickSMSOrCampaign().buildmodel(0, ExtendedUser.LogOnRespons.Id, 2, 1, 1, ExtendedUser.LogOnRespons.GetIPAddress, SearchText);
                    return PartialView("_BulkSMSTable", model);
                }
                else if (DiffId == "4")
                {
                    var model = new QuickSMSOrCampaign().buildmodel(0, ExtendedUser.LogOnRespons.Id, 2, 1, 4, ExtendedUser.LogOnRespons.GetIPAddress, SearchText);
                    return PartialView("_BulkSMSTable", model);
                }
                else
                {
                    var model = new QuickSMSOrCampaign().buildmodel(0, ExtendedUser.LogOnRespons.Id, 2, 1, 2, ExtendedUser.LogOnRespons.GetIPAddress, SearchText);
                    return PartialView("_CustomSMSTable", model);
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("GetCampaignSearchText ::CustomerId :-{0} Error :- {1}", SearchText);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        public ActionResult LoadContactsQSMS(string Groupids = "")
        {
            Logger.InfoFormat("LoadContactsQSMS :: start :: {0}", Groupids);
            try
            {
                List<Contact> ContactsList = new List<Contact>();
                var extendedUser = SessionExtensions.GetItem<Core.Models.Extensions.CustomeUser>(HttpContext.Session);
                ContactsList = new Contact { CreatedBy = extendedUser.LogOnRespons.Id }.buildContacts(Groupids, ExtendedUser.LogOnRespons.GetIPAddress).Where(w => w.Fstatus == (Cstatus)1).ToList();
                return PartialView("_GetContacts", ContactsList);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("LoadContactsQSMS :-{0} Error :- {1}", Groupids);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        [HttpPost]
        [Synapse.Web.CampaignPlugin.Helpers.ValidateJsonAntiForgeryToken]
        public ActionResult LoadTemplates(int CampType)
        {
            Logger.InfoFormat("LoadTemplates :: start :: {0}", CampType);
            try
            {
                var extendedUser = SessionExtensions.GetItem<Core.Models.Extensions.CustomeUser>(HttpContext.Session);
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
        [ValidateAntiForgeryToken]
        public ActionResult GettingColumnList(int Id)
        {
            var templates = new templatemapcolumns { }.buildmodelForGetColumns(Id);
            return Json(templates);
        }

        [HttpPost]
        [Synapse.Web.CampaignPlugin.Helpers.ValidateJsonAntiForgeryToken]
        public ActionResult GetStageCounts(int CampID, string Stages)
        {
            Logger.InfoFormat("GetStageCounts :: start :: {0}", CampID, Stages);
            try
            {
                string returnval = "";
                using (var _clientAccess = new AuthenticateSecurityClient())
                {
                    var response = _clientAccess.GetCampStageCounts(new GetStageCountsOnRequest
                    {
                        CampID = CampID,
                        StageIDs = Stages
                    });
                    if (response.Result.ResumeCnt != null)
                    {
                        if (response.Result.ResumeCnt > 0)
                            returnval = "PauseEnable";
                    }
                    if (response.Result.PauseCnt != null)
                    {
                        if (response.Result.PauseCnt > 0)
                            returnval = "ResumeEnable";
                    }
                    if (response.Result.PauseCnt == 0 && response.Result.ResumeCnt == 0)
                    {
                        returnval = "DisableRow";
                    }
                    //return Json(returnval);
                }
                return Json(returnval);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("GetStageCounts :-{0} Error :- {1}", CampID, Stages);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        [HttpPost]
        [Synapse.Web.CampaignPlugin.Helpers.ValidateJsonAntiForgeryToken]

        public ActionResult CampaignEvents(int CampID, string StageIDs, int Status, string schtype, string cmd = "")
        {
            var selAction =
                UserActions.FirstOrDefault(
                    w => w.ActionName.Equals("Index") &&
                        w.ControllerName.Equals(ControllerName, StringComparison.OrdinalIgnoreCase));
            using (var _clientAccess = new AuthenticateSecurityClient())
            {

                var response = _clientAccess.SetCampaignEvents(new SetCampEventsOnRequest
                {
                    CampID = CampID,
                    StageIDs = StageIDs,
                    Status = Status,
                    CreatedBy = ExtendedUser.LogOnRespons.Id,
                    CurrentStatus = ExtendedUser.LogOnRespons.RoleId == 1 ? 1 : ((selAction != null && selAction.IsCheckerRequired) ? 2 : 1),
                    UserIp = ExtendedUser.LogOnRespons.GetIPAddress

                });
                return Json(response.Result);
            }
            return Json(-1);
        }

        private string ConvertFromXlsToCsv(string filePath, string extension)
        {
            try
            {
                using (FileStream stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    IExcelDataReader excelReader = null;
                    switch (extension)
                    {
                        case ".xls":
                            excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                            break;
                        case ".xlsx":
                            excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                            break;
                    }
                    excelReader.IsFirstRowAsColumnNames = true;
                    DataSet result = excelReader.AsDataSet();
                    if (result.Tables[0] != null)
                    {
                        var csvData = "";
                        var row_no = 0;
                        var columns = result.Tables[0].Columns.Cast<DataColumn>().Select(s => s.ColumnName);
                        foreach (var column in columns)
                        {
                            csvData += column + ",";
                        }
                        csvData = csvData.TrimEnd(',') + "\n";

                        while (row_no < result.Tables[0].Rows.Count)
                        {
                            for (int i = 0; i < result.Tables[0].Columns.Count; i++)
                            {
                                csvData += result.Tables[0].Rows[row_no][i].ToString() + ",";
                            }
                            csvData = csvData.TrimEnd(',');
                            row_no++;
                            csvData += "\n";
                        }
                        string output = Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath) + ".csv"); // define your own filepath & filename
                        using (StreamWriter csv = new StreamWriter(output, false))
                        {
                            csv.Write(csvData);
                            csv.Close();
                        }
                        return output;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("While converting xls to csv, throuing fatal error :: {0}", ex.ToString());
            }
            return string.Empty;
        }

        [HttpPost("FileChangedEvent")]
        [RequestTimeout("LongRunningPolicy")]
        //[Synapse.Web.CampaignPlugin.Helpers.ValidateJsonAntiForgeryToken]
        public IActionResult FileChangedEvent(string file, string fileName, int type = 1)//Type Added by Murty
        {
            try
            {
                fileName = AESEncrytDecry.DecryptStringAES(fileName);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("FileChangedEvent :: Exception Error :" + ex.Message.ToString());
                return Json(new { Invalid = true, Message = lz.InvalidInputParameters });
            }

            string namefile = fileName.Substring(0, fileName.IndexOf('.'));
            if (!Regex.IsMatch(namefile, "^(?:[A-Za-z0-9_-]+)(?:[A-Za-z0-9 _-]*)$"))
            {
                return Json("Special Characters");
            }

            if (file != null)
            {
                try
                {
                    var splititems = file.Split(',');
                    if (splititems.Length != 2) { return Json("Invalid File"); }
                    var fileContent = Convert.FromBase64String(file.Split(',')[1]);
                    if (fileContent == null)
                    {
                        return Json("Invalid File");
                    }
                    var MainDirectory = _configuration["tempPath"]?.ToString();
                    var currentMonthYear = DateTime.Now.ToString("MMMyyyy");
                    //added by Murty
                    string typeDirName = "Custom";
                    if (type == 2) typeDirName = "Bulk";
                    //added by Murty
                    if (!Directory.Exists(Path.Combine(MainDirectory, currentMonthYear, typeDirName)))
                    {
                        //Changed by Murty
                        Directory.CreateDirectory(Path.Combine(MainDirectory, currentMonthYear, typeDirName));
                    }
                    var extension = Path.GetExtension(fileName);
                    // TypeDirName added by Murty
                    var filepath = Path.Combine(MainDirectory, currentMonthYear, typeDirName) + "\\" + DateTime.Now.Ticks + "_" + Path.GetFileNameWithoutExtension(fileName) + ((Path.GetExtension(fileName) == ".csv" || Path.GetExtension(fileName) == ".txt") ? ".csv" : Path.GetExtension(fileName));
                    // System.IO.File.WriteAllBytes(filepath, fileContent);
                    var fwext = Path.GetFileNameWithoutExtension(filepath);
                    var fwedir = Path.GetDirectoryName(filepath);
                    System.IO.File.WriteAllBytes(Path.Combine(fwedir, (fwext.Replace("&", "") + Path.GetExtension(filepath))), fileContent);

                    var dir = Path.GetDirectoryName(filepath);
                    var orginalfilename = Path.GetFileNameWithoutExtension(filepath);
                    System.IO.File.WriteAllBytes(Path.Combine(dir, (orginalfilename.Replace("&", "") + "_client.csv")), fileContent);

                    var fileData = Helpers.ExcelParser.ParseFile(Path.Combine(dir, (orginalfilename + ((extension == ".csv" || extension == ".txt") ? ".csv" : extension))));
                    //var fileData = Helpers.ExcelParser.ParseFile(Path.Combine("", (orginalfilename + ((extension == ".csv" || extension == ".txt") ? ".csv" : extension))));
                    if (fileData == null) { return Json("Invalid File"); }
                    if (fileData != null && fileData.FirstOrDefault() != null && fileData.FirstOrDefault().FileRecords.Count() == 0) { return Json("Invalid File"); }
                    var fdat = fileData.FirstOrDefault();
                    foreach (var cl in fdat.Columns)
                    {
                        if (cl.ToString().IsNumeric() && cl.Contains("Column"))
                        {
                            var mfile = System.IO.File.Exists(filepath);
                            var cfile = System.IO.File.Exists(Path.Combine(dir, (orginalfilename + "_client.csv")));
                            if (mfile == true && cfile == true)
                            {
                                System.IO.File.Delete(Path.Combine(dir, (orginalfilename + "_client.csv")));
                                System.IO.File.Delete(filepath);
                            }
                            return Json("File should contain header information");
                        }
                    }

                    //Session["FilePath"]=

                    var fileDetails = SessionExtensions.GetItem<List<FileUploadDet>>(HttpContext.Session);
                    //fileDetails[0].FilePath = orginalfilename;
                    if (fileDetails != null)
                    {
                        fileDetails = null;
                        fileDetails = fileData;
                        //fileDetails[0].FilePath = orginalfilename;

                        SessionExtensions.AddItem<List<FileUploadDet>>(HttpContext.Session, fileDetails);
                    }
                    else
                    {
                        SessionExtensions.AddItem<List<FileUploadDet>>(HttpContext.Session, fileData);
                    }

                    //fileData[0].FilePath = orginalfilename;
                    fileData.ForEach(x =>
                    {
                        x.FileRecords = new List<JsonElement>();
                    });

                    var ext = extension == ".txt" ? ".csv" : extension;
                    fileData[0].FilePath = currentMonthYear + "\\" + typeDirName + "\\" + orginalfilename + ext; //@"Sep2019\637039625076021123_1234";//orginalfilename ;//Changed by Murty
                    return Json(fileData);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("FileChangedEvent :-{0} Error :- {1}", file, fileName);
                    ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }
            return Json("");
        }

        [HttpPost]
        [Synapse.Web.CampaignPlugin.Helpers.ValidateJsonAntiForgeryToken]
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
                return Json(new { Invalid = true, Message = lz.InvalidInputParameters });
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
                        X.FileRecords = new List<JsonElement>();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFile(string FilePath)
        {
            Logger.InfoFormat("DeleteFile :: start :: {0}", FilePath);
            try
            {
                var extension = Path.GetExtension(FilePath);
                var FileName = Path.GetFileNameWithoutExtension(FilePath);
                if (
                    System.IO.File.Exists(Path.Combine(Path.GetDirectoryName(FilePath),
                        FileName + ((extension == ".csv" || extension == ".txt") ? ".csv" : extension))))
                {
                    System.IO.File.Delete(FilePath);
                    var dir = Path.GetDirectoryName(FilePath);
                    var orginalfilename = Path.GetFileNameWithoutExtension(FilePath);
                    System.IO.File.Delete(Path.Combine(dir, (orginalfilename + "_client.csv")));
                    SessionExtensions.RemoveItem<List<FileUploadDet>>(HttpContext.Session);
                    return Json(7);
                }
                return Json("");
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("DeleteFile  :-{0} Error :- {1}", FilePath);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    return Json(new { Invalid = true, Message = lz.InvalidInputParameters });
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

        //[PreventSpam]        
        //[ValidateAntiForgeryToken]
        [HttpPost("InsertQSMS")]
        public ActionResult InsertQSMS(string QSMSID, string SenderID, string LangID, string Message, string DLR, 
            string SenderName, string Module, string MobileNos, string CharCount, string Credits, 
            string MbcID, string ModuleID, string category)
        {
            string QuickSMSFilesToBeSaved = Path.Combine(_configuration["tempPath"]?.ToString(), DateTime.Now.ToString("MMMyyyy"), "QuickSMS");
            if (!Directory.Exists(QuickSMSFilesToBeSaved))
            {
                Directory.CreateDirectory(QuickSMSFilesToBeSaved);
            }
            Logger.InfoFormat("InsertQSMS :: start :: {0}", QSMSID, SenderID, LangID, Message, DLR, SenderName, Module, MobileNos, CharCount, Credits, MbcID, ModuleID);
            var selAction =
                UserActions.FirstOrDefault(
                    w => w.ActionName.Equals("Index") &&
                        w.ControllerName.Equals(ControllerName, StringComparison.OrdinalIgnoreCase));
            var extendedUser = SessionExtensions.GetItem<Core.Models.Extensions.CustomeUser>(HttpContext.Session);
            string ReturnVal = string.Empty;
            try
            {
                QSMSID = AESEncrytDecry.DecryptStringAES(QSMSID);
                SenderID = AESEncrytDecry.DecryptStringAES(SenderID);
                LangID = AESEncrytDecry.DecryptStringAES(LangID);
                Message = AESEncrytDecry.DecryptStringAES(Message);
                DLR = AESEncrytDecry.DecryptStringAES(DLR);
                SenderName = AESEncrytDecry.DecryptStringAES(SenderName);
                MobileNos = AESEncrytDecry.DecryptStringAES(MobileNos);
                CharCount = AESEncrytDecry.DecryptStringAES(CharCount);
                Credits = AESEncrytDecry.DecryptStringAES(Credits);
                MbcID = AESEncrytDecry.DecryptStringAES(MbcID);
                Module = AESEncrytDecry.DecryptStringAES(Module);
                ModuleID = AESEncrytDecry.DecryptStringAES(ModuleID);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("InsertQSMS() :: Exception Error :" + ex.Message.ToString());
                return Json(new { Invalid = true, Message = lz.InvalidInputParameters });
            }
            using (var ClientAccess = new AuthenticateSecurityClient())
            {
                DataSet objDs = null; if (objDs == null) { objDs = null; }
                string strDup = string.Empty; string strInvalid = string.Empty; string validMobileNos = string.Empty;
                List<string> Stage = new List<string>();
                //int SrvcStage = 0;
                var fileContent = new StringBuilder();
                int ValidMobNosCnt = 0;
                //var LogPath = _configuration["tempPath"] + "\\QuickSMS" + "QuickSMS.txt";
                var LogPath = Path.Combine(QuickSMSFilesToBeSaved, "QuickSMSLog.txt");
                string logText = "Time : " + DateTime.Now.ToString() + ", Sender Id : " + SenderName + ", Mobilenos : " + MobileNos + ", LangID : " + LangID + ", Message : " + Message + ", Credits : " + Credits + ",User Name : " + ExtendedUser.LogOnRespons.UserName;
                fileContent.AppendLine(logText);

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
                                    Logger.Info("validate length e :: " + totLengthValid.Count());
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
                                    ReturnVal = lz.InValidNos;
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
                        var response = ClientAccess.InsertQSMS(new InsertQSMSOnRequest
                        {
                            QSMSID = Convert.ToInt32(QSMSID),
                            SenderID = Convert.ToInt32(SenderID),
                            LangID = Convert.ToInt32(LangID),
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
                            Status = (selAction != null && selAction.IsCheckerRequired) ? 2 : 0,
                            CurrentStatus = ExtendedUser.LogOnRespons.RoleId == 1 ? 1 : ((selAction != null && selAction.IsCheckerRequired) ? 2 : 1),

                            UserName = ExtendedUser.LogOnRespons.UserName,
                            UserIp = extendedUser.LogOnRespons.GetIPAddress
                        });

                        //var result = response.Result.Split('$')[0];
                        var result = (response != null) ? response.Result.nReturn.ToString() : "0";
                        if (_configuration["IndianSynapse"]?.ToString() == "true" && !Regex.IsMatch(SenderName, @"^[a-zA-Z]+$"))
                        {
                            var str = string.Empty;
                            var str1 = string.Empty;
                            var resul = MobileNos.Split(',');

                            foreach (var item in resul)
                            {
                                str = str + item.Substring(2, 10) + ",";
                            }

                            var NoCheck = DndNumberCheck(str);

                            HashSet<string> noche = new HashSet<string>(NoCheck.strDndNumbers.Select(s => s.DNDNumbers));

                            List<string> nondnd = MobileNos.Split(',').Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

                            foreach (var item in noche)
                            {
                                str1 = str1 + ("91" + item) + ",";
                                nondnd.Remove(("91" + item));

                            }

                            MobileNos = string.Join(",", nondnd);

                            var nId = (response != null) ? response.Result.nId.ToString() : "0";
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
                                if (noche.Count > 0)
                                {
                                    Logger.Info("SimpleSMS :: DndCheck Start ");

                                    var mob1 = string.Join(",", str1).TrimEnd(',');
                                    var xmlContent1 = "<root iscustome='false' priority='5'><sendsms userid='" + extendedUser.LogOnRespons.Id + "'  username='" + ExtendedUser.LogOnRespons.UserName + "' campainid='" + 0 + "' sender='" + WebUtility.HtmlEncode(SenderName) +
                                     "' language='" + ((LangID)) + "' message='" + WebUtility.HtmlEncode(Message).Replace("\n", "&#10;") +
                                            "' mobile=''><mobile>" + mob1 + "</mobile></sendsms></root>";
                                    var fPath = Path.Combine(
                                            (_configuration["tempPath"]?.ToString() + "QuickSMS"),
                                            fileName);
                                    if (!Directory.Exists(fPath))
                                    {
                                        Directory.CreateDirectory(fPath);
                                    }
                                    System.IO.File.WriteAllText(Path.Combine(fPath, fileName + "_dnd" + ".xml"), xmlContent1);
                                    Logger.Info("SimpleSMS :: DndCheck End ");
                                }

                                if (nondnd.Count > 0)
                                {
                                    Logger.Info("SimpleSMS :: Both Dnd and NonDnd check ");

                                    var xmlContent = "<root iscustome='false' priority='5'><sendsms userid='" + extendedUser.LogOnRespons.Id + "'  username='" + ExtendedUser.LogOnRespons.UserName + "' campainid='" + 0 + "' sender='" + WebUtility.HtmlEncode(SenderName) +
                                        "' language='" + ((LangID)) + "' message='" + WebUtility.HtmlEncode(Message).Replace("\n", "&#10;") +
                                            "' mobile=''><mobile>" + MobileNos + "</mobile></sendsms></root>";

                                    var fPath1 = Path.Combine(
                                            (_configuration["tempPath"]?.ToString() + "QuickSMS"),
                                            fileName);
                                    if (!Directory.Exists(fPath1))
                                    {
                                        Directory.CreateDirectory(fPath1);
                                    }
                                    System.IO.File.WriteAllText(Path.Combine(fPath1, fileName + ".xml"), xmlContent);

                                    Logger.Info("SimpleSMS :: Both Dnd and NonDnd check End ");
                                }
                                var QMsg = "action=start&camp_id=" + nId + "&camp_type=0&dir_name=" + fileName + "&count=" + tCounts;
                                var Qresult = new CampaignQLog().PushMessageToQ(QMsg);
                            }
                        }
                        else
                        {
                            Logger.Info("SimpleSMS :: Start");
                            var nId = (response != null) ? response.Result.nId.ToString() : "0";
                            //var nId = response.Result.Split('$')[1];
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
                                //string category = string.Empty;
                                //string? sessionCategory = HttpContext.Session.GetString("category");
                                if (!string.IsNullOrWhiteSpace(category))
                                {
                                    category = category.TrimEnd(',');
                                }
                                var xmlContent = "<root iscustome='false' priority='5'><sendsms userid='" + extendedUser.LogOnRespons.Id + "'  username='" + ExtendedUser.LogOnRespons.UserName + "' campainid='" + 0 + "' sender='" + WebUtility.HtmlEncode(SenderName) +
                                    "' language='" + ((LangID)) + "' message='" + WebUtility.HtmlEncode(Message).Replace("\n", "&#10;") +
                                        "' mobile='' category='" + category + "'><mobile>" + MobileNos + "</mobile></sendsms></root>";

                                var fPath = Path.Combine(
                                        (_configuration["tempPath"]?.ToString() + "QuickSMS"),
                                        fileName);
                                if (!Directory.Exists(fPath))
                                {
                                    Directory.CreateDirectory(fPath);
                                }
                                System.IO.File.WriteAllText(Path.Combine(fPath, fileName + ".xml"), xmlContent);

                                var QMsg = "action=start&camp_id=" + nId + "&camp_type=0&dir_name=" + fileName + "&count=" + tCounts;
                                var Qresult = new CampaignQLog().PushMessageToQ(QMsg);
                                Logger.Info("SimpleSMS :: End QMsg:: " + QMsg);
                            }
                        }
                        var nID = (response != null) ? response.Result.nId.ToString() : "0";
                        //var nID = response.Result.Split('$')[1];
                        if (result == "7" && !(selAction != null && selAction.IsCheckerRequired))
                        {  //Send request to campaign service                            
                            String strInputXml = String.Empty;
                            strInputXml = "<XML>";
                            strInputXml += "</XML>";
                            ReturnVal = "MsgSubmitSuccess";
                        }
                        else if (result == "7" && (selAction != null && selAction.IsCheckerRequired))
                        {
                            ReturnVal = "MsgSubittedToChecker";
                        }
                        else if (result == "1")
                        {
                            ReturnVal = "CustInActive";
                        }
                        else if (result == "2")
                        {
                            ReturnVal = "CustExpired";
                        }
                        else if (result == "3")
                        {
                            ReturnVal = "CustPrefInActive";
                        }
                        else if (result == "4")
                        {
                            ReturnVal = "InsufficentCredits";
                        }
                        else if (result == "5")
                        {
                            ReturnVal = "UpdatedSuccessfully";
                        }
                    }
                    else
                    {
                        ReturnVal = "InValidNos";
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
                    Logger.ErrorFormat("InsertQSMS :-{0} Error :- {1}", QSMSID, SenderID, LangID, Message, DLR, SenderName, Module, MobileNos, CharCount, Credits, MbcID, ModuleID);
                    ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }
            return Json("");
        }

        //[PreventSpam]
        [HttpPost("OnView")]
        //[Synapse.Web.CampaignPlugin.Helpers.ValidateJsonAntiForgeryToken]
        public async Task<ActionResult> OnView(string QSMSName, string Sender, string Lang, string Message, string CreditsUsed, string TotalCredits, string Dlr, string AddedDate, string Status, string strRecipentID, string GrpName, string ValidCount, string ScheduleDate, string ActualFileName, string cmd = "")
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
                    Message = Message.Replace("Ø", "\\").Replace("^", "\"").Replace("ÞÞ", "^").Replace("&#39;", "'").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">").Replace("$nl$", "\n").Replace("$sq$", "'");
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
                    return Json(new { Invalid = true, Message = lz.InvalidInputParameters });
                }
                //  GrpName == "QSMS" ? "" : GrpName;
                QSMSName = QSMSName.Replace("^", "\\").Replace("ÞÞ", "^");
                //Message = Message.Replace("~", "^").Replace("Þ", "~");
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
                    var html = await RenderRazorViewToString(cmd, model);
                    return Json(new
                    {
                        PartialResult = html
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

        [HttpPost]
        [Synapse.Web.CampaignPlugin.Helpers.ValidateJsonAntiForgeryToken]
        public ActionResult OnCustomPreview(string message, string GroupIds, string uploadType, string PlaceHolders, string MobileField, string SheetName = "", string cmd = "", string msgt = "", string mobs = "")
        {
            Logger.InfoFormat("OnView :: start :: {0}", message, GroupIds, uploadType, PlaceHolders, MobileField);
            try
            {
                var fileDetails = SessionExtensions.GetItem<List<FileUploadDet>>(HttpContext.Session);
                List<CustomPreView> CustomPrvmdl = new List<CustomPreView>();
                var holders = PlaceHolders.Trim(',').Split(',').ToList();
                int previewCnt = Convert.ToInt32(_configuration["PreViewCnt"]?.ToString());
                var splitItemms = new List<string>();
                var selectedRecords = new List<DataRow>();
                var MainDirectory = _configuration["tempPath"]?.ToString();

                if (fileDetails != null)
                {
                    if (!fileDetails.FirstOrDefault().FilePath.Contains("DocumentTemp"))
                    {
                        fileDetails.FirstOrDefault().FilePath = MainDirectory + fileDetails.FirstOrDefault().FilePath;
                    }

                }

                uploadType = string.IsNullOrWhiteSpace(GroupIds) ? "FILE" : "GROUP";
                if (uploadType == "FILE")
                {
                    var datatable = Synapse.Web.CampaignPlugin.Helpers.ExcelParser.ParseFileOnPreView(fileDetails.FirstOrDefault().FilePath, SheetName);
                    if (datatable != null && datatable.Rows.Count > 0)
                    {
                        datatable = datatable.Rows.Cast<DataRow>().GroupBy(g => g[MobileField].ToString()).Select(s => s.First()).Where(w => !string.IsNullOrWhiteSpace(w[MobileField].ToString())).CopyToDataTable();
                        if (!string.IsNullOrWhiteSpace(msgt))
                        {
                            splitItemms.AddRange(msgt.Split(new string[] { "|-|" }, StringSplitOptions.None).Where(x => !string.IsNullOrWhiteSpace(x)).ToList());
                            if (splitItemms.Count > 0)
                            {
                                message = splitItemms[0];
                                var mobnums = splitItemms[1].Split(',').Where(x => !string.IsNullOrWhiteSpace(x)).Select(s => s.Trim());
                                selectedRecords = (from row in datatable.Rows.Cast<DataRow>().GroupBy(g => g[MobileField].ToString()).Select(s => s.First())
                                                   join mobno in mobnums on row[MobileField].ToString() equals mobno
                                                   select row).ToList();
                                if (selectedRecords.Count < previewCnt)
                                {
                                    var dcolumns = datatable.Columns.Cast<DataColumn>().Select(s => s.ColumnName);
                                    foreach (var mob in mobnums)
                                    {
                                        var r = datatable.Rows.Cast<DataRow>().FirstOrDefault(w => w[MobileField].ToString().Equals(mob));
                                        if (r == null)
                                        {
                                            var ro = datatable.NewRow();
                                            ro[MobileField] = mob;
                                            foreach (var c in dcolumns)
                                            {
                                                if (!c.ToLower().Equals(MobileField.ToLower()))
                                                    ro[c] = "";
                                            }
                                            selectedRecords.Add(ro);
                                        }
                                    }

                                }
                            }
                        }
                        selectedRecords = (splitItemms.Count == 0) ? datatable.Rows.Cast<DataRow>().Take(previewCnt).ToList() : selectedRecords;
                        foreach (DataRow x in selectedRecords)
                        {
                            var repMsg = message;
                            var replacedMsg = "";
                            foreach (var y in holders)
                            {
                                replacedMsg = repMsg.Replace(System.Environment.NewLine, "").Replace("<$" + y + "$>", x[y].ToString() ?? "XXX");
                                repMsg = replacedMsg;
                            }
                            ;

                            CustomPrvmdl.Add(new CustomPreView
                            {
                                MobileNo = x[MobileField].ToString(),
                                ReplacedMsg = replacedMsg
                            });
                        }
                    }

                }
                else if (uploadType == "GROUP")
                {
                    List<Contact> ContactsList = new List<Contact>();
                    var extendedUser = SessionExtensions.GetItem<Core.Models.Extensions.CustomeUser>(HttpContext.Session);
                    ContactsList = new Contact { CreatedBy = extendedUser.LogOnRespons.Id }.buildContacts(GroupIds.Trim(','), ExtendedUser.LogOnRespons.GetIPAddress).Where(w => (w.Fstatus == (Cstatus)1 && w.Status == "Active")).ToList();
                    var SelectedContacts = new List<Contact>();
                    if (!string.IsNullOrWhiteSpace(msgt))
                    {
                        splitItemms.AddRange(msgt.Split(new string[] { "|-|" }, StringSplitOptions.None).Where(x => !string.IsNullOrWhiteSpace(x)).ToList());
                        if (splitItemms.Count > 0)
                        {
                            var mobnums = splitItemms[1].Split(',').Where(x => !string.IsNullOrWhiteSpace(x)).Select(s => s.Trim());
                            message = splitItemms[0];
                            SelectedContacts = (from row in ContactsList.GroupBy(g => g.MobileNo).Select(s => s.First())
                                                join mobno in mobnums on row.MobileNo equals mobno
                                                select row).ToList();
                            if (SelectedContacts.Count < previewCnt)
                            {
                                foreach (var mob in mobnums)
                                {
                                    var c = ContactsList.FirstOrDefault(f => f.MobileNo.Equals(mob));
                                    if (c == null)
                                    {
                                        SelectedContacts.Add(new Contact { MobileNo = mob });
                                    }
                                }
                            }
                        }
                    }
                    SelectedContacts = (splitItemms.Count == 0) ? (from cnt in ContactsList.GroupBy(g => g.MobileNo).Select(s => s.First())
                                                                       // join g in GroupIds.Split(',') on cnt.
                                                                   select cnt).Take(previewCnt).ToList() : SelectedContacts;

                    int gt = cmd == "gts" ? 0 : 1;
                    foreach (var x in SelectedContacts)
                    {
                        if (gt == 0)
                        {
                            var repMsg = message;
                            var replacedMsg = "";
                            foreach (var y in holders)
                            {
                                replacedMsg = repMsg.Replace(System.Environment.NewLine, "").Replace("<$" + y + "$>", (x.GetType().GetProperty(y).GetValue(x) != null ? x.GetType().GetProperty(y).GetValue(x).ToString() : "XXX"));
                                repMsg = replacedMsg;
                            }

                            CustomPrvmdl.Add(new CustomPreView
                            {
                                MobileNo = x.MobileNo,
                                ReplacedMsg = replacedMsg
                            });
                        }
                        else
                        {
                            var repMsg = message;
                            var replacedMsg = "";
                            foreach (var y in holders)
                            {
                                //replacedMsg = repMsg.Replace(System.Environment.NewLine, "").Replace("<$" + y + "$>", x.GetType().GetProperty(y).GetValue(x).ToString());
                                replacedMsg = repMsg.Replace(System.Environment.NewLine, "").Replace("<$" + y + "$>", (x.GetType().GetProperty(y).GetValue(x) != null ? x.GetType().GetProperty(y).GetValue(x).ToString() : "XXX"));
                                repMsg = replacedMsg;
                            }
                            ;

                            CustomPrvmdl.Add(new CustomPreView
                            {
                                MobileNo = x.MobileNo,
                                ReplacedMsg = replacedMsg
                            });
                        }

                    }
                }
                if (cmd == "gts")
                {
                    return Json(new
                    {
                        prMsg = CustomPrvmdl[0].ReplacedMsg

                    });
                }
                if (cmd != "" && cmd != "gts")
                {
                    return Json(new
                    {
                        PartialResult = RenderRazorViewToString(cmd, CustomPrvmdl)

                    });
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat(" OnCustomPreview :: Error :: {0}", ex.ToString());
            }
            return Json("");
        }

        private string DuplicateCount(string filePath, string columnName, string extension, string sheetName = "")
        {
            try
            {
                Logger.InfoFormat("Invalid Duplicate Started :: Start :: {0}", DateTime.Now);
                var allLines = System.IO.File.ReadLines(filePath);
                if (allLines.Any())
                {
                    var dt = (extension.Equals(".xls") || extension.Equals(".xlsx"))
                        ? IEnumerableExtension.BuildCsvToTable(filePath, sheetName)
                        : IEnumerableExtension.BuildCsvToTable(filePath);
                    var mobileColumn = dt.Rows.Cast<DataRow>().Where(s => !string.IsNullOrWhiteSpace(s[columnName].ToString())).Select(s => s[columnName].ToString()).ToList();
                    var totalCount = mobileColumn.Count;// -1;
                    var filterdCount = mobileColumn.Duplicates(false);
                    var DupfileName = string.Empty;
                    if (filterdCount.Any())
                    {
                        DupfileName = Path.Combine(Path.GetDirectoryName(filePath),
                           (Path.GetFileNameWithoutExtension(filePath) + "_Duplicates.csv"));

                        System.IO.File.WriteAllText(DupfileName, string.Join(Environment.NewLine, filterdCount));
                    }
                    var resultCount = mobileColumn.GroupBy(i => i)
                        .Where(g => g.Count() > 1).Select(s => s.First()).Count();
                    Logger.InfoFormat("Invalid Duplicate End :: End :: {0}", DateTime.Now);
                    var modifiedDt = dt.Rows.Cast<DataRow>().GroupBy(g => g[columnName]).Select(s => s.First());
                    var dt1 = modifiedDt.CopyToDataTable();

                    //System.IO.File.Delete(filePath);
                    var res = dt1.DataTableToCsvParse(Path.GetFileNameWithoutExtension(filePath), Path.GetDirectoryName(filePath), extension);
                    return totalCount + "|" + filterdCount.Count() + "|" + DupfileName;
                }
            }
            catch (Exception ex)
            {
                var err = ex.StackTrace;
                Logger.ErrorFormat("Error throuing while duplicate no. checking :: {0}", ex.StackTrace);
            }
            return string.Empty;
        }

        [PreventSpam]
        [HttpPost("IsUnicode")]
        public ActionResult IsUnicode(string sms)
        {
            Logger.InfoFormat("IsUnicode :: start :: {0}", sms);
            try
            {
                try
                {
                    sms = AESEncrytDecry.DecryptStringAES(sms);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("IsUnicode() :: Exception Error :" + ex.Message.ToString());
                    return Json(new { Invalid = true, Message = lz.InvalidInputParameters });
                }
                byte[] obytes = Encoding.Unicode.GetBytes(sms);
                int num2 = (obytes.Length - 1);
                int i = 1;
                bool result = false;
                while ((i <= num2))
                {
                    byte num3 = obytes[i];
                    if ((num3 != 3) && (num3 > 0))
                    {
                        result = true;
                    }
                    i = (i + 2);
                }

                return Json(result);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("IsUnicode:-{0} Error :- {1}", sms);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        [PreventSpam]
        [HttpPost("IsHavingUnseenJunk")]
        public ActionResult IsHavingUnseenJunk(string sms)
        {
            Logger.InfoFormat("IsHavingUnseenJunk :: start :: {0}", sms);
            try
            {
                try
                {
                    sms = AESEncrytDecry.DecryptStringAES(sms);
                    //  sms = sms;
                }
                catch (Exception ex)
                {
                    return Json(new { Invalid = true, Message = lz.InvalidInputParameters });
                }
                // bool result = false;
                int junklength = 0;
                foreach (char c in sms)
                {
                    int cdigit = (int)c;
                    if (c.ToString().Trim() == "" && cdigit != 32)
                    {
                        c.ToString().Replace(Convert.ToString(c), "");


                        junklength++;
                        //  return Json(junklength);
                    }
                    if (c == '\n')
                    {
                        junklength = 0;
                    }
                }
                return Json(junklength);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("IsHavingUnseenJunk  :-{0} Error :- {1}", sms);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        private List<MobileLengthValidationResponse> ValidateMobileNumbers(int senderid)
        {
            Logger.InfoFormat("ValidateMobileNumbers :: start :: {0}", senderid);
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
                Logger.ErrorFormat("ValidateMobileNumbers ::userId :-{0} Error :- {1}", senderid);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return null;
        }



        private DndNonDndNumbers DndNumberCheck1(DataTable dt1, string mobilecolumn)
        {
            Logger.InfoFormat("DndNumberCheck1 - start :: {0} :: Count :: {1}", DateTime.Now, dt1.Rows.Count);
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            dt2 = dt1.Rows.Cast<DataRow>().GroupBy(s => s[mobilecolumn]).Select(x => x.First()).CopyToDataTable();
            var ddt = dt2.Rows.Cast<DataRow>().Where(s => s[mobilecolumn].ToString().Trim().Length < 13).Select(x => x);

            if (ddt.Count() > 0)
                dt = ddt.CopyToDataTable();

            bool flag = false;
            DndNonDndNumbers dnnumber = new DndNonDndNumbers();
            if (dt.Rows.Count >= 200000)
            {
                try
                {
                    var list1 = dt.AsEnumerable().Select(s => s[mobilecolumn]).ToList();
                    int nSize = 200000;
                    for (int i = 0; i < list1.Count; i += nSize)
                    {
                        var list = list1.GetRange(i, Math.Min(nSize, list1.Count - i));

                        var mobilelistmob1 = string.Join(",", list);
                        var NoCheck1 = DndNumberCheck(mobilelistmob1);

                        if (!flag)
                        {
                            dnnumber = NoCheck1;
                            flag = true;
                        }
                        else
                        {
                            dnnumber.strNonDndNumbers.AddRange(NoCheck1.strNonDndNumbers);
                            dnnumber.strDndNumbers.AddRange(NoCheck1.strDndNumbers);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("DndNumberCheck ::userId :-{0} ", ex.Message);
                    ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }
            else if (dt.Rows.Count > 0)
            {
                var list1 = dt.AsEnumerable().Select(s => s[mobilecolumn]).ToList();
                var mobilelistmob = string.Join(",", list1);
                dnnumber = DndNumberCheck(mobilelistmob);
                flag = true;
            }
            var ddt2 = dt2.Rows.Cast<DataRow>().Where(s => s[mobilecolumn].ToString().Trim().Length > 12).Select(x => x);
            if (ddt2.Count() > 0)
            {
                dt2 = ddt2.CopyToDataTable();
                dt2.Columns[mobilecolumn].ColumnName = "NonDNDNumbers";
                dt2.AcceptChanges();

                var nondnd = dt2.ToList<strNonDndNumbers>();
                if (flag)
                    dnnumber.strNonDndNumbers.AddRange(nondnd);
                else
                {
                    var dnd = new DataTable();
                    dnd.Columns.Add("DNDNumbers", typeof(String));
                    dnnumber.strNonDndNumbers = nondnd;
                    dnnumber.strDndNumbers = dnd.ToList<strDndNumbers>();
                }
            }
            Logger.InfoFormat("DndNumberCheck1 - end :: {0} :: DNDCount :: {1}", DateTime.Now, dnnumber.strDndNumbers.Count);
            Logger.InfoFormat("DndNumberCheck1 - end :: {0} :: NonDNDCount :: {1}", DateTime.Now, dnnumber.strNonDndNumbers.Count);
            return dnnumber;
        }

        private DndNonDndNumbers DndNumberCheck(string str)
        {
            try
            {
                using (var ClientAccess = new AuthenticateSecurityClient())
                {
                    var response = ClientAccess.DndNumberCheck(str);

                    return response.Result;

                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("DndNumberCheck ::userId :-{0} Error :- {1}", str.Count(), ex.Message);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return null;
        }

        private bool IsValidCountryCode(string ccode, List<MobileLengthValidationResponse> dbcountries)
        {
            var isValid = false;
            var isvalnumb = _configuration["IsValidationEnable"]?.ToString();
            try
            {
                if (!Regex.IsMatch(ccode, @"^-?[0-9][0-9,\.]+$"))
                {
                    if (isvalnumb == "true")
                    {
                        if (dbcountries.Any())
                        {
                            var select1 = (from w in dbcountries where w.series != null select w).ToList();
                            var select = (from w in select1
                                          where w.CountryCode.ToString() == ccode.Substring(0, w.CountryCode.ToString().Length) &&
                                                w.TotalLength.Equals(ccode.Trim().Length) && w.series.Equals(ccode.Substring(w.CountryCode.ToString().Length, w.series.Length))
                                          select w).ToList();
                            if (!select.Any())
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Info("Fatal Error :: " + ex.ToString());
            }
            return isValid;
        }
        private string ValidateMobileNumbers(int senderid, string filePath, string columnName, string extension, int MessageType, string messagePlaceHolder, string sheetName = "", bool allowdupli = false)
        {
            var invaliedMobs = new List<DataRow>();
            var InvalidMobsCollection = new List<string>();
            var mobileNum = new List<DataRow>();
            bool IsMsgEmpty = false;
            var tcount = 0;
            try
            {
                var dt = new DataTable();
                var gextension = Path.GetExtension(filePath);
                if (extension.Equals(".xls") || extension.Equals(".xlsx"))
                {
                    dt =
                        IEnumerableExtension.BuildCsvToTable(
                            Path.Combine(Path.GetDirectoryName(filePath),
                                Path.GetFileNameWithoutExtension(filePath) + extension), sheetName);
                    dt = dt.Rows.Cast<DataRow>().Where(row => !Array.TrueForAll(row.ItemArray, value =>
                    { return value.ToString().Length == 0; })).CopyToDataTable();
                }
                else
                {
                    var allLines = System.IO.File.ReadLines(filePath);
                    if (allLines.Any())
                    {
                        dt = IEnumerableExtension.BuildCsvToTable(filePath);
                        if (dt == null) { dt = new DataTable(); }
                        else
                        {
                            dt = dt.Rows.Cast<DataRow>().Where(row => !Array.TrueForAll(row.ItemArray, value =>
                            { return value.ToString().Length == 0; })).CopyToDataTable();
                        }

                    }
                }
                if (!string.IsNullOrWhiteSpace(messagePlaceHolder))
                {
                    //Added by murty
                    messagePlaceHolder = messagePlaceHolder.Replace("<$", "").Replace("$>", "");
                    var emptyMessageCol = dt.Rows.Cast<DataRow>().Where(w => string.IsNullOrWhiteSpace(w[messagePlaceHolder].ToString())).ToList();
                    if (emptyMessageCol.Count == dt.Rows.Count)
                    {
                        emptyMessageCol.ForEach(f => dt.Rows.Remove(f));
                        dt.AcceptChanges();
                        IsMsgEmpty = dt.Rows.Count > 2 ? false : true;
                    }
                    //Added by murty
                }
                if (MessageType == 1)
                {
                    var columnsToRemove = new List<DataColumn>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.ColumnName.ToLower() != columnName.ToLower())
                            columnsToRemove.Add(col);
                    }

                    foreach (DataColumn colToRemove in columnsToRemove)
                    {
                        // Get the DataColumnCollection from a DataTable in a DataSet.
                        DataColumnCollection columns;
                        columns = dt.Columns;
                        if (columns.Contains(colToRemove.ColumnName))
                            columns.Remove(colToRemove);
                    }

                    dt.AcceptChanges();
                }


                dt = dt.Rows.Cast<DataRow>().Where(row => !Array.TrueForAll(row.ItemArray, value =>
                { return value.ToString().Length == 0; })).CopyToDataTable();

                var nullspaces = dt.Rows.Cast<DataRow>().Where(w => string.IsNullOrWhiteSpace(w[columnName].ToString())).ToList();
                nullspaces.ForEach(f => dt.Rows.Remove(f));
                dt.AsEnumerable().Where(row => row.ItemArray.All(field => field == null || field == DBNull.Value)).ToList().ForEach(row => row.Delete());

                dt.AcceptChanges();

                dt.AsEnumerable().Where(row => row.ItemArray.All(field => field != null || field != DBNull.Value)).ToList()
                    .Select(r => r[columnName] = r[columnName].ToString().Trim());

                if (allowdupli)
                    dt = dt.Rows.Cast<DataRow>().Select(s => s).CopyToDataTable();
                else
                //Need to remove the duplicates...
                {
                    var dtRows = dt.Rows.Cast<DataRow>().GroupBy(s => s[columnName])
                            .Select(s => s.First());
                    if (dtRows.Any())
                        dt = dtRows.CopyToDataTable();
                }
                tcount = dt.Rows.Count;
                var result = ValidateMobileNumbers(senderid);
                var isvalnumb = _configuration["IsValidationEnable"]?.ToString();
                if (result != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        Logger.InfoFormat("Invalid validation Started :: Start :: {0}", DateTime.Now);

                        var emptyMobileNo =
                            dt.Rows.Cast<DataRow>()
                                .Where(w => string.IsNullOrWhiteSpace(w[columnName].ToString()))
                                .ToList();
                        if (isvalnumb == "false")
                        {
                            if (emptyMobileNo.Any())
                            {
                                InvalidMobsCollection.AddRange(emptyMobileNo.Select(s => s[columnName].ToString()));
                                emptyMobileNo.ForEach(f => dt.Rows.Remove(f));
                            }
                        }
                        var notnumbers =
                            dt.Rows.Cast<DataRow>()
                                .Where(
                                    w =>
                                        !string.IsNullOrWhiteSpace(
                                            IEnumerableExtension.IsSpecialCharHaving(w[columnName].ToString())))
                                .ToList();
                        if (isvalnumb == "true")
                        {
                            if (notnumbers.Any())
                            {

                                InvalidMobsCollection.AddRange(notnumbers.Select(s => s[columnName].ToString()));
                                notnumbers.ForEach(f => dt.Rows.Remove(f));
                            }
                        }
                        var leadingzeros =
                            dt.Rows.Cast<DataRow>().Where(w => w[columnName].ToString().StartsWith("0")).ToList();

                        if (isvalnumb == "true")
                        {
                            if (leadingzeros.Any())
                            {

                                InvalidMobsCollection.AddRange(leadingzeros.Select(s => s[columnName].ToString()));
                                leadingzeros.ForEach(f => dt.Rows.Remove(f));
                            }
                        }
                        Logger.InfoFormat("Invalid three validations Ended :: End :: {0} :: invlaid count :: {1}",
                            DateTime.Now, invaliedMobs.Count);

                        var dbCountryCodes = result.Select(s => s.CountryCode.ToString().Length);

                        var ccodelengthvalidations = (from row in dt.Rows.Cast<DataRow>()
                                                      let ccodelength =
                                                          dbCountryCodes.Where(w => row[columnName].ToString().Length > 3)
                                                              .Select<int, string>(a => row[columnName].ToString().Substring(0, a))
                                                      where !ccodelength.Any()
                                                      select row).ToList();
                        if (isvalnumb == "true")
                        {
                            if (ccodelengthvalidations.Any())
                            {

                                InvalidMobsCollection.AddRange(ccodelengthvalidations.Select(s => s[columnName].ToString()).ToList());
                                ccodelengthvalidations.ForEach(f => dt.Rows.Remove(f));
                            }
                        }



                        Logger.InfoFormat("Invalid two arrays validations Ended :: End :: {0} :: invlaid count :: {1}",
                            DateTime.Now, invaliedMobs.Count);

                        dt.AsEnumerable().Where(row => row.Field<object>(columnName).ToString().Trim().Length > 0)
                        .Select(b => b[columnName] = b[columnName].ToString().Trim())
                        .ToList();

                        var ccodevalidate =
                            dt.Rows.Cast<DataRow>()
                                .Where(row => IsValidCountryCode(row[columnName].ToString(), result))
                                .ToList();

                        if (isvalnumb == "true")
                        {
                            if (ccodevalidate.Any())
                            {
                                var ccodestrings = ccodevalidate.Select(s => s[columnName].ToString()).ToList();

                                if (ccodestrings.Any())
                                    InvalidMobsCollection.AddRange(ccodestrings);
                                ccodevalidate.ForEach(f => dt.Rows.Remove(f));
                            }
                        }

                    }
                }
                var invalidMobfileName = string.Empty;
                var dupMobs = new List<string>();
                var DupfileName = string.Empty;
                if (InvalidMobsCollection.Any())
                {
                    invalidMobfileName = Path.Combine(Path.GetDirectoryName(filePath),
                        (Path.GetFileNameWithoutExtension(filePath) + "_Invalid.csv"));
                    System.IO.File.WriteAllText(invalidMobfileName, string.Join(Environment.NewLine, InvalidMobsCollection));
                }
                var dCount = 0;
                if (dt.Rows.Count > 0)
                {
                    //Added by murty
                    if (!string.IsNullOrWhiteSpace(messagePlaceHolder))
                    {
                        messagePlaceHolder = messagePlaceHolder.Replace("<$", "").Replace("$>", "");
                    }
                    if (allowdupli == false)
                    {
                        var dupValidationMobs =
                        dt.Rows.Cast<DataRow>().Select(s => s[columnName].ToString()).ToList();
                        var dupDt = IEnumerableExtension.BuildTableFromLinesOfList(dupMobs, columnName);
                        if (!allowdupli)
                        {
                            var resp = string.Empty;
                            DupfileName = Path.Combine(Path.GetDirectoryName(filePath),
                                   (Path.GetFileNameWithoutExtension(filePath) + "_Duplicates.csv"));
                            if (dupDt.Rows.Count > 0)
                            {
                                resp = extension.Equals(".xls") || extension.Equals(".xlsx")
                                    ? dupDt
                                        .DataTableToExceParse(Path.GetFileNameWithoutExtension(DupfileName),
                                            Path.GetDirectoryName(DupfileName), extension)
                                    : dupDt
                                        .DataTableToCsvParse(Path.GetFileNameWithoutExtension(DupfileName),
                                            Path.GetDirectoryName(DupfileName), extension);

                                var notdups = dt.Rows.Cast<DataRow>().GroupBy(g => g[columnName].ToString()).Select(s => s.First()).ToList();

                                if (notdups.Any())
                                {
                                    dt = new DataTable();

                                    dt = notdups.CopyToDataTable();
                                }
                            }
                        }
                        dCount = dupMobs.Count;
                        var dt1 = dt.Copy();
                        if (dt1.Rows.Count > 0)
                        {
                            var Dictionary = new Dictionary<string, string>();
                            foreach (var cc in result.GroupBy(g => g.Name).Select(s => s.First()).ToList())
                            {
                                var countryCodesByName = from row in dt1.Rows.Cast<DataRow>()
                                                         let rowcountrycode =
                                                             row[columnName].ToString().Substring(0, cc.CountryCode.ToString().Length)
                                                         where rowcountrycode == cc.CountryCode.ToString()
                                                         select row;
                                if (isvalnumb == "true")
                                {
                                    if (countryCodesByName.Any())
                                    {
                                        Dictionary.Add(cc.Name, countryCodesByName.Count().ToString());
                                    }
                                }
                            }
                            if (Dictionary.Any())
                            {
                                //HttpContext.Session["CountryWiseCollection"] = Dictionary;
                                HttpContext.Session.SetString("CountryWiseCollection", JsonSerializer.Serialize(Dictionary));
                            }
                        }
                    }
                    Logger.InfoFormat("Invalid validation completed :: End :: {0}", DateTime.Now);
                    //Modified by murty - added Message Empty Flag(IsMsgEmpty)
                    return InvalidMobsCollection.Count() + "|" +
                           (Path.GetDirectoryName(filePath) + "\\" + Path.GetFileNameWithoutExtension(invalidMobfileName) + ".csv") + "|" + tcount + "|" + dCount + "|" + DupfileName + "|" + IsMsgEmpty;

                }
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                Logger.ErrorFormat("Error throuing while invalid no. checking :: {0}", ex.StackTrace);
            }
            return invaliedMobs.Count() + "" + "" + "" + "";
        }

        [HttpPost]
        public ActionResult BackCampaign(string filepath)
        {
            Logger.InfoFormat("BackCampaign :: start :: {0}", filepath);
            try
            {
                //filepath = Session["importfilepath"] != null ? Session["importfilepath"].ToString() : string.Empty;
                filepath = HttpContext.Session.GetString("importfilepath");
                if (!string.IsNullOrWhiteSpace(filepath))
                {
                    filepath = filepath.TrimEnd(',');
                }
                if (!string.IsNullOrWhiteSpace(filepath))
                {
                    filepath = filepath.Replace("^", "\\");
                    if (System.IO.File.Exists(filepath))
                    {
                        var extention = Path.GetExtension(filepath);
                        System.IO.File.Delete(filepath);
                        var FileName = Path.GetFileNameWithoutExtension(filepath);
                        var DirectoryName = Path.GetDirectoryName(filepath);
                        System.IO.File.Delete(Path.Combine(DirectoryName, FileName + "_Duplicates.csv"));
                        System.IO.File.Delete(Path.Combine(DirectoryName, FileName + "_Invalid.csv"));
                        System.IO.File.Delete(Path.Combine(DirectoryName, FileName + ".csv"));
                        System.IO.File.Move(Path.Combine(DirectoryName, FileName + "_client.csv"), filepath);
                    }
                }
                return Json("");
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("BackCampaign  :-{0} Error :- {1}", filepath);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult GetGroupContacts(string gids)
        {
            Logger.InfoFormat("GetGroupContacts :: start :: {0}", gids);
            try
            {
                var sItem = SessionExtensions.GetItem<GroupContactsMain>(HttpContext.Session);
                if (sItem != null)
                {
                    sItem = null;
                }
                using (var clientAcces = new AuthenticateSecurityClient())
                {
                    var response =
                        clientAcces.GetGroupByContacts(new ReUsableRequest
                        {
                            groupname = gids
                        });
                    if (response != null)
                    {
                        sItem = response.Result;

                        HttpContext.Session.AddItem<GroupContactsMain>(sItem);
                        if (sItem.GroupswithContacts.Count == 0)
                        {
                            return Json(new { Isvalid = false, Message = "No Contacts in Selected Group / Uploaded File", Nocontacts = true });
                        }
                        return Json(new { IsValid = true, Message = "" });
                    }

                    return Json("");
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("GetGroupContacts  :-{0} Error :- {1}", gids);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        private int GetCustomeCampTotalCount(string message, String filePath, int langId, string ruleid, string sheetname,
            string columnname, int allowDuplicates)//InsertBulkSMSOnRequest model)
        {
            try
            {
                var messageTemplates = message.StringBetween("<$", "$>");
                var dt = IEnumerableExtension.BuildCsvToTable(filePath, sheetname);
                dt = dt.Rows.Cast<DataRow>().Where(row => !Array.TrueForAll(row.ItemArray, value =>
                { return value.ToString().Length == 0; })).CopyToDataTable();

                var nullspaces = dt.Rows.Cast<DataRow>().Where(w => string.IsNullOrWhiteSpace(w[columnname].ToString())).ToList();
                nullspaces.ForEach(f => dt.Rows.Remove(f));

                var tcount = 0;
                List<DataRow> rows = new List<DataRow>();
                if (allowDuplicates == 1)
                    rows = dt.Rows.Cast<DataRow>().Select(s => s).ToList();
                else
                //Need to remove the duplicates...
                {
                    var dtRows = dt.Rows.Cast<DataRow>().GroupBy(s => s[columnname])
                            .Select(s => s.First());
                    if (dtRows.Any())
                        rows = dtRows.Cast<DataRow>().Select(s => s).ToList();
                    else
                        rows = dt.Rows.Cast<DataRow>().Select(s => s).ToList();
                }
                if (messageTemplates.Count == dt.Columns.Count || messageTemplates.Count < dt.Columns.Count && Convert.ToInt32(ruleid) > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        var msg = message;
                        foreach (var item in messageTemplates)
                        {
                            msg = msg.Replace("<$" + item + "$>", (row[item] != null && row[item].ToString() != "") ? row[item].ToString() : " ");
                        }
                        bool langcheck = Regex.IsMatch(msg, @"^[\u0000-\u007F]+$");
                        if (langcheck == false)
                        {
                            langId = 2;
                        }
                        else
                        {
                            langId = 1;
                        }
                        tcount = (msg.CreditsCountValidation(langId) + tcount);
                    }
                    if (tcount == 0 && Convert.ToInt32(ruleid) > 0)
                    {
                        TempData["LangId"] = 0;
                        return -5;
                    }
                    TempData["LangId"] = langId;
                    return tcount;
                }
                if (Convert.ToInt32(ruleid) == 0)
                {
                    foreach (DataRow row in rows)
                    {
                        var msg = message;
                        foreach (var item in messageTemplates)
                        {
                            msg = msg.Replace("<$" + item + "$>", (row[item] != null && row[item].ToString() != "") ? (row[item].ToString() == "" ? " " : row[item].ToString()) : " ");
                        }
                        bool langcheck = Regex.IsMatch(msg, @"^[\u0000-\u007F]+$");
                        if (langcheck == false)
                        {
                            langId = 2;
                        }
                        else
                        {
                            langId = 1;
                        }
                        tcount = (msg.CreditsCountValidation(langId) + tcount);
                    }
                    if (tcount == 0 && Convert.ToInt32(ruleid) == 0)
                    {
                        TempData["LangId"] = 0;
                        return -6;
                    }
                    TempData["LangId"] = langId;
                    return tcount;
                }
                else
                {
                    if (Convert.ToInt32(ruleid) > 0)
                    {
                        TempData["LangId"] = 0;
                        return -5;
                    }
                    else
                    {
                        TempData["LangId"] = 0;
                        return -6;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("getcustomecampTotalCount() :: Exception Error :" + ex.Message.ToString());
                if (Convert.ToInt32(ruleid) > 0)
                {
                    TempData["LangId"] = 0;
                    return -5;
                }
                else
                {
                    TempData["LangId"] = 0;
                    return -6;
                }
            }

            //  return 0;
        }

        [PreventSpam]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoadSubmitCamp(InsertBulkSMSOnRequest LocalModel, string cmd = "")
        {
            string CampaignFilesToBeSaved = Path.Combine(_configuration["tempPath"]?.ToString(), DateTime.Now.ToString("MMMyyyy"), "QuickSMS");
            if (!Directory.Exists(CampaignFilesToBeSaved))
            {
                Directory.CreateDirectory(CampaignFilesToBeSaved);
            }
            Logger.InfoFormat("LoadSubmitCamp :: start :: {0}", LocalModel);
            try
            {
                try
                {
                    LocalModel.CampID = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.CampID[0]));
                    LocalModel.Name = AESEncrytDecry.DecryptStringAES(LocalModel.Name);
                    LocalModel.SenderID = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.SenderID[0]));
                    LocalModel.Sender = AESEncrytDecry.DecryptStringAES(LocalModel.Sender);
                    LocalModel.CampaignTypeID = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.CampaignTypeID[0]));
                    LocalModel.CampaignType = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.CampaignType)) == 1 ? "Promotional" : "Transactional";
                    LocalModel.MobileField = AESEncrytDecry.DecryptStringAES(LocalModel.MobileField);
                    LocalModel.Language = AESEncrytDecry.DecryptStringAES(LocalModel.Language);
                    LocalModel.LangID = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.LangID[0]));
                    LocalModel.Message = AESEncrytDecry.DecryptStringAES(LocalModel.Message);
                    LocalModel.DLR = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.DLR[0]));
                    LocalModel.AllowDuplicates = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.AllowDuplicates[0]));
                    LocalModel.TotalScheduleString = AESEncrytDecry.DecryptStringAES(LocalModel.TotalScheduleString);
                    LocalModel.ScheduledType = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.ScheduledType[0]));
                    LocalModel.CharCount = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.CharCount[0]));
                    LocalModel.CreditsUsed = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.CreditsUsed[0]));
                    LocalModel.XMLSchedule = AESEncrytDecry.DecryptStringAES(LocalModel.XMLSchedule);
                    LocalModel.Criteria = AESEncrytDecry.DecryptStringAES(LocalModel.Criteria);
                    LocalModel.PlaceHolders = AESEncrytDecry.DecryptStringAES(LocalModel.PlaceHolders);
                    LocalModel.MessageType = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.MessageType[0]));
                    LocalModel.Status = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.Status[0]));
                    LocalModel.Type = AESEncrytDecry.DecryptStringAES(LocalModel.Type);
                    LocalModel.ImportFileName = AESEncrytDecry.DecryptStringAES(LocalModel.ImportFileName);
                    LocalModel.ActualFileName = AESEncrytDecry.DecryptStringAES(LocalModel.ActualFileName);
                    LocalModel.SheetName = AESEncrytDecry.DecryptStringAES(LocalModel.SheetName);
                    LocalModel.RecipientsType = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.RecipientsType[0]));
                    LocalModel.RuleId = AESEncrytDecry.DecryptStringAES(LocalModel.RuleId);
                    LocalModel.GroupIds = AESEncrytDecry.DecryptStringAES(LocalModel.GroupIds);
                    LocalModel.SchStatus = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.SchStatus[0]));
                    LocalModel.GroupOldFilePath = AESEncrytDecry.DecryptStringAES(LocalModel.GroupOldFilePath);
                    LocalModel.BeforeEditSchTime = AESEncrytDecry.DecryptStringAES(LocalModel.BeforeEditSchTime);
                    LocalModel.MessageField = AESEncrytDecry.DecryptStringAES(LocalModel.MessageField);
                    //Added by murty for bulk campaign
                    if (LocalModel.MessageType == 1)
                        LocalModel.AllowDuplicates = 1;
                    //Added by murty for bulk campaign
                    if (LocalModel.GroupIds != string.Empty)
                    {

                    }
                    if (LocalModel.CampID > 0)
                    {
                        var ValidPath = LocalModel.GroupOldFilePath.Split('_');
                        LocalModel.GroupOldFilePath = "";
                        if (ValidPath.Length == 3)
                        {
                            if (ValidPath[2] == "3")
                                LocalModel.GroupOldFilePath = ValidPath.Length > 0 ? _configuration["tempPath"]?.ToString() +
                                                "\\Schedule" + "\\" + ValidPath[0] : "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("LoadSubmitCamp() :: Exception Error :" + ex.Message.ToString());
                    return Json(new { Invalid = true, Message = lz.InvalidInputParameters });
                }
                if (Convert.ToInt32(LocalModel.CampID) == 0)
                {
                    var message = ValidateCampaignName(LocalModel.Name);
                    if (!string.IsNullOrWhiteSpace(message))
                    {
                        return Json(new
                        {
                            IsValid = false,
                            Message = message
                        });
                    }
                }
                var selAction =
               UserActions.FirstOrDefault(
                   w => w.ActionName.Equals("Index") &&
                       w.ControllerName.Equals(ControllerName, StringComparison.OrdinalIgnoreCase));
                var extendedUser = SessionExtensions.GetItem<Core.Models.Extensions.CustomeUser>(HttpContext.Session);
                int PreProcessInterval = 0; //extendedUser.LogOnRespons.PreProcessorInterval;
                int vldcnt = 100; if (vldcnt == 100) { vldcnt = 100; }
                string XMLSchedule = "";
                DateTime ScheduleTime = DateTime.Now;
                DateTime PreProcessTime = DateTime.Now;
                var tCount = 0; var DupeCount = 0;
                var dupPath = string.Empty;

                if (Convert.ToInt32(LocalModel.ScheduledType) == 2)
                {
                    if (Convert.ToInt32(LocalModel.CampID) == 0)
                    {
                        string starttime = LocalModel.TotalScheduleString.Split(',')[1].Split(' ')[6].Substring(0, 5);
                        var StartDate = LocalModel.TotalScheduleString.Split(',')[1].Split(' ');
                        string SchDate = StartDate[3] + "-" + StartDate[2] + "-" + StartDate[4];
                        ScheduleTime = Convert.ToDateTime(SchDate + " " + starttime + ":" + DateTime.Now.Second);
                        PreProcessTime = Convert.ToDateTime(SchDate + " " + starttime + ":" + DateTime.Now.Second).AddMinutes(-PreProcessInterval);
                        XMLSchedule = "<XML><RECURRENCE SCHEDULE='5' STARTTIME='" + starttime + "' INTERVAL='" + PreProcessInterval + "' STARTDATE='" + SchDate + "' ENDON='1' ENDONDATE='' SENDALERTON='' EVERYNWEEK='0' WEEKDAYS='' DAYS='' MONTHS='' /></XML>";
                        if (ScheduleTime < DateTime.Now)
                        {
                            return Json(new
                            {
                                IsValid = false,
                                Message = lz.ScheduletimeshouldnotbeLessthancurrentDateTime
                            });
                        }
                    }
                    else
                    {
                        string starttime = LocalModel.TotalScheduleString.Split(',')[1].Split(' ')[6].Substring(0, 5);
                        var StartDate = LocalModel.TotalScheduleString.Split(',')[1].Split(' ');
                        string SchDate = StartDate[3] + "-" + StartDate[2] + "-" + StartDate[4];


                        ScheduleTime = Convert.ToDateTime(SchDate + " " + starttime + ":" + DateTime.Now.Second);
                        //ScheduleTime = Convert.ToDateTime(SchDate + " " + LocalModel.BeforeEditSchTime + ":" + DateTime.Now.Second);

                        //PreProcessTime = Convert.ToDateTime(SchDate + " " + starttime + ":" + DateTime.Now.Second).AddMinutes(-10);
                        PreProcessTime = Convert.ToDateTime(LocalModel.BeforeEditSchTime + ":" + DateTime.Now.Second).AddMinutes(-10);

                        XMLSchedule = "<XML><RECURRENCE SCHEDULE='5' STARTTIME='" + starttime + "' INTERVAL='" + PreProcessInterval + "' STARTDATE='" + SchDate + "' ENDON='1' ENDONDATE='' SENDALERTON='' EVERYNWEEK='0' WEEKDAYS='' DAYS='' MONTHS='' /></XML>";
                        if (PreProcessTime < DateTime.Now)
                        {
                            return Json(new
                            {
                                IsValid = false,
                                Message = "Cannot submit the campaign as preprocessor time has started already"
                            });
                        }
                    }
                }
                var invlaidmobcount = 0;
                var invlaidmobpath = string.Empty;
                var dupPathVerify = dupPath;
                Dictionary<string, string> Dictionary = new Dictionary<string, string>();
                //Added By murty
                string msgHolderName = LocalModel.MessageField;
                //Added By murty
                if (string.IsNullOrWhiteSpace(LocalModel.GroupIds))
                {
                    if (LocalModel.CampID == 0)
                    {
                        LocalModel.ImportFileName = _configuration["tempPath"]?.ToString() + LocalModel.ImportFileName;
                    }
                    if (LocalModel.CampID != 0)
                    {
                        bool Check = LocalModel.ImportFileName.Contains("DocumentTemp");
                        if (Check == false)
                        {
                            LocalModel.ImportFileName = _configuration["tempPath"]?.ToString() + LocalModel.ImportFileName;
                        }
                        //LocalModel.ImportFileName = _configuration["tempPath"] + LocalModel.ImportFileName;
                    }
                    var dir = Path.GetDirectoryName(LocalModel.ImportFileName);
                    var orginalfilename = Path.GetFileNameWithoutExtension(LocalModel.ImportFileName);
                    if (!System.IO.File.Exists(Path.Combine(dir, (orginalfilename + "_client.csv"))))
                    {
                        System.IO.File.Copy(LocalModel.ImportFileName, Path.Combine(dir, (orginalfilename + "_client.csv")));
                    }
                    var invalidMobileCollection = ValidateMobileNumbers(Convert.ToInt32(LocalModel.SenderID), LocalModel.ImportFileName,
                       LocalModel.MobileField, Path.GetExtension(LocalModel.ImportFileName), LocalModel.MessageType, msgHolderName,
                           (Path.GetExtension(LocalModel.ImportFileName) != ".csv") ? LocalModel.SheetName : "", (Convert.ToInt32(LocalModel.AllowDuplicates) == 1)
                       );

                    if (!string.IsNullOrWhiteSpace(invalidMobileCollection))
                    {
                        var fExt = Path.GetExtension(LocalModel.ImportFileName) == ".csv" || Path.GetExtension(LocalModel.ImportFileName) == ".txt";
                        var fExt1 = Path.GetExtension(LocalModel.ImportFileName) == ".xlsx" || Path.GetExtension(LocalModel.ImportFileName) == ".xls";
                        if (invalidMobileCollection == "0" && fExt)
                        {
                            return Json(new
                            {
                                IsValid = false,
                                Message = "invalidnumbersinfile"
                            });
                        }

                        if (invalidMobileCollection == "0" && fExt1)
                        {
                            return Json(new
                            {
                                IsValid = false,
                                Message = "Selected column doesn't contain valid data"
                            });
                        }
                        var invalidmobcol = invalidMobileCollection.Split('|');
                        invlaidmobcount = !string.IsNullOrWhiteSpace(invalidmobcol[0])
                            ? Convert.ToInt32(invalidmobcol[0])
                            : 0;
                        invlaidmobpath = _configuration["Filterlogpath"]?.ToString() + "//" +
                        DateTime.Now.ToString("MMMyyyy") + "//" + Path.GetFileName(invalidmobcol[1]);
                        tCount = tCount == 0 ? Convert.ToInt32(invalidmobcol[2]) : tCount;
                        DupeCount = Convert.ToInt32(invalidmobcol[3]);
                        dupPath = _configuration["Filterlogpath"]?.ToString() + "//" +
                        DateTime.Now.ToString("MMMyyyy") + "//" + Path.GetFileName(invalidmobcol[4]);

                    }
                    var tempImportFileName = LocalModel.ImportFileName;

                    HttpContext.Session.SetString("importfilepath", tempImportFileName.Replace(@"\", "^"));
                    var ext = Path.GetExtension(LocalModel.ImportFileName);
                    var dt =
                        IEnumerableExtension.BuildCsvToTable(Path.Combine(Path.GetDirectoryName(LocalModel.ImportFileName),
                        Path.GetFileNameWithoutExtension(LocalModel.ImportFileName) + ((ext.Equals(".csv") || ext.Equals(".txt")) ? ".csv" : ext)), LocalModel.SheetName);

                    if (dt.Rows.Count > 0)
                    {
                        var top5rows =
                        dt.Rows.Cast<DataRow>().Where(s => !string.IsNullOrWhiteSpace(s[LocalModel.MobileField].ToString())).Select(s => s[LocalModel.MobileField].ToString()).Take(5).ToList();
                        if (top5rows.Any())
                        {
                            var valNubers =
                                top5rows.Select(s => IEnumerableExtension.IsSpecialCharHaving(s)).Where(w => !string.IsNullOrWhiteSpace(w))
                                    .ToList();
                            if (valNubers.Count == top5rows.Count)
                            {
                                if (cmd != "")
                                {
                                    return Json(new
                                    {
                                        IsValid = false,
                                        Message = lz.Invalidmobilefield
                                    });
                                }
                            }
                        }
                    }
                    string? jsonString = HttpContext.Session.GetString("CountryWiseCollection");
                    if (!string.IsNullOrEmpty(jsonString))
                    {
                        Dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);
                    }

                    var isExisted = false;
                    if (!string.IsNullOrWhiteSpace(dupPathVerify))
                    {
                        dupPathVerify = dupPathVerify.Replace(_configuration["Filterlogpath"]?.ToString(), _configuration["tempPath"]?.ToString());
                        if (System.IO.File.Exists(dupPathVerify))
                        {
                            isExisted = true;
                        }
                        if (!isExisted)
                        {

                            if (System.IO.File.Exists(dupPathVerify))
                            {
                                isExisted = true;
                            }
                            if (!isExisted)
                            {
                                dupPathVerify = dupPathVerify.Replace(".csv", ".xlsx");
                                if (System.IO.File.Exists(dupPathVerify))
                                {
                                    isExisted = true;
                                }
                                if (!isExisted)
                                {
                                    dupPathVerify = dupPath;
                                    dupPathVerify = dupPathVerify.Replace(_configuration["Filterlogpath"]?.ToString(), _configuration["tempPath"]?.ToString()).Replace(".csv", ".xls");
                                    if (System.IO.File.Exists(dupPathVerify))
                                    {
                                        isExisted = true;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    var sItems = HttpContext.Session.GetItem<GroupContactsMain>();
                    var SchildCount = new List<GroupContacts>();



                    foreach (var item in sItems.GroupswithContacts)
                    {
                        //if(item.GroupContacts[])
                        SchildCount.AddRange(item.GroupContacts);
                    }
                    var groups = LocalModel.GroupIds;
                    var groupname = extendedUser.LogOnRespons.UserName + DateTime.Now.Ticks;
                    var MainDirectory = _configuration["tempPath"]?.ToString();
                    var currentMonthYear = Path.Combine(DateTime.Now.ToString("MMMyyyy"), "Groups");
                    if (!Directory.Exists(Path.Combine(MainDirectory, currentMonthYear)))
                    {
                        Directory.CreateDirectory(Path.Combine(MainDirectory, currentMonthYear));
                    }
                    var filepath = Path.Combine(MainDirectory, currentMonthYear) + "\\" + groupname;
                    try
                    {
                        IEnumerableExtension.WriteExcelFileFromDataTable(Path.Combine(filepath + ".xls"), IEnumerableExtension.ConvertToDatatable(SchildCount)); //.DataTableToCsvParse(fileName, filepath);
                        LocalModel.ImportFileName = Path.Combine(filepath + ".xls");
                    }
                    catch (Exception ex)
                    {
                        var err = ex.StackTrace;
                    }
                    LocalModel.ImportFileName = BuildCsvGroup(filepath + "_" + DateTime.Now.Ticks, SchildCount); //Commented for 18-09-2024
                    var invalidMobileCollection = ValidateMobileNumbers(Convert.ToInt32(LocalModel.SenderID), LocalModel.ImportFileName,
                       LocalModel.MobileField, Path.GetExtension(LocalModel.ImportFileName), LocalModel.MessageType, msgHolderName,
                           (Path.GetExtension(LocalModel.ImportFileName) != ".csv") ? LocalModel.SheetName : "",
                           (Convert.ToInt32(LocalModel.AllowDuplicates) == 1)
                       );

                    if (!string.IsNullOrWhiteSpace(invalidMobileCollection))
                    {
                        var fExt = Path.GetExtension(LocalModel.ImportFileName) == ".csv" || Path.GetExtension(LocalModel.ImportFileName) == ".txt";
                        if (invalidMobileCollection == "0" && fExt)
                        {
                            return Json(new
                            {
                                IsValid = false,
                                Message = "invalidnumbersinfile"
                            });
                        }
                        var invalidmobcol = invalidMobileCollection.Split('|');
                        invlaidmobcount = !string.IsNullOrWhiteSpace(invalidmobcol[0])
                            ? Convert.ToInt32(invalidmobcol[0])
                            : 0;
                        invlaidmobpath = _configuration["Filterlogpath"]?.ToString() + "//" +
                            DateTime.Now.ToString("MMMyyyy") + "//" + Path.GetFileName(invalidmobcol[1]);
                        tCount = tCount == 0 ? Convert.ToInt32(invalidmobcol[2]) : tCount;
                        DupeCount = Convert.ToInt32(invalidmobcol[3]);
                        dupPath = _configuration["Filterlogpath"]?.ToString() + "//" +
                            DateTime.Now.ToString("MMMyyyy") + "//" + Path.GetFileName(invalidmobcol[4]);
                    }

                }
                //LocalModel.Message = LocalModel.Message.Replace(@"\", "^");
                var isvalnumb = _configuration["IsValidationEnable"]?.ToString();
                var model = new InsertBulkSMSOnRequest
                {
                    CustomerID = extendedUser.LogOnRespons.CustomerId,
                    CampID = LocalModel.CampID,
                    Name = LocalModel.Name,
                    SenderID = LocalModel.SenderID,
                    LangID = LocalModel.LangID,
                    Language = LocalModel.Language,
                    CampaignTypeID = LocalModel.CampaignTypeID,
                    CampaignType = LocalModel.CampaignType,

                    Message = LocalModel.Message.Trim(),
                    CharCount = LocalModel.CharCount,
                    CreditsUsed = LocalModel.CreditsUsed,
                    ScheduledType = LocalModel.ScheduledType,
                    TotalScheduleString = LocalModel.TotalScheduleString,
                    XMLSchedule = XMLSchedule,
                    Criteria = LocalModel.Criteria,
                    PlaceHolders = LocalModel.PlaceHolders,
                    DLR = LocalModel.DLR,
                    AllowDuplicates = LocalModel.AllowDuplicates,
                    MessageType = LocalModel.MessageType,
                    Status = LocalModel.Status,
                    CreatedBy = extendedUser.LogOnRespons.Id,
                    IpAddress = LocalModel.IpAddress,
                    Sender = LocalModel.Sender,
                    Type = LocalModel.Type,
                    CurrentStatus = ExtendedUser.LogOnRespons.RoleId == 1 ? 1 : ((selAction != null && selAction.IsCheckerRequired) ? 2 : 1),
                    ImportFileName = LocalModel.ImportFileName,
                    GroupOldFilePath = LocalModel.GroupOldFilePath,
                    ActualFileName = LocalModel.ActualFileName,
                    SheetName = LocalModel.SheetName,
                    ValidCount = tCount - (invlaidmobcount + DupeCount),//100,
                    InValidCount = invlaidmobcount,//10,
                    DuplicateCount = DupeCount,//5,
                    // TotalCount = tCount,//115,
                    TotalCount = tCount - (invlaidmobcount + DupeCount),
                    ProcessedCount = tCount * LocalModel.CreditsUsed - (invlaidmobcount + DupeCount),//111,//Totalcount - (Invalid count + if(dupcheck) Duplicates)
                    DuplicatePath = dupPathVerify.Replace(_configuration["tempPath"]?.ToString(), _configuration["Filterlogpath"]?.ToString()),
                    InvalidMobPath = invlaidmobpath,
                    RecipientsType = LocalModel.RecipientsType,
                    MobileField = LocalModel.MobileField,
                    GroupIds = LocalModel.GroupIds,
                    RuleId = LocalModel.RuleId,
                    TempTableName = LocalModel.TempTableName,
                    Remarks = LocalModel.Remarks,
                    IsDone = LocalModel.IsDone,
                    Schedule = ScheduleTime,
                    PreprocessTime = PreProcessTime,
                    IsProcess = LocalModel.IsProcess,
                    PreProcessStatus = LocalModel.PreProcessStatus,
                    SchStatus = LocalModel.SchStatus,
                    Stageids = LocalModel.Stageids,
                    //  TotalCreditsReq = isvalnumb == "true" ? (Dictionary.Aggregate(0, (current, item) => current + Convert.ToInt32(item.Value)) * LocalModel.CreditsUsed) : LocalModel.CreditsUsed,  //tCount - (invlaidmobcount + DupeCount),//100,
                    // TotalCreditsReq = (Dictionary.Aggregate(0, (current, item) => current + Convert.ToInt32(item.Value)) * LocalModel.CreditsUsed) ,  //tCount - (invlaidmobcount + DupeCount),//100,
                    TotalCreditsReq = (Convert.ToInt32(LocalModel.MessageType) == 2) ? GetCustomeCampTotalCount(LocalModel.Message.Trim(), LocalModel.ImportFileName, Convert.ToInt32(LocalModel.LangID), LocalModel.RuleId, LocalModel.SheetName, LocalModel.MobileField, Convert.ToInt32(LocalModel.AllowDuplicates)) - (LocalModel.CreditsUsed * invlaidmobcount) : Convert.ToInt32(LocalModel.CreditsUsed) * (tCount - invlaidmobcount), //changes done on sep3
                    // TotalCreditsReq = (Convert.ToInt32(LocalModel.MessageType) == 2) ? Convert.ToInt32(LocalModel.CreditsUsed) * tCount - invlaidmobcount : Convert.ToInt32(LocalModel.CreditsUsed) * tCount - invlaidmobcount,
                    CountryWiseCnt = Dictionary,
                    UserName = ExtendedUser.LogOnRespons.UserName,
                    UserIp = extendedUser.LogOnRespons.GetIPAddress,
                    //Language = (LocalModel.LangID == 1 && Lang == "2" && LocalModel.MessageType == 2) ? "Arabic" : "English"
                };

                if (LocalModel.MessageType == 2)
                {
                    string Lang = TempData["LangId"].ToString();
                    if ((LocalModel.LangID == 1 || LocalModel.LangID == 2) && Lang == "2")
                    {
                        model.LangID = 2;
                        model.Language = "Arabic";
                    }
                    else
                    {
                        model.LangID = 1;
                        model.Language = "English";
                    }
                }

                if (model.TotalCreditsReq == -5)
                {
                    return Json(new { IsValid = false, Message = "-5" });
                }
                if (model.TotalCreditsReq == -6)
                {
                    return Json(new { IsValid = false, Message = "-6" });
                }
                if (model.RecipientsType == 3)
                {
                    if (model.GroupIds != null && model.ValidCount == 0)
                    {
                        return Json(new
                        {
                            IsValid = false,
                            // Message = lz.NoContactsinSelectedGroup
                            //Message = "No Contacts in Selected Group / Uploaded File"
                            Message = "Please select Mobile no column"
                        });
                    }
                }
                else
                {
                    if (model.ValidCount == 0)
                    {
                        return Json(new
                        {
                            IsValid = false,
                            //Message = lz.InvalidFileMobileColumnShouldnotbeempty
                            Message = "Selected column doesn't contain valid data"
                        });
                    }
                }
                var CampDetails = SessionExtensions.GetItem<InsertBulkSMSOnRequest>(HttpContext.Session);
                if (CampDetails != null)
                {
                    CampDetails = null;
                    CampDetails = model;
                    SessionExtensions.AddItem<InsertBulkSMSOnRequest>(HttpContext.Session, CampDetails);
                }
                else
                {
                    SessionExtensions.AddItem<InsertBulkSMSOnRequest>(HttpContext.Session, model);
                }
                if (cmd != "")
                {
                    return Json(new
                    {
                        IsValid = true,
                        PartialResult = RenderRazorViewToString(cmd, model)

                    });
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("LoadSubmitCamp  :-{0} Error :- {1}", LocalModel);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return Json("");
        }

        private string BuildCsv<T>(string fileName, List<T> colleciton)
        {
            try
            {
                var filepath = _configuration["tempPath"]?.ToString();
                IEnumerableExtension.WriteExcelFileFromDataTable(Path.Combine(filepath, fileName + ".xls"), IEnumerableExtension.ConvertToDatatable(colleciton)); //.DataTableToCsvParse(fileName, filepath);
                return Path.Combine(filepath, fileName + ".xls");
            }
            catch (Exception ex)
            {
                var err = ex.StackTrace;
            }
            return string.Empty;
        }

        private string BuildCsvGroup(string fileName, List<GroupContacts> colleciton)
        {
            try
            {
                //var filepath = _configuration["tempPath"];
                var sbAppender = new StringBuilder();
                // PropertyInfo[] props = typeof(GroupContacts).GetProperties();//string.Join(",", props.Select(s => s.Name))

                sbAppender.Append("Email,FirstName,LastName,MobileNo" + Environment.NewLine);
                foreach (var item in colleciton)
                {
                    //GroupContacts i = Convert.ChangeType(item, typeof(T));
                    //sbAppender.Append(item.Email + "," + item.FirstName + "," + item.LastName + "," + item.MobileNo + Environment.NewLine); //Old Commented by murty on 18-09-2024
                    sbAppender.Append(item.Email + "," + ((item.FirstName != null && item.FirstName != string.Empty) ? item.FirstName.Replace(",", "").Replace("@", "") : item.FirstName) + ","
                            + ((item.LastName != null && item.LastName != string.Empty) ? item.LastName.Replace(",", "").Replace("@", "") : item.LastName) + "," + item.MobileNo + Environment.NewLine);

                }
                System.IO.File.WriteAllText(Path.Combine(fileName + ".csv"), sbAppender.ToString());
                //IEnumerableExtension.WriteExcelFileFromDataTable(Path.Combine(filepath, fileName + ".xls"), IEnumerableExtension.ConvertToDatatable(colleciton)); //.DataTableToCsvParse(fileName, filepath);
                return Path.Combine(fileName + ".csv");
            }
            catch (Exception ex)
            {
                var err = ex.StackTrace;
            }
            return string.Empty;
        }

        private string BuildCsvEDB(string fileName, List<MobileNos> colleciton)
        {
            try
            {
                var filepath = _configuration["tempPath"]?.ToString();
                var sbAppender = new StringBuilder();

                sbAppender.Append("MobileNo" + Environment.NewLine);
                foreach (var item in colleciton)
                {
                    sbAppender.Append(item.MobileNo + Environment.NewLine);
                }
                System.IO.File.WriteAllText(Path.Combine(filepath, fileName + ".csv"), sbAppender.ToString());
                return Path.Combine(filepath, fileName + ".csv");
            }
            catch (Exception ex)
            {
                var err = ex.StackTrace;
            }
            return string.Empty;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestTimeout("LongRunningPolicy")]
        public ActionResult SubmitCampaign(string cmd = "")
        {
            Logger.InfoFormat("SubmitCampaign :: start :: {0}", cmd);
            //HttpContext.Server.ScriptTimeout = 200;

            var extendedUser = SessionExtensions.GetItem<Core.Models.Extensions.CustomeUser>(HttpContext.Session);
            string ReturnVal = string.Empty;
            var CampDetails = SessionExtensions.GetItem<InsertBulkSMSOnRequest>(HttpContext.Session);
            string? sessionCategory = HttpContext.Session.GetString("category");
            if (!string.IsNullOrWhiteSpace(sessionCategory))
            {
                CampDetails.category = sessionCategory.TrimEnd(',');
            }
            var type = string.Empty;
            if (CampDetails.MessageType == 1)
            {
                type = "BulkSms";
            }
            else
            {
                type = "CustomSms";
            }

            var selAction = UserActions.FirstOrDefault(
                        w => w.ActionName.Equals(type) &&
                            w.ControllerName.Equals(ControllerName, StringComparison.OrdinalIgnoreCase));

            if (selAction.IsCheckerRequired == true && ExtendedUser.LogOnRespons.CustomerId != 1)
            {
                CampDetails.Status = -1;
                CampDetails.CurrentStatus = 2;
                CampDetails.IsDone = -1;
                CampDetails.IsProcess = -1;
            }

            if (CampDetails.TotalCreditsReq == 0)
            {
                ReturnVal = "Invalidcampaigncount";
                return Json(ReturnVal);
            }
            using (var ClientAccess = new AuthenticateSecurityClient())
            {
                try
                {
                    if (CampDetails != null)
                    {
                        if (Convert.ToInt32(CampDetails.CampID) > 0)
                        {
                            var QMsg = "action=stop&camp_id=" +
                               CampDetails.CampID + "&camp_type=" +
                               (Convert.ToInt32(CampDetails.ScheduledType) == 2 ? "2" : "1") + "&dir_name=&count=";

                            Logger.InfoFormat("CampaignQLog started ");
                            var Qresult = new CampaignQLog().PushMessageToQ(QMsg);
                            Logger.InfoFormat("CampaignQLog Ended");
                        }
                        //if (CampDetails.TotalCount == 0)
                        //{
                        //    return Json("notempty");
                        //}

                        var engineFilePath = !string.IsNullOrWhiteSpace(CampDetails.GroupIds) ? CampDetails.ImportFileName : CampDetails.ActualFileName;


                        var actualfilesplititems = !string.IsNullOrWhiteSpace(CampDetails.GroupIds) ? Path.GetFileName(CampDetails.ImportFileName).Split('_') : CampDetails.ActualFileName.Split('_');
                        // var originalDirPath = Path.GetDirectoryName(CampDetails.ImportFileName);
                        var originalDirPath = !string.IsNullOrWhiteSpace(CampDetails.GroupIds) ? _configuration["tempPath"]?.ToString() : Path.GetDirectoryName(CampDetails.ImportFileName);
                        var modifiedPath = _configuration["tempPath"]?.ToString() +
                                           ((Convert.ToInt32(CampDetails.ScheduledType) == 2) ? "\\Schedule" : "\\NonSchedule") + "\\" +
                                           actualfilesplititems[0];
                        CampDetails.ActualFileName = modifiedPath + "\\" + engineFilePath;
                        // CampDetails.ActualFileName = !string.IsNullOrWhiteSpace(CampDetails.GroupIds) ? engineFilePath: modifiedPath + "\\" + engineFilePath;

                        var response = ClientAccess.InsertBulkSMS(CampDetails);
                        var result = response.Result.Split('$')[0];
                        var nID = response.Result.Split('$')[1];
                        switch (result)
                        {
                            case "7":
                                ReturnVal = !(selAction != null && selAction.IsCheckerRequired)
                                    ? "MsgSubmitSuccess"
                                    : "MsgSubittedToChecker";
                                BuildNotifications(CampDetails, response.Result.Split('$')[2], modifiedPath,
                                    engineFilePath, actualfilesplititems[0], Convert.ToInt32(CampDetails.AllowDuplicates));
                                HttpContext.Session.RemoveItem<GroupContactsMain>();
                                break;
                            case "-1":
                                ReturnVal = "error";
                                break;
                            case "1":
                                ReturnVal = "CustInActive";
                                break;
                            case "2":
                                ReturnVal = "CustExpired";
                                break;
                            case "3":
                                ReturnVal = "CustPrefInActive";
                                break;
                            case "4":
                                ReturnVal = "InsufficentCredits";
                                break;
                            case "5":
                                ReturnVal = "UpdatedSuccessfully";
                                ReturnVal = BuildNotifications(CampDetails, response.Result.Split('$')[2], modifiedPath,
                                    engineFilePath, actualfilesplititems[0], Convert.ToInt32(CampDetails.AllowDuplicates));
                                break;
                            case "8":
                                ReturnVal = "DuplicateName";
                                break;
                            case "9":
                                ReturnVal = "InvalidSchedule";
                                break;
                            case "10":
                                var ctimes = MessageTimings(CampDetails.CampaignType.ToString());
                                return Json(new { ReturnVal = "InvalidTime", campTimes = ctimes });
                            //break;
                            case "11":
                                ReturnVal = "KickOff";
                                break;
                        }
                    }
                    else
                    {
                        ReturnVal = "Unable Process, Please contact admin.";
                    }
                    return Json(ReturnVal);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("SubmitCampaign  :-{0} Error :- {1}", cmd);
                    ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }
            return Json("");
        }
        public async Task<List<Core.Models.Dtos.Responses.Synapse.ManageMobilityCenter.CampaignTimingsOnResponse>> MessageTimings(string camptype)
        {
            var ct = camptype == "Promotional" ? "1" : "2";

            using (var ClientAccess = new AuthenticateSecurityClient())
            {
                var response = await ClientAccess.MessageTimings(new CampaignTimingsOnRequest
                {
                    CAMPTYPEID = Convert.ToInt32(ct),
                    STATUS = 2,
                    UserId = ExtendedUser.LogOnRespons.Id,
                    UserIp = ExtendedUser.LogOnRespons.GetIPAddress

                });
                return response.ToList();
            }
        }

        public string? ValidateCampaignName(string campName)
        {
            Logger.InfoFormat("ValidateCampaignName :: start :: {0}", campName);
            try
            {
                if (!string.IsNullOrWhiteSpace(campName))
                {
                    using (var clientAcces = new AuthenticateSecurityClient())
                    {
                        var response = clientAcces.ValidateCampaignName(new ReUsableRequest { CustomerName = campName });
                        return (!response.Result) ? lz.Campaignnamealreadyexistedindb : "";
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("ValidateCampaignName  :-{0} Error :- {1}", campName);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return string.Empty;
        }

        public List<KeyValuePair<string, string>> BuildXmlFromFile(string fileName, string filePath, string userId, string UserName, string campaignId,
            string sender, string message, string mobColumnName, int languageId, List<string> fileNames, string SheetName, int isPromo, bool AllowDuplicates = false, bool isCustome = false)
        {

            if (!string.IsNullOrWhiteSpace(fileName))
            {

                var extension = Path.GetExtension(fileName);
                switch (extension)
                {
                    case ".csv":
                    case ".txt":
                        return BuildCsvToXml(fileName, filePath, userId, UserName, campaignId, sender, message, mobColumnName, languageId,
                            isCustome, fileNames, SheetName, isPromo, AllowDuplicates, 1);
                    case ".xls":
                    case ".xlsx":
                        return BuildCsvToXml(fileName, filePath, userId, UserName, campaignId, sender, message, mobColumnName, languageId,
                         isCustome, fileNames, SheetName, isPromo, AllowDuplicates, 2);
                        //return BuildXlsToXml(fileName, filePath, userId, UserName, campaignId, sender, message, mobColumnName, languageId,
                        //    isCustome, fileNames, SheetName, isPromo, AllowDuplicates);

                }
            }
            return new List<KeyValuePair<string, string>>();
        }

        public List<KeyValuePair<string, string>> BuildCsvToXml(string fileName, string filePath, string userId, string UserName, string campaignId,
            string sender, string message, string mobColumnName, int languageId, bool isCustome, List<string> fileNames, string SheetName, int isPromo, bool AllowDuplicates, int filetype)
        {
            DataTable dt = new DataTable();
            DataSet result = new DataSet();
            if (filetype == 2)
            {
                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    var extension = Path.GetExtension(fileName);
                    IExcelDataReader excelReader = null;
                    switch (extension)
                    {
                        case ".xls":
                            /*  Reading from a binary Excel file ('97-2003 format; *.xls)   */
                            excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                            break;
                        case ".xlsx":
                            /*  Reading from a OpenXml Excel file (2007 format; *.xlsx) */
                            excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                            //result = GetFileDataFromFormatedExcel(stream);
                            break;
                    }

                    if (result.Tables.Count == 0)
                    {
                        /*  DataSet - Create column names from first row    */
                        excelReader.IsFirstRowAsColumnNames = true;
                        result = excelReader.AsDataSet();
                        excelReader.Close();
                    }
                    dt = !string.IsNullOrWhiteSpace(SheetName) ? result.Tables[SheetName] : result.Tables[0];
                }
            }
            else
            {
                dt = BuildCsvToTable(filePath);
            }

            var filterdt = dt.Rows.Cast<DataRow>().Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s);

            dt = filterdt.Any() ? filterdt.CopyToDataTable() : dt.Clone();

            dt.AsEnumerable().Where(row => row.Field<object>(mobColumnName).ToString().Trim().TrimEnd(',').Length == 10)
                .Select(b => b[mobColumnName] = "91" + b[mobColumnName].ToString().Trim().TrimEnd(','))
                .ToList();

            var nonumbers = dt.Rows.Cast<DataRow>().Where(w => IEnumerableExtension.IsHavingSpecialChar(w[mobColumnName].ToString())).ToList();

            dt = nonumbers.CopyToDataTable();

            var returnElements = new List<KeyValuePair<string, string>>();
            List<DataRow> mobRows = new List<DataRow>();
            mobRows = dt.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First()).ToList();
            var cieledvalue = 0;
            var actualFileName = Path.GetFileNameWithoutExtension(fileName);
            fileNames.Clear();
            fileNames.Add(actualFileName + "_duplicate");
            var itrations = _configuration["CampaignXMLGeneratedCount"]?.ToString() != null ? Convert.ToInt32(_configuration["CampaignXMLGeneratedCount"]?.ToString()) : 25000; //(int)Math.Ceiling((double)mobileColumn.Count / (double)25000);//fileNames.Count);
            var currentItreation = 0;
            try
            {
                StringBuilder xElement = new StringBuilder();
                StringBuilder xElementnondnd = new StringBuilder();
                //dynamic mobileColumn;
                if (!AllowDuplicates)
                {
                    //List<DataRow> mobRows = new List<DataRow>();
                    var tcsv = fileNames.FirstOrDefault();
                    var InvalidMobsCollection = new List<string>();
                    var emptyMobileNo =
                        dt.Rows.Cast<DataRow>()
                           .Where(w => string.IsNullOrWhiteSpace(w[mobColumnName].ToString()))
                           .ToList();
                    if (emptyMobileNo.Any())
                    {
                        InvalidMobsCollection.AddRange(emptyMobileNo.Select(s => s[mobColumnName].ToString()));
                        emptyMobileNo.ForEach(f => dt.Rows.Remove(f));
                    }
                    var Duplicates = dt.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Where(g => g.Count() > 1).SelectMany(ss => ss.Skip(1)).Select(s => s[mobColumnName].ToString()).ToList();
                    if (Duplicates.Any())
                    {
                        if (conCode != null && conCode == 91)
                        {
                            for (int i = 0; i < Duplicates.Count; i++)
                            {
                                if (Duplicates[i].Length == conMobLength)
                                {
                                    Duplicates[i] = Duplicates[i].Replace(Duplicates[i], (conCode.ToString() + Duplicates[i]));
                                }
                            }
                        }

                        if (!isCustome)
                        {
                            foreach (var t in fileNames)
                            {
                                xElement.Clear();
                                //Duplicates = dt.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Where(g => g.Count() > 1).SelectMany(ss => ss.Skip(1)).Select(s => conCode == 91 ? (s[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + s[mobColumnName].ToString()) : s[mobColumnName].ToString()) : s[mobColumnName].ToString()).Skip(currentItreation).Take(itrations).ToList();
                                Duplicates = dt.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Where(g => g.Count() > 1).SelectMany(ss => ss.Skip(1)).Select(s => WebUtility.HtmlEncode(conCode == 91 ? (s[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + s[mobColumnName].ToString()) : s[mobColumnName].ToString()) : s[mobColumnName].ToString())).ToList();
                                var mobilJoinedString = string.Join(",", Duplicates);
                                // mobilJoinedString = "91" + mobilJoinedString;
                                returnElements.Add(new KeyValuePair<string, string>(t,
                                    "<root iscustome='" + (isCustome == true ? "true" : "false") +
                                    "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "' priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                    WebUtility.HtmlEncode(sender) +
                                    "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                                    "' mobile=''><mobile>" + mobilJoinedString + "</mobile></sendsms></root>"));
                                //currentItreation = currentItreation + itrations;
                            }
                        }
                        else
                        {

                            var messageTemplates = IEnumerableExtension.StringBetween(message, "<$", "$>");
                            foreach (var t in fileNames)
                            {
                                xElement.Clear();
                                var rows = dt.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Where(g => g.Count() > 1).SelectMany(ss => ss.Skip(1)).Distinct().ToList();
                                //var rows = dt.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Where(g => g.Count() > 1).SelectMany(ss => ss.Skip(1)).Skip(currentItreation).Take(itrations).ToList();

                                foreach (DataRow row in rows)
                                {
                                    var msg = message;
                                    foreach (var item in messageTemplates)
                                    {
                                        msg = msg.Replace("<$" + item + "$>", (row[item] != null && row[item].ToString() != "") ? row[item].ToString() : "");
                                    }
                                    var lid = languageId;
                                    var ccountlid = msg.validateMessage(lid).Split(',');
                                    var ccount = ccountlid[0];
                                    lid = Convert.ToInt32(ccountlid[1]);
                                    languageId = lid;
                                    msg = WebUtility.HtmlEncode(msg).Replace("\n", "&#10;");

                                    if (conCode != null && conCode == 91)
                                    {
                                        xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                        WebUtility.HtmlEncode(sender) +
                                        "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                                        "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) + "'></sendsms>");
                                    }
                                    else
                                    {
                                        xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                        WebUtility.HtmlEncode(sender) +
                                        "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                                        "' mobile='" + WebUtility.HtmlEncode(row[mobColumnName].ToString()) + "'></sendsms>");
                                    }

                                }

                                returnElements.Add(new KeyValuePair<string, string>(t,
                                    "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + xElement.ToString() +
                                    "</root>"));
                            }
                        }
                    }
                }
                var actualFileName1 = Path.GetFileNameWithoutExtension(fileName);

                //* promotional area

                if ((_configuration["IndianSynapse"]?.ToString() == "true" && !Regex.IsMatch(sender, @"^[a-zA-Z]+$")) ||
                    (_configuration["IndianSynapse"]?.ToString() == "true" && Regex.IsMatch(sender, @"^[a-zA-Z]+$") && isPromo == 0))
                {
                    var NoCheck = DndNumberCheck1(dt, mobColumnName);

                    Logger.InfoFormat("File Promotional start {0} ::", DateTime.Now);
                    if (!isCustome)
                    {
                        if (NoCheck.strDndNumbers.Count > 0)
                        {
                            currentItreation = 0;
                            //To Fix Warning : The result of the expression is always 'true' since a value of type 'int' is never equal to 'null' of type 'int?'
                            // change to itrations!=null changed to itrations!=0
                            cieledvalue = (int)Math.Ceiling((double)NoCheck.strDndNumbers.Count / (double)(itrations != 0 ? itrations : 25000));

                            fileNames.Clear();
                            for (var i = 0; i < cieledvalue; i++)
                            {
                                fileNames.Add(actualFileName1 + "_" + i + "_dnd");
                            }

                            foreach (var t in fileNames)
                            {
                                xElement.Clear();
                                HashSet<string> noche = new HashSet<string>(NoCheck.strDndNumbers.Select(s => s.DNDNumbers).Skip(currentItreation).Take(itrations).ToList());

                                //HashSet<string> noche = new HashSet<string>(NoCheck.strDndNumbers.Select(s => WebUtility.HtmlEncode(conCode == 91 ? (s.DNDNumbers.ToString().Length == conMobLength ? (conCode.ToString() + s.DNDNumbers.ToString()) : s.DNDNumbers.ToString()) : s.DNDNumbers.ToString())).Skip(currentItreation).Take(itrations).ToList());
                                var mob2 = ""; //string.Join(",", noche);
                                //mob2 = "91" + mob2;
                                foreach (var item in noche)
                                {
                                    mob2 += "," + WebUtility.HtmlEncode(conCode == 91 ? (item.Length == conMobLength ? (conCode.ToString() + item) : item) : item);
                                }
                                if (noche.Count > 0)
                                {
                                    returnElements.Add(new KeyValuePair<string, string>(t,
                                        "<root iscustome='" + (isCustome == true ? "true" : "false") +
                                        "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                            WebUtility.HtmlEncode(sender) +
                                            "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                                             "' mobile=''><mobile>" + mob2.Trim(',') + "</mobile></sendsms></root>"));
                                    currentItreation = currentItreation + itrations;
                                }

                            }
                        }

                        currentItreation = 0;
                        //  HashSet<string> nocheNonDND = new HashSet<string>(NoCheck.strNonDndNumbers.Select(s => s.NonDNDNumbers));
                        if (NoCheck.strNonDndNumbers.Count > 0)
                        {
                            //To Fix Warning : The result of the expression is always 'true' since a value of type 'int' is never equal to 'null' of type 'int?'
                            // change to itrations!=null changed to itrations!=0
                            cieledvalue = (int)Math.Ceiling((double)NoCheck.strNonDndNumbers.Count / (double)(itrations != 0 ? itrations : 25000));
                            fileNames.Clear();
                            for (var i = 0; i < cieledvalue; i++)
                            {
                                fileNames.Add(actualFileName1 + "_" + i);
                            }
                            foreach (var t in fileNames)
                            {
                                xElement.Clear();
                                //HashSet<string> nocheNonDND1 = new HashSet<string>(NoCheck.strNonDndNumbers.Select(s => WebUtility.HtmlEncode(conCode == 91 ? (s.NonDNDNumbers.ToString().Length == conMobLength ? (conCode.ToString() + s.NonDNDNumbers.ToString()) : s.NonDNDNumbers.ToString()) : s.NonDNDNumbers.ToString())).Skip(currentItreation).Take(itrations).ToList());
                                HashSet<string> nocheNonDND1 = new HashSet<string>(NoCheck.strNonDndNumbers.Select(s => s.NonDNDNumbers).Skip(currentItreation).Take(itrations).ToList());
                                var mob3 = ""; // string.Join(",", nocheNonDND1);
                                //mob3 = "91" + mob3;
                                foreach (var item in nocheNonDND1)
                                {
                                    mob3 += "," + WebUtility.HtmlEncode(conCode == 91 ? (item.Length == conMobLength ? (conCode.ToString() + item) : item) : item);
                                }
                                returnElements.Add(new KeyValuePair<string, string>(t,
                                    "<root iscustome='" + (isCustome == true ? "true" : "false") +
                                    "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                        WebUtility.HtmlEncode(sender) +
                                        "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                                         "' mobile=''><mobile>" + mob3.Trim(',') + "</mobile></sendsms></root>"));
                                currentItreation = currentItreation + itrations;

                            }
                        }
                    }
                    else
                    {
                        var actualFileName2 = Path.GetFileNameWithoutExtension(fileName);

                        var messageTemplates = IEnumerableExtension.StringBetween(message, "<$", "$>");
                        xElement.Clear();
                        xElementnondnd.Clear();

                        int dndcount = 0;
                        int nondndcount = 0;
                        int dndindex = 0;
                        int nondndindex = 0;

                        DataTable filterdnd = new DataTable();
                        DataTable filternondnd = new DataTable();



                        if (NoCheck.strDndNumbers.Count > 0)
                        {
                            var filterdnd1 = (from item in dt.Rows.Cast<DataRow>()
                                              join item1 in NoCheck.strDndNumbers on item[mobColumnName].ToString().Trim() equals (item1.DNDNumbers.ToString())
                                              select item);
                            filterdnd = filterdnd1.Any() ? filterdnd1.CopyToDataTable() : dt.Clone();
                        }
                        if (NoCheck.strNonDndNumbers.Count > 0)
                        {
                            var filternondnd1 = (from item in dt.Rows.Cast<DataRow>()
                                                 join item1 in NoCheck.strNonDndNumbers on item[mobColumnName].ToString().Trim() equals (item1.NonDNDNumbers.ToString())
                                                 select item);
                            filternondnd = filternondnd1.Any() ? filternondnd1.CopyToDataTable() : dt.Clone();
                        }
                        if (filterdnd.Rows.Count > 0)
                        {
                            var rows1 = new List<DataRow>();
                            rows1 = (AllowDuplicates) ? filterdnd.Rows.Cast<DataRow>().ToList() :
                            filterdnd.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First()).ToList();

                            foreach (DataRow row in rows1)
                            {
                                row[mobColumnName] = row[mobColumnName].ToString().Trim();
                                var msg = message;
                                foreach (var item in messageTemplates)
                                {
                                    msg = msg.Replace("<$" + item + "$>", row[item].ToString());
                                }

                                var lid = languageId;
                                var ccountlid = msg.validateMessage(lid).Split(',');
                                var ccount = ccountlid[0];
                                lid = Convert.ToInt32(ccountlid[1]);
                                languageId = lid;
                                msg = WebUtility.HtmlEncode(msg).Replace("\n", "&#10;");

                                if (conCode != null && conCode == 91)
                                {
                                    xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                        WebUtility.HtmlEncode(sender) +
                                        "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                                        "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) + "'></sendsms>");
                                }
                                else
                                {
                                    xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                        WebUtility.HtmlEncode(sender) +
                                        "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                                        "' mobile='" + WebUtility.HtmlEncode(row[mobColumnName].ToString()) + "'></sendsms>");
                                }
                                dndcount++;
                                if (dndcount >= itrations)
                                {
                                    returnElements.Add(new KeyValuePair<string, string>(actualFileName1 + "_" + dndindex + "_dnd",
                                    "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + xElement.ToString() +
                                    "</root>"));
                                    dndindex++;
                                    xElement.Clear();
                                    dndcount = 0;
                                }

                            }
                        }


                        if (filternondnd.Rows.Count > 0)
                        {
                            var rows1 = new List<DataRow>();
                            rows1 = (AllowDuplicates) ? filternondnd.Rows.Cast<DataRow>().ToList() :
                            filternondnd.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First()).ToList();

                            foreach (DataRow row in rows1)
                            {
                                row[mobColumnName] = row[mobColumnName].ToString().Trim();
                                var msg = message;
                                foreach (var item in messageTemplates)
                                {
                                    msg = msg.Replace("<$" + item + "$>", row[item].ToString());
                                }

                                var lid = languageId;
                                var ccountlid = msg.validateMessage(lid).Split(',');
                                var ccount = ccountlid[0];
                                lid = Convert.ToInt32(ccountlid[1]);
                                languageId = lid;
                                msg = WebUtility.HtmlEncode(msg).Replace("\n", "&#10;");


                                if (conCode != null && conCode == 91)
                                {
                                    xElementnondnd.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                        WebUtility.HtmlEncode(sender) +
                                        "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                                        "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) + "'></sendsms>");
                                }
                                else
                                {
                                    xElementnondnd.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                        WebUtility.HtmlEncode(sender) +
                                        "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                                        "' mobile='" + WebUtility.HtmlEncode(row[mobColumnName].ToString()) + "'></sendsms>");
                                }
                                nondndcount++;
                                if (nondndcount >= itrations)
                                {
                                    returnElements.Add(new KeyValuePair<string, string>(actualFileName1 + "_" + nondndindex,
                                    "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + xElementnondnd.ToString() +
                                    "</root>"));
                                    nondndindex++;
                                    xElementnondnd.Clear();
                                    nondndcount = 0;
                                }
                            }
                        }

                        if (dndcount > 0)
                        {
                            returnElements.Add(new KeyValuePair<string, string>(actualFileName1 + "_" + dndindex + "_dnd",
                            "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + xElement.ToString() +
                            "</root>"));
                        }
                        if (nondndcount > 0)
                        {
                            returnElements.Add(new KeyValuePair<string, string>(actualFileName1 + "_" + nondndindex,
                            "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + xElementnondnd.ToString() +
                            "</root>"));
                        }
                    }
                }
                //* trnasational area


                if (Regex.IsMatch(sender, @"^[a-zA-Z]+$") && isPromo != 0)
                {
                    Logger.InfoFormat("File Transactional end {0} ::", DateTime.Now);

                    if (!AllowDuplicates)
                    {
                        cieledvalue = (int)Math.Ceiling((double)mobRows.Count / (double)(_configuration["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(_configuration["CampaignXMLGeneratedCount"]) : 25000));
                    }
                    else
                    {
                        cieledvalue = (int)Math.Ceiling((double)dt.Rows.Count / (double)(_configuration["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(_configuration["CampaignXMLGeneratedCount"]) : 25000));
                    }

                    fileNames.Clear();
                    for (var i = 0; i < cieledvalue; i++)
                    {
                        fileNames.Add(actualFileName1 + "_" + i);
                    }

                    if (!isCustome)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            foreach (var t in fileNames)
                            {
                                xElement.Clear();
                                var currentItreation1 = 0;
                                var Duplicates = dt.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Where(g => g.Count() > 1).SelectMany(ss => ss.Skip(1)).Select(s => s[mobColumnName].ToString()).ToList();
                                mobRows = dt.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First()).ToList();

                                var m = mobRows.Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).Skip(currentItreation).Take(itrations).ToList();
                                var r = dt.Rows.Cast<DataRow>().Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).Skip(currentItreation).Take(itrations).ToList();
                                if (m.Count == 0 || r.Count == 0)
                                {
                                    var mob = (Duplicates.Any()) ? mobRows.Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => WebUtility.HtmlEncode(conCode == 91 ? (s[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + s[mobColumnName].ToString()) : s[mobColumnName].ToString()) : s[mobColumnName].ToString())).Skip(currentItreation1).Take(itrations).ToList() : dt.Rows.Cast<DataRow>().Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => WebUtility.HtmlEncode(conCode == 91 ? (s[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + s[mobColumnName].ToString()) : s[mobColumnName].ToString()) : s[mobColumnName].ToString())).Skip(currentItreation1).Take(itrations).ToList();

                                    var mob3 = string.Join(",", mob);
                                    //mob3 = "91" + mob3;
                                    if (mob3 != "")
                                    {
                                        returnElements.Add(new KeyValuePair<string, string>(t,
                                        "<root iscustome='" + (isCustome == true ? "true" : "false") +
                                        "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                            WebUtility.HtmlEncode(sender) +
                                            "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                                            "' mobile=''><mobile>" + mob3 + "</mobile></sendsms></root>"));
                                        currentItreation = currentItreation + itrations;
                                    }
                                }
                                else
                                {
                                    var mob = (Duplicates.Any()) ? mobRows.Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => WebUtility.HtmlEncode(conCode == 91 ? (s[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + s[mobColumnName].ToString()) : s[mobColumnName].ToString()) : s[mobColumnName].ToString())).Skip(currentItreation).Take(itrations).ToList() : dt.Rows.Cast<DataRow>().Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => WebUtility.HtmlEncode(conCode == 91 ? (s[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + s[mobColumnName].ToString()) : s[mobColumnName].ToString()) : s[mobColumnName].ToString())).Skip(currentItreation).Take(itrations).ToList();
                                    var mob3 = string.Join(",", mob);
                                    //mob3 = "91" + mob3;
                                    if (mob3 != "")
                                    {
                                        returnElements.Add(new KeyValuePair<string, string>(t,
                                        "<root iscustome='" + (isCustome == true ? "true" : "false") +
                                        "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                            WebUtility.HtmlEncode(sender) +
                                            "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                                            "' mobile=''><mobile>" + mob3 + "</mobile></sendsms></root>"));
                                        currentItreation = currentItreation + itrations;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        var messageTemplates = IEnumerableExtension.StringBetween(message, "<$", "$>");
                        currentItreation = 0;
                        foreach (var t in fileNames)
                        {
                            xElement.Clear();

                            // var rows = dt.Rows.Cast<DataRow>().Skip(currentItreation).Take(itrations).ToList();
                            var rows = new List<DataRow>();
                            rows = (AllowDuplicates) ? dt.Rows.Cast<DataRow>().Skip(currentItreation).Take(itrations).ToList() :
                            dt.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First())
                                            .Skip(currentItreation)
                                            .Take(itrations)
                                            .ToList();
                            foreach (DataRow row in rows)//.Where(row => messageTemplates.Any()))
                            {
                                var msg = message;
                                foreach (var item in messageTemplates)
                                {
                                    msg = msg.Replace("<$" + item + "$>", row[item].ToString());
                                }
                                // var ccount = msg.CreditsCountValidation(languageId);
                                var lid = languageId;
                                var ccountlid = msg.validateMessage(lid).Split(',');
                                var ccount = ccountlid[0];
                                lid = Convert.ToInt32(ccountlid[1]);
                                languageId = lid;
                                msg = WebUtility.HtmlEncode(msg).Replace("\n", "&#10;");

                                if (conCode != null && conCode == 91)
                                {
                                    xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                        WebUtility.HtmlEncode(sender) +
                                        "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                                        "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) + "'></sendsms>");
                                }
                                else
                                {
                                    xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                        WebUtility.HtmlEncode(sender) +
                                        "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                                        "' mobile='" + WebUtility.HtmlEncode(row[mobColumnName].ToString()) + "'></sendsms>");
                                }
                            }

                            returnElements.Add(new KeyValuePair<string, string>(t,
                                "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + xElement.ToString() +
                                "</root>"));
                            currentItreation = currentItreation + itrations;
                        }
                    }
                }
                Logger.InfoFormat("File end {0} ::", DateTime.Now);

            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("While parsing message from bulk sms / custome sms fatal error throughing :: {0} Error :: {1}",
                    ex.StackTrace, ex.ToString());
            }
            return returnElements;
        }

        private string msgrep(DataRow dRow, string message)//, int languageId, string userId, string UserName, string campaignId, string sender, string mobColumnName)
        {
            //string xmlelement = "";
            var messageTemplates = IEnumerableExtension.StringBetween(message, "<$", "$>");

            foreach (var item in messageTemplates)
            {
                message = message.Replace("<$" + item + "$>", (dRow[item] != null && dRow[item].ToString() != "") ? dRow[item].ToString() : "");
            }
            return message;
        }

        public DataTable ConvertListToDataTable(List<strDndNumbers> list)
        {
            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 0;
            columns = list.Count();

            foreach (var array in list)
            {
                if (array.DNDNumbers.Length > columns)
                {
                    columns = array.DNDNumbers.Length;
                }
            }

            // Add columns.
            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add();
            }

            // Add rows.
            foreach (var array in list)
            {
                table.Rows.Add(array);
            }

            return table;
        }

        public List<KeyValuePair<string, string>> BuildXlsToXml(string fileName, string filePath, string userId, string UserName, string campaignId,
            string sender, string message, string mobColumnName, int languageId, bool isCustome, List<string> fileNames, string SheetName, int isPromo, bool AllowDuplicates)
        {
            var returnElements = new List<KeyValuePair<string, string>>();
            var resultSet = new DataTable();
            var result = new DataSet();
            try
            {
                //StringBuilder xElement = new StringBuilder();
                StringBuilder xElementnondnd = new StringBuilder();
                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    var extension = Path.GetExtension(fileName);
                    IExcelDataReader excelReader = null;
                    switch (extension)
                    {
                        case ".xls":
                            /*  Reading from a binary Excel file ('97-2003 format; *.xls)   */
                            excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                            break;
                        case ".xlsx":
                            /*  Reading from a OpenXml Excel file (2007 format; *.xlsx) */
                            excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                            //result = GetFileDataFromFormatedExcel(stream);
                            break;
                    }

                    if (result.Tables.Count == 0)
                    {
                        /*  DataSet - Create column names from first row    */
                        excelReader.IsFirstRowAsColumnNames = true;
                        result = excelReader.AsDataSet();
                        excelReader.Close();
                    }
                    resultSet = !string.IsNullOrWhiteSpace(SheetName) ? result.Tables[SheetName] : result.Tables[0];
                }
                resultSet.Columns[mobColumnName].ColumnName = "MobileNo";
                resultSet.AcceptChanges();
                message = message.Replace("<$" + mobColumnName + "$>", "<$MobileNo$>");
                mobColumnName = "MobileNo";

                List<DataRow> mobRows = new List<DataRow>();
                mobRows = resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First()).ToList();
                var Dup = resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Where(g => g.Count() > 1).SelectMany(ss => ss.Skip(1)).Select(s => s[mobColumnName].ToString()).ToList();
                var cieledvalue = 0;
                cieledvalue = (int)Math.Ceiling((double)Dup.Count / (double)(_configuration["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(_configuration["CampaignXMLGeneratedCount"]) : 25000));
                var actualFileName = Path.GetFileNameWithoutExtension(fileName);
                fileNames.Clear();
                for (var i = 0; i < cieledvalue; i++)
                {
                    fileNames.Add(actualFileName + "_duplicate_" + i);
                }
                var itrations = _configuration["CampaignXMLGeneratedCount"]?.ToString() != null ? Convert.ToInt32(_configuration["CampaignXMLGeneratedCount"]) : 25000; //(int)Math.Ceiling((double)mobileColumn.Count / (double)25000);//fileNames.Count);
                var currentItreation = 0;

                StringBuilder xElement = new StringBuilder();
                dynamic mobileColumn;
                if (!AllowDuplicates)
                {
                    var InvalidMobsCollection = new List<string>();
                    var emptyMobileNo =
                        resultSet.Rows.Cast<DataRow>()
                           .Where(w => string.IsNullOrWhiteSpace(w[mobColumnName].ToString()))
                           .ToList();
                    if (emptyMobileNo.Any())
                    {
                        InvalidMobsCollection.AddRange(emptyMobileNo.Select(s => s[mobColumnName].ToString()));
                        emptyMobileNo.ForEach(f => resultSet.Rows.Remove(f));
                    }

                    var Duplicates = resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Where(g => g.Count() > 1).SelectMany(ss => ss.Skip(1)).Select(s => s[mobColumnName].ToString()).ToList();
                    if (Duplicates.Any())
                    {
                        if (conCode != null && conCode == 91)
                        {
                            for (int i = 0; i < Duplicates.Count; i++)
                            {
                                if (Duplicates[i].Length == conMobLength)
                                {
                                    Duplicates[i] = Duplicates[i].Replace(Duplicates[i], (conCode.ToString() + Duplicates[i]));
                                }
                            }
                        }
                        if (!isCustome)
                        {
                            foreach (var t in fileNames)
                            {
                                xElement.Clear();
                                Duplicates = resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Where(g => g.Count() > 1).SelectMany(ss => ss.Skip(1)).Select(s => s[mobColumnName].ToString()).Skip(currentItreation).Take(itrations).ToList();
                                var mobilJoinedString = string.Join(",91", Duplicates);
                                mobilJoinedString = "91" + mobilJoinedString;

                                returnElements.Add(new KeyValuePair<string, string>(t,
                                    "<root iscustome='" + (isCustome == true ? "true" : "false") +
                                    "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                    WebUtility.HtmlEncode(sender) +
                                    "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                                    "' mobile=''><mobile>" + WebUtility.HtmlEncode(mobilJoinedString) + "</mobile></sendsms></root>"));
                                currentItreation = currentItreation + itrations;
                            }
                        }
                        else
                        {
                            foreach (var t in fileNames)
                            {
                                xElement.Clear();
                                var messageTemplates = IEnumerableExtension.StringBetween(message, "<$", "$>");
                                var lid = languageId;
                                languageId = lid;
                                var rows = resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Where(g => g.Count() > 1).SelectMany(ss => ss.Skip(1)).Distinct().ToList();
                                var el = from row in rows
                                         let msg //= message
                                         = messageTemplates.Aggregate(message, (current, item) => current.Replace("<$" + item + "$>", (row[item] != null && row[item] != "") ? row[item].ToString() : ""))
                                         let ccountlid = msg.validateMessage(lid).Split(',')
                                         let ccount = ccountlid[0]
                                         select "<sendsms ccount='" + ccount + "' userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId +
                                         "' sender='" + WebUtility.HtmlEncode(sender) +
                                         "' language='" + ((Convert.ToInt32(ccountlid[1]) == 2) ? 8 : IsHavingAttheRate(msg)) +
                                         "' message='" + WebUtility.HtmlEncode(msg).Replace("\n", "&#10;") +
                                         "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) +
                                         "'></sendsms>";

                                returnElements.Add(new KeyValuePair<string, string>(t,
                                    "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + string.Join("", el) +
                                    "</root>"));
                                currentItreation = currentItreation + itrations;
                            }
                        }

                    }
                    mobileColumn = (Duplicates.Any()) ? mobRows.Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).ToList() : resultSet.Rows.Cast<DataRow>().Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).ToList();
                }
                else
                {
                    mobileColumn = resultSet.Rows.Cast<DataRow>().Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).ToList();

                }

                List<DataRow> mobRowss = new List<DataRow>();
                var NoCheck = DndNumberCheck1(resultSet, mobColumnName);
                cieledvalue = (int)Math.Ceiling((double)NoCheck.strDndNumbers.Count / (double)(_configuration["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(_configuration["CampaignXMLGeneratedCount"]) : 25000));
                var actualFileName1 = Path.GetFileNameWithoutExtension(fileName);
                fileNames.Clear();
                for (var i = 0; i < cieledvalue; i++)
                {
                    fileNames.Add(actualFileName1 + "_dnd_" + i);
                }

                if (_configuration["IndianSynapse"]?.ToString() == "true" && !Regex.IsMatch(sender, @"^[a-zA-Z]+$"))
                {

                    currentItreation = 0;
                    if (!isCustome)
                    {
                        foreach (var t in fileNames)
                        {
                            xElement.Clear();
                            HashSet<string> noche = new HashSet<string>(NoCheck.strDndNumbers.Select(s => s.DNDNumbers).Skip(currentItreation).Take(itrations).ToList());
                            var mob2 = string.Join(",91", noche);
                            mob2 = "91" + mob2;

                            if (noche.Count > 0)
                            {
                                returnElements.Add(new KeyValuePair<string, string>(t,
                                    "<root iscustome='" + (isCustome == true ? "true" : "false") +
                                    "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                        WebUtility.HtmlEncode(sender) +
                                        "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                                         "' mobile=''><mobile>" + mob2 + "</mobile></sendsms></root>"));
                                currentItreation = currentItreation + itrations;
                            }
                        }
                    }
                    else
                    {
                        List<string> filenamesnondnd = new List<string>();
                        var actualFileName2 = Path.GetFileNameWithoutExtension(fileName);

                        var messageTemplates = IEnumerableExtension.StringBetween(message, "<$", "$>");
                        xElement.Clear();
                        xElementnondnd.Clear();

                        int dndcount = 0;
                        int nondndcount = 0;
                        int dndindex = 0;
                        int nondndindex = 0;
                        var rows1 = new List<DataRow>();
                        rows1 = (AllowDuplicates) ? resultSet.Rows.Cast<DataRow>().ToList() :
                        resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First()).ToList();

                        //var noche1 = string.Join(",",NoCheck.strDndNumbers.Select(s => s.DNDNumbers));
                        var noche2 = string.Join(",", NoCheck.strNonDndNumbers.Select(s => s.NonDNDNumbers));

                        foreach (DataRow row in rows1)
                        {
                            var msg = message;
                            foreach (var item in messageTemplates)
                            {
                                msg = msg.Replace("<$" + item + "$>", row[item].ToString());
                            }

                            var lid = languageId;
                            var ccountlid = msg.validateMessage(lid).Split(',');
                            var ccount = ccountlid[0];
                            lid = Convert.ToInt32(ccountlid[1]);
                            languageId = lid;
                            msg = WebUtility.HtmlEncode(msg).Replace("\n", "&#10;");

                            if (noche2.Contains(row[mobColumnName].ToString()))
                            {
                                if (conCode != null && conCode == 91)
                                {
                                    xElementnondnd.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                        WebUtility.HtmlEncode(sender) +
                                        "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                                        "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) + "'></sendsms>");
                                }
                                else
                                {
                                    xElementnondnd.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                        WebUtility.HtmlEncode(sender) +
                                        "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                                        "' mobile='" + WebUtility.HtmlEncode(row[mobColumnName].ToString()) + "'></sendsms>");
                                }
                                nondndcount++;
                                if (nondndcount >= itrations)
                                {
                                    returnElements.Add(new KeyValuePair<string, string>(actualFileName2 + "_" + nondndindex,
                                    "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + xElementnondnd.ToString() +
                                    "</root>"));
                                    nondndindex++;
                                    xElementnondnd.Clear();
                                    nondndcount = 0;
                                }
                            }

                            else
                            {
                                if (conCode != null && conCode == 91)
                                {
                                    xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                        WebUtility.HtmlEncode(sender) +
                                        "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                                        "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) + "'></sendsms>");
                                }
                                else
                                {
                                    xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                        WebUtility.HtmlEncode(sender) +
                                        "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                                        "' mobile='" + WebUtility.HtmlEncode(row[mobColumnName].ToString()) + "'></sendsms>");
                                }
                                dndcount++;
                                if (dndcount >= itrations)
                                {
                                    returnElements.Add(new KeyValuePair<string, string>(actualFileName1 + "_dnd_" + dndindex,
                                    "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + xElement.ToString() +
                                    "</root>"));
                                    dndindex++;
                                    xElement.Clear();
                                    dndcount = 0;
                                }
                            }
                        }
                        if (dndcount > 0)
                        {
                            returnElements.Add(new KeyValuePair<string, string>(actualFileName1 + "_dnd_" + dndindex,
                            "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + xElement.ToString() +
                            "</root>"));
                        }
                        if (nondndcount > 0)
                        {
                            returnElements.Add(new KeyValuePair<string, string>(actualFileName2 + "_" + nondndindex,
                            "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + xElementnondnd.ToString() +
                            "</root>"));
                        }
                    }
                }
                if (NoCheck.strNonDndNumbers.Count() == 0 || Regex.IsMatch(sender, @"^[a-zA-Z]+$"))
                {
                    cieledvalue = (int)Math.Ceiling((double)mobRows.Count / (double)(_configuration["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(_configuration["CampaignXMLGeneratedCount"]) : 25000));
                    var actualFileName2 = Path.GetFileNameWithoutExtension(fileName);
                    fileNames.Clear();
                    for (var i = 0; i < cieledvalue; i++)
                    {
                        fileNames.Add(actualFileName2 + "_" + i);
                    }
                }
                else
                {
                    cieledvalue = (int)Math.Ceiling((double)NoCheck.strNonDndNumbers.Count / (double)(_configuration["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(_configuration["CampaignXMLGeneratedCount"]) : 25000));
                    var actualFileName2 = Path.GetFileNameWithoutExtension(fileName);
                    fileNames.Clear();
                    for (var i = 0; i < cieledvalue; i++)
                    {
                        fileNames.Add(actualFileName2 + "_" + i);
                    }
                }

                if (!isCustome)
                {
                    if (!Regex.IsMatch(sender, @"^[a-zA-Z]+$"))
                    {
                        currentItreation = 0;
                        //HashSet<string> noche = new HashSet<string>(NoCheck.strDndNumbers.Select(s => s.DNDNumbers).Skip(currentItreation).Take(itrations).ToList());
                        HashSet<string> nocheNonDND = new HashSet<string>(NoCheck.strNonDndNumbers.Select(s => s.NonDNDNumbers).Skip(currentItreation).Take(itrations).ToList());
                        var mob3 = string.Join(",91", nocheNonDND);
                        mob3 = "91" + mob3;
                        if (nocheNonDND.Count > 0)
                        {
                            foreach (var t in fileNames)
                            {
                                xElement.Clear();
                                returnElements.Add(new KeyValuePair<string, string>(t,
                                    "<root iscustome='" + (isCustome == true ? "true" : "false") +
                                    "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                        WebUtility.HtmlEncode(sender) +
                                        "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                                            "' mobile=''><mobile>" + mob3 + "</mobile></sendsms></root>"));
                                currentItreation = currentItreation + itrations;
                            }
                        }
                    }
                    else
                    {
                        if (resultSet.Rows.Count > 0)
                        {

                            foreach (var t in fileNames)
                            {
                                xElement.Clear();
                                // var mobileNumbers = string.Join(",", mobileColumn.Skip(currentItreation).Take(itrations).ToList());
                                var Duplicates = resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Where(g => g.Count() > 1).SelectMany(ss => ss.Skip(1)).Select(s => s[mobColumnName].ToString()).ToList();
                                mobRows = resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First()).ToList();

                                var mob = (Duplicates.Any()) ? mobRows.Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).Skip(currentItreation).Take(itrations).ToList() : resultSet.Rows.Cast<DataRow>().Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).Skip(currentItreation).Take(itrations).ToList();

                                var mob3 = string.Join(",91", mob);
                                mob3 = "91" + mob3;

                                //var mobileNumbers = string.Join(",", (mobileColumn as List<string>).Skip(currentItreation).Take(itrations).ToList());
                                returnElements.Add(new KeyValuePair<string, string>(t,
                                    "<root iscustome='" + (isCustome == true ? "true" : "false") +
                                    "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId +
                                    "' sender='" + WebUtility.HtmlEncode(sender) +
                                    "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                                    "' mobile=''><mobile>" + mob3 + "</mobile></sendsms></root>"));
                                currentItreation = currentItreation + itrations;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Logger.ErrorFormat("While processing xls throuing fatal error :: {0} ", ex.StackTrace);
                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    var extension = Path.GetExtension(fileName);
                    extension = extension.Equals(".xls") ? ".xlsx" : ".xls";
                    IExcelDataReader excelReader = null;
                    switch (extension)
                    {
                        case ".xls":
                            /*  Reading from a binary Excel file ('97-2003 format; *.xls)   */
                            excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                            break;
                        case ".xlsx":
                            /*  Reading from a OpenXml Excel file (2007 format; *.xlsx) */
                            excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                            //result = GetFileDataFromFormatedExcel(stream);
                            break;
                    }
                    /*  DataSet - Create column names from first row    */
                    excelReader.IsFirstRowAsColumnNames = true;
                    result = excelReader.AsDataSet();
                    excelReader.Close();
                    dynamic mobileColumn;
                    List<DataRow> mobRows = new List<DataRow>();
                    mobRows = resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First()).ToList();
                    var Dup = resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Where(g => g.Count() > 1).SelectMany(ss => ss.Skip(1)).Select(s => s[mobColumnName].ToString()).ToList();
                    var cieledvalue = 0;
                    cieledvalue = (int)Math.Ceiling((double)Dup.Count / (double)(_configuration["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(_configuration["CampaignXMLGeneratedCount"]) : 25000));
                    var actualFileName = Path.GetFileNameWithoutExtension(fileName);
                    fileNames.Clear();
                    for (var i = 0; i < cieledvalue; i++)
                    {
                        fileNames.Add(actualFileName + "_duplicate_" + i);
                    }
                    var itrations = _configuration["CampaignXMLGeneratedCount"]?.ToString() != null ? Convert.ToInt32(_configuration["CampaignXMLGeneratedCount"]) : 25000; //(int)Math.Ceiling((double)mobileColumn.Count / (double)25000);//fileNames.Count);
                    var currentItreation = 0;
                    StringBuilder xElement = new StringBuilder();
                    if (!AllowDuplicates)
                    {


                        //var t = fileNames.FirstOrDefault() + "_duplicate";
                        var InvalidMobsCollection = new List<string>();
                        var emptyMobileNo =
                            resultSet.Rows.Cast<DataRow>()
                               .Where(w => string.IsNullOrWhiteSpace(w[mobColumnName].ToString()))
                               .ToList();
                        if (emptyMobileNo.Any())
                        {
                            InvalidMobsCollection.AddRange(emptyMobileNo.Select(s => s[mobColumnName].ToString()));
                            emptyMobileNo.ForEach(f => resultSet.Rows.Remove(f));
                        }
                        var Duplicates = resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Where(g => g.Count() > 1).SelectMany(ss => ss.Skip(1)).Select(s => s[mobColumnName].ToString()).ToList();
                        if (Duplicates.Any())
                        {

                            //Indian Synapse
                            if (conCode != null && conCode == 91)
                            {
                                for (int i = 0; i < Duplicates.Count; i++)
                                {
                                    if (Duplicates[i].Length == conMobLength)
                                    {
                                        Duplicates[i] = Duplicates[i].Replace(Duplicates[i], (conCode + Duplicates[i]));
                                    }
                                }
                            }
                            if (!isCustome)
                            {
                                xElement.Clear();
                                foreach (var t in fileNames)
                                {
                                    var mobilJoinedString = string.Join(",", Duplicates);
                                    returnElements.Add(new KeyValuePair<string, string>(t,
                                        "<root iscustome='" + (isCustome == true ? "true" : "false") +
                                        "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                        WebUtility.HtmlEncode(sender) +
                                        "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                                        "' mobile=''><mobile>" + mobilJoinedString + "</mobile></sendsms></root>"));
                                    currentItreation = currentItreation + itrations;
                                }
                            }
                            else
                            {
                                var lid = languageId;
                                languageId = lid;
                                var messageTemplates = IEnumerableExtension.StringBetween(message, "<$", "$>");
                                foreach (var t in fileNames)
                                {
                                    xElement.Clear();
                                    // var rows = result.Tables[0].Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First()).ToList();
                                    var rows = resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Where(g => g.Count() > 1).SelectMany(ss => ss.Skip(1)).Distinct().ToList();
                                    var el = from row in rows
                                             let msg //= message
                                             = messageTemplates.Aggregate(message, (current, item) => current.Replace("<$" + item + "$>", (row[item] != null && row[item].ToString() != "") ? row[item].ToString() : ""))
                                             // let ccount = msg.CreditsCountValidation(languageId)
                                             let ccountlid = msg.validateMessage(lid).Split(',')
                                             let ccount = ccountlid[0]

                                             select "<sendsms ccount='" + ccount + "' userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId +
                                             "' sender='" + WebUtility.HtmlEncode(sender) +
                                             "' language='" + ((Convert.ToInt32(ccountlid[1]) == 2) ? 8 : IsHavingAttheRate(msg)) +
                                             "' message='" + WebUtility.HtmlEncode(msg).Replace("\n", "&#10;") +
                                             "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) +
                                             "'></sendsms>";

                                    returnElements.Add(new KeyValuePair<string, string>(t,
                                        "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + string.Join("", el) +
                                        "</root>"));
                                }
                            }

                        }
                        mobileColumn = (Duplicates.Any()) ? mobRows.Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).ToList() : resultSet.Rows.Cast<DataRow>().Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).ToList();
                    }
                    else
                    {
                        mobileColumn = resultSet.Rows.Cast<DataRow>().Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).ToList();

                    }


                    //var mobilelistmob = string.Join(",", new HashSet<string>(resultSet.ToList<MobileNos>().Select(s => s.MobileNo)));
                    var NoCheck = DndNumberCheck1(resultSet, mobColumnName);

                    DataTable dnd = new DataTable();
                    DataTable nondnd = new DataTable();

                    HashSet<string> dndcheck = new HashSet<string>(NoCheck.strDndNumbers.Select(s => s.DNDNumbers).ToList());
                    HashSet<string> nondndcheck = new HashSet<string>(NoCheck.strNonDndNumbers.Select(s => s.NonDNDNumbers).ToList());
                    if (isCustome)
                    {
                        dnd = resultSet.Clone();
                        nondnd = resultSet.Clone();

                        foreach (DataRow drtableOld in resultSet.Rows)
                        {
                            if (dndcheck.Contains(drtableOld[mobColumnName]))
                            {
                                dnd.ImportRow(drtableOld);
                            }
                            if (nondndcheck.Contains(drtableOld[mobColumnName]))
                            {
                                nondnd.ImportRow(drtableOld);
                            }
                        }
                    }

                    cieledvalue = (int)Math.Ceiling((double)NoCheck.strDndNumbers.Count / (double)(_configuration["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(_configuration["CampaignXMLGeneratedCount"]) : 25000));
                    var actualFileName1 = Path.GetFileNameWithoutExtension(fileName);
                    fileNames.Clear();
                    for (var i = 0; i < cieledvalue; i++)
                    {
                        fileNames.Add(actualFileName1 + "_dnd_" + i);
                    }

                    if (_configuration["IndianSynapse"] == "true" && !Regex.IsMatch(sender, @"^[a-zA-Z]+$"))
                    {
                        currentItreation = 0;
                        if (!isCustome)
                        {
                            foreach (var t in fileNames)
                            {
                                xElement.Clear();
                                HashSet<string> noche = new HashSet<string>(NoCheck.strDndNumbers.Select(s => s.DNDNumbers).Skip(currentItreation).Take(itrations).ToList());
                                var mob2 = string.Join(",91", noche);
                                mob2 = "91" + mob2;

                                if (noche.Count > 0)
                                {
                                    returnElements.Add(new KeyValuePair<string, string>(t,
                                        "<root iscustome='" + (isCustome == true ? "true" : "false") +
                                        "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                            WebUtility.HtmlEncode(sender) +
                                            "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                                             "' mobile=''><mobile>" + mob2 + "</mobile></sendsms></root>"));
                                    currentItreation = currentItreation + itrations;

                                }
                            }
                        }
                        else
                        {
                            var messageTemplates = IEnumerableExtension.StringBetween(message, "<$", "$>");
                            foreach (var t in fileNames)
                            {
                                //  var ccount = msg.validateMessage(lid);
                                var lid = languageId;
                                languageId = lid;
                                var rows = new List<DataRow>();
                                rows = (AllowDuplicates) ? dnd.Rows.Cast<DataRow>().Skip(currentItreation).Take(itrations).ToList() :
                                dnd.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First())
                                                .Skip(currentItreation)
                                                .Take(itrations)
                                                .ToList();

                                var el = from row in rows
                                         let msg //= message
                                                 // = messageTemplates.Aggregate(message, (current, item) => current.Replace("<$" + item + "$>", row[item].ToString()))
                                           = messageTemplates.Aggregate(message, (current, item) => current.Replace("<$" + item + "$>", (row[item] != null && row[item].ToString() != "") ? row[item].ToString() : ""))
                                         //  let ccount=msg.CreditsCountValidation(languageId)
                                         let ccountlid = msg.validateMessage(lid).Split(',')
                                         let ccount = ccountlid[0]
                                         select "<sendsms ccount='" + ccount + "' userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId +
                                         "' sender='" + WebUtility.HtmlEncode(sender) + "' language='" + ((Convert.ToInt32(ccountlid[1]) == 2) ? 8 : IsHavingAttheRate(msg)) +
                                         "' message='" + WebUtility.HtmlEncode(msg).Replace("\n", "&#10;") +
                                         "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) +
                                         "'></sendsms>";

                                returnElements.Add(new KeyValuePair<string, string>(t,
                                    "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + string.Join("", el) +
                                    "</root>"));
                                currentItreation = currentItreation + itrations;
                            }
                        }
                    }

                    if (NoCheck.strNonDndNumbers.Count() == 0 || Regex.IsMatch(sender, @"^[a-zA-Z]+$"))
                    {
                        cieledvalue = (int)Math.Ceiling((double)mobRows.Count / (double)(_configuration["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(_configuration["CampaignXMLGeneratedCount"]) : 25000));
                        var actualFileName2 = Path.GetFileNameWithoutExtension(fileName);
                        fileNames.Clear();
                        for (var i = 0; i < cieledvalue; i++)
                        {
                            fileNames.Add(actualFileName2 + "_" + i);
                        }
                    }
                    else
                    {
                        cieledvalue = (int)Math.Ceiling((double)NoCheck.strNonDndNumbers.Count / (double)(_configuration["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(_configuration["CampaignXMLGeneratedCount"]) : 25000));
                        var actualFileName2 = Path.GetFileNameWithoutExtension(fileName);
                        fileNames.Clear();
                        for (var i = 0; i < cieledvalue; i++)
                        {
                            fileNames.Add(actualFileName2 + "_" + i);
                        }
                    }

                    if (!isCustome)
                    {
                        if (!Regex.IsMatch(sender, @"^[a-zA-Z]+$"))
                        {
                            currentItreation = 0;
                            //HashSet<string> noche = new HashSet<string>(NoCheck.strDndNumbers.Select(s => s.DNDNumbers).Skip(currentItreation).Take(itrations).ToList());
                            HashSet<string> nocheNonDND = new HashSet<string>(NoCheck.strNonDndNumbers.Select(s => s.NonDNDNumbers).Skip(currentItreation).Take(itrations).ToList());
                            var mob3 = string.Join(",91", nocheNonDND);
                            mob3 = "91" + mob3;
                            if (nocheNonDND.Count > 0)
                            {
                                foreach (var t in fileNames)
                                {
                                    xElement.Clear();
                                    returnElements.Add(new KeyValuePair<string, string>(t,
                                        "<root iscustome='" + (isCustome == true ? "true" : "false") +
                                        "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                                            WebUtility.HtmlEncode(sender) +
                                            "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                                                "' mobile=''><mobile>" + mob3 + "</mobile></sendsms></root>"));
                                    currentItreation = currentItreation + itrations;
                                }
                            }
                        }
                        else
                        {
                            if (resultSet.Rows.Count > 0)
                            {

                                foreach (var t in fileNames)
                                {
                                    xElement.Clear();
                                    // var mobileNumbers = string.Join(",", mobileColumn.Skip(currentItreation).Take(itrations).ToList());
                                    var Duplicates = resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Where(g => g.Count() > 1).SelectMany(ss => ss.Skip(1)).Select(s => s[mobColumnName].ToString()).ToList();
                                    mobRows = resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First()).ToList();

                                    var mob = (Duplicates.Any()) ? mobRows.Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).Skip(currentItreation).Take(itrations).ToList() : resultSet.Rows.Cast<DataRow>().Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).Skip(currentItreation).Take(itrations).ToList();

                                    var mob3 = string.Join(",91", mob);
                                    mob3 = "91" + mob3;

                                    //var mobileNumbers = string.Join(",", (mobileColumn as List<string>).Skip(currentItreation).Take(itrations).ToList());
                                    returnElements.Add(new KeyValuePair<string, string>(t,
                                        "<root iscustome='" + (isCustome == true ? "true" : "false") +
                                        "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId +
                                        "' sender='" + WebUtility.HtmlEncode(sender) +
                                        "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                                        "' mobile=''><mobile>" + mob3 + "</mobile></sendsms></root>"));
                                    currentItreation = currentItreation + itrations;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!Regex.IsMatch(sender, @"^[a-zA-Z]+$"))
                        {
                            //StringBuilder xElement = new StringBuilder();
                            currentItreation = 0;
                            var messageTemplates = IEnumerableExtension.StringBetween(message, "<$", "$>");
                            foreach (var t in fileNames)
                            {
                                //  var ccount = msg.validateMessage(lid);
                                var lid = languageId;
                                languageId = lid;
                                var rows = new List<DataRow>();
                                rows = (AllowDuplicates) ? nondnd.Rows.Cast<DataRow>().Skip(currentItreation).Take(itrations).ToList() :
                                nondnd.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First())
                                                .Skip(currentItreation)
                                                .Take(itrations)
                                                .ToList();

                                var el = from row in rows
                                         let msg //= message
                                                 // = messageTemplates.Aggregate(message, (current, item) => current.Replace("<$" + item + "$>", row[item].ToString()))
                                           = messageTemplates.Aggregate(message, (current, item) => current.Replace("<$" + item + "$>", (row[item] != null && row[item].ToString() != "") ? row[item].ToString() : ""))
                                         //  let ccount=msg.CreditsCountValidation(languageId)
                                         let ccountlid = msg.validateMessage(lid).Split(',')
                                         let ccount = ccountlid[0]
                                         select "<sendsms ccount='" + ccount + "' userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId +
                                         "' sender='" + WebUtility.HtmlEncode(sender) + "' language='" + ((Convert.ToInt32(ccountlid[1]) == 2) ? 8 : IsHavingAttheRate(msg)) +
                                         "' message='" + WebUtility.HtmlEncode(msg).Replace("\n", "&#10;") +
                                         "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) +
                                         "'></sendsms>";

                                returnElements.Add(new KeyValuePair<string, string>(t,
                                    "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + string.Join("", el) +
                                    "</root>"));
                                currentItreation = currentItreation + itrations;
                            }
                            #region "Commented Lines"
                            //var messageTemplates = IEnumerableExtension.StringBetween(message, "<$", "$>");
                            ////xElement.Clear();

                            //HashSet<string> nocheNonDND = new HashSet<string>(NoCheck.strNonDndNumbers.Select(s => s.NonDNDNumbers));

                            //if (nocheNonDND.Count > 0)
                            //{
                            //    foreach (var t in fileNames)
                            //    {
                            //        xElement.Clear();
                            //        HashSet<string> nocheDND1 = new HashSet<string>(NoCheck.strNonDndNumbers.Select(s => s.NonDNDNumbers).Skip(currentItreation).Take(itrations).ToList());
                            //        foreach (var smobileno in nocheDND1)
                            //        {
                            //           // string searchExpression = string.Format("{0} = ('{1}')",mobColumnName, smobileno.ToString()); //mobColumnName + "='" + smobileno + "'";
                            //            string searchExpression = string.Format("Convert({0}, 'System.String') = '{1}'", mobColumnName, smobileno.ToString()); //mobColumnName + "='" + smobileno + "'"; 
                            //            try
                            //            {
                            //                DataRow[] foundRows = resultSet.Select(searchExpression);
                            //                if (AllowDuplicates)
                            //                {
                            //                    foreach (DataRow row in foundRows)
                            //                    {
                            //                        var msg = message;
                            //                        foreach (var item in messageTemplates)
                            //                        {
                            //                            msg = msg.Replace("<$" + item + "$>", (row[item] != null && row[item] != "") ? row[item].ToString() : "");
                            //                        }
                            //                        var lid = languageId;
                            //                        var ccountlid = msg.validateMessage(lid).Split(',');
                            //                        var ccount = ccountlid[0];
                            //                        lid = Convert.ToInt32(ccountlid[1]);
                            //                        languageId = lid;
                            //                        msg = WebUtility.HtmlEncode(msg).Replace("\n", "&#10;");

                            //                        if (conCode != null && conCode == 91)
                            //                        {
                            //                            xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                            //                            WebUtility.HtmlEncode(sender) +
                            //                            "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                            //                            "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) + "'></sendsms>");
                            //                        }
                            //                        else
                            //                        {
                            //                            xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                            //                            WebUtility.HtmlEncode(sender) +
                            //                            "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                            //                            "' mobile='" + WebUtility.HtmlEncode(row[mobColumnName].ToString()) + "'></sendsms>");
                            //                        }
                            //                    }
                            //                }

                            //                else
                            //                {
                            //                    var row = foundRows[0];
                            //                    var msg = message;
                            //                    foreach (var item in messageTemplates)
                            //                    {
                            //                        msg = msg.Replace("<$" + item + "$>", (row[item] != null && row[item] != "") ? row[item].ToString() : "");
                            //                    }
                            //                    var lid = languageId;
                            //                    var ccountlid = msg.validateMessage(lid).Split(',');
                            //                    var ccount = ccountlid[0];
                            //                    lid = Convert.ToInt32(ccountlid[1]);
                            //                    languageId = lid;
                            //                    msg = WebUtility.HtmlEncode(msg).Replace("\n", "&#10;");

                            //                    if (conCode != null && conCode == 91)
                            //                    {
                            //                        xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                            //                        WebUtility.HtmlEncode(sender) +
                            //                        "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                            //                        "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) + "'></sendsms>");
                            //                    }
                            //                    else
                            //                    {
                            //                        xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                            //                        WebUtility.HtmlEncode(sender) +
                            //                        "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                            //                        "' mobile='" + WebUtility.HtmlEncode(row[mobColumnName].ToString()) + "'></sendsms>");
                            //                    }
                            //                }
                            //            }
                            //            catch (Exception ex)
                            //            {
                            //                Logger.ErrorFormat("Special characters in mobile no :: {0}, exception :: {1}", smobileno,ex.Message);
                            //            };
                            //        }
                            //        returnElements.Add(new KeyValuePair<string, string>(t,
                            //                    "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + xElement.ToString() +
                            //                    "</root>"));
                            //        currentItreation = currentItreation + itrations;
                            //    }

                            //    //var cieledvalue = 0;
                            //    //cieledvalue = (int)Math.Ceiling((double)nocheNonDND.Count / (double)(_configuration["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(_configuration["CampaignXMLGeneratedCount"]) : 25000));
                            //    //var actualFileName = Path.GetFileNameWithoutExtension(fileName);
                            //    //fileNames.Clear();
                            //    //for (var i = 0; i < cieledvalue; i++)
                            //    //{
                            //    //    fileNames.Add(actualFileName + "_" + i);
                            //    //}
                            //}
                            #endregion
                        }
                        else
                        {
                            var messageTemplates = IEnumerableExtension.StringBetween(message, "<$", "$>");
                            var lid = languageId;
                            languageId = lid;
                            foreach (var t in fileNames)
                            {
                                xElement.Clear();
                                var rows = new List<DataRow>();
                                rows = (AllowDuplicates) ? resultSet.Rows.Cast<DataRow>().Skip(currentItreation).Take(itrations).ToList() :
                                resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First())
                                                .Skip(currentItreation)
                                                .Take(itrations)
                                                .ToList();
                                if (rows.Count > 0)
                                {
                                    var el = from row in rows
                                             let msg
                                             = messageTemplates.Aggregate(message, (current, item) => current.Replace("<$" + item + "$>", (row[item] != null && row[item] != "") ? row[item].ToString() : ""))
                                             let ccountlid = msg.validateMessage(lid).Split(',')
                                             let ccount = ccountlid[0]
                                             select "<sendsms ccount='" + ccount + "' userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId +
                                             "' sender='" + WebUtility.HtmlEncode(sender) + "' language='" + ((Convert.ToInt32(ccountlid[1]) == 2) ? 8 : IsHavingAttheRate(msg)) +
                                             "' message='" + WebUtility.HtmlEncode(msg).Replace("\n", "&#10;") +
                                             "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) +
                                             "'></sendsms>";

                                    returnElements.Add(new KeyValuePair<string, string>(t,
                                        "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + string.Join("", el) +
                                        "</root>"));
                                    currentItreation = currentItreation + itrations;
                                }
                            }
                        }
                        return returnElements;
                    }
                    #region "Commented Lines"
                    //cieledvalue = (int)Math.Ceiling((double)NoCheck.strDndNumbers.Count / (double)(_configuration["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(_configuration["CampaignXMLGeneratedCount"]) : 25000));
                    //var actualFileName1 = Path.GetFileNameWithoutExtension(fileName);
                    //fileNames.Clear();
                    //for (var i = 0; i < cieledvalue; i++)
                    //{
                    //    fileNames.Add(actualFileName1 + "_dnd_" + i);
                    //}

                    ////var Dcsv = fileNames.FirstOrDefault() + "_dnd";
                    //List<DataRow> mobRowss = new List<DataRow>();
                    //if (_configuration["IndianSynapse"] == "true" && !Regex.IsMatch(sender, @"^[a-zA-Z]+$"))
                    //{
                    //    currentItreation = 0;
                    //    if (!isCustome)
                    //    {
                    //        xElement.Clear();
                    //        //int filecount = 0;

                    //        foreach (var t in fileNames)
                    //        {
                    //            HashSet<string> noche = new HashSet<string>(NoCheck.strDndNumbers.Select(s => s.DNDNumbers).Skip(currentItreation).Take(itrations).ToList());
                    //            var mob2 = string.Join(",91", noche);
                    //            mob2 = "91" + mob2;

                    //            //HashSet<string> nocheNonDND = new HashSet<string>(NoCheck.strNonDndNumbers.Select(s => s.NonDNDNumbers));
                    //            //var mob3 = string.Join(",91", nocheNonDND);
                    //            //mob3 = "91" + mob3;
                    //            if (noche.Count > 0)
                    //            {
                    //                //foreach (var t in fileNames)
                    //                //{
                    //                returnElements.Add(new KeyValuePair<string, string>(t,
                    //                    "<root iscustome='" + (isCustome == true ? "true" : "false") +
                    //                    "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                    //                        WebUtility.HtmlEncode(sender) +
                    //                        "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                    //                         "' mobile=''><mobile>" + mob2 + "</mobile></sendsms></root>"));
                    //                currentItreation = currentItreation + itrations;
                    //                //var cieledvalue = 0;
                    //                //cieledvalue = (int)Math.Ceiling((double)noche.Count / (double)(_configuration["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(_configuration["CampaignXMLGeneratedCount"]) : 25000));
                    //                //var actualFileName = Path.GetFileNameWithoutExtension(fileName);
                    //                //fileNames.Clear();
                    //                //for (var i = 0; i < cieledvalue; i++)
                    //                //{
                    //                //    fileNames.Add(actualFileName + "_" + i);
                    //                //}
                    //            }
                    //            //filecount += 1;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        var messageTemplates = IEnumerableExtension.StringBetween(message, "<$", "$>");
                    //        xElement.Clear();

                    //        HashSet<string> noche = new HashSet<string>(NoCheck.strDndNumbers.Select(s => s.DNDNumbers));
                    //        //var mob2 = string.Join(",91", noche);
                    //        //mob2 = "91" + mob2;

                    //        //HashSet<string> nocheNonDND = new HashSet<string>(NoCheck.strNonDndNumbers.Select(s => s.NonDNDNumbers).Distinct());
                    //        //var mob3 = string.Join(",91", nocheNonDND);
                    //        //mob3 = "91" + mob3;
                    //        if (noche.Count > 0)
                    //        {
                    //            foreach (var t in fileNames)
                    //            {
                    //                HashSet<string> nochee = new HashSet<string>(NoCheck.strDndNumbers.Select(s => s.DNDNumbers).Skip(currentItreation).Take(itrations).ToList());
                    //                foreach (var smobileno in nochee)
                    //                {
                    //                    string searchExpression = mobColumnName + "='" + smobileno + "'";

                    //                    DataRow[] foundRows = resultSet.Select(searchExpression);
                    //                    if (AllowDuplicates)
                    //                    {
                    //                        foreach (DataRow row in foundRows)
                    //                        {
                    //                            var msg = message;
                    //                            foreach (var item in messageTemplates)
                    //                            {
                    //                                msg = msg.Replace("<$" + item + "$>", (row[item] != null && row[item] != "") ? row[item].ToString() : "");
                    //                            }
                    //                            var lid = languageId;
                    //                            var ccountlid = msg.validateMessage(lid).Split(',');
                    //                            var ccount = ccountlid[0];
                    //                            lid = Convert.ToInt32(ccountlid[1]);
                    //                            languageId = lid;
                    //                            msg = WebUtility.HtmlEncode(msg).Replace("\n", "&#10;");

                    //                            if (conCode != null && conCode == 91)
                    //                            {
                    //                                xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                    //                                WebUtility.HtmlEncode(sender) +
                    //                                "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                    //                                "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) + "'></sendsms>");
                    //                            }
                    //                            else
                    //                            {
                    //                                xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                    //                                WebUtility.HtmlEncode(sender) +
                    //                                "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                    //                                "' mobile='" + WebUtility.HtmlEncode(row[mobColumnName].ToString()) + "'></sendsms>");
                    //                            }
                    //                        }
                    //                    }
                    //                    else
                    //                    {
                    //                        var row = foundRows[0];
                    //                        var msg = message;
                    //                        foreach (var item in messageTemplates)
                    //                        {
                    //                            msg = msg.Replace("<$" + item + "$>", (row[item] != null && row[item] != "") ? row[item].ToString() : "");
                    //                        }
                    //                        var lid = languageId;
                    //                        var ccountlid = msg.validateMessage(lid).Split(',');
                    //                        var ccount = ccountlid[0];
                    //                        lid = Convert.ToInt32(ccountlid[1]);
                    //                        languageId = lid;
                    //                        msg = WebUtility.HtmlEncode(msg).Replace("\n", "&#10;");

                    //                        if (conCode != null && conCode == 91)
                    //                        {
                    //                            xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                    //                            WebUtility.HtmlEncode(sender) +
                    //                            "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                    //                            "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) + "'></sendsms>");
                    //                        }
                    //                        else
                    //                        {
                    //                            xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                    //                            WebUtility.HtmlEncode(sender) +
                    //                            "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                    //                            "' mobile='" + WebUtility.HtmlEncode(row[mobColumnName].ToString()) + "'></sendsms>");
                    //                        }
                    //                    }
                    //                }
                    //                returnElements.Add(new KeyValuePair<string, string>(t,
                    //                        "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + xElement.ToString() +
                    //                        "</root>"));
                    //                currentItreation = currentItreation + itrations;
                    //            }
                    //        }
                    //    }
                    //}
                    ////    string str = "";
                    ////    dynamic DndNumbersCheck;
                    ////    foreach (DataRow dr in resultSet.Rows)
                    ////    {
                    ////        str = str + Convert.ToString(dr["MobileNo"]) + ",";
                    ////    }

                    ////    var NoCheck = DndNumberCheck(str);

                    ////    if (!isCustome)
                    ////    {
                    ////        HashSet<string> noche = new HashSet<string>(NoCheck.strDndNumbers.Select(s => s.DNDNumbers));
                    ////        DataTable dndrows = new DataTable();
                    ////        dndrows = resultSet.Clone();

                    ////        if (noche.Count > 0)
                    ////        {
                    ////            foreach (var item in noche)
                    ////            {
                    ////                string searchExpression = mobColumnName + "='" + item + "'";

                    ////                DataRow[] foundRows = resultSet.Select(searchExpression);

                    ////                foreach (DataRow frow in foundRows)
                    ////                {
                    ////                    dndrows.Rows.Add(frow.ItemArray);
                    ////                    resultSet.Rows.Remove(frow);
                    ////                }
                    ////                resultSet.AcceptChanges();
                    ////                dndrows.AcceptChanges();
                    ////            }

                    ////            StringBuilder xElement = new StringBuilder();
                    ////            var mobilJoinedString = string.Join(",", noche);

                    ////            var str6 = "";
                    ////            foreach (var item in noche)
                    ////            {
                    ////                str6 = str6 + ("91" + item) + ",";
                    ////            }
                    ////            returnElements.Add(new KeyValuePair<string, string>(Dcsv,
                    ////            "<root iscustome='" + (isCustome == true ? "true" : "false") +
                    ////            "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                    ////                WebUtility.HtmlEncode(sender) +
                    ////                "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                    ////                 "' mobile=''><mobile>" + str6.TrimEnd(',') + " </mobile></sendsms></root>"));

                    ////            mobRowss = dndrows.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First()).ToList();
                    ////            var cieledvalue = 0;
                    ////            cieledvalue = (int)Math.Ceiling((double)mobRowss.Count / (double)(_configuration["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(_configuration["CampaignXMLGeneratedCount"]) : 25000));
                    ////            var actualFileName = Path.GetFileNameWithoutExtension(fileName);
                    ////            fileNames.Clear();
                    ////            for (var i = 0; i < cieledvalue; i++)
                    ////            {
                    ////                fileNames.Add(actualFileName + "_" + i);
                    ////            }
                    ////        }
                    ////    }
                    ////    else
                    ////    {
                    ////        HashSet<string> noche = new HashSet<string>(NoCheck.strDndNumbers.Select(s => s.DNDNumbers));
                    ////        DataTable dndrows = new DataTable();
                    ////        dndrows = resultSet.Clone();

                    ////        if (noche.Count > 0)
                    ////        {
                    ////            foreach (var item in noche)
                    ////            {
                    ////                string searchExpression = mobColumnName + "='" + item + "'";

                    ////                DataRow[] foundRows = resultSet.Select(searchExpression);
                    ////                if (AllowDuplicates == false)
                    ////                {
                    ////                    dndrows.Rows.Add(foundRows[0].ItemArray);
                    ////                    foreach (DataRow frow in foundRows)
                    ////                        resultSet.Rows.Remove(frow);
                    ////                }
                    ////                else
                    ////                {
                    ////                    foreach (DataRow frow in foundRows)
                    ////                    {
                    ////                        dndrows.Rows.Add(frow.ItemArray);
                    ////                        resultSet.Rows.Remove(frow);
                    ////                    }
                    ////                }
                    ////                resultSet.AcceptChanges();
                    ////                dndrows.AcceptChanges();

                    ////            }

                    ////            var messageTemplates = IEnumerableExtension.StringBetween(message, "<$", "$>");
                    ////            var lid = languageId;
                    ////            languageId = lid;
                    ////            var rows = dndrows.Rows.Cast<DataRow>().ToList();
                    ////            var el = from row in rows
                    ////                     let msg //= message
                    ////                     = messageTemplates.Aggregate(message, (current, item) => current.Replace("<$" + item + "$>", (row[item] != null && row[item] != "") ? row[item].ToString() : ""))
                    ////                     let ccountlid = msg.validateMessage(lid).Split(',')
                    ////                     let ccount = ccountlid[0]
                    ////                     select "<sendsms ccount='" + ccount + "' userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId +
                    ////                     "' sender='" + WebUtility.HtmlEncode(sender) +
                    ////                     "' language='" + ((Convert.ToInt32(ccountlid[1]) == 2) ? 8 : IsHavingAttheRate(msg)) +
                    ////                     "' message='" + WebUtility.HtmlEncode(msg).Replace("\n", "&#10;") +
                    ////                     "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) +
                    ////                     "'></sendsms>";

                    ////            returnElements.Add(new KeyValuePair<string, string>(Dcsv,
                    ////                "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + string.Join("", el) +
                    ////                "</root>"));


                    ////            mobRowss = dndrows.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First()).ToList();
                    ////            // mobRows = result.Tables[0].Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Where(g => g.Count() > 1).SelectMany(ss => ss.Skip(1)).Distinct().ToList();
                    ////            var cieledvalue = 0;
                    ////            cieledvalue = (int)Math.Ceiling((double)mobRowss.Count / (double)(_configuration["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(_configuration["CampaignXMLGeneratedCount"]) : 25000));
                    ////            var actualFileName = Path.GetFileNameWithoutExtension(fileName);
                    ////            fileNames.Clear();
                    ////            for (var i = 0; i < cieledvalue; i++)
                    ////            {
                    ////                fileNames.Add(actualFileName + "_" + i);
                    ////            }
                    ////        }
                    ////    }
                    ////}
                    //if (NoCheck.strNonDndNumbers.Count() == 0 || Regex.IsMatch(sender, @"^[a-zA-Z]+$"))
                    //{
                    //    cieledvalue = (int)Math.Ceiling((double)mobRows.Count / (double)(_configuration["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(_configuration["CampaignXMLGeneratedCount"]) : 25000));
                    //    var actualFileName2 = Path.GetFileNameWithoutExtension(fileName);
                    //    fileNames.Clear();
                    //    for (var i = 0; i < cieledvalue; i++)
                    //    {
                    //        fileNames.Add(actualFileName2 + "_" + i);
                    //    }
                    //}
                    //else
                    //{
                    //    cieledvalue = (int)Math.Ceiling((double)NoCheck.strNonDndNumbers.Count / (double)(_configuration["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(_configuration["CampaignXMLGeneratedCount"]) : 25000));
                    //    var actualFileName2 = Path.GetFileNameWithoutExtension(fileName);
                    //    fileNames.Clear();
                    //    for (var i = 0; i < cieledvalue; i++)
                    //    {
                    //        fileNames.Add(actualFileName2 + "_" + i);
                    //    }
                    //}
                    //if (!isCustome)
                    //{
                    //    if (!Regex.IsMatch(sender, @"^[a-zA-Z]+$"))
                    //    {
                    //        xElement.Clear();
                    //        currentItreation = 0;
                    //        //HashSet<string> noche = new HashSet<string>(NoCheck.strDndNumbers.Select(s => s.DNDNumbers).Skip(currentItreation).Take(itrations).ToList());
                    //        HashSet<string> nocheNonDND = new HashSet<string>(NoCheck.strNonDndNumbers.Select(s => s.NonDNDNumbers));
                    //        if (nocheNonDND.Count > 0)
                    //        {
                    //            foreach (var t in fileNames)
                    //            {
                    //                HashSet<string> nocheNonDND1 = new HashSet<string>(NoCheck.strNonDndNumbers.Select(s => s.NonDNDNumbers).Skip(currentItreation).Take(itrations).ToList());
                    //                var mob3 = string.Join(",91", nocheNonDND1);
                    //                mob3 = "91" + mob3;

                    //                returnElements.Add(new KeyValuePair<string, string>(t,
                    //                    "<root iscustome='" + (isCustome == true ? "true" : "false") +
                    //                    "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                    //                        WebUtility.HtmlEncode(sender) +
                    //                        "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                    //                         "' mobile=''><mobile>" + mob3 + "</mobile></sendsms></root>"));
                    //                currentItreation = currentItreation + itrations;

                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (resultSet.Rows.Count > 0)
                    //        {
                    //            foreach (var t in fileNames)
                    //            {
                    //                xElement.Clear();
                    //                var currentItreation1 = 0;
                    //                var Duplicates = resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Where(g => g.Count() > 1).SelectMany(ss => ss.Skip(1)).Select(s => s[mobColumnName].ToString()).ToList();
                    //                mobRows = resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First()).ToList();

                    //                var m = mobRows.Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).Skip(currentItreation).Take(itrations).ToList();
                    //                var r = resultSet.Rows.Cast<DataRow>().Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).Skip(currentItreation).Take(itrations).ToList();
                    //                if (m.Count == 0 || r.Count == 0)
                    //                {
                    //                    var mob = (Duplicates.Any()) ? mobRows.Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).Skip(currentItreation1).Take(itrations).ToList() : resultSet.Rows.Cast<DataRow>().Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).Skip(currentItreation1).Take(itrations).ToList();

                    //                    var mob3 = string.Join(",91", mob);
                    //                    mob3 = "91" + mob3;

                    //                    //var str4 = "";
                    //                    //foreach (var item in mob)
                    //                    //{
                    //                    //    str4 = str4 + ("91" + item) + ",";
                    //                    //}

                    //                    returnElements.Add(new KeyValuePair<string, string>(t,
                    //                    "<root iscustome='" + (isCustome == true ? "true" : "false") +
                    //                    "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                    //                        WebUtility.HtmlEncode(sender) +
                    //                        "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                    //                        "' mobile=''><mobile>" + mob3 + "</mobile></sendsms></root>"));
                    //                    currentItreation = currentItreation + itrations;
                    //                }
                    //                else
                    //                {
                    //                    var mob = (Duplicates.Any()) ? mobRows.Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).Skip(currentItreation).Take(itrations).ToList() : resultSet.Rows.Cast<DataRow>().Where(x => !string.IsNullOrWhiteSpace(x[mobColumnName].ToString())).Select(s => s[mobColumnName].ToString()).Skip(currentItreation).Take(itrations).ToList();
                    //                    var mob3 = string.Join(",91", mob);
                    //                    mob3 = "91" + mob3;
                    //                    //var str4 = "";
                    //                    //foreach (var item in mob)
                    //                    //{
                    //                    //    str4 = str4 + ("91" + item) + ",";
                    //                    //}
                    //                    returnElements.Add(new KeyValuePair<string, string>(t,
                    //                    "<root iscustome='" + (isCustome == true ? "true" : "false") +
                    //                    "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                    //                        WebUtility.HtmlEncode(sender) +
                    //                        "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                    //                        "' mobile=''><mobile>" + mob3 + "</mobile></sendsms></root>"));
                    //                    currentItreation = currentItreation + itrations;

                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    if (!Regex.IsMatch(sender, @"^[a-zA-Z]+$"))
                    //    {
                    //        currentItreation = 0;
                    //        var messageTemplates = IEnumerableExtension.StringBetween(message, "<$", "$>");
                    //        xElement.Clear();

                    //        HashSet<string> nocheNonDND = new HashSet<string>(NoCheck.strNonDndNumbers.Select(s => s.NonDNDNumbers).Distinct());

                    //        if (nocheNonDND.Count > 0)
                    //        {
                    //            HashSet<string> nocheDND1 = new HashSet<string>(NoCheck.strNonDndNumbers.Select(s => s.NonDNDNumbers).Distinct().Skip(currentItreation).Take(itrations).ToList());
                    //            foreach (var t in fileNames)
                    //            {
                    //                foreach (var smobileno in nocheDND1)
                    //                {
                    //                    string searchExpression = mobColumnName + "='" + smobileno + "'";

                    //                    DataRow[] foundRows = resultSet.Select(searchExpression);
                    //                    if (AllowDuplicates)
                    //                    {
                    //                        foreach (DataRow row in foundRows)
                    //                        {
                    //                            var msg = message;
                    //                            foreach (var item in messageTemplates)
                    //                            {
                    //                                msg = msg.Replace("<$" + item + "$>", (row[item] != null && row[item] != "") ? row[item].ToString() : "");
                    //                            }
                    //                            var lid = languageId;
                    //                            var ccountlid = msg.validateMessage(lid).Split(',');
                    //                            var ccount = ccountlid[0];
                    //                            lid = Convert.ToInt32(ccountlid[1]);
                    //                            languageId = lid;
                    //                            msg = WebUtility.HtmlEncode(msg).Replace("\n", "&#10;");

                    //                            if (conCode != null && conCode == 91)
                    //                            {
                    //                                xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                    //                                WebUtility.HtmlEncode(sender) +
                    //                                "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                    //                                "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) + "'></sendsms>");
                    //                            }
                    //                            else
                    //                            {
                    //                                xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                    //                                WebUtility.HtmlEncode(sender) +
                    //                                "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                    //                                "' mobile='" + WebUtility.HtmlEncode(row[mobColumnName].ToString()) + "'></sendsms>");
                    //                            }
                    //                        }
                    //                    }
                    //                    else
                    //                    {
                    //                        var row = foundRows[0];
                    //                        var msg = message;
                    //                        foreach (var item in messageTemplates)
                    //                        {
                    //                            msg = msg.Replace("<$" + item + "$>", (row[item] != null && row[item] != "") ? row[item].ToString() : "");
                    //                        }
                    //                        var lid = languageId;
                    //                        var ccountlid = msg.validateMessage(lid).Split(',');
                    //                        var ccount = ccountlid[0];
                    //                        lid = Convert.ToInt32(ccountlid[1]);
                    //                        languageId = lid;
                    //                        msg = WebUtility.HtmlEncode(msg).Replace("\n", "&#10;");

                    //                        if (conCode != null && conCode == 91)
                    //                        {
                    //                            xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                    //                            WebUtility.HtmlEncode(sender) +
                    //                            "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                    //                            "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) + "'></sendsms>");
                    //                        }
                    //                        else
                    //                        {
                    //                            xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                    //                            WebUtility.HtmlEncode(sender) +
                    //                            "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                    //                            "' mobile='" + WebUtility.HtmlEncode(row[mobColumnName].ToString()) + "'></sendsms>");
                    //                        }
                    //                    }
                    //                }
                    //                returnElements.Add(new KeyValuePair<string, string>(t,
                    //                           "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + xElement.ToString() +
                    //                           "</root>"));
                    //                currentItreation = currentItreation + itrations;
                    //            }

                    //            //var cieledvalue = 0;
                    //            //cieledvalue = (int)Math.Ceiling((double)nocheNonDND.Count / (double)(_configuration["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(_configuration["CampaignXMLGeneratedCount"]) : 25000));
                    //            //var actualFileName = Path.GetFileNameWithoutExtension(fileName);
                    //            //fileNames.Clear();
                    //            //for (var i = 0; i < cieledvalue; i++)
                    //            //{
                    //            //    fileNames.Add(actualFileName + "_" + i);
                    //            //}
                    //        }
                    //    }
                    //    else
                    //    {
                    //        var messageTemplates = IEnumerableExtension.StringBetween(message, "<$", "$>");
                    //        foreach (var t in fileNames)
                    //        {
                    //            xElement.Clear();
                    //            var rows = new List<DataRow>();
                    //            rows = (AllowDuplicates) ? resultSet.Rows.Cast<DataRow>().Skip(currentItreation).Take(itrations).ToList() :
                    //            resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First())
                    //                            .Skip(currentItreation)
                    //                            .Take(itrations)
                    //                            .ToList();
                    //            if (rows.Count > 0)
                    //            {
                    //                foreach (DataRow row in rows)
                    //                {
                    //                    var msg = message;
                    //                    foreach (var item in messageTemplates)
                    //                    {
                    //                        msg = msg.Replace("<$" + item + "$>", row[item].ToString());
                    //                    }
                    //                    var lid = languageId;
                    //                    var ccountlid = msg.validateMessage(lid).Split(',');
                    //                    var ccount = ccountlid[0];
                    //                    lid = Convert.ToInt32(ccountlid[1]);
                    //                    languageId = lid;
                    //                    msg = WebUtility.HtmlEncode(msg).Replace("\n", "&#10;");

                    //                    if (conCode != null && conCode == 91)
                    //                    {
                    //                        xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                    //                            WebUtility.HtmlEncode(sender) +
                    //                            "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                    //                            "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) + "'></sendsms>");
                    //                    }
                    //                    else
                    //                    {
                    //                        xElement.Append("<sendsms ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId + "' sender='" +
                    //                            WebUtility.HtmlEncode(sender) +
                    //                            "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(msg)) + "' message='" + (msg) +
                    //                            "' mobile='" + WebUtility.HtmlEncode(row[mobColumnName].ToString()) + "'></sendsms>");
                    //                    }
                    //                }

                    //                returnElements.Add(new KeyValuePair<string, string>(t,
                    //                    "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + xElement.ToString() +
                    //                    "</root>"));
                    //                currentItreation = currentItreation + itrations;
                    //            }
                    //        }
                    //    }
                    //}
                    ////if (resultSet != null)
                    ////{
                    ////    //mobileColumn = result.Tables[0].Rows.Cast<DataRow>().Select(s => s[mobColumnName].ToString()).ToList();
                    ////    if (conCode != null && conCode == 91)
                    ////    {
                    ////        for (int i = 0; i < mobileColumn.Count; i++)
                    ////        {
                    ////            if (mobileColumn[i].Length == conMobLength)
                    ////            {
                    ////                mobileColumn[i] = mobileColumn[i].Replace(mobileColumn[i], (conCode.ToString() + mobileColumn[i]));
                    ////            }
                    ////        }
                    ////    }

                    //    //var xElement = string.Empty;
                    //    //var itrations = (int)Math.Ceiling((double)mobileColumn.Count / (double)fileNames.Count);
                    //    //var currentItreation = 0;
                    //    //if (!isCustome)
                    //    //{
                    //    //    foreach (var t in fileNames)
                    //    //    {
                    //    //        xElement.Clear();
                    //    //        //  var mobileNumbers = string.Join(",", mobileColumn.Skip(currentItreation).Take(itrations).ToList());
                    //    //        var mobileNumbers = string.Join(",", (mobileColumn as List<string>).Skip(currentItreation).Take(itrations).ToList());
                    //    //        returnElements.Add(new KeyValuePair<string, string>(t,
                    //    //            "<root iscustome='" + (isCustome == true ? "true" : "false") +
                    //    //            "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'><sendsms userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId +
                    //    //            "' sender='" + WebUtility.HtmlEncode(sender) +
                    //    //            "' language='" + ((languageId == 2) ? 8 : IsHavingAttheRate(message)) + "' message='" + WebUtility.HtmlEncode(message).Replace("\n", "&#10;") +
                    //    //            "' mobile=''><mobile>" + mobileNumbers + "</mobile></sendsms></root>"));
                    //    //        currentItreation = currentItreation + itrations;
                    //    //    }
                    //    //}
                    //    //else
                    //    //{
                    //    //    var messageTemplates = IEnumerableExtension.StringBetween(message, "<$", "$>");
                    //    //    foreach (var t in fileNames)
                    //    //    {
                    //    //        xElement.Clear();
                    //    //        var rows = new List<DataRow>();
                    //    //        rows = (AllowDuplicates) ? resultSet.Rows.Cast<DataRow>().Skip(currentItreation).Take(itrations).ToList() :
                    //    //        resultSet.Rows.Cast<DataRow>().GroupBy(p => p[mobColumnName]).Select(s => s.First())
                    //    //                        .Skip(currentItreation)
                    //    //                        .Take(itrations)
                    //    //                        .ToList();
                    //    //        foreach (DataRow row in rows)
                    //    //        {

                    //    //            var msg = message;
                    //    //            //if (messageTemplates.Any())
                    //    //            //{

                    //    //            //}
                    //    //            var lid = languageId;
                    //    //            languageId = lid;
                    //    //            msg = messageTemplates.Aggregate(msg, (current, item) => current.Replace("<$" + item + "$>", row[item].ToString()));
                    //    //            var ccountlid = msg.validateMessage(lid).Split(',');
                    //    //            var ccount = ccountlid[0];
                    //    //            xElement += "<sendsms  ccount='" + ccount + "'  userid='" + userId + "' username='" + UserName + "' campainid='" + campaignId +
                    //    //                        "' sender='" +
                    //    //                        WebUtility.HtmlEncode(sender) +
                    //    //                        "' language='" + ((Convert.ToInt32(ccountlid[1]) == 2) ? 8 : IsHavingAttheRate(msg)) +
                    //    //                        "' message='" + WebUtility.HtmlEncode(msg).Replace("\n", "&#10;") +
                    //    //                        "' mobile='" + WebUtility.HtmlEncode(conCode == 91 ? (row[mobColumnName].ToString().Length == conMobLength ? (conCode.ToString() + row[mobColumnName].ToString()) : row[mobColumnName].ToString()) : row[mobColumnName].ToString()) +
                    //    //                        "'></sendsms>";
                    //    //        }
                    //    //        returnElements.Add(new KeyValuePair<string, string>(t,
                    //    //            "<root iscustome='" + (isCustome == true ? "true" : "false") + "' ispromotional='" + (isPromo == 0 ? "true" : "false") + "'  priority='" + (isPromo == 0 ? 1 : 3) + "'>" + xElement +
                    //    //            "</root>"));
                    //    //        currentItreation = currentItreation + itrations;
                    //    //    }
                    //    //}
                    //    return returnElements;
                    ////}
                    #endregion
                }
            }
            return returnElements;
        }
        public int IsHavingAttheRate(string value)
        {
            return value.Contains('@') || value.Contains('}') || value.Contains('{') ? 1 : 1;

        }
        public DataTable BuildCsvToTable(string filePath, string sheetName = "")
        {
            var dtCsv = new DataTable();
            try
            {
                var extension = Path.GetExtension(filePath);
                var result = new DataSet();
                if (extension.Equals(".xls") || extension.Equals(".xlsx"))
                {
                    using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        IExcelDataReader excelReader = null;
                        switch (extension)
                        {
                            case ".xls":
                                /*  Reading from a binary Excel file ('97-2003 format; *.xls)   */
                                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                                break;
                            case ".xlsx":
                                /*  Reading from a OpenXml Excel file (2007 format; *.xlsx) */
                                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                                //  result = GetFileDataFromFormatedExcel(stream);
                                break;
                        }
                        if (excelReader != null)
                        {
                            /*  DataSet - Create column names from first row    */
                            excelReader.IsFirstRowAsColumnNames = true;
                            result = excelReader.AsDataSet();
                            excelReader.Close();
                            if (result.Tables[sheetName] == null)
                            {
                                sheetName = "Table1";
                                if (result.Tables[sheetName] == null) { sheetName = ""; }
                            }

                            return dtCsv = !string.IsNullOrWhiteSpace(sheetName) ? result.Tables[sheetName] : result.Tables[0];
                        }
                    }
                }
                else
                {
                    var lines = System.IO.File.ReadAllLines(filePath);
                    var headers = lines[0].Split(',');

                    foreach (var hrows in headers)
                    {
                        dtCsv.Columns.Add(hrows);
                    }

                    var bodyRows = lines.Skip(1);
                    foreach (var row in bodyRows)
                    {
                        DataRow dr = dtCsv.NewRow();
                        var colrow = row.Trim().TrimEnd(',').Split(',');
                        for (var ind = 0; ind < colrow.Length; ind++)
                        {
                            dr[ind] = colrow[ind].ToString();
                        }
                        dtCsv.Rows.Add(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                var err = ex.StackTrace;
                var extension = Path.GetExtension(filePath);
                var result = new DataSet();
                extension = extension.Equals(".xls") ? ".xlsx" : ".xls";
                if (extension.Equals(".xls") || extension.Equals(".xlsx"))
                {
                    using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        IExcelDataReader excelReader = null;
                        switch (extension)
                        {
                            case ".xls":
                                /*  Reading from a binary Excel file ('97-2003 format; *.xls)   */
                                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                                break;
                            case ".xlsx":
                                /*  Reading from a OpenXml Excel file (2007 format; *.xlsx) */
                                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                                //  result = GetFileDataFromFormatedExcel(stream);
                                break;
                        }
                        if (excelReader != null)
                        {
                            /*  DataSet - Create column names from first row    */
                            excelReader.IsFirstRowAsColumnNames = true;
                            result = excelReader.AsDataSet();
                            excelReader.Close();
                            // return dtCsv = result.Tables[0];
                            return dtCsv = !string.IsNullOrWhiteSpace(sheetName) ? result.Tables[sheetName] : result.Tables[0];
                        }
                    }
                }
            }
            return dtCsv;
        }
        private string BuildNotifications(InsertBulkSMSOnRequest request, string campId, string modifiedPath, string engineFilePath, string dirName, int allowDuplicates)
        {
            try
            {
                var message = "Campaign ID - " + campId + ", Directory Name - " + dirName;
                var splitedFiles = new List<string>();
                var cieledvalue = 0;
                cieledvalue = (int)Math.Ceiling((double)request.TotalCount / (double)(_configuration["CampaignXMLGeneratedCount"] != null ? Convert.ToInt32(_configuration["CampaignXMLGeneratedCount"]) : 25000));
                var actualFileName = Path.GetFileNameWithoutExtension(request.ImportFileName);
                for (var i = 0; i < cieledvalue; i++)
                {
                    splitedFiles.Add(actualFileName + "_" + i);
                }

                if (!Directory.Exists(modifiedPath))
                {
                    Directory.CreateDirectory(modifiedPath);
                }
                else
                {
                    Directory.Delete(modifiedPath, true);
                    Directory.CreateDirectory(modifiedPath);
                }

                if (!string.IsNullOrWhiteSpace(modifiedPath))
                {
                    if (splitedFiles.Any())
                    {
                        engineFilePath = splitedFiles.Aggregate(engineFilePath, (current, item) => current + (item + ".xml,"));
                        engineFilePath = engineFilePath.TrimEnd(',');
                    }
                }
                var ext = Path.GetExtension(request.ActualFileName);
                if (_configuration["IndianSynapse"]?.ToString() == "true")
                {
                    List<KeyValuePair<string, string>> xmlContent =
                        BuildXmlFromFile(
                        Path.GetFileNameWithoutExtension(request.ActualFileName) + ((ext.Equals(".txt") || ext.Equals(".txt")) ? ".csv" : ext),
                            Path.Combine(Path.GetDirectoryName(request.ImportFileName),
                                Path.GetFileNameWithoutExtension(request.ActualFileName) + ((ext.Equals(".txt") || ext.Equals(".txt")) ? ".csv" : ext)),
                            request.CreatedBy.ToString(), request.UserName, campId, request.Sender,
                            request.Message, request.MobileField, Convert.ToInt32(request.LangID), splitedFiles, request.SheetName,
                            (request.CampaignType.Equals("Promotional", StringComparison.OrdinalIgnoreCase) ? 0 : 1), (Convert.ToInt32(request.AllowDuplicates) == 0 ? false : true),
                            (Convert.ToInt32(request.MessageType) == 2));

                    if (xmlContent.Any())
                    {
                        var totalCredits = 0;
                        var customcampId = 0;
                        var username = "";
                        splitedFiles.Clear();
                        splitedFiles.AddRange(xmlContent.Select(s => s.Key));
                        foreach (var item in splitedFiles)
                        {
                            try
                            {
                                var _xmlContent =
                                    xmlContent.FirstOrDefault(
                                        x => x.Key.Equals(item, StringComparison.OrdinalIgnoreCase));
                                if (!string.IsNullOrWhiteSpace(_xmlContent.Value))
                                {
                                    var fPath = Path.Combine(modifiedPath,
                                        Path.GetFileNameWithoutExtension(item) + ".xml");
                                    var FileContent = _xmlContent.Value;
                                    var xdoc = XDocument.Parse(FileContent);
                                    //  var xdoc = _xmlContent.Value;
                                    var customflag = xdoc.Root.Attribute(XName.Get("iscustome")).Value;
                                    if (customflag != "false")
                                    {
                                        var allCounts = xdoc.Root.Elements().Select(s => Convert.ToInt32(s.Attribute(XName.Get("ccount")).Value));
                                        //var allCounts = xdoc.Root.Elements();
                                        totalCredits = allCounts.Sum();
                                        xdoc.Descendants().Attributes(XName.Get("ccount")).Remove();
                                        FileContent = xdoc.ToString();
                                    }
                                    FileContent = FileContent.Replace("&#xA;", "&#10;");
                                    customcampId = xdoc.Root.Elements().Select(s => Convert.ToInt32(s.Attribute(XName.Get("campainid")).Value)).FirstOrDefault();
                                    username = xdoc.Root.Elements().Select(s => s.Attribute(XName.Get("username")).Value).FirstOrDefault();
                                    Logger.InfoFormat("Campaign Id :: {0} & File Name :: {1}", campId, fPath);
                                    System.IO.File.WriteAllText(fPath, FileContent); //_xmlContent.Value);
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.ErrorFormat("while generating XML file througing fatal error :: {0}", ex.StackTrace);
                                new QMail().sendMail(message);

                            }
                        }

                        using (var clientAcces = new AuthenticateSecurityClient())
                        {
                            var res = clientAcces.InsertCustomSMSActualCredits(new InsertBulkSMSOnRequest
                            {
                                UserName = username,
                                CampID = customcampId.ToString(),
                                MessageType = request.MessageType,
                                TotalCreditsReq = totalCredits,
                                PreprocessTime = DateTime.Now,
                                Schedule = DateTime.Now
                            });
                        }
                        var QMsg = "action=start&camp_id=" +
                                   campId + "&camp_type=" +
                                   (Convert.ToInt32(request.ScheduledType) == 2 ? "2" : "1") + "&dir_name=" +
                                   dirName + "&count=" + request.ProcessedCount;
                        var Qresult = new CampaignQLog().PushMessageToQ(QMsg);
                        if (Qresult == false)
                        {
                            string cn = request.Name;
                            var ChangeStatus = CampaignStatusChange(cn);
                            if (request.GroupOldFilePath != "")
                            {

                                if (Directory.Exists(request.GroupOldFilePath))
                                    Directory.Delete(request.GroupOldFilePath, true);
                                else
                                    Logger.ErrorFormat("The Groupfile path not existed :: {0}", request.GroupOldFilePath);

                            }
                            if (Directory.Exists(modifiedPath))
                                Directory.Delete(modifiedPath, true);
                            else
                                Logger.ErrorFormat("The file path not existed :: {0}", modifiedPath);

                            Logger.InfoFormat("ActiveMQ Connection Failed - Campaign Name :: {0}", request.Name);
                        }
                        return "UpdatedSuccessfully";
                    }
                }
                else
                {
                    List<KeyValuePair<string, string>> xmlContent =
                        IEnumerableExtension.BuildXmlFromFile(
                        Path.GetFileNameWithoutExtension(request.ActualFileName) + ((ext.Equals(".txt") || ext.Equals(".txt")) ? ".csv" : ext),
                            Path.Combine(Path.GetDirectoryName(request.ImportFileName),
                                Path.GetFileNameWithoutExtension(request.ActualFileName) + ((ext.Equals(".txt") || ext.Equals(".txt")) ? ".csv" : ext)),
                            request.CreatedBy.ToString(), request.UserName, campId, request.Sender,
                            request.Message, request.MobileField, Convert.ToInt32(request.LangID), splitedFiles, request.SheetName,
                            (request.CampaignType.Equals("Promotional", StringComparison.OrdinalIgnoreCase) ? 0 : 1), (Convert.ToInt32(request.AllowDuplicates) == 0 ? false : true),
                            (Convert.ToInt32(request.MessageType) == 2));

                    if (xmlContent.Any())
                    {
                        var totalCredits = 0;
                        var customcampId = 0;
                        var username = "";
                        splitedFiles.Clear();
                        splitedFiles.AddRange(xmlContent.Select(s => s.Key));
                        foreach (var item in splitedFiles)
                        {
                            try
                            {
                                var _xmlContent =
                                    xmlContent.FirstOrDefault(
                                        x => x.Key.Equals(item, StringComparison.OrdinalIgnoreCase));
                                if (!string.IsNullOrWhiteSpace(_xmlContent.Value))
                                {
                                    var fPath = Path.Combine(modifiedPath,
                                        Path.GetFileNameWithoutExtension(item) + ".xml");
                                    var FileContent = _xmlContent.Value;
                                    var xdoc = XDocument.Parse(FileContent);
                                    //  var xdoc = _xmlContent.Value;
                                    var customflag = xdoc.Root.Attribute(XName.Get("iscustome")).Value;
                                    if (customflag != "false")
                                    {
                                        var allCounts = xdoc.Root.Elements().Select(s => Convert.ToInt32(s.Attribute(XName.Get("ccount")).Value));
                                        //var allCounts = xdoc.Root.Elements();
                                        //totalCredits = allCounts.Sum();   //Old One
                                        if (allowDuplicates == 0)
                                        {
                                            if (!_xmlContent.Key.Contains("_duplicate"))
                                                totalCredits += allCounts.Sum();
                                        }
                                        else
                                        {
                                            totalCredits += allCounts.Sum();    //Modified by Murty on 30/08/2023
                                        }
                                        xdoc.Descendants().Attributes(XName.Get("ccount")).Remove();
                                        FileContent = xdoc.ToString();
                                    }
                                    FileContent = FileContent.Replace("&#xA;", "&#10;");
                                    customcampId = xdoc.Root.Elements().Select(s => Convert.ToInt32(s.Attribute(XName.Get("campainid")).Value)).FirstOrDefault();
                                    username = xdoc.Root.Elements().Select(s => s.Attribute(XName.Get("username")).Value).FirstOrDefault();
                                    Logger.InfoFormat("Campaign Id :: {0} & File Name :: {1}", campId, fPath);
                                    System.IO.File.WriteAllText(fPath, FileContent); //_xmlContent.Value);
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.ErrorFormat("while generating XML file througing fatal error :: {0}", ex.StackTrace);
                                new QMail().sendMail(message);

                            }
                        }

                        using (var clientAcces = new AuthenticateSecurityClient())
                        {
                            var res = clientAcces.InsertCustomSMSActualCredits(new InsertBulkSMSOnRequest
                            {
                                UserName = username,
                                CampID = customcampId.ToString(),
                                MessageType = request.MessageType,
                                TotalCreditsReq = totalCredits,
                                PreprocessTime = DateTime.Now,
                                Schedule = DateTime.Now
                            });
                        }

                        var type = string.Empty;
                        if (request.MessageType == 1)
                        {
                            type = "BulkSms";
                        }
                        else
                        {
                            type = "CustomSms";
                        }
                        var selAction = UserActions.FirstOrDefault(w => w.ActionName.Equals(type) && w.ControllerName.Equals(ControllerName, StringComparison.OrdinalIgnoreCase));

                        if ((selAction.IsCheckerRequired == false) || (ExtendedUser.LogOnRespons.CustomerType == 1) || (ExtendedUser.LogOnRespons.CustomerId == 1))
                        {
                            var QMsg = "action=start&camp_id=" +
                                       campId + "&camp_type=" +
                                       (Convert.ToInt32(request.ScheduledType) == 2 ? "2" : "1") + "&dir_name=" +
                                       dirName + "&count=" + request.ProcessedCount;
                            var Qresult = new CampaignQLog().PushMessageToQ(QMsg);
                            if (Qresult == false)
                            {
                                string cn = request.Name;
                                var ChangeStatus = CampaignStatusChange(cn);
                                if (request.GroupOldFilePath != "")
                                {

                                    if (Directory.Exists(request.GroupOldFilePath))
                                        Directory.Delete(request.GroupOldFilePath, true);
                                    else
                                        Logger.ErrorFormat("The Groupfile path not existed :: {0}", request.GroupOldFilePath);

                                }
                                if (Directory.Exists(modifiedPath))
                                    Directory.Delete(modifiedPath, true);
                                else
                                    Logger.ErrorFormat("The file path not existed :: {0}", modifiedPath);

                                Logger.InfoFormat("ActiveMQ Connection Failed - Campaign Name :: {0}", request.Name);
                            }
                        }
                        return "UpdatedSuccessfully";
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("while generating engine files througing fatal error :: {0}", ex.StackTrace);
                return "Error";
            }
            return "";
        }

        private string CampaignStatusChange(string cn)
        {
            Logger.InfoFormat("CampaignStatusChange :: start :: {0}", cn);
            try
            {
                using (var clientAcces = new AuthenticateSecurityClient())
                {
                    var response = clientAcces.CampaignStatusChange(cn);
                    return response.Result;
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("CampaignStatusChange  :-{0} Error :- {1}", cn);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return string.Empty;
        }

        [PreventSpam]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertTestSMS(InsertTestSMSOnRequest TestSMSModel, int MbcID = 0)
        {
            Logger.InfoFormat("InsertTestSMS :: start :: {0}", TestSMSModel.CustomerID);
            try
            {
                TestSMSModel.LanguageId = TestSMSModel.LanguageId;
                TestSMSModel.MessageId = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(TestSMSModel.MessageId[0]));
                TestSMSModel.Priority = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(TestSMSModel.Priority[0]));
                TestSMSModel.SentStatus = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(TestSMSModel.SentStatus[0]));
                TestSMSModel.CharSet = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(TestSMSModel.CharSet[0]));
                TestSMSModel.DLRRequired = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(TestSMSModel.DLRRequired[0]));
                TestSMSModel.MobileNo = AESEncrytDecry.DecryptStringAES(TestSMSModel.MobileNo);
                TestSMSModel.SmsType = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(TestSMSModel.SmsType[0]));
                TestSMSModel.Message = AESEncrytDecry.DecryptStringAES(TestSMSModel.Message);
                TestSMSModel.CharCount = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(TestSMSModel.CharCount[0]));
                TestSMSModel.Credits = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(TestSMSModel.Credits[0]));
                TestSMSModel.ReferenceId = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(TestSMSModel.ReferenceId[0]));
                TestSMSModel.ShortCode = AESEncrytDecry.DecryptStringAES(TestSMSModel.ShortCode);
                TestSMSModel.Sender = AESEncrytDecry.DecryptStringAES(TestSMSModel.Sender);
                TestSMSModel.OrgAddress = AESEncrytDecry.DecryptStringAES(TestSMSModel.OrgAddress);
                TestSMSModel.ModuleId = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(TestSMSModel.ModuleId[0]));
                TestSMSModel.ModuleType = AESEncrytDecry.DecryptStringAES(TestSMSModel.ModuleType);
                TestSMSModel.ModuleUniqId = AESEncrytDecry.DecryptStringAES(TestSMSModel.ModuleUniqId);
                TestSMSModel.SenderName = AESEncrytDecry.DecryptStringAES(TestSMSModel.SenderName);
                TestSMSModel.ModuleName = AESEncrytDecry.DecryptStringAES(TestSMSModel.ModuleName);
                TestSMSModel.SmppReferenceId = AESEncrytDecry.DecryptStringAES(TestSMSModel.SmppReferenceId);
                TestSMSModel.SmppSenderName = AESEncrytDecry.DecryptStringAES(TestSMSModel.SmppSenderName);
                TestSMSModel.MessageQNo = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(TestSMSModel.MessageQNo[0]));
                TestSMSModel.SmppStage = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(TestSMSModel.SmppStage[0]));
                TestSMSModel.SenderId = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(TestSMSModel.SenderId[0]));
                TestSMSModel.MobileColumn = AESEncrytDecry.DecryptStringAES(TestSMSModel.MobileColumn);
                TestSMSModel.msgtemptext = AESEncrytDecry.DecryptStringAES(TestSMSModel.msgtemptext) == null ? "" : AESEncrytDecry.DecryptStringAES(TestSMSModel.msgtemptext);
                TestSMSModel.GroupIds = TestSMSModel.GroupIds;
                TestSMSModel.CampaignType = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(TestSMSModel.CampaignType)) == 1 ? "Promotional" : "Transactional";
                TestSMSModel.Sheet = AESEncrytDecry.DecryptStringAES(TestSMSModel.Sheet);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("InsertTestSMS() :: Exception Error :" + ex.Message.ToString());
                return Json(new { Invalid = true, Message = lz.InvalidInputParameters });
            }
            var selAction =
                UserActions.FirstOrDefault(
                    w => w.ActionName.Equals("Index") &&
                        w.ControllerName.Equals(ControllerName, StringComparison.OrdinalIgnoreCase));
            var extendedUser = SessionExtensions.GetItem<Core.Models.Extensions.CustomeUser>(HttpContext.Session);
            string ReturnVal = string.Empty;
            using (var ClientAccess = new AuthenticateSecurityClient())
            {
                var fileContent = new StringBuilder();
                int ValidMobNosCnt = 0;
                var MainDirectory = _configuration["tempPath"]?.ToString();
                var currentMonthYear = Path.Combine(DateTime.Now.ToString("MMMyyyy"), "TestSMS");
                if (!Directory.Exists(Path.Combine(MainDirectory, currentMonthYear)))
                {
                    Directory.CreateDirectory(Path.Combine(MainDirectory, currentMonthYear));
                }
                var filepath = Path.Combine(MainDirectory, currentMonthYear);
                var LogPath = Path.Combine(filepath, "TestSMSLog.txt");
                List<InsertTestSMSOnRequest> TotalRecs = new List<InsertTestSMSOnRequest>();
                string logText = "Time : " + DateTime.Now.ToString() + ", Sender Id : " + TestSMSModel.SenderName + ", Mobilenos : " + TestSMSModel.MobileNo + ", LangID : " + TestSMSModel.LanguageId + ", Message : " + TestSMSModel.Message + ", Credits : " + TestSMSModel.Credits + ", UserName : " + extendedUser.LogOnRespons.UserName;
                fileContent.AppendLine(logText);
                //fileContent.AppendLine("---------------------- lOG :: SenderID : " + TestSMSModel.SenderName + " :: Time : " + DateTime.Now + " ----------------------");
                try
                {
                    var isValid = false;
                    if (isValid == false) { isValid = false; }
                    List<MobileLengthValidationResponse> sender_countrycodes = ValidateMobileNumbers(TestSMSModel.SenderId);
                    if (sender_countrycodes.Any())
                    {
                        foreach (var mob in TestSMSModel.MobileNo.Split(','))
                        {
                            if (!string.IsNullOrWhiteSpace(mob))
                            {
                                var totLengthValid = sender_countrycodes.Where(w => w.TotalLength.Equals(mob.Length));
                                if (!totLengthValid.Any())
                                {
                                    ReturnVal = "InValidNos";
                                    return Json(ReturnVal);
                                }
                                var validcountrycode = from n in sender_countrycodes
                                                       let countrycode = n.CountryCode
                                                       let countrycodelength = n.CountryCode.ToString().Length
                                                       where mob.Substring(0, countrycodelength) == n.CountryCode.ToString()
                                                       select mob;
                                if (!validcountrycode.Any())
                                {
                                    ReturnVal = "InValidNos";
                                    return Json(ReturnVal);
                                }
                                if (totLengthValid.Any() && validcountrycode.Any())
                                {
                                    isValid = true;
                                    ValidMobNosCnt = TestSMSModel.MobileNo.Split(',').Count();
                                }
                            }
                        }
                    }

                    if (ValidMobNosCnt > 0)
                    {
                        var fileDetails = SessionExtensions.GetItem<List<FileUploadDet>>(HttpContext.Session);
                        var Mainpath = _configuration["tempPath"]?.ToString();

                        if (fileDetails != null)
                        {
                            bool Check = fileDetails[0].FilePath.Contains("Synapse4P0");
                            if (Check == false)
                            {
                                fileDetails[0].FilePath = Mainpath + fileDetails[0].FilePath;
                            }
                        }

                        List<Contact> ContactsList = new List<Contact>();
                        if (TestSMSModel.GroupIds != null)
                        {
                            fileDetails = null;
                            ContactsList = new Contact { CreatedBy = extendedUser.LogOnRespons.Id }.buildContacts(TestSMSModel.GroupIds.Trim(','), ExtendedUser.LogOnRespons.GetIPAddress).Where(w => (w.Fstatus == (Cstatus)1 && w.Status == "Active")).ToList();
                        }
                        //Added By murty - Created New instance
                        string category = string.Empty;
                        string? sessionCategory = HttpContext.Session.GetString("category");
                        if (!string.IsNullOrWhiteSpace(sessionCategory))
                        {
                            category = sessionCategory.TrimEnd(',');
                        }
                        InsertTestSMSOnRequest insertTestSMSOnRequest = new InsertTestSMSOnRequest
                        {
                            TotalCredReq = ValidMobNosCnt * TestSMSModel.Credits,
                            InsertTestRecords = TotalRecs,
                            UserID = extendedUser.LogOnRespons.Id,
                            SenderId = Convert.ToInt32(TestSMSModel.SenderId),
                            Sender = TestSMSModel.Sender,
                            LanguageId = Convert.ToInt32(TestSMSModel.LanguageId),
                            Message = TestSMSModel.Message,
                            MobileNo = TestSMSModel.MobileNo,
                            IsCustomTest = TestSMSModel.IsCustomTest,
                            CampaignType = TestSMSModel.CampaignType,
                            UserName = extendedUser.LogOnRespons.UserName,
                            Filepath = fileDetails != null ? fileDetails[0].FilePath : "",
                            // SheetName = fileDetails != null ? fileDetails[0].SheetName : "",
                            SheetName = TestSMSModel.Sheet != null ? TestSMSModel.Sheet : "",
                            MobileColumn = TestSMSModel.MobileColumn,
                            msgtemptext = TestSMSModel.msgtemptext,
                            GroupIds = TestSMSModel.GroupIds,
                            //SmppSenderName = str1,
                            ContactList = ContactsList.Select(s => new ContactList
                            {
                                FirstName = s.FirstName,
                                LastName = s.LastName,
                                Email = s.Email,
                                MobileNo = s.MobileNo
                            }).ToList(),
                            category = category
                        };
                        SessionExtensions.AddItem<Core.Models.Dtos.Requests.Synapse.UserCampaigns.InsertTestSMSOnRequest>(HttpContext.Session, insertTestSMSOnRequest);
                        //Added the final object to the session - to use this in Update Credits for test SMS - Method: UpdateTestSMSCredits()
                        var res = ClientAccess.InsertTestSMSCamp(insertTestSMSOnRequest);//Modified by Murty
                        ReturnVal = res.Result;
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
                        ErrorSignal.FromCurrentContext().Raise(ex);
                    }
                }
            }
            return Json("");
        }
        //Added By Murty
        public ActionResult UpdateTestSMSCredits()
        {
            try
            {
                var insertTestSMSOnRequest = SessionExtensions.GetItem<Core.Models.Dtos.Requests.Synapse.UserCampaigns.InsertTestSMSOnRequest>(HttpContext.Session);
                using (var ClientAccess = new AuthenticateSecurityClient())
                {
                    var res = ClientAccess.UpdateTestSMSCredits(insertTestSMSOnRequest);
                    return Json(res.Result);
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("InsertTestSMS() :: Exception Error :" + ex.Message.ToString());
                return Json(new { Invalid = true, Message = lz.InvalidInputParameters });
            }
        }
        //Added By Murty
        private async Task<string> RenderRazorViewToString(string viewName,object model)
        {
            ViewData.Model = model;

            await using var sw = new StringWriter();

            var viewEngine =
                HttpContext.RequestServices.GetRequiredService<IRazorViewEngine>();
            ViewEngineResult viewResult =
                viewEngine.FindView(
                    ControllerContext,
                    viewName,
                    isMainPage: false);

            if (!viewResult.Success)
            {
                viewResult =
                    viewEngine.GetView(
                        executingFilePath: null,
                        viewPath: viewName,
                        isMainPage: false);
            }

            if (!viewResult.Success)
            {
                var searchedLocations =
                    string.Join(
                        Environment.NewLine,
                        viewResult.SearchedLocations ?? []);

                throw new InvalidOperationException(
                    $"Unable to find view '{viewName}'. " +
                    $"Searched locations:{Environment.NewLine}" +
                    searchedLocations);
            }

            var tempDataProvider =
                HttpContext.RequestServices
                    .GetRequiredService<ITempDataProvider>();

            var viewContext = new ViewContext(
                ControllerContext,
                viewResult.View,
                ViewData,
                new TempDataDictionary(
                    HttpContext,
                    tempDataProvider),
                sw,
                new HtmlHelperOptions()
            );

            await viewResult.View.RenderAsync(viewContext);

            return sw.ToString();
        }

        [HttpPost]
        public ActionResult CreateQuickSMSorCampaign(QuickSMSOrCampaignMain request, string command)
        {
            Logger.InfoFormat("CreateQuickSMSorCampaign :: start :: {0}", request.QuicksmsorCampaign, command);
            try
            {
                var selAction =
                  UserActions.FirstOrDefault(
                      w =>
                          w.ControllerName.Equals(ControllerName, StringComparison.OrdinalIgnoreCase));

                if (request.QuicksmsorCampaign != null)
                {
                    ViewBag.IsModelState = false;
                    ViewBag.Message = lz.Invalidinputdetails;
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("CreateQuickSMSorCampaign  :-{0} Error :- {1}", request.QuicksmsorCampaign, command);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        public async Task<ActionResult> ExterDBCountview(string Incomegroup, string Nationality, string City, string Gender)
        {
            Logger.InfoFormat("ExterDBCountview :: start :: {0}", Incomegroup, Nationality, City, Gender);
            try
            {
                Incomegroup = AESEncrytDecry.DecryptStringAES(Incomegroup);
                Nationality = AESEncrytDecry.DecryptStringAES(Nationality);
                City = AESEncrytDecry.DecryptStringAES(City);
                Gender = AESEncrytDecry.DecryptStringAES(Gender);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("ExterDBCountView() :: Exception Error :" + ex.Message.ToString());
                return Json(new { Invalid = true, Message = lz.InvalidInputParameters });
            }

            try
            {
                using (var ClientAccess = new AuthenticateSecurityClient())
                {
                    var response = ClientAccess.Externaldbcount(new ExternalDB
                    {
                        Incomegroup = Incomegroup.Trim(),
                        Nationality = Nationality.Trim(),
                        City = City.Trim(),
                        Gender = Gender.Trim()
                    });
                    return Json(response.Result);
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("ExterDBCountview  :-{0} Error :- {1}", Incomegroup, Nationality, City, Gender);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return Json("");
        }

        [PreventSpam]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoadSubmitCamp(InsertBulkSMSOnRequest LocalModel, string cmd = "")
        {
            Logger.InfoFormat("ExternalLoadSubmitCamp :: start :: {0}", LocalModel);
            try
            {
                try
                {
                    LocalModel.CampID = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.CampID[0]));
                    LocalModel.Name = AESEncrytDecry.DecryptStringAES(LocalModel.Name);
                    LocalModel.SenderID = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.SenderID[0]));
                    LocalModel.Sender = AESEncrytDecry.DecryptStringAES(LocalModel.Sender);
                    LocalModel.CampaignTypeID = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.CampaignTypeID[0]));
                    LocalModel.CampaignType = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.CampaignType)) == 1 ? "Promotional" : "Transactional";
                    LocalModel.MobileField = AESEncrytDecry.DecryptStringAES(LocalModel.MobileField);
                    LocalModel.Language = AESEncrytDecry.DecryptStringAES(LocalModel.Language);
                    LocalModel.LangID = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.LangID[0]));
                    LocalModel.Message = AESEncrytDecry.DecryptStringAES(LocalModel.Message);
                    LocalModel.DLR = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.DLR[0]));
                    LocalModel.AllowDuplicates = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.AllowDuplicates[0]));
                    LocalModel.TotalScheduleString = AESEncrytDecry.DecryptStringAES(LocalModel.TotalScheduleString);
                    LocalModel.ScheduledType = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.ScheduledType[0]));
                    LocalModel.CharCount = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.CharCount[0]));
                    LocalModel.CreditsUsed = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.CreditsUsed[0]));
                    LocalModel.XMLSchedule = AESEncrytDecry.DecryptStringAES(LocalModel.XMLSchedule);
                    LocalModel.Criteria = AESEncrytDecry.DecryptStringAES(LocalModel.Criteria);
                    LocalModel.PlaceHolders = AESEncrytDecry.DecryptStringAES(LocalModel.PlaceHolders);
                    LocalModel.MessageType = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.MessageType[0]));
                    LocalModel.Status = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.Status[0]));
                    LocalModel.Type = AESEncrytDecry.DecryptStringAES(LocalModel.Type);
                    LocalModel.ImportFileName = AESEncrytDecry.DecryptStringAES(LocalModel.ImportFileName);
                    LocalModel.ActualFileName = AESEncrytDecry.DecryptStringAES(LocalModel.ActualFileName);
                    LocalModel.SheetName = AESEncrytDecry.DecryptStringAES(LocalModel.SheetName);
                    LocalModel.RecipientsType = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.RecipientsType[0]));
                    LocalModel.RuleId = AESEncrytDecry.DecryptStringAES(LocalModel.RuleId);
                    LocalModel.GroupIds = AESEncrytDecry.DecryptStringAES(LocalModel.GroupIds);
                    LocalModel.SchStatus = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LocalModel.SchStatus[0]));
                    LocalModel.GroupOldFilePath = AESEncrytDecry.DecryptStringAES(LocalModel.GroupOldFilePath);
                    LocalModel.BeforeEditSchTime = AESEncrytDecry.DecryptStringAES(LocalModel.BeforeEditSchTime);

                    LocalModel.IncomeGroup = AESEncrytDecry.DecryptStringAES(LocalModel.IncomeGroup);
                    LocalModel.Nationality = AESEncrytDecry.DecryptStringAES(LocalModel.Nationality);
                    LocalModel.City = AESEncrytDecry.DecryptStringAES(LocalModel.City);
                    LocalModel.Gender = AESEncrytDecry.DecryptStringAES(LocalModel.Gender);
                    LocalModel.CampaignCount = AESEncrytDecry.DecryptStringAES(LocalModel.CampaignCount);
                    LocalModel.FromRange = AESEncrytDecry.DecryptStringAES(LocalModel.FromRange);
                    LocalModel.ToRange = AESEncrytDecry.DecryptStringAES(LocalModel.ToRange);

                    if (LocalModel.CampID > 0)
                    {
                        var ValidPath = LocalModel.GroupOldFilePath.Split('_');
                        LocalModel.GroupOldFilePath = "";
                        if (ValidPath.Length == 3)
                        {
                            if (ValidPath[2] == "3")
                                LocalModel.GroupOldFilePath = ValidPath.Length > 0 ? _configuration["tempPath"]?.ToString() +
                                                "\\Schedule" + "\\" + ValidPath[0] : "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("ExternalLoadSubmitCampign() :: Exception Error :" + ex.Message.ToString());
                    return Json(new { Invalid = true, Message = lz.InvalidInputParameters });
                }


                var externaldbfilter = Externaldbfiltersub(LocalModel.IncomeGroup.Trim(), LocalModel.Nationality.Trim(), LocalModel.City.Trim(), LocalModel.Gender.Trim(), LocalModel.CampaignCount, LocalModel.FromRange, LocalModel.ToRange);

                if (externaldbfilter.Count() != Convert.ToInt32(LocalModel.CampaignCount))
                {
                    return Json(new
                    {
                        IsValid = false,
                        Message = "Externaldbfiltercount"
                    });
                }

                if (Convert.ToInt32(LocalModel.CampID) == 0)
                {
                    var message = ValidateCampaignName(LocalModel.Name);
                    if (!string.IsNullOrWhiteSpace(message))
                    {
                        return Json(new
                        {
                            IsValid = false,
                            Message = message
                        });
                    }
                }
                //var selAction =
                //UserActions.FirstOrDefault(
                //    w => w.ActionName.Equals("Index") &&
                //        w.ControllerName.Equals(ControllerName, StringComparison.OrdinalIgnoreCase));
                var extendedUser = SessionExtensions.GetItem<Core.Models.Extensions.CustomeUser>(HttpContext.Session);
                int PreProcessInterval = 0;
                int vldcnt = 100; if (vldcnt == 100) { vldcnt = 100; }
                string XMLSchedule = "";
                DateTime ScheduleTime = DateTime.Now;
                DateTime PreProcessTime = DateTime.Now;
                var tCount = 0; var DupeCount = 0;
                var dupPath = string.Empty;

                if (Convert.ToInt32(LocalModel.ScheduledType) == 2)
                {
                    if (Convert.ToInt32(LocalModel.CampID) == 0)
                    {
                        string starttime = LocalModel.TotalScheduleString.Split(',')[1].Split(' ')[6].Substring(0, 5);
                        var StartDate = LocalModel.TotalScheduleString.Split(',')[1].Split(' ');
                        string SchDate = StartDate[3] + "-" + StartDate[2] + "-" + StartDate[4];
                        ScheduleTime = Convert.ToDateTime(SchDate + " " + starttime + ":" + DateTime.Now.Second);
                        PreProcessTime = Convert.ToDateTime(SchDate + " " + starttime + ":" + DateTime.Now.Second).AddMinutes(-PreProcessInterval);
                        XMLSchedule = "<XML><RECURRENCE SCHEDULE='5' STARTTIME='" + starttime + "' INTERVAL='" + PreProcessInterval + "' STARTDATE='" + SchDate + "' ENDON='1' ENDONDATE='' SENDALERTON='' EVERYNWEEK='0' WEEKDAYS='' DAYS='' MONTHS='' /></XML>";
                        if (ScheduleTime < DateTime.Now)
                        {
                            return Json(new
                            {
                                IsValid = false,
                                Message = lz.ScheduletimeshouldnotbeLessthancurrentDateTime
                            });
                        }
                    }
                    else
                    {
                        string starttime = LocalModel.TotalScheduleString.Split(',')[1].Split(' ')[6].Substring(0, 5);
                        var StartDate = LocalModel.TotalScheduleString.Split(',')[1].Split(' ');
                        string SchDate = StartDate[3] + "-" + StartDate[2] + "-" + StartDate[4];

                        ScheduleTime = Convert.ToDateTime(SchDate + " " + starttime + ":" + DateTime.Now.Second);
                        PreProcessTime = Convert.ToDateTime(LocalModel.BeforeEditSchTime + ":" + DateTime.Now.Second).AddMinutes(-10);

                        XMLSchedule = "<XML><RECURRENCE SCHEDULE='5' STARTTIME='" + starttime + "' INTERVAL='" + PreProcessInterval + "' STARTDATE='" + SchDate + "' ENDON='1' ENDONDATE='' SENDALERTON='' EVERYNWEEK='0' WEEKDAYS='' DAYS='' MONTHS='' /></XML>";
                        if (PreProcessTime < DateTime.Now)
                        {
                            return Json(new
                            {
                                IsValid = false,
                                Message = "Cannot submit the campaign as preprocessor time has started already"
                            });
                        }
                    }
                }
                var invlaidmobcount = 0;
                var invlaidmobpath = string.Empty;
                var dupPathVerify = dupPath;
                Dictionary<string, string> Dictionary = new Dictionary<string, string>();

                var sItems = HttpContext.Session.GetItem<GroupContactsMain>();
                //var SchildCount = new List<GroupContacts>();

                //foreach (var item in sItems.GroupswithContacts)
                //{
                //    SchildCount.AddRange(item.GroupContacts);
                //}

                //var sItems = Session.GetItem<MobileNos>();
                //externaldbfilter = new List<string>();

                //foreach (var item in externaldbfilter)
                //{
                //    SchildCount.AddRange(item);
                //}

                //var groups = LocalModel.GroupIds;
                var externaldb = "ExternalDatabase" + DateTime.Now.Ticks;
                LocalModel.ImportFileName = BuildCsv(externaldb, externaldbfilter);
                var MainDirectory = _configuration["tempPath"]?.ToString();
                var currentMonthYear = DateTime.Now.ToString("MMMyyyy");
                if (!Directory.Exists(Path.Combine(MainDirectory, currentMonthYear)))
                {
                    Directory.CreateDirectory(Path.Combine(MainDirectory, currentMonthYear));
                }
                var filepath = Path.Combine(MainDirectory, currentMonthYear) + "\\" + externaldb;

                LocalModel.ImportFileName = BuildCsvEDB(filepath + "_" + DateTime.Now.Ticks, externaldbfilter);

                LocalModel.MobileField = "MobileNo";

                var invalidMobileCollection = ValidateMobileNumbers(Convert.ToInt32(LocalModel.SenderID), LocalModel.ImportFileName,
                   LocalModel.MobileField, Path.GetExtension(LocalModel.ImportFileName), LocalModel.MessageType,
                       (Path.GetExtension(LocalModel.ImportFileName) != ".csv") ? LocalModel.SheetName : "", (Convert.ToInt32(LocalModel.AllowDuplicates) == 1)
                   );

                if (!string.IsNullOrWhiteSpace(invalidMobileCollection))
                {
                    var fExt = Path.GetExtension(LocalModel.ImportFileName) == ".csv" || Path.GetExtension(LocalModel.ImportFileName) == ".txt";
                    if (invalidMobileCollection == "0" && fExt)
                    {
                        return Json(new
                        {
                            IsValid = false,
                            Message = "invalidnumbersinfile"
                        });
                    }
                    var invalidmobcol = invalidMobileCollection.Split('|');
                    invlaidmobcount = !string.IsNullOrWhiteSpace(invalidmobcol[0])
                        ? Convert.ToInt32(invalidmobcol[0])
                        : 0;
                    invlaidmobpath = _configuration["Filterlogpath"]?.ToString() + "//" +
                    DateTime.Now.ToString("MMMyyyy") + "//" + Path.GetFileName(invalidmobcol[1]);
                    tCount = tCount == 0 ? Convert.ToInt32(invalidmobcol[2]) : tCount;
                    DupeCount = Convert.ToInt32(invalidmobcol[3]);
                    dupPath = _configuration["Filterlogpath"]?.ToString() + "//" +
                    DateTime.Now.ToString("MMMyyyy") + "//" + Path.GetFileName(invalidmobcol[4]);
                }


                var isvalnumb = _configuration["IsValidationEnable"]?.ToString();
                var model = new InsertBulkSMSOnRequest
                {
                    CustomerID = extendedUser.LogOnRespons.CustomerId,
                    CampID = LocalModel.CampID,
                    Name = LocalModel.Name,
                    SenderID = LocalModel.SenderID,
                    LangID = LocalModel.LangID,
                    Language = LocalModel.Language,
                    CampaignTypeID = LocalModel.CampaignTypeID,
                    CampaignType = LocalModel.CampaignType,
                    Message = LocalModel.Message.Trim(),
                    CharCount = LocalModel.CharCount,
                    CreditsUsed = LocalModel.CreditsUsed,
                    ScheduledType = LocalModel.ScheduledType,
                    TotalScheduleString = LocalModel.TotalScheduleString,
                    XMLSchedule = XMLSchedule,
                    Criteria = LocalModel.Criteria,
                    PlaceHolders = LocalModel.PlaceHolders,
                    DLR = LocalModel.DLR,
                    AllowDuplicates = LocalModel.AllowDuplicates,
                    MessageType = LocalModel.MessageType,
                    Status = LocalModel.Status,
                    CreatedBy = extendedUser.LogOnRespons.Id,
                    IpAddress = LocalModel.IpAddress,
                    Sender = LocalModel.Sender,
                    Type = LocalModel.Type,
                    CurrentStatus = 1,
                    ImportFileName = LocalModel.ImportFileName,
                    GroupOldFilePath = LocalModel.GroupOldFilePath,
                    ActualFileName = LocalModel.ActualFileName,
                    SheetName = LocalModel.SheetName,
                    ValidCount = tCount - (invlaidmobcount + DupeCount),
                    InValidCount = invlaidmobcount,
                    DuplicateCount = DupeCount,
                    TotalCount = tCount - (invlaidmobcount + DupeCount),
                    ProcessedCount = tCount * LocalModel.CreditsUsed - (invlaidmobcount + DupeCount),
                    DuplicatePath = dupPathVerify.Replace(_configuration["tempPath"]?.ToString(), _configuration["Filterlogpath"]?.ToString()),
                    InvalidMobPath = invlaidmobpath,
                    RecipientsType = LocalModel.RecipientsType,
                    MobileField = LocalModel.MobileField,
                    GroupIds = LocalModel.GroupIds,
                    RuleId = LocalModel.RuleId,
                    TempTableName = LocalModel.TempTableName,
                    Remarks = LocalModel.Remarks,
                    IsDone = LocalModel.IsDone,
                    Schedule = ScheduleTime,
                    PreprocessTime = PreProcessTime,
                    IsProcess = LocalModel.IsProcess,
                    PreProcessStatus = LocalModel.PreProcessStatus,
                    SchStatus = LocalModel.SchStatus,
                    Stageids = LocalModel.Stageids,
                    TotalCreditsReq = (Convert.ToInt32(LocalModel.MessageType) == 2) ? GetCustomeCampTotalCount(LocalModel.Message.Trim(), LocalModel.ImportFileName, Convert.ToInt32(LocalModel.LangID), LocalModel.RuleId, LocalModel.SheetName, LocalModel.MobileField, Convert.ToInt32(LocalModel.AllowDuplicates)) - (LocalModel.CreditsUsed * invlaidmobcount) : Convert.ToInt32(LocalModel.CreditsUsed) * (tCount - invlaidmobcount), //changes done on sep3
                    CountryWiseCnt = Dictionary,
                    UserName = ExtendedUser.LogOnRespons.UserName,
                    UserIp = extendedUser.LogOnRespons.GetIPAddress,
                    IncomeGroup = LocalModel.IncomeGroup,
                    Nationality = LocalModel.Nationality,
                    City = LocalModel.City,
                    Gender = LocalModel.Gender,
                    CampaignCount = LocalModel.CampaignCount
                };

                if (model.TotalCreditsReq == -5)
                {
                    return Json(new { IsValid = false, Message = "-5" });
                }
                if (model.TotalCreditsReq == -6)
                {
                    return Json(new { IsValid = false, Message = "-6" });
                }
                if (model.RecipientsType == 3)
                {
                    if (model.GroupIds != null && model.ValidCount == 0)
                    {
                        return Json(new
                        {
                            IsValid = false,
                            Message = "Please select Mobile no column"
                        });
                    }
                }
                else
                {
                    if (model.ValidCount == 0)
                    {
                        return Json(new
                        {
                            IsValid = false,
                            Message = "Selected column doesn't contain valid data"
                        });
                    }
                }
                var CampDetails = SessionExtensions.GetItem<InsertBulkSMSOnRequest>(HttpContext.Session);
                if (CampDetails != null)
                {
                    CampDetails = null;
                    CampDetails = model;
                    SessionExtensions.AddItem<InsertBulkSMSOnRequest>(HttpContext.Session, CampDetails);
                }
                else
                {
                    SessionExtensions.AddItem<InsertBulkSMSOnRequest>(HttpContext.Session, model);
                }
                if (cmd != "")
                {
                    return Json(new
                    {
                        IsValid = true,
                        PartialResult = RenderRazorViewToString(cmd, model)

                    });
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("LoadSubmitCamp  :-{0} Error :- {1}", LocalModel);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return Json("");
        }

        //var edbf = Externaldbfiltersub(LocalModel.IncomeGroup.Trim(), LocalModel.Nationality.Trim(), LocalModel.City.Trim(), LocalModel.Gender.Trim(), LocalModel.CampaignCount);

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestTimeout("LongRunningPolicy")]
        public ActionResult SubmitCampaignExternalDB(string cmd = "")
        {
            Logger.InfoFormat("SubmitCampaignExternalDB :: start :: {0}", cmd);
            //HttpContext.Server.ScriptTimeout = 200;
            var selAction =
                UserActions.FirstOrDefault(
                    w => w.ActionName.Equals("Index") &&
                        w.ControllerName.Equals(ControllerName, StringComparison.OrdinalIgnoreCase));
            var extendedUser = SessionExtensions.GetItem<Core.Models.Extensions.CustomeUser>(HttpContext.Session);
            string ReturnVal = string.Empty;
            var CampDetails = SessionExtensions.GetItem<InsertBulkSMSOnRequest>(HttpContext.Session);
            if (CampDetails.TotalCreditsReq == 0)
            {
                ReturnVal = "Invalidcampaigncount";
                return Json(ReturnVal);
            }
            using (var ClientAccess = new AuthenticateSecurityClient())
            {
                try
                {
                    if (CampDetails != null)
                    {
                        if (Convert.ToInt32(CampDetails.CampID) > 0)
                        {
                            var QMsg = "action=stop&camp_id=" +
                               CampDetails.CampID + "&camp_type=" +
                               (Convert.ToInt32(CampDetails.ScheduledType) == 2 ? "2" : "1") + "&dir_name=&count=";

                            Logger.InfoFormat("CampaignQLog started ");
                            var Qresult = new CampaignQLog().PushMessageToQ(QMsg);
                            Logger.InfoFormat("CampaignQLog Ended");
                        }

                        var engineFilePath = CampDetails.ImportFileName;
                        var actualfilesplititems = Path.GetFileName(CampDetails.ImportFileName).Split('_');
                        var originalDirPath = Path.GetDirectoryName(CampDetails.ImportFileName);
                        var modifiedPath = _configuration["tempPath"]?.ToString() +
                                           ((Convert.ToInt32(CampDetails.ScheduledType) == 2) ? "\\Schedule" : "\\NonSchedule") + "\\" +
                                           actualfilesplititems[0];
                        CampDetails.ActualFileName = modifiedPath + "\\" + engineFilePath;

                        var response = ClientAccess.InsertExternalDB(CampDetails);
                        var result = response.Result.Split('$')[0];
                        var nID = response.Result.Split('$')[1];
                        switch (result)
                        {
                            case "7":
                                ReturnVal = !(selAction != null && selAction.IsCheckerRequired)
                                    ? "MsgSubmitSuccess"
                                    : "MsgSubittedToChecker";
                                BuildNotifications(CampDetails, response.Result.Split('$')[2], modifiedPath,
                                    engineFilePath, actualfilesplititems[0], Convert.ToInt32(CampDetails.AllowDuplicates));
                                HttpContext.Session.RemoveItem<GroupContactsMain>();
                                break;
                            case "-1":
                                ReturnVal = "error";
                                break;
                            case "1":
                                ReturnVal = "CustInActive";
                                break;
                            case "2":
                                ReturnVal = "CustExpired";
                                break;
                            case "3":
                                ReturnVal = "CustPrefInActive";
                                break;
                            case "4":
                                ReturnVal = "InsufficentCredits";
                                break;
                            case "5":
                                ReturnVal = "UpdatedSuccessfully";
                                ReturnVal = BuildNotifications(CampDetails, response.Result.Split('$')[2], modifiedPath,
                                    engineFilePath, actualfilesplititems[0], Convert.ToInt32(CampDetails.AllowDuplicates));
                                break;
                            case "8":
                                ReturnVal = "DuplicateName";
                                break;
                            case "9":
                                ReturnVal = "InvalidSchedule";
                                break;
                            case "10":
                                var ctimes = MessageTimings(CampDetails.CampaignType.ToString());
                                return Json(new { ReturnVal = "InvalidTime", campTimes = ctimes });
                            // break;
                            case "11":
                                ReturnVal = "KickOff";
                                break;
                        }
                    }
                    else
                    {
                        ReturnVal = "Unable Process, Please contact admin.";
                    }
                    return Json(ReturnVal);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("SubmitCampaignExternalDB  :-{0} Error :- {1}", cmd);
                    ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }
            return Json("");
        }
        private List<MobileNos> Externaldbfiltersub(string income, string nationality, string city, string gender, string campcount, string fromrange, string torange)
        {
            Logger.InfoFormat("Externaldbfiltersub :: start :: {0}", campcount);
            try
            {

                using (var ClientAccess = new AuthenticateSecurityClient())
                {
                    var result = ClientAccess.Externaldbfilter(income, nationality, city, gender, campcount, fromrange, torange);
                    return result.Result;
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Externaldbfiltersub ::userId :-{0} Error :- {1}", campcount);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return null;
        }
        [HttpPost("LoadSenderByCategory")]
        public ActionResult LoadSenderByCategory(string category)
        {
            try
            {
                var extendedUser = SessionExtensions.GetItem<Core.Models.Extensions.CustomeUser>(HttpContext.Session);
                LoadSenderByCategory loadSender = new LoadSenderByCategory()
                {
                    category = category,
                    userId = extendedUser.LogOnRespons.Id
                };

                using (var ClientAccess = new AuthenticateSecurityClient())
                {
                    var result = ClientAccess.LoadSenderByCategory(loadSender);
                    if (result == null) return View();
                    if (result.Result.Count > 0)
                    {
                        return Json(result.Result);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Externaldbfiltersub ::Error :- {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }

            return Json("");
        }

        [HttpGet]
        public ActionResult downloadFile(string filename)
        {
            var basePathJ = HttpContext.Session.GetString("QuicksmsorCampaignCollections");
            if (basePathJ?.ToString() != string.Empty)
            {
                //List<QuickSMSOrCampaign> lst = (List<QuickSMSOrCampaign>)Session["QuicksmsorCampaignCollections"];
                var ischeckerrequierd =
                UserActions.IsCheckerRequiredVerification(
                    x => x.ActionName.Equals("BulkSms", StringComparison.OrdinalIgnoreCase)
                         && x.ControllerName.Equals(ControllerName, StringComparison.OrdinalIgnoreCase) &&
                         x.IsCheckerRequired);
                bool ic = ischeckerrequierd == false ? true : ischeckerrequierd;

                List<QuickSMSOrCampaign> lst = new QuickSMSOrCampaign().buildmodel(0, ExtendedUser.LogOnRespons.Id, 2, 1, 1, ExtendedUser.LogOnRespons.GetIPAddress, "", ic);
                foreach (QuickSMSOrCampaign item in lst)
                {
                    if (item.ActualFileName.Contains(filename))
                    {
                        var ImportFilename = item.ImportFileName.Replace("^", "\\");
                        var localDirLen = _configuration["LocalDIR"]?.ToString();
                        var fileName = string.Join("\\", ImportFilename.Split(new string[] { "\\" }, StringSplitOptions.None).Skip(Convert.ToInt32(localDirLen)).ToList());
                        var fileClientName = System.IO.Path.GetFileNameWithoutExtension(fileName).Replace("&", "") + "_client.csv";
                        var basePath = _configuration["tempPathVirtualPath"]?.ToString() + "\\" + System.IO.Path.GetDirectoryName(fileName) + "\\" + fileClientName;
                        return File(System.IO.File.OpenRead(ImportFilename), "text/csv", Path.GetFileName(fileClientName));
                    }
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult downloadFileCustom(string filename)
        {
            string basePathJ = HttpContext.Session.GetString("QuicksmsorCampaignCollectionsC");
            if (basePathJ != string.Empty)
            {
                //List<QuickSMSOrCampaign> lst = (List<QuickSMSOrCampaign>)Session["QuicksmsorCampaignCollections"];
                var ischeckerrequierd =
                UserActions.IsCheckerRequiredVerification(
                    x => x.ActionName.Equals("CustomSms", StringComparison.OrdinalIgnoreCase)
                         && x.ControllerName.Equals(ControllerName, StringComparison.OrdinalIgnoreCase) &&
                         x.IsCheckerRequired);
                bool ic = ischeckerrequierd == false ? true : ischeckerrequierd;

                List<QuickSMSOrCampaign> lst = new QuickSMSOrCampaign().buildmodel(0, ExtendedUser.LogOnRespons.Id, 2, 1, 2, ExtendedUser.LogOnRespons.GetIPAddress, "", ic);
                foreach (QuickSMSOrCampaign item in lst)
                {
                    if (item.ActualFileName.Contains(filename))
                    {
                        var ImportFilename = item.ImportFileName.Replace("^", "\\");
                        var localDirLen = _configuration["LocalDIR"]?.ToString();
                        var fileName = string.Join("\\", ImportFilename.Split(new string[] { "\\" }, StringSplitOptions.None).Skip(Convert.ToInt32(localDirLen)).ToList());
                        var fileClientName = System.IO.Path.GetFileNameWithoutExtension(fileName).Replace("&", "") + "_client.csv";
                        var basePath = _configuration["tempPathVirtualPath"]?.ToString() + "\\" + System.IO.Path.GetDirectoryName(fileName) + "\\" + fileClientName;
                        return File(System.IO.File.OpenRead(ImportFilename), "text/csv", Path.GetFileName(fileClientName));
                    }
                }
            }
            return View();
        }
    }
}
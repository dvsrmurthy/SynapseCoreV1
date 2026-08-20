using Core.Models.Dtos.Requests.Synapse.AlertsManager;
using Core.Models.Dtos.Requests.Synapse.MailBox;
using Core.Models.Dtos.Requests.Synapse.ManageMobilityCenter;
using Core.Models.Dtos.Requests.Synapse.UserCampaigns;
using Core.Models.Dtos.Requests.Synapse.UserContacts;
using Core.Models.Dtos.Responses.Synapse.UserCampaigns;
using Core.Models.Enums;
using Synapse.Web.CampaignPlugin.Helpers.SecureAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace Synapse.Web.CampaignPlugin.Models
{
    public class QuickSMSOrCampaignMain
    {
        public QuickSMSOrCampaign QuicksmsorCampaign { get; set; }
        public List<QuickSMSOrCampaign> QuicksmsorCampaignCollections { get; set; }
        public List<QuickSMS> QuicksmsCollections { get; set; }
        public List<CampaignSMS> CamapignsmsCollections { get; set; }
        public List<Sender> Senders { get; set; }
        public List<CampsCampaignType> CampaignTypes { get; set; }
        public string? ExtGSMCharecters { get; set; }
        public List<MessageDetails> MsgDetails { get; set; }
        public List<Group> Groups { get; set; }
        public List<TemplateCreation> TemplateCreations { get; set; }
        public List<templatemapcolumns> TemplateMapColumns { get; set; }
        public List<Nationality> Nationality { get; set; }
        public List<City> City { get; set; }
        public List<Incomegroups> Incomegroup { get; set; }
    }
    public class QuickSMSOrCampaign
    {
       
        public int CustID { get; set; }
        public int LogOnUserId { get; set; }
        public long QuickOrCampaignId { get; set; }
        public string? CampaignNameorRecipient { get; set; }
        public string? Sender { get; set; }
        public long SenderId { get; set; }
        public string? AnySender { get; set; }
        public bool CampTypeID { get; set; }
        public string? CampType { get; set; }
        public int LanguageId { get; set; }
        public string? Language { get; set; }
        //[AllowHtml]
        public string? Message { get; set; }
        public int CharCount { get; set; }
        public int CreditsUsed { get; set; }
        public int TotalQSMSCredits { get; set; }
        public long TotalCAMPCredits { get; set; }
        public long TotalRecipientsCount { get; set; }
        public string? SendDate { get; set; }
        public int ScheduledType { get; set; }
        public string? Schedule { get; set; }
        public string? Criteria { get; set; }
        public string? PlaceHolders { get; set; }
        public long DuplicateRecipients { get; set; }
        public bool AllowDups { get; set; }
        public int Dlr { get; set; }
        public string? TypeofCampaign { get; set; }
        public string? Status { get; set; }
        public int intStatus { get; set; }
        public long CreatedBy { get; set; }
        public string? CreatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public string? UpdatedOn { get; set; }
        public string? AddedDate { get; set; }
        public string? ScheduleDate { get; set; }
        public FILETYPE RecipientsType { get; set; }
        public int RecipentID { get; set; }
        public string? strRecipentID { get; set; }
        public string? ActualFileName { get; set; }
        public string? ImportFileName { get; set; }
        public string? GroupIds { get; set; }
        public long ValidCount { get; set; }
        public long InValidCount { get; set; }
        public Cstatus CurrentStatus { get; set; }
        public string? Recipients { get; set; }
        public string? RejectNote { get; set; }
        public string? Stage { get; set; }
        public string? MobileField { get; set; }
        public string? PreprocessTime { get; set; }

        public mob MobileNumberField { get; set; }
        public mob MessageField { get; set; }
        public bool Template { get; set; }
        public int TemplateID { get; set; }
        public bool DeliveryReport { get; set; }
        public string? PreviewMessage { get; set; }

        public string? ExcelFile { get; set; }
        public string? TextFile { get; set; }
        public string? GroupFile { get; set; }

        public string? Module { get; set; }
        public string? UserList { get; set; }

        public string? ScheduleType { get; set; }
        public string? ScheduleStartTime { get; set; }
        public string? ScheduleStartMinutes { get; set; }
        public string? ScheduleStartDate { get; set; }
        public int RuleId { get; set; }
        public string? Remarks { get; set; }
        public string? MobileNumber { get; set; }
        public int SheetID { get; set; }
        public string? Sheet { get; set; }
        public string? TestMessagePrv { get; set; }
        public bool isRowEnable { get; set; }
        public int ProcessedCount { get; set; }

        public string? IncomeGroup { get; set; }
        public string? Nationality { get; set; }
        public string? City { get; set; }
        public string? Gender { get; set; }
        public string? CampaignCount { get; set; }
        public string? FromRange { get; set; }
        public string? ToRange { get; set; }
        public bool IsCheckerRequired { get; set; }
        public string? category { get; set; }

        public List<QuickSMSOrCampaign> buildmodel(int quickId, int userId, int status, int testSMSC, int ModuleType, string UserIp, string sText, bool isCheckerRequired = false)
        {
            using (var clientAccess = new AuthenticateSecurityClient())
            {
                var response =
                    clientAccess.ShowGridQuick(new ShowGridQuickOnRequest
                    {
                        CreatedUserId = userId,
                        QuickOrCampaignId = quickId,
                        Status = status,
                        FromDate = "",
                        ToDate = "",
                        SearchCampaign = sText,
                        UserIp=UserIp,
                        ModuleType = ModuleType,
                        ischecker = isCheckerRequired,
                    });


               // return (response.Result != null && response.Result.Any()) ?
                var returnresponse = new List<QuickSMSOrCampaign>();
                foreach (var x in response.Result)
                {
                    var res = new QuickSMSOrCampaign();
                    
                        res.QuickOrCampaignId = x.ID;
                        res.CampaignNameorRecipient = x.Name;
                        //res.CampTypeID = ( x.CAMPAIGNTYPE == 1 ? false : true);
                        res.CampTypeID = (x.CAMPAIGNTYPE == 1 ? false : true);
                        res.SenderId = x.SenderId;
                        res.Sender = x.Sender;
                        res.Language = x.LanguageName;
                        res.LanguageId = x.Language;
                      // res.Message = !string.IsNullOrWhiteSpace(x.Message) ? (x.Message.Replace(System.Environment.NewLine, "").Replace("'", "$HDC$").Replace("\"", "$HDCD$")) : string.Empty;
                     //    res.Message = !string.IsNullOrWhiteSpace(x.Message) ? System.Web.Security.AntiXss.AntiXssEncoder.HtmlEncode(x.Message, useNamedEntities: true) : string.Empty;
                        // res.Message = x.Message;
                         res.Message = !string.IsNullOrWhiteSpace(x.Message) ? (x.Message.Replace("\n", "$nl$").Replace("\r", "$rl$").Replace("'", "$sq$").Replace("\"", "^^")).Replace("/\r\n/g", "**").Replace("&lt;", "<").Replace("&gt;", ">") : string.Empty;
                        res.CharCount = x.CharCount;
                        res.CreditsUsed = x.CreditsUsed;
                        res.TotalQSMSCredits = !string.IsNullOrWhiteSpace(x.Name) ? x.Name.Split(',').Length * x.CreditsUsed : 0;

                        res.ScheduledType = x.ScheduledType;
                        res.Schedule = x.Schedule;
                        res.Criteria = (x.MessageType == 1 ? x.Criteria.Replace("'", "$sq$").Replace("\"", "^^"): x.Criteria);
                        res.PlaceHolders = x.PlaceHolders;
                        res.AllowDups = (x.DuplicateRecipients == 1) ? true : false;

                        res.Dlr = x.Dlr;
                        res.TypeofCampaign = ((x.MessageType == 3) ? "QuickSMS" : (x.MessageType == 2) ? "CustomSMS" : "BulkSMS");
                       // res.Status = ((x.Status == 1 && x.MessageType == 3) ? "Submitted" : (x.Status == 0 && x.MessageType == 3) ? "Pending" : (x.Status == 0 && x.MessageType != 3) ? "Deactivated" : (x.Status == 8 && x.MessageType != 3) ? "Paused" : (x.Status == 12 && x.MessageType != 3) ? "Suspended" : (x.Status == 4 && x.MessageType != 3) ? "Submitted" : (x.Status == 5 && x.MessageType != 3) ? "Completed" : "Pending");
                        res.Status = ((x.Status == 0 ) ? "Scheduled" : (x.Status == 1 ) ? "In Progress" : (x.Status == 2 ) ? "Stopped" : (x.Status == 3 ) ? "Paused" : (x.Status == 4 ) ? "Suspended" : (x.Status == 5) ? "Completed" : "Pending");
                        res.isRowEnable = (x.Status == 0 && x.MessageType != 3) ? false : true;
                        res.intStatus = x.Status;
                        res.CreatedBy = x.CreatedBy;
                        res.CreatedOn = x.CreatedOn;
                        res.UpdatedBy = x.UpdatedBy;
                        res.UpdatedOn = x.UpdatedOn;
                        res.AddedDate = x.AddedDate;
                        res.ScheduleDate = x.ScheduleDate;
                        res.PreprocessTime = x.PreprocessTime;
                        res.RecipientsType = (FILETYPE)x.RecipientsType;
                        res.RecipentID = x.RecipientsType;
                      //  res.strRecipentID = Convert.ToString(x.RecipientsType);
                        res.ActualFileName = !string.IsNullOrWhiteSpace(x.ActualFileName) ? (x.ActualFileName.Replace("\\\\", "\\")).Replace("\\", "\\\\") : string.Empty;
                        res.ImportFileName = (x.ImportFileName != null) ? x.ImportFileName.Replace(System.Environment.NewLine, "").Replace("\\", "^").Replace("/\r\n/g", "**") : x.ImportFileName;
                        res.GroupIds = x.GroupIds;
                        res.RuleId = x.RuleId;
                        res.Remarks = x.Remarks;
                        res.ValidCount = x.ValidCount;
                        res.TotalCAMPCredits = x.ValidCount * x.CreditsUsed;
                        res.CurrentStatus = (Cstatus)x.CurrentStatus;                        
                        res.MobileField = x.MobileField;
                        res.Sheet = x.SheetName;
                        res.ProcessedCount = x.ProcessedCount;
                        res.Recipients = (x.MessageType != 3 ? (GetRecipientType(x.RecipientsType, x.ActualFileName, x.ImportFileName, x.GroupIds)) : "");
                        res.IsCheckerRequired = (x.IsCheckerRequired == 1 ? true : false); 
                        res.CustID = x.CustID;
                        res.category = x.Category;
                        returnresponse.Add(res);
                }
                return returnresponse;                
            }
        }

        public List<Sender> buildSenders(int UserId)
        {
            using (var clientAccess =new AuthenticateSecurityClient())
            {
                var response = clientAccess.loadSenderIDCampaigns(new LoadSenderIDCampaignsOnRequest
                {
                    USERID = UserId
                });
                return(response.Result!=null && response.Result.Any())?
                    response.Result.Select(x=>new Sender{
                    SenderID=x.Id,
                    SenderName = x.Code
                    }).ToList():new List<Sender>();
                    
            }
        }

        public List<Nationality> buildNationality()
        {
            using (var clientAccess = new AuthenticateSecurityClient())
            {
                var response = clientAccess.loadNationalityCampaigns(new LoadNationalityCampaignsOnRequest { });
                return (response.Result != null && response.Result.Any()) ?
                    response.Result.Select(x => new Nationality
                    {
                        Id = x.Id,
                        NationalName = x.NationalName
                    }).ToList() : new List<Nationality>();

            }
        }

        public List<City> buildCity()
        {
            using (var clientAccess = new AuthenticateSecurityClient())
            {
                var response = clientAccess.loadCityCampaigns(new LoadCityCampaignsOnRequest{});
                return (response.Result != null && response.Result.Any()) ?
                    response.Result.Select(x => new City
                    {
                        Id = x.Id,
                        CityName = x.CityName
                    }).ToList() : new List<City>();

            }
        }

        public List<Incomegroups> buildIncomegroup()
        {
            using (var clientAccess = new AuthenticateSecurityClient())
            {
                var response = clientAccess.loadIncomegroupCampaigns(new LoadIncomegroupCampaignsOnRequest { });
                return (response.Result != null && response.Result.Any()) ?
                    response.Result.Select(x => new Incomegroups
                    {
                        Id = x.Id,
                        Incomegroup = x.Incomegroup
                    }).ToList() : new List<Incomegroups>();

            }
        }

        private string GetRecipientType(int RecipientsType, string ActualFileName, string ImportFileName, string GroupIds)
        {

            string strReturn = string.Empty;           
            if (RecipientsType == Convert.ToInt32(FILETYPE.EXCEL) | RecipientsType == Convert.ToInt32(FILETYPE.NOTEPAD))
            {
                string strExt = ActualFileName.Substring(ActualFileName.LastIndexOf(".") + 1);
                string strFile = ImportFileName.Substring(ImportFileName.LastIndexOf("\\") + 1);
                string strPath = ImportFileName.Replace(strFile, "");
                if (strExt.ToLower() == "txt" | strExt.ToLower() == "csv")
                {
                    strReturn = strFile + "','" + Uri.EscapeDataString(strPath);
                }
                else if (strExt.ToLower() == "xls" | strExt.ToLower() == "xlsx")
                {
                    strReturn = strFile + "','" + Uri.EscapeDataString(strPath);
                }
            }
            else if (RecipientsType == Convert.ToInt32(FILETYPE.GROUPS))
            {
                if (!string.IsNullOrEmpty(GroupIds))
                {
                    strReturn = "";
                }
            }
            return strReturn;
        }
    }

    public class CustomPreView 
    {
        public string? MobileNo { get; set; }
        public string? ReplacedMsg { get; set; }
        public List<CustomPreView> CustomPreViews { get; set; }
    }

    public class QuickSMS
    {
        public int QuickId { get; set; }
        public string? Sender { get; set; }
        public string? Language { get; set; }
        public string? Message { get; set; }
        public int CharCount { get; set; }
        public int CreditsUsed { get; set; }
        public bool Dlr { get; set; }
        public string? SentDate { get; set; }
        public string? PreprocessTime { get; set; }
        public string? Status { get; set; }
        public string? Module { get; set; }
        public string? UserList { get; set; }
        public Cstatus CurrentStatus { get; set; }
        public List<QuickSMS> buildmodel(int quickId, int userId, int status, int testSMSC, string UserIp, string requestpage = "")
        {
            using (var clientAccess = new AuthenticateSecurityClient())
            {
                var response =
                    clientAccess.ShowGridQuick(new ShowGridQuickOnRequest
                    {
                        CreatedUserId = userId,
                        QuickOrCampaignId = quickId,
                        Status = status,
                        FromDate = "",
                        ToDate = "",
                        SearchCampaign = "",
                        UserIp=UserIp
                    });
                return (response.Result != null && response.Result.Any()) ?
                    (from x in response.Result
                     select new QuickSMS
                     {
                         QuickId = Convert.ToInt32(x.ID),
                         Sender = x.Sender,
                         Language = ((x.Language == 1) ? "English" : "Arabic"),
                         Message = x.Message,
                         CharCount = x.CharCount,
                         CreditsUsed = x.CreditsUsed,
                         Dlr = x.Dlr == 1 ? true : false,
                         SentDate = x.ScheduleDate,
                         PreprocessTime = x.PreprocessTime,
                         Status = ((x.Status == 1) ? "Submitted" : "Pending"),
                         Module = ((x.MessageType == 3) ? "QuickSMS" : (x.MessageType == 2) ? "CustomSMS" : "BulkSMS"),
                         UserList = x.Name,
                         CurrentStatus = (Cstatus)x.CurrentStatus
                     }).ToList() : null;
            }
        }        
    }
    public class CampaignSMS
    {
        public int Id { get; set; }
        public bool IsCustomeSMS { get; set; }
        public Compose Compose { get; set; }
        public RecipientDetails RecipientDetails { get; set; }
        public MessageDetails MessageDetails { get; set; }
        public List<CampaignSMS> buildmodel(int QuickId, int UserId, EventStatus status, int TestSMSC)
        {
            return new List<CampaignSMS>();
        }
    }
    public class Compose
    {
        public string? CampaignName { get; set; }
        public int SenderId { get; set; }
    }
    public class RecipientDetails
    {
        public string? FileUploadedPath { get; set; }
        public string?[] MobileNoFields { get; set; }
        public List<Group> Groups { get; set; }
    }
    public class MessageDetails
    {
        public int GsmID { get; set; }
        public string? GsmChar { get; set; }
        public string? GsmType { get; set; }
        public string? ExtGSMChar { get; set; }
        public List<MessageDetails> buildGSMChars()
        {
            using (var clientAccess = new AuthenticateSecurityClient())
            {
                var response =  clientAccess.GetgGSMCharsQSMSCamp();
                return (response.Result != null && response.Result.Any()) ?
                    response.Result.Select(x => new MessageDetails
                    {
                        GsmID = x.Id,
                        GsmChar = x.Char,
                        GsmType = x.Type
                    }).ToList() : new List<MessageDetails>();
            }
        }

    }
    public class Group
    {
        public int GrpId { get; set; }
        public string? GrpName { get; set; }
        public Cstatus CurrentStatus { get; set; }
        public int ContactsCount { get; set; }

        public List<Group> buildGroups(int UserID)
        {
            using (var clientAccess = new AuthenticateSecurityClient())
            {
                var response = clientAccess.PopulateGroups(new GetGroupsContactsOnRequest {
                GROUPID=0,
                CREATEDBY=UserID,
                STATUS=1,
                REQUESTEDBY=""
                });
                return (response.Result != null && response.Result.Any()) ?
                    response.Result.Select(x => new Group
                    {
                        GrpId = x.GroupId,
                        GrpName = x.GroupName,
                        ContactsCount = x.ContactsCount,
                        CurrentStatus = (Cstatus)x.CurrentStatus
                    }).ToList() : new List<Group>();
            }
        }
    }
    public class ScheduleLater
    {
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> ScheduleCollection { get; set; }

        public DateTime ScheduleDate { get; set; }
        public DateTime PreprocessTime { get; set; }
    }

    // ***********************
    public class QuickSMSOrCampaign_
    {
        public string? TypeofCampaign { get; set; }
        public string? CampaignName { get; set; }
        public mob MobileNumberField { get; set; }
        public mob MessageField { get; set; }
        public bool Template { get; set; }
        public string? Message { get; set; }
        public bool DeliveryReport { get; set; }
        public string? Schedule { get; set; }      
        

        [DisplayName]
        public string? file { get; set; }
        public string? Name { get; set; }
        public string? SMSType { get; set; }
        public string? SenderID { get; set; }
        public string? Language { get; set; }
        public int ValidRecipients { get; set; }
        public int Credits { get; set; }
        public DateTime StartDate { get; set; }
        public string? Status { get; set; }
        public List<QuickSMSOrCampaign> buildmodel()
        {
            return new List<QuickSMSOrCampaign>
            {};
        }


    }
    public class Campmain
    {
        public QuickSMSOrCampaign Camp { get; set; }
        public List<QuickSMSOrCampaign> camps { get; set; }
    }
    public enum id
    {
        Babyshop,
        Splah,
        HomeCenter
    }
    public enum mob
    {
        Destination,
        Message
    }



    public class Contact
    {
        public int CreatedBy { get; set; }
        public string? GroupName { get; set; }
        public string? MobileNo { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; }
        public Cstatus Fstatus { get; set; }

        public List<Contact> buildContacts(string Groupids, string UserIp)
        {
            using (var clientAcces = new AuthenticateSecurityClient())
            {
                var response = clientAcces.ShowGridContacts(new ShowGridContactsOnRequest
                {
                    USERGROUPID = 0,
                    CREATEDBY = CreatedBy,
                    GroupID=Groupids,
                    STRSEARCH = "",
                    SearchText = "",
                    STATUS = 1,
                    UserIp = UserIp
                });
                return ((response.Result != null && response.Result.Any()) ?
                         response.Result.Select(x => new Contact
                         {
                             MobileNo = x.Mobileno,
                             FirstName = x.FirstName,
                             LastName=x.LastName,
                             Email=x.Email,
                             Status = (x.Status == true) ? "Active" : "InActive",
                             Fstatus = (Cstatus)x.CurrentStatus,
                             CreatedBy = x.CreatedBy
                         }).ToList() : new List<Contact>());
            }
        }

    }
    public class Sender
    {
        public int SenderID { get; set; }
        public string? SenderName { get; set; }
    }

    public class Nationality
    {
        public int Id { get; set; }
        public string? NationalName { get; set; }
    }

    public class City
    {
        public int Id { get; set; }
        public string? CityName { get; set; }
    }

    public class Incomegroups
    {
        public int Id { get; set; }
        public string? Incomegroup { get; set; }
    }

    public class Template
    {
        public int ID { get; set; }
        public string? NAME { get; set; }
        public string? TEXT { get; set; }
        public int LANGUAGEID { get; set; }
        public int TYPE { get; set; }
        public bool STATUS { get; set; }
        public List<Template> buildTemplates(int custID,int UserID,int campType)
        {
            using (var clientAccess = new AuthenticateSecurityClient())
            {
                var response = clientAccess.LoadTemplateCampaigns(new LoadTemplateCampaignsOnRequest { 
                CUSTID=custID,
                USERID=UserID,
                CAMPTYPE=campType,
                STATUS=2,
                TEMPID=""
                });
                return (response.Result != null && response.Result.Any()) ?
                    response.Result.Select(x => new Template
                    {
                        ID = x.TempId,
                        NAME = x.TemplateName,
                        TEXT = x.Text,
                        LANGUAGEID = x.Language,
                        TYPE = x.Type,
                        STATUS = x.TempStatus
                    }).ToList() : new List<Template>();
            }
        }
    }

    public class TemplateCreation
    {
        public int ID { get; set; }
        public string? NAME { get; set; }
        public string? TEXT { get; set; }
        public int LANGUAGEID { get; set; }
        public int TYPE { get; set; }
        public bool STATUS { get; set; }

        public List<TemplateCreation> buildmodel(int cust, int createdby, string UserIp, string requestPage = "", bool isCheckerRequired = false)
        {
            using (var clientAcces = new AuthenticateSecurityClient())
            {
                var response =
                    clientAcces.ShowGridTemplateDetails(new GetTemplateDetailsRequest
                    {
                        customer = cust,
                        Tempid = 0,
                        Status = 2,
                        CreatedByUser = createdby,
                        RequestPage = requestPage,
                        Return = 0,
                        UserIp = UserIp
                    });

                return (response.Result != null && response.Result.Any()) ?
                    response.Result.Select(x => new TemplateCreation
                    {
                        ID = x.TempId,
                        NAME = x.TemplateName,
                        TEXT = x.Text,
                        LANGUAGEID = x.Language,
                        TYPE = x.Type,
                        STATUS = x.TempStatus,
                    }).ToList() : new List<TemplateCreation>();
            }
        }

        public class templatemapcolumns
        {
            public int id { get; set; }
            public string? MessageFields { get; set; }
            public string? ColumnList { get; set; }
            public List<string> lbparamvaluesdisp { get; set; }
        }
    }

    public class templatemapcolumns
    {
        public int id { get; set; }
        public string? MessageFields { get; set; }
        public string? ColumnList { get; set; }
        public List<string> lbparamvaluesdisp { get; set; }
        public List<templatemapcolumns> buildmodelForGetColumns(int tempid)
        {
            using (var clientAcces = new AuthenticateSecurityClient())
            {
                var response =
                    clientAcces.ShowTemplateMapColumns(new GetTemplateDetailsRequest
                    {
                        Tempid = tempid,
                        Status = 2,
                        CreatedByUser = 0,
                        RequestPage = "",
                        Return = 0
                    });

                return (response.Result != null && response.Result.Any()) ?
                      response.Result.Select(x => new templatemapcolumns
                      {
                          id = x.id,
                          ColumnList = x.columns,
                          lbparamvaluesdisp = x.columns.Split(',').ToList(),
                          MessageFields = x.columns,
                      }).ToList() : new List<templatemapcolumns>();

            }

        }
    }
    public class CampsCampaignType
    {
        public int CampTypeID { get; set; }
        public string? CampType { get; set; }
        public List<CampsCampaignType> buildCampTypes()
        {
            using (var clientAccess = new AuthenticateSecurityClient())
            {
                var response = clientAccess.LoadCampaignTypes(new CampainTimingsLoadCampOnRequest
                {
                    CAMPTYPEID = 0,
                    CAMPTYPENAME = "",
                    STATUS = 2
                });
                return (response.Result != null && response.Result.Any()) ?
                    response.Result.Select(x => new CampsCampaignType
                    {
                        CampTypeID = x.Id,
                        CampType = x.Name
                    }).ToList() : new List<CampsCampaignType>();
            }
        }
    }

    public class FileUploadDet
    {
        public string? SheetName { get; set; }
        public List<string> Columns { get; set; }
        public JsonElement? FileRecord { get; set; }
        public List<JsonElement> FileRecords { get; set; }
        public string? FilePath { get; set; }
    }
}
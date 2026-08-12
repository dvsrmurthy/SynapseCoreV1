using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Dtos.Requests.Synapse.UserGroup;
using Core.Models.Dtos.Responses.Synapse.UserGroup;
using Core.Models.Dtos.Requests.Synapse.UserMoKeyWordConfig;
using Core.Models.Dtos.Responses.Synapse.UserMoKeyWordConfig;
using Core.Models.Dtos.Responses.Synapse.UserMoCampaignConfiguration;
using Core.Models.Dtos.Requests.Synapse.UserMoCampaignConfiguration;
using Core.Models.Dtos.Requests.Synapse.UserManagement;
using Core.Models.Dtos.Responses.Synapse.UserManagement;
using Core.Models.Dtos.Responses.Synapse.SecurityManagement;
using Core.Models.Dtos.Requests.Synapse.SecurityManagement;
using Core.Models.Dtos.Requests.Synapse.CreditsManagement;
using Core.Models.Dtos.Responses.Synapse.CreditsManagement;
using Core.Models.Dtos.Responses.Synapse.SMSCSettings;
using Core.Models.Dtos.Requests.Synapse.SMSCSettings;
using Core.Models.Dtos.Requests.Synapse.Customers;
using Core.Models.Dtos.Responses.Synapse.Customers;
using Core.Data.Utilities;
using Core.Models.Dtos.CommonDtos;
using Core.Models.Enums;
using Core.Models.Dtos.Responses.Synapse.UserCampaigns;
using Core.Models.Dtos.Requests.Synapse.UserCampaigns;
using Core.Models.Dtos.Requests.Synapse.UserContacts;
using Core.Models.Dtos.Responses.Synapse.UserContacts;
using Core.Models.Dtos.Requests.Synapse.ManageMobilityCenter;
using Core.Models.Dtos.Responses.Synapse.ManageMobilityCenter;
using Core.Models.Dtos.Responses.Synapse.AdminOperation;
using Core.Models.Dtos.Requests.Synapse.AdminOperation;
using Core.Models.Dtos.Requests.Synapse.UserDND;
using Core.Models.Dtos.Responses.Synapse.UserDND;
using Core.Models.Dtos.Requests.Synapse.UserMoInbox;
using Core.Models.Dtos.Responses.Synapse.UserMoInbox;
using Core.Models.Dtos.Responses.Synapse.SMPP;
using Core.Models.Dtos.Requests.Synapse.SMPP;
using Core.Models.Dtos.Responses.Synapse.MailBox;
using Core.Models.Dtos.Requests.Synapse.MailBox;
using Core.Models.Dtos.Responses.Synapse.AlertsManager;
using Core.Models.Dtos.Requests.Synapse.AlertsManager;
using Core.Models.Dtos.Responses.Synapse.DBAlerts;
using Core.Models.Dtos.Requests.Synapse.DBAlerts;
using Core.Models.Dtos.Responses.Synapse.Reports;
using Core.Models.Dtos.Requests.Synapse.Reports;
using Core.Models.Extensions;
using Core.Models.Dtos.Responses.Synapse.Analytics;
using Core.Models.Dtos.Requests.Synapse.Analytics;
using Core.Models.Dtos.Responses.Synapse.HlrLookup;
using Core.Models.Dtos.Requests.Synapse.HlrLookup;
using Core.Models.Dtos.Responses.Synapse.EmailToSms;
using Core.Models.Dtos.Requests.Synapse.EmailToSms;
using Core.Models.Dtos.Responses.Synapse.EmailAndPushNotifications;
using Core.Models.Dtos.Requests.Synapse.EmailAndPushNotifications;
using Core.Models.Dtos.Responses.Synapse.StatusMonitor;
using Core.Models.Dtos.Requests.Synapse.StatusMonitor;



namespace Core.Data.IDataInterfaces.ISynapse
{


    public interface ISynapseCoreData
    {
        #region DashBoard Analystics

        Task<SMSMOResponse> GetSMSMOAnalyticsAsync(SMSMOAnalyticsRequest request);
        Task<DashBoardAnalyticsResponse> GetDashBoardAnalyticsAsync(DashBoardAnalyticsRequest request);
        Task<DashBoardAnalyticsResponse> GetDashBoardModulesAsync(DashBoardAnalyticsRequest request);
        Task<DashBoardAnalyticsResponse> GetDashBoardSucessRatioAsync(DashBoardAnalyticsRequest request);
        Task<DashBoardAnalyticsResponse> GetDashBoardPullSmsesAsync(DashBoardAnalyticsRequest request);
        Task<DashBoardAnalyticsResponse> GetDashBoardThroughPutAsync(DashBoardAnalyticsRequest request);
        Task<DashBoardAnalyticsResponse> GetDashBoardCampaignActivitiesAsync(DashBoardAnalyticsRequest request);
        Task<DashBoardAnalyticsResponse> GetDashBoardSMSCAsync(DashBoardAnalyticsRequest request);
        Task<DashBoardAnalyticsResponse> GetDashBoardWorldMapAsync(DashBoardAnalyticsRequest request);

        Task<bool> UpdateSmsForAnalytics(ReUsableRequest request);

        #endregion

        #region User

        #region Quick SMS / Bulk SMS(Campaign)
        //// Quick SMS or bulk sms menu item functions
        //Task<List<ShowGridQuickOnResponse>> ShowGridQuick(ShowGridQuickOnRequest request);
        //Task<List<ShowGridCampaignsOnResponse>> ShowGridCampaigns(ShowGridCampaignsOnRequest request);
        //Task<IsUnicodeCharacters> IsUnicodeCharsFound();
        //Task<long> GetRolesPriviliges(RolesPriviligesOnRequest request);
        //Task<LoadCampaignTypeOnResponse> GetLoadCampaignType(LoadCampaignTypeOnRequest request);
        //Task<List<LoadSenderIDCampaignsOnResponse>> loadSenderIDCampaigns(LoadSenderIDCampaignsOnRequest request);
        //Task<List<LoadTemplateCampaignsOnResponse>> LoadTemplateCampaigns(LoadTemplateCampaignsOnRequest request);

        // Quick SMS or bulk sms menu item functions
        Task<List<ShowGridQuickOnResponse>> ShowGridQuick(ShowGridQuickOnRequest request);
        //Task<List<ShowGridCampaignsOnResponse>> ShowGridCampaigns(ShowGridCampaignsOnRequest request);
        //Task<IsUnicodeCharacters> IsUnicodeCharsFound();
        //Task<long> GetRolesPriviliges(RolesPriviligesOnRequest request);
        //Task<LoadCampaignTypeOnResponse> GetLoadCampaignType(LoadCampaignTypeOnRequest request);
        Task<List<LoadSenderIDCampaignsOnResponse>> loadSenderIDCampaigns(LoadSenderIDCampaignsOnRequest request);

        Task<List<LoadNationalityCampaignsOnResponse>> loadNationalityCampaigns(LoadNationalityCampaignsOnRequest request);
        Task<List<LoadCityCampaignsOnResponse>> loadCityCampaigns(LoadCityCampaignsOnRequest request);
        Task<List<LoadIncomegroupCampaignsOnResponse>> loadIncomegroupCampaigns(LoadIncomegroupCampaignsOnRequest request);

        Task<List<LoadTemplateCampaignsOnResponse>> LoadTemplateCampaigns(LoadTemplateCampaignsOnRequest request);
        Task<string> InsertQSMS(InsertQSMSOnRequest request);
        Task<List<GetgGSMCharsQSMSCampOnResponse>> GetgGSMCharsQSMSCamp();
        //Task<List<KeywordList>> GetKeywordsOnloadAsync();
        Task<int> CheckerUpdateQuickSMS(CheckerUpdateQSMSOnRequest Request);
        Task<string> InsertBulkSMS(InsertBulkSMSOnRequest request);
        Task<string> InsertTestSMSCamp(InsertTestSMSOnRequest request);
        Task<GetStageCountsOnResponse> GetCampStageCounts(GetStageCountsOnRequest request);
        Task<int> SetCampaignEvents(SetCampEventsOnRequest request);
        Task<List<MobileLengthValidationResponse>> ValidateMobileNums(ReUsableRequest request);
        Task<bool> ValidateCampaignName(ReUsableRequest request);
        Task<string> InsertCustomSMSActualCredits(InsertBulkSMSOnRequest request);
        Task<GroupContactsMain> GetGroupByContacts(ReUsableRequest request);
        //Task<CampaignDndnumbers> DndNumberCheck(ReUsableRequest request);
        Task<DndNonDndNumbers> DndNumberCheck(ReUsableRequest request);
        Task<string> CampaignStatusChange(ReUsableRequest request);

        Task<int> Externaldbcount(ExternalDB request);
        Task<List<MobileNos>> Externaldbfilter(ReUsableRequest request);
        Task<string> InsertExternalDB(InsertBulkSMSOnRequest request);
        Task<string> UpdateTestSMSCredits(InsertTestSMSOnRequest request);//Added By murty
        #endregion

        #region MO Operations Menu Item

        #region Inbox
        // Inbox menu item functions
        Task<List<MoInboxOnResponse>> MoInboxShowGrid(MoInboxOnRequest request);

        Task<List<MoForwardOnResponse>> MoForwardGrid(MoForwardOnRequest request);

        Task<List<MoSentBoxOnResponse>> MoSentboxShowGrid(MoSentBoxOnrequest request);

        #endregion

        #region KeyWord Configuration
        Task<List<MoKeyWordConfigOnResponse>> ShowGridMoKeyWordConfig(MoKeyWordConfigOnRequest request);
        Task<EditMoKeyWordConfigOnResponse> ShowGridMoKeyWordConfigForEdit(MoKeyWordConfigOnRequest request);
        Task<int> InsertMoKeyWordConfig(InsertMoKeyWordConfigOnRequest request);
        Task<int> ChangeStatusMoKeyWordConfig(ChangeStatusMoKeyWordConfigOnRequest request);

        Task<bool> ValidateIskeyExistedOrNot(ReUsableRequest request);
        //Task<bool> KeyActiveStatuChange(ReUsableRequest request);
        //Task<bool> KeyActiveStatusChange(ReUsableRequest request);

        Task<Int32> KeyActiveStatuChange(ReUsableRequest request);
        Task<Int32> SaveMOKeyword(InsertMoKeyWordConfigOnRequest request);
        Task<List<MoKeyWordConfigOnResponse>> GetMOKeyword(InsertMoKeyWordConfigOnRequest Request);


        Task<List<ShortcodeMO>> GetShortcodeByUserid(ReUsableRequest request);
        Task<ShortcodeAnalyticResponse> ShortcodeAnalytic(ShortcodeAnalyticRequest request);
        #endregion

        Task<List<WinnerResponse>> GetMOWinner(WinnerRequest Request);
        Task<List<WinnerReportResponse>> GetMOWinnerReport(WinnerReportRequest request);
        Task<Int32> SaveMOWinner(WinnerRequest request);
        Task<List<MOCampList>> GetCampaignByCustomerId(ReUsableRequest request);
        Task<List<MOCampList>> GetWinnerCampaignByCustomerId(ReUsableRequest request);
        Task<List<MOCampList>> LoadCampaignByWinnersettingId(ReUsableRequest request);
        Task<Int32> MOWinnerStatusChange(ReUsableRequest request);

        #region Campaign Configuration
        // Campaign Configuration men item functions
        #region MoCampaign
        Task<List<MoCampaignOnResponse>> ShowMoCampaigns(ShowMoCampaignOnRequest request);
        Task<GetMoCampaignNamesOnResponse> ViewMoCampaignNames(GetMoCampaignNamesOnRequest request);
        Task<int> InsertMoCampaigns(SaveMoCamapignOnRequest request);

        //code added  on 21092017

        Task<List<MoCampaignOnResponse>> GetAllMoCampaign(MoCampaignConfigRequest Request);
        Task<string> SaveMOCampaignConfig(MoCampaignConfigRequest request);

        Task<List<MoCampaignSearchResponse>> GetCustomerByMoUsersLookUp(MoCampaignSerchRequest request);
        Task<List<MoCampaignSearchResponse>> GetUserByMoUsersLookUp(MoCampaignSerchRequest request);

        Task<int> ChangeStatusMoCampaigns(ChangeStatusMoCampaignsOnRequest request);
        Task<List<LoadSenderIDsOnResponse>> BindSenderIDs(LoadSenderIDsOnRequest request);
        Task<List<LoadSMSCSOnResponse>> BindSMSCs(LoadSMSCSOnRequest request);
        Task<List<LoadKeywordsOnResponse>> BindKeywords(LoadKeywordsOnRequest request);

        Task<List<ShortCodeList>> GetShortcodeByUserAsync(ReUsableRequest request);
        Task<List<ShortCodeList>> GetMOShortcodeByUserAsync(ReUsableRequest request);
        Task<List<SendersList>> GetSenderByUserasync(ReUsableRequest request);
        Task<List<SMPPUserList>> GetSMPPUserByUserasync(ReUsableRequest request);
        Task<List<SmscList>> GetOutboundByUsersAsync(ReUsableRequest request);
        Task<List<KeywordList>> GetKeywordByUserasync(ReUsableRequest request);

        Task<Int32> MOCampaignActiveStatusChange(ReUsableRequest request);
        #endregion

        #region Moreply
        Task<List<MoReply>> ShowMoReplyDetails(ShowMoReplyOnRequest request);
        Task<MoReply> ViewMoReply(ShowMoReplyOnRequest request);
        Task<int> InsertMoReply(SaveMoReplyOnRequest request);
        #endregion

        #region MoForward
        Task<List<MoforwardOnResponse>> ShowMoForwardDetails(ShowMoForwardOnRequest request);
        Task<MoforwardOnResponse> ViewMoforward(ShowMoForwardOnRequest request);
        Task<int> InsertMoForward(SaveMoForwardOnRequest request);
        Task<DeleteMoForwardOnresponse> DeleteMoForward(DeleteMoForwardOnRequest request);
        #endregion

        #region MoSmppForward
        Task<List<MoSmppForwardOnResponse>> ShowMoSmppForwardDetails(ShowMoSmppForwardOnRequest request);
        Task<MoSmppForwardOnResponse> ViewMoSmppForward(ShowMoSmppForwardOnRequest request);
        Task<int> InsertMoSmppForward(SaveMoSmppForwardOnRequest request);
        Task<int> ChangeStatusMoSmppForward(ChangeStatusMoSmppOnRequest request);
        #endregion

        #endregion

        #region OutBox
        // Out box menu item functions

        #endregion

        #region MO Today
        // Mo Today menu item functions

        #endregion

        #region MO Analytics
        // MO Analytics menu item functions

        #endregion

        #region MoSelectByDropDownSearch
        //Task<List<MoSearchOnResponse>> MOSelectByDropDown(MoSearchOnRequest request);
        #endregion MoSelectByDropDownSearch

        #region MoSurvey
        Task<bool> VerifyIsSurveyExisted(ReUsableRequest request);
        Task<List<MoSurveyResponse>> GetMoSurvey(MOsurveyRequest request);
        Task<List<MoSenderGetResponse>> GetSendersByMoUsersLookUp(MoSenderGetRequest request);
        Task<List<MOMobileLengthValidationResponse>> MOValidateMobileNums(ReUsableRequest request);
        Task<string> InsertMOSurvey(MoSureveySaveRequest request);
        Task<List<MOCampaignbyuserresponse>> GetCampByUserIdAsync(MOCampaignbyuserrequest request);
        Task<int> SetMOActivities(Mosurveystatus request);
        #endregion


        #endregion

        #region SMStoEmail

        Task<List<GetSMSToEmailResponse>> GetSMSToEmail(GetSMSToEmailRequest request);
        Task<string> SaveSMStoEmail(SaveSMSToEmailRequest request);
        Task<List<GetMOSMSToEmailResponse>> GetSendersByUsersEmail(GetMOSMSToEmailRequest request);
        Task<List<GetMOSMSToEmailResponse>> GetMOCampaignbyUserId(GetMOSMSToEmailRequest request);
        Task<List<GetMOSMSToEmailResponse>> GetMOTemplatebyUserId(GetMOSMSToEmailRequest request);
        Task<List<GetMOSMSToEmailResponse>> GetMailByUsers(GetMOSMSToEmailRequest request);
        #endregion

        #region DB Alerts
        // DB Alerts menu item functions
        // Task<List<DBAlertsResponse>> GetDBAlerts(DBAlertsRequest request);

        #endregion

        #region Phone Book

        #region Groups
        // Groups menu item functions

        Task<List<ShowGridGroupsOnResponse>> ShowGridGroups(ShowGridGroupsOnRequests request);
        Task<ShowGridGroupsOnResponse> ShowGridGroup(ShowGridGroupsOnRequests request);
        Task<int> InsertGroups(SaveGroupsOnRequests request);
        Task<Int32> ChangeStatusGroups(ReUsableRequest request);
        Task<Int32> ApproveUserGroup(ApproveUserGroupOnRequest request);
        Task<Int32> RejectUserGroup(ApproveUserGroupOnRequest request);
        //Task<List<CustomerPreferencesOnResponse>> CustomerPreference(CustomerPreferencesOnRequests request);
        //Task<int> DeleteGroups(DeleteGroupsOnRequests request);
        #endregion

        #region Contacts

        Task<List<GetGroupsContactsOnResponse>> PopulateGroups(GetGroupsContactsOnRequest request);
        Task<List<ShowGridContactsOnResponse>> ShowGridContacts(ShowGridContactsOnRequest request);
        Task<ShowGridContactsOnResponse> ShowContactForEdit(ShowGridContactsOnRequest request);
        Task<int> InsertContacts(InsertContactsOnRequest request);
        Task<int> ChangeStatusContacts(ChangeStatusContactsOnRequest request);
        Task<int> DeleteContacts(DeleteContactsOnRequest request);
        Task<List<ExportContactsOnResponse>> ExportContacts(ExportContactsOnRequest request);
        Task<int> ImportContacts(ImportContactsCSVOnRequest request);
        #endregion
        #region DND
        Task<List<ShowGridDNDOnResponse>> ShowGridDND(ShowGridDNDOnRequest request);
        #endregion

        #endregion

        #region Change Password
        // Change password menu item functions

        #endregion

        #region EmailtoSMS
        Task<List<MailServerSettingsResponse>> GetMailServerSettings(MailServerSettingsRequest Request);
        Task<string> SaveMailServerSettings(MailServerSettingsRequest request);
        Task<Int32> MailServerSettingsStatusChange(ReUsableRequest request);
        Task<List<EmailToSmsResponse>> GetEmailToSMS(EmailToSmsRequest Request);
        Task<string> SaveEmailToSMS(EmailToSmsRequest request);
        Task<Int32> EmailToSMSStatusChange(ReUsableRequest request);

        Task<List<EmailTemplateResponse>> GetEmailTemplate(EmailTemplateRequest Request);
        Task<bool> VerifyIsTemplateExistedOrNotAsync(ReUsableRequest request);
        Task<Int32> SaveEmailTemplate(EmailTemplateRequest request);
        Task<Int32> ChangeStatusSE(ReUsableRequest request);
        Task<Int32> EmailTemplateStatusChange(ReUsableRequest request);
        Task<List<UsersList>> GetUsersBySMTPCustomerAsync(ReUsableRequest request);
        #endregion

        #endregion

        #region Admin

        #region Customers Menu Item
        // Customers Module Functions

        #region Creat Customers

        //Create Customers menu item functions

        Task<List<CustomerCreationResponse>> GetAllCustomersByIDAsync(ReUsableRequest request);

        Task<bool> IsCustomerNameExists(ReUsableRequest request);

        Task<CustomerCreationResponse> CreateCustomerAsync(CustomerCreationRequest request);

        Task<bool> AINCustomerAsynch(ReUsableRequest request);

        Task<bool> ApproveOrRejectCustomer(ReUsableRequest request);


        #endregion

        #region Customers Division

        // Customers menu item functions
        Task<List<DivisionsResponse>> GetAllDivisionsByIDAsync(DivisionsRequest request);
        #endregion

        #region Map Division

        // Map division menu item functions
        Task<List<MapDivisionsResponse>> GetAllMapDivsByIDAsync(MapDivisionsRequest request);
        #endregion

        #region Customer Preferences

        Task<CustomerAppPreferencesResponse> BuildCustomerAppPreferences(ReUsableRequest request);
        Task<List<CustomerAppPreferencesResponse>> GetCustomerAppPreferencesGrid(ReUsableRequest request);

        Task<bool> SaveCustomerAppPreferences(CustomerAppPreferencesResponse request);

        Task<bool> UpdateApproveOrRejectCustomerAppPreferences(ReUsableRequest request);

        #endregion

        #region Map Account Manager
        Task<Int32> UpdateAccount(MapAccountRequest request);
        #endregion
        #endregion

        #region User Management Menu Item

        #region User Creation
        // user creation menu item functions
        Task<List<SynapseUser>> GetAllUsersByIdAsync(GetUsersRequest request);

        Task<Int32> ApproveUserAsync(ApproveUserRequest request);

        Task<Int32> RejectUserAsync(ApproveUserRequest request);

        Task<SynapseUser> GetUserByIdAsync(GetUsersRequest request);

        Task<bool> ChangeStatusUC(ReUsableRequest request);

        Task<Int32> UpdateUserByUserId(UpdateUserRequest request);

        Task<ADResponse> ValidateIsUserExistedOrNot(ReUsableRequest request);

        #endregion

        #region IP Whitelist
        Task<List<GetUserIPWhiteListResponse>> GetIPWhiteList(GetUserIPwhiteListRequest request);
        Task<string> SetIPWhiteList(SetUserIPwhiteListRequest request);
        Task<Int32> ChangeStatusIP(ReUsableRequest request);
        Task<List<UsersList>> GetHTTPUsersByCustomerIdAsync(ReUsableRequest request);
       
        #endregion

        #region Account Manager Creation

        Task<List<AccountManagersResponse>> GetAccountManagersAsync();

        Task<int> CreateOrUpdateAccountManagerAsync(AccountManagersResponse request);

        Task<bool> ApproveOrRejectAccountManager(ReUsableRequest request);

        Task<bool> AccountStatusChange(ReUsableRequest request);

        #endregion

        #endregion

        #region Security Management Menu Item

        #region Roles Creation
        //Roles Creation menu item functions

        // Task<List<RolesCreation>> GetAllRolesByIdAsync(RolesCreationRequest request);
        Task<List<RolesCreation>> GetAllRolesByIdAsync(RolesCreationRequest request);

        Task<Int32> UpdateRolesByUserId(EditRolesCreation request);

        Task<Int32> CheckerRole(EditRolesCreation request);

        Task<bool> ValidateIsRoleExistedOrNot(ReUsableRequest request);

        Task<bool> RoleActiveStatusChange(ReUsableRequest request);

        #endregion

        #region Role Privilages Setup
        // Role Privilages Setup menu item functions
        Task<FeatureMainResponse> GetAllPrivilagesAsync(ReUsableRequest request);

        Task<FeatureMainResponse> GetAllprivilagesByCustomerAsync(ReUsableRequest request);

        Task<bool> SetRolePrivilagesByRoleId(RolePrivilageRequest request);
        #endregion

        #region Password Change Rules Menu Item
        // Password change menu item functions

        Task<Int32> SetTwoFactorRules(PasswordPreferenceRequest request);

        Task<Int32> Setpasswordpreference(PasswordPreferenceRequest request);
        Task<GetPasswordPreferenceResponse> GetPasswordPreference(GetPasswordPreferenceRequest request);
        Task<Int32> CheckerPasswordChangeRules(PSRCheckerRequest request);

        #endregion

        #region Feature Privilages Setup

        Task<FeaturePrivilagesSetupMain> buildFeaturePrivilagesSetup(ReUsableRequest request);

        Task<int> SetFeatureprivilagesSetyp(ReUsableRequest request);

        Task<bool> ActiveOrInActiveFeature(ReUsableRequest request);

        #endregion

        #region Departments

        Task<List<DepartmentsResponse>> GetAllDepartments(ReUsableRequest request);

        Task<bool> SaveOrUpdateDepartment(DepartemntsRequest request);

        Task<MessageResponse> IsDepartmentExistedByName(ReUsableRequest request);

        Task<bool> ApproveOrReject(ReUsableRequest request);

        Task<bool> DepartmentActiveStatusChange(ReUsableRequest request);

        #endregion

        #endregion

        #region Mobility Center Menu Item

        #region Manage Mobility Center
        // Manage Mobility Center menu item functions

        #endregion

        #region Application Configuration
        // application configuration menu item functions

        #endregion

        #region Campaign Timings
        // Campaign Timings menu item functions
        Task<List<CampaignTimingsOnResponse>> ShowGridCampaignTimings(CampaignTimingsOnRequest request);
        Task<List<CampainTimingsLoadCampOnResponse>> LoadCampaignTypes(CampainTimingsLoadCampOnRequest request);
        Task<int> InsertCampTimings(CampainTimingsInsertCampOnRequest request);
        Task<int> ChangestatusCampTimings(CampaignTimingsChangeStatusOnRequest request);
        Task<int> CheckerUpdateCampaignTimings(CheckerUpdateCampaignTimingsOnRequest request);

        Task<bool> AINMobilityAsynch(ReUsableRequest request);
        #endregion

        #endregion

        #region Admin Operations Menu Item



        #region Change Password
        // Change Password menu item functions
        Task<bool> IsValidPassAsync(ReUsableRequest request);
        Task<ChangePasswordResponse> GetPreferenceValue(ChangePasswordRequest request);
        Task<bool> UpdatekPassByUserId(ReUsableRequest request);
        #endregion

        #region Application Configuration
        // Application Configuration Menu item functions
        Task<Int32> SaveAppConfig(ApplicationConfigurationReq request);
        Task<List<ApplicationConfigurationRes>> GetAppConfig(ApplicationConfigurationReq request);
        #endregion

        #region Filter Words
        Task<List<FilterWordsRes>> GetFilterWords(FilterWordsReq request);
        Task<Int32> AddFilterWordById(SetFWReq request);
        Task<int> ImportFilterWords(ImportFWReq request);
        Task<Int32> FiterDBCheck(CheckerFilterWordsRequest request);
        Task<bool> ChangeStatusFW(ReUsableRequest request);
        Task<Int32> CheckerFilterWords(CheckerFilterWordsRequest request);

        // Filter Words menu item functions

        #endregion

        #region DND List
        // DND List menu item functions
        Task<List<DNDListResponse>> GetAllDNDListAsync(DNDListRequest request);
        Task<Int32> CheckerDNDList(StatusUpdateDNDList request);

        Task<Int32> InsertDNDList(InsertDNDList request);
        Task<int> ImportDNDS(ImportDNDRequest request);
        Task<Int32> FileDBCheckDND(StatusUpdateDNDList request);
        Task<bool> ChangeStatusDND(ReUsableRequest request);
        Task<List<ExportDNDRes>> ExportDNDS(ExportDNDReq request);
        Task<List<ExternalDBResponse>> GetExternalDBGrid(ExternalDBRequest request);
        Task<string> ImportExternalDBS(ExternalDBRequest request);
        //Task<int> ImportExternalDBS(ExternalDBRequest request);
        #endregion

        #region WhiteListNumbers
        Task<List<WhiteListNumbersResponse>> GetAllWhiteListNumberstAsync(WhiteListNumbersRequest request);

        Task<List<WhitelistResponse>> GetAllWhiteListAsync(WhitelistRequest request);
        Task<bool> WhitelistStatus(ReUsableRequest request);
        Task<Int32> InsertWhitelist(InsertWhitelist request);
        //Task<List<ExportWhitelistResponse>> ExportWhitelist(ExportWhitelistRequest request);
        Task<int> ImportWhitelistNumbers(ImportWlistnoRequest request);

        #endregion

        #region Un-Lock User
        // Un-Lock user menu item functions

        Task<List<UnlockUserResponse>> GetAllUnlockByIdAsync(UnlockUserRequest request);
        Task<Int32> UpdateLockUserByUserId(UpdateLockStatus request);
        //Checker 
        Task<bool> UnlockStatus(ReUsableRequest request);
        Task<Int32> ApproveUnlockUserAsync(UpdateLockStatus request);
        Task<Int32> RejectUnlockUserAsync(UpdateLockStatus request);
        #endregion

        #region AuditLog
        //Task<List<AuditLogResponse>> GetAuditLog(AuditLogRequest request);
        Task<List<AuditLogsResponse>> GetAuditlogAsync(AuditLogsRequest request);
        Task<List<AuditLogsDetailedResponse>> GetDetailedAuditlogAsync(AuditLogsRequest request);
        Task<List<UsersList>> GetUsersByCustomerIdAsyncString(ReUsableRequest request);
        Task<List<PriviligeList>> GetpriviligesbyCustomerId(ReUsableRequest request);
        #endregion

        //#region HLR LookUp Rules

        //Task<List<HlrLookUpRulesResponse>> GetHlrLookUpAsync(HlrLookupRequest request);

        //Task<List<HlrLookUpRulesResponse>> GetAllHlrLookUpsByCustomerAync(ReUsableRequest request);

        //Task<List<UserLookup>> GetUsersByCustomerLookUpAsync(ReUsableRequest request);

        //Task<List<SnderLookUp>> GetSendersByUsersLookUpAsync(ReUsableRequest request);

        //Task<List<Int32>> SaveHlrLookUpResult(List<HlrLookupRequest> request);
        ////Task<Int32> SaveHlrLookUpResult(List<HlrLookupRequest> request);
        //Task<bool> HlrLookupStatus(ReUsableRequest request);
        //Task<Int32> CheckerHlrLookup(HlrLookupRequest request);
        //#endregion
        #region HLR LookUp Rules

        Task<List<HlrLookUpRulesResponse>> GetHlrLookUpAsync(HlrLookupRequest request);

        Task<List<HlrLookUpRulesResponse>> GetAllHlrLookUpsByCustomerAync(ReUsableRequest request);

        Task<List<UserLookup>> GetUsersByCustomerLookUpAsync(ReUsableRequest request);

        Task<List<SnderLookUp>> GetSendersByUsersLookUpAsync(ReUsableRequest request);

        Task<List<Int32>> SaveHlrLookUpResult(List<HlrLookupRequest> request);
        //Task<Int32> SaveHlrLookUpResult(List<HlrLookupRequest> request);
        Task<bool> HlrLookupStatus(ReUsableRequest request);
        Task<Int32> CheckerHlrLookup(HlrLookupRequest request);
        #endregion

        #endregion

        #region SMSC Settings Menu Item

        #region Country Master
        // Country Master Menu item functions

        Task<List<CountryMasterResponse>> GetAllCountryByIdAsync(CountryDetails Request);

        Task<String> AddEditCountryMaster(AddEditCountry request);

        Task<bool> ApproveOrRejectCountry(ApproveOrRejectRequestCountry request);

        Task<bool> CountryActiveStatusChange(ReUsableRequest request);

        #endregion

        #region Operators
        // Operators menu item functions
        Task<List<OperatorsResponse>> GetAllOperatorsByIDAsync(OperatorsRequest request);

        Task<String> OperatorAddEdit(AddEditOperator request);

        Task<Int32> SeriesDuplicaeCheck(ReUsableRequest request);

        Task<DtoBulkOperatorsMainResponse> BulkSeriesAdd(DtoBulkOperatorsMainRequest request);

        Task<List<OperatorSeriesResponse>> GetAllOperatorSeries(int request);

        Task<bool> ChangeStatusOP(ReUsableRequest request);

        Task<bool> DeleteOperatorLegs(ReUsableRequest request);

        Task<bool> ApproveOrRejectOperator(ApproveOrRejectRequest request);
        #endregion

        #region SMSC Master
        // SMSC master menu item functions
        Task<List<GetSMSCINTLDetailsResponse>> ShowGridSMSCMasterDetails(GetSMSCINTLDetailsRequest request);
        Task<List<GetINTLVendorsResponse>> GetVendors(GetINTLVendorsRequest request);
        Task<List<GetInstanceResponse>> GetInstance(ReUsableRequest request);
        // Task<List<GetRouteStagesResponse>> GetActiveStages(GetRouteStagesRequest request);

        //Task<List<GetCountryCodesResponse>> GetCountryCodes(GetCountryCodesRequest request);
        //Task<List<GetOperatorsResponse>> GetOperators(GetOperatorsRequest request);
        Task<List<GetIntlSMSCIdResponse>> GetIntlSMSCId(GetIntlSMSCIdRequest request);
        Task<GetConnectionsResponse> ShowSMSCDetailsForedit(GetConnectionsRequest request);
        //Task<int> InsertSMSCINTLDetails(SetSMSCINTLDetailsRequest request);
        List<string> ChangeStatusSMSCINTL(UpdateSMSCINTLStatusRequest request);
        Task<int> CheckerUpdateSMSCMaster(CheckerUpdateUserSMSCMasterRequest request);

        //Task<int> InsertSMSCIntlConnectionDetailsHTTP(SetConnectionsHTTPRequest request);
        Task<List<int>> InsertSMSCIntlConnectionDetailsSMPP(SetConnectionsSMPPRequest request);

        //code added on 07082017  one extra action call to verify the connection to route dependancy check
        //check the dependancy for the connection on routes    value 3
        Task<int> CheckDependancyConectionRoute(SetConnectionsSMPPRequest request);

        #endregion

        #region Sender ID / Short Code
        // Sender Id / Short Code menu item functions
        // Task<List<SenderIDResponse>> GetAllSenderByIdAsync(SenderIDRequest Request);
        Task<List<SenderIDResponse>> GetAllSenderByIdAsync(SenderIDRequest Request);
        Task<List<SenderIDResponse>> GetAllSenderByIdAsyncSearch(SenderIDRequest Request);
        Task<String> AddEditSenderIDShortCode(AddEditSender request);

        //Task<bool> AINSenderIDShortCodeAsynch(ReUsableRequest request);
        Task<Int32> AINSenderIDShortCodeAsynch(ReUsableRequest request);



        Task<Int32> SIDSCCheck(AorRSIDSC request);

        #endregion

        #region Map SMSC
        // Map SMSC menu item functions

        Task<List<SenderIdMapperResponse>> GetAllMapSenderDetailsAsync(SmscTableRequest request);
        Task<List<RouteResponseNew>> GetAllRoutesAsync(SMSCRoutes request);

        //Task<Int32> SaveOrUpdateMapSender(MapSenderRequest request);
        Task<string> SaveOrUpdateMapSender(MapSenderRequest request);

        List<string> ChangeStatusMS(ReUsableRequest request);
        Task<Int32> ApproveOrRejectMapSender(MapSenderRequest request);

        Task<MapSenderShortCodesNRoutes> GetShortCodesnRoutes(ReUsableRequest request);
        #endregion

        #region User Route

        Task<List<UserRouteOnResponse>> ShowGridUserRoute(UserRouteOnRequest request);
        Task<List<BindSeriesUserRouteOnResponse>> BindSeriesUserRoute(BindSeriesUserRouteOnRequest request);
        Task<List<GetVendorsUserRouteOnResponse>> GetVendorsUserRoute(GetVendorsUserRouteOnRequest request);
        Task<List<GetSMSCUserRouteOnResponse>> GetSMSCUserRoute(GetSMSCUserRouteOnRequest request);
        Task<List<GetCountriesUserRouteOnResponse>> GetCountriesUserRoute();
        Task<List<GetOperatorsUserRouteOnResponse>> GetOperatorsUserRoute();
        Task<int> InsertRouteUserRoute(InsertRouteUserRouteOnRequest request);
        List<string> ChangeStatusUserRoute(CheckDefaultRouteOnRequest request);
        Task<int> CheckerUpdateUserRoute(CheckerUpdateUserRouteOnRequest request);

        #endregion

        #region Test SMSC
        // Test SMSC menu item functions
        Task<List<TestSMSCResponse>> GetTestSMSCByIdAsync(TestSMSCRequest Request);
        //Task<Int32> AddTestSMSCByUser(AddSMSRequest request);
        Task<string> AddTestSMSCByUser(AddSMSRequest request);
        Task<Int32> CheckerTestSMSC(CheckTestSMSC request);



        #endregion

        #region Vendoe Master

        Task<List<AllVendorsResponse>> GetAllVendorsAsync(ReUsableRequest request);        
        Task<Int32> SetVendors(SetVendorsRequest request);

        Task<AllVendorsResponse> GetVendoeByVendorId(ReUsableRequest request);

        Task<Int32> ApproveOrRejectVendorAsync(ReUsableRequest request);

        Task<bool> VendorStautsChange(ReUsableRequest request);
        #endregion

        #region MO Map Sender
        Task<List<MOMapSenderResponse>> GetAllMOMapSenderByIdAsync(MOMapSenderRequest request);
        Task<string> AddMOMapSenderAsync(MOMapSenderRequest request);
        Task<Int32> MOShortcodeStatusChange(MOMapSenderRequest request);

        #endregion

        #region Prefered Route
        // Test SMSC menu item functions

        Task<List<PreferedRouteResponse>> GetAllPreferredByIdAsync(PreferedRouteRequest request);
        Task<Int32> AddPreferedByIdAsync(AddPreferedRouteReq request);
        //Task<Int32> InsertDNDList(AddPreferedRouteReq request);
        Task<Int32> CheckerPreferedRouteUser(CheckerPreferedRoute request);

        List<string> ChangeStatus(PreferedStatus request);
        #endregion

        #region SMSCSwitchover
        Task<SMSCSwithResponse> GetAllSMSCSwitchAsync(SMSCSwithRequest request);
        Task<string> UpdateSMSCSwitchOverasync(SMSCSwithRequest request);
        Task<SMSCSwithResponse> GetAllSMSCRoute(SMSCSwithRequest request);

        #endregion
        
        #region RateCard
        Task<List<RateCardResponse>> GetRateCard(RateCardRequest request);
        Task<List<RateCardResponse>> GetRateCardSearch(RateCardRequest request);
        Task<Int32> AddRateCardByIdAsync(InsertRateCardRequest request);
        Task<Int32> CheckerRateCard(CheckerRateCardRequest request);
        // Task<List<PackagesbyVIdRes>> Getpackagebyid(PackagesbyVIdReq request);

        Task<bool> RatecardStatus(ReUsableRequest request);
        Task<List<RateCardResponse>> GetRateCardHistory(RateCardRequest request);
        #endregion

        #endregion

        #region Credits Management Menu Item

        #region Customer Credits
        // Customer Credits menu item functions
        Task<List<CustomerCreditsResponse>> GetAllCreditsByIdAsync(CustomerCreditsRequest request);
        Task<List<CustomerCreditsResponse>> GetAllCreditsBySearchAsync(CustomerCreditsRequest request);
        Task<CustomerCreditsResponse_1> GetCustDetails(CustomerCreditsRequest request);
        Task<int> SaveCustomerCredits(SaveCustomerCreditsOnRequest request);

        Task<int> ApproveCustomerCredits(ApproveCustomerCreditOnRequest request);
        Task<int> RejectCustomerCredits(ApproveCustomerCreditOnRequest request);

        Task<List<GetCustomersOnResponse>> GetAllCustomers();

        //Task<int> SaveCustomerCredits(ShowGridCustomerCreditsOnRequest request);
        //Task<int> SetCustomerCredits(ShowGridCustomerCreditsOnRequest request);
        //Task<int> UpdateAvailableCredits(ShowGridCustomerCreditsOnRequest request);

        #endregion

        #endregion

        #region More

        #region Running Compaigns
        //Running Compaingns menu item functions

        #endregion

        #endregion

        #region mailbox menu item

        Task<List<UserMailBoxMappingResponse>> GetUserMappingMailAsync(UserMailBoxMappingRequest request);
        Task<Int32> SetUserMailboxAsync(InsertUserMailboxMappingRequest request);
        Task<List<UserbyCustomerIdRes>> GetUserbyId(UserbyCustomerIdReq request);
        Task<List<Core.Models.Dtos.Responses.Synapse.SMSCSettings.SenderbyUserIdRes>> GetSenderbyId(SenderbyUserIdReq request);
        //Task<Int32> CheckerUserMailbox(UserMailconfigReq request);
        #region Configuration
        Task<List<MailBoxConfigurationResponse>> GetAllMailAsync(MailBoxConfigurationRequest request);
        Task<Int32> SetAllMailAsync(AddMailBoxConfiguration request);
        Task<Int32> CheckerMailConfig(CheckerMailconfigRequest request);
        #endregion


        #endregion


        #region Profiles
        int TestConnections(TestConnectionOnRequest Request);
        List<DefaultDBsOnResponse> Getdefultdbs(TestConnectionOnRequest Request);
        Task<List<GetProfileOnResponse>> GetProfiles(GetProfilesOnRequest Request);
        Task<int> SaveProfiles(ProfilesCreationOnRequest Request);
        Task<List<GetEditProfilesOnResonse>> GetEditProfiles(GetEditProfileOnRequest Request);
        Task<int> UpdateProfileStatus(UpdateProfileStatusOnRequest Request);
        Task<int> CheckerUpdateProfiles(ApproveRejectProfileCreation request);
        #endregion

        #region BusinessRules

        Task<List<GetBusinessProfilesOnResponse>> GetBusinessProfiles(GetBusinessProfilesOnRequest request);
        Task<int> SaveOrUpdateBusinessRules(InsertOrUpdateBusinessOnRequest request);
        Task<int> EMailOrSMSSettings(ReUsableRequest request);
        Task<List<GetBusinessRulesOnResponse>> ShowGridBusinessRules(GetBusinessRulesOnRequest request);
        Task<int> ChangeStatusBusinessRule(StatusUpdatedOnRequest request);
        Task<int> ApproveOrRejectBusinessRule(ApproveBusinessRuleOnRequest request);
        Task<string[]> TestStatement(TestStatementOnRequest Request);
        Task<TestStatementViewOnResponse> TestStatementView(TestStatementOnRequest Request);
        Task<List<BankInformationOnResponse>> GetBankDetails(GetBankInformationDetailsRequest request);
        Task<Int32> CheckAlertForScheduleOnOff(ReUsableRequest request);
        #endregion BusinessRules

        #region Online Alerts
        Task<int> InsertOnlineCreation(SetOnlineAlertsRequest request);
        Task<List<GetBusinessRulesResponse>> GetBusinessRules(GetBusinessRulesRequest request);
        Task<List<GetSenderResponse>> GetSenderDetails(GetSenderRequest request);
        Task<List<GetTemplatesResponse>> GetTemplateDetails(GetTemplatesRequest request);
        Task<List<GetTemplatesResponse>> GetTemplateDetailsByTempId(GetTemplatesRequest request);
        Task<List<SetAlertsRes>> GetOnlineAlertsDetails(Core.Models.Dtos.Requests.Synapse.AlertsManager.DBAlertsRequest request);
        Task<int> ChangeOnlineAlertsStatus(ChangeOnlineAlertsStatusRequest request);
        Task<GetOnlineAlertsDetailsResponse> ShowOnlineAlertsDetailsForedit(GetOnlineAlertsDetailsRequest request);
        Task<int> CheckerUpdateOnlineAlerts(ApproveRejectAlertsCreation request);
        Task<string[]> GetDBQueryOnlineAlerts(GetBusinessRulesRequest request);
        Task<string[]> GetPreviewDetailsOnlineAlerts(GetBusinessRulesRequest request);
        Task<List<GetTemplatesResponse>> GetEmailTemplateDetails(GetTemplatesRequest request);
        Task<List<GetTemplatesResponse>> GetTemplateByUser(ReUsableRequest request);
        #endregion

        #region Offline Alerts
        Task<int> InsertOfflineCreation(SetOfflineAlertsRequest request);
        Task<List<GetBusinessOfflineRulesResponse>> GetBusinessOfflineRules(GetBusinessOfflineRulesRequest request);
        Task<List<GetOfflineAlerts>> GetOfflineAlertsDetails(GetOfflineAlertsRequest request);
        Task<int> ChangeOfflineAlertsStatus(ChangeOfflineAlertsStatusRequest request);
        Task<GetOfflineAlertsDetailsResponse> ShowOfflineAlertsDetailsForedit(GetOfflineAlertsRequest request);
        Task<int> CheckerUpdateOfflineAlerts(ApproveRejectOfflineAlerts request);
        #endregion

        # region Templates
        Task<List<GetTemplateDetailsResponse>> ShowGridTemplateDetails(GetTemplateDetailsRequest request);
        Task<List<gettemplatecolumnsresponse>> ShowTemplateMapColumns(GetTemplateDetailsRequest request);
        Task<int> ChangeTemplateStatus(ChangeTemplateStatusRequest request);
        Task<int> CheckerUpdateTemplates(ApproveRejectTemplateCreation request);
        Task<int> InsertTemplateCreation(SetTemplatesRequest request);
        Task<GetTemplateDetailsResponse> ShowTemplateDetailsForedit(GetTemplateDetailsRequest request);
        #region UserMappings
        Task<List<GetTemplateUserMapDetailsResponse>> ShowGridTemplateUserMapDetails(GetTemplateUserMapDetailsRequest request);
        Task<GetTemplateUserMapDetailsResponse> ShowGridTemplateUserMapDetailsForEdit(GetTemplateUserMapDetailsRequest request);
        Task<int> CheckerTemplateUserMap(ApproveRejectTemplateCreation request);
        Task<int> ChangeTemplateUserMapStatus(ChangeTemplateUserMapStatusRequest request);
        Task<List<GetCustomersDetailsResponse>> ShowCustomersDetails(GetCustomersDetailsRequest request);
        Task<List<GetUsersDetailsResponse>> ShowUsersDetails(GetUsersDetailsRequest request);
        Task<int> InsertTemplateUserMapping(SetTemplateUserMappingRequest request);
        #endregion
        #endregion

        #region Reset User Password
        Task<bool> UpdateUserPassword(ReUsableRequest request);
        #endregion

        #region SMPP
        Task<List<GetSmppResponse>> GetSMPPSenderAsync(GetSmppRequest request);
        // Task<List<SetSmppResponse>> SetSMPPSenderAsync(SetSmppRequest request);
        Task<Int32> SetSMPPSenderAsync(SetSmppRequest request);
        Task<List<SmppIdRes>> GetSmppById(SmppIdReq request);

        #region SMPPMASTER

        Task<List<GetUsersSMPPMasterOnResponse>> GetUsersSMPPMaster(GetUsersSMPPMasterOnRequest request);
        Task<List<SMPPMasterOnResponse>> ShowGridSMPPMaster(SMPPMasterOnRequest request);
        Task<List<GetCustomerSMPPMasterOnResponse>> GetCustomerSMPPMaster(GetCustomerSMPPMasterOnRequest request);
        Task<int> InsertSMPPMaster(InsertSMPPMasterOnRequest request);
        Task<int> ChangeStatusSMPPMaster(ChangeStatusSMPPMasterOnRequest request);
        Task<int> CheckerUpdateSMPPMaster(CheckerUpdateSMPPMasterOnRequest request);
        Task<List<GetInstanceResponseSMPP>> GetInstanceSMPP(ReUsableRequest request);

        #endregion

        #region SMPPIPALLOCATION

        Task<List<GetSmppMasterIPAllocationOnResponse>> GetSMPPMasterIPAllocation(GetSmppMasterIPAllocationOnRequest request);
        Task<List<GetSmppIPAllocationOnResponse>> GetSMPPIPAllocation(GetSmppIPAllocationOnRequest request);
        Task<int> InsertSMPPIPAllocation(SetSmppIPAllocationOnRequest request);
        //Task<int> CheckerUpdateSMPPIP(CheckerUpdateSMPPIPOnRequest request);

        #endregion

        #endregion

        #region Reports
        //SmscTracking
        Task<NewSmsTrackingMain> GetSmstrackingAsync(SmsTrackingRequest request);

        //QuickSMSQuery
        Task<NewSmsQueryMain> GetQuickSMSQueryAsync(SmsQueryRequest request);

        //DetailedAnalysis
        Task<NewSmsQueryMain> GetSMSQueryAsync(SmsQueryRequest request);
        Task<NewSmsQueryMain> GetSMSQuerySummary(SmsQueryRequest request);
        Task<NewSmsQueryMain> GetSMSQueryDetailed(SmsQueryRequest request);
        Task<NewSmsQueryMain> GetObjectData(SmsQueryRequest request);

        //smsTraffic
        Task<SmsTrafficMain> GetSmsTrafficOfMonthAsync(SmsTrafficRequest request);

        Task<ReportsCommonLookups> GetAllActiveAccountManagersAsync();

        //Customer view
        Task<CustomerSmsTrafficMain> GetCustomerSmsTrafficOfMonthAsync(CustomerViewrequest request);
        Task<List<SubCustomerList>> GetSubCustomersByCustomerIdAsync(ReUsableRequest request);
        Task<List<UsersList>> GetUsersByCustomerIdAsync(ReUsableRequest request);

        Task<List<UsersList>> GetUsersByCustomerIdAsyncRep(ReUsableRequest request);

        Task<List<UsersList>> GetUsersByCustomerMOIdAsync(ReUsableRequest request);

        Task<List<UsersList>> GetUsersByCustomerMOIdAsyncRep(ReUsableRequest request);

        //BusinessRuleReport
        Task<NewBusinessRuleReport> GetBusinessRuleReportsAsync(BusinessRuleReportRequest request);

        Task<NewBusinessRuleReport> GetBusinessRuleReportSecondResultSetAsync(BusinessRuleReportRequest request);

        //Accontmanager
        Task<AccountManagerMain> GetAccountManagerTrafficOfMonthAsync(AccountManagerrequest request);

        //vendor
        Task<VendorMain> GetVendorSmsTrafficOfMonthAsync(Vendorrequest request);
        Task<List<Vendorlist>> GetUsersByVendorIdAsync(ReUsableRequest request);
        //finance

        Task<FinanceMain> GetFinanceViewAsync(FinanceViewRequest request);

        //vendorfinanceview
        Task<VendorFinanceviewMain> GetVendorFinanceViewAsync(VendorFinanceviewrequest request);
        //customerfinance

        Task<CustomerFinanceviewMain> GetCustomerFinanceViewAsync(CustomerFinanceviewrequest request);

        //Hlr
        Task<HlrMain> GetHlrReportViewDetailsAsync(HlrRequest request);

        Task<List<SendersList>> GetSenderbyUserId(ReUsableRequest request);

        Task<List<SendersList>> GetSenderbyUserIds(ReUsableRequest request);

        Task<List<SendersList>> GetSenderByUserIdcamp(ReUsableRequest request);

        Task<List<SendersList>> GetSendersByUsersEId(ReUsableRequest request);

        Task<List<SendersList>> GetSenderbyUserIdTest(ReUsableRequest request);

        Task<List<CountryGlobalTable>> GetCountrybyUserId(ReUsableRequest request);
        Task<List<OperatorList>> GetOperatorbyUserId(ReUsableRequest request);
        Task<List<UsersList>> GetVendorbyUserId(ReUsableRequest request);
        Task<List<OperatorList>> GetOperatorbyCountry(ReUsableRequest request);

        Task<List<CampList>> GetCampaignByDay(ReUsableRequest request);
        Task<NewCampaignDetailedMain> GetCampaignDetailedAsync(CampaignDetailedRequest request);
       
        Task<NewCampaignSummaryMain> GetCampaignSummaryAsync(CampaignSummaryRequest request);
        Task<NewCampaignSummaryMain> GetCampaignSecondSummaryAsync(CampaignSummaryRequest request);
        Task<MISReportResponseMain> GetMISReportAsync(MISReportRequest request);
        Task<SenderWiseResponseMain> GetSenderWiseAsync(SenderWiseRequest request);
        Task<MoSummaryResponseMain> GetMoSummaryAsync(MoSummaryRequest request);
        Task<MoDetailedResponseMain> GetMoDetailedAsync(MoDetailedRequest request);
        Task<MoDetailedResponseMain> GetMoDetailedRCAsync(MoDetailedRequest request);
        Task<List<ShortcodeMO>> GetShortcodesByUser(ReUsableRequest request);
        Task<List<KeywordMO>> GetKeywordsByUser(ReUsableRequest request);

        Task<List<MOSurveyResponse>> GetMoSurveyAsync(MOSuyveyRequest request);
        Task<List<SurveyMO>> GetSurveyByDay(ReUsableRequest request);

        Task<DownloadDlrResponseMain> GetDownloadDlrWiseAsync(DownloadDlrRequest request);

        Task<ExternalCampaignResponseMain> GetExternalCampaignAsync(ExternalCampaignRequest request);
        #endregion

        #region userstatistics
        Task<List<GetGBLSenderResponse>> ShowINTLSenderDetails(GetGBLSenderDetailsRequest request);
        Task<List<GETINTLUSERSTATDETAILSResponse>> SHOWINTLUSERSTATDETAILS(GETINTLUSERSTATDETAILSREquest request);
        #endregion

        #region Bank Information
        Task<int> InsertBankCreation(BankInformationOnRequest request);
        Task<BankInformationOnResponse> ShowBankInformationDetailsForedit(GetBankInformationDetailsRequest request);
        Task<List<BankInformationOnResponse>> GetBankInformationDetails(GetBankInformationDetailsRequest request);
        Task<int> ChangeBankInformationStatus(GetBankInformationDetailsRequest request);
        Task<int> ApproveBankInformationCreation(ApproveRejectBankInformationDetailsCreation request);
        #endregion

        #region HlrLookUp
        Task<List<HlrLookupUploadResponse>> GetAllHlrLookupUpload(HlrLookupRequestUpload request);
        Task<int> SaveHlrLookupUpload(SaveHlrLookupRequestUpload request);
        Task<List<CountryMasterHlrResponse>> GetAllCountryByIdHlrAsync(CountryDetailsHlr Request);
        Task<int> ChangeStatusHlrLookupUpload(StatusHlrUpdatedOnRequest request);
        #endregion

 

        #region SenderConfiguration
        Task<int> InsertSenderConfiguration(SenderConfigurationRequest request);
        Task<GetSenderConfigurationResponseforedit> ShowSenderConfigurationDetailsForedit(GetSenderConfigurationDetailsRequest request);
        Task<List<SenderConfigurationResponse>> GetSenderConfigurationDetails(GetSenderConfigurationDetailsRequest request);
        //Task<SenderConfigurationResponse> ShowSenderConfigurationDetailsForedit(GetSenderConfigurationDetailsRequest request);
        Task<int> ChangeSenderConfigurationStatus(GetSenderConfigurationDetailsRequest request);
      //  Task<int> CheckerUpdateSenderConfiguration(ApproveRejectSenderConfigurationOnRequest request);

        #endregion

        #region CardBin
        Task<int> InsertCardBinCreation(CardBinOnRequest request);
        Task<List<CardBinOnResponse>> GetCardBinInformationDetails(GetCardBinDetailsRequest request);
        Task<int> ChangeCardBinStatus(GetCardBinDetailsRequest request);
        Task<CardBinOnResponse> ShowCardBinInformationDetailsForedit(GetCardBinDetailsRequest request);
        Task<int> ApproveCardBinDetails(ApproveRejectCardBinDetails request);
        Task<List<BankCardBinList>> GetCardbinbyBankName(ReUsableRequest request);
        #endregion

        #region EmailPlugin

        #region Register Email

        Task<List<RegisterEmailResponse>> GetRegisteredEmails(ReUsableRequest request);

        Task<RegisteremailSaveResponse> SaveOrUpdateRegisteredFromMail(RegisterEmailRequest request);

        Task<RegisteremailSaveResponse> ValidateFromMail(RegisterEmailRequest request);
        Task<Int32> ChangeStatusRegEmail(RegisterEmailRequest request);
        #endregion

        #region Map Register Mail

        Task<List<MapRegisterEmailResponse>> GetMapRegisteredEmails(ReUsableRequest request);

        Task<RegisteremailSaveResponse> SaveOrUpdateMapRegisterMail(MapRegisterEmailRequest request);
        Task<Int32> ChangeStatusMapEmail(MapRegisterEmailRequest request);
        #endregion

        #region Email Campaign

        Task<RegisteremailSaveResponse> SaveOrUpdateEmailCampaign(EmailCampaignPDb request);

        Task<List<EmailCampaign>> GetEmailCampaigns(EmailCampaignRequest request);
        #endregion

        #region EmailAnalysis
        Task<EmailAnalysisResponseMain> GetEmailAnalysis(EmailAnalysisRequest request);
        Task<EmailAnalysisResponseMain> GetEmailAnalysisSummary(EmailAnalysisRequest request);
        Task<EmailAnalysisResponseMain> GetEmailAnalysisDetailed(EmailAnalysisRequest request);
        #endregion

        #endregion

        #region Push Notifications Plugin

        #region Register PushNotification

        Task<List<RegisterPushNotificationsResponse>> GetRegisterPushNotificationCollection(
            RegisterPushNotificationRequest request);

        Task<RegisteremailSaveResponse> SaveOrUpdateRegisterPushNotifications(RegisterPushNotificationRequest request);
        Task<Int32> ChangeStatusAppReg(RegisterPushNotificationRequest request);

        #endregion

        #region PushNotification Campaign

        //Task<List<AppRegistrationResponse>> GetSecretKeyAndLabels(ReUsableRequest request);
        Task<int> SetPNCampActivities(EmailCampaignPDb request);

        #endregion

        #region PushNotificationAnalysis
        Task<PNAnalysisResponseMain> GetPNAnalysis(PNAnalysisRequest request);
        Task<PNAnalysisResponseMain> GetPNAnalysisSummary(PNAnalysisRequest request);
        Task<PNAnalysisResponseMain> GetPNAnalysisDetailed(PNAnalysisRequest request);
        #endregion

        #endregion

        #endregion

        string Getdata();

        #region vitis refresh
        Task<string> BuildConfigurationData(string type, string smscid = null, string groupname = null, string esmeid = null);
        #endregion

        Task<string> OTPQSMS(InsertQSMSOnRequest request);
        Task<string> GetRadioData(InsertQSMSOnRequest request);

        #region "StatusMonitor"
        Task<List<StatusCustUserResponse>> GetCustomerUserAsync(CustUserSearch request);
        Task<List<PromoSummaryResponse>> GetPromoSummaryAsync(PromoSummarySearch request);
        Task<List<StatusMapSenderResponse>> GetMapSenderAsync(MapSenderSearch request);
        Task<List<StatusSMSCResponse>> GetSMSCMasterAsync(SMSCSearch request);
        Task<List<StatusCustomerResponse>> GetCustomerMasterAsync(CustomerSearch request);
        Task<List<StatusUserResponse>> GetUsersMasterAsync(UserSearch request);
        Task<OperatorResponse> GetDlrPercentageAsync(DLRPercentageSearch request);
        Task<CountryResponse> GetDlrPercentageCAsync(DLRPercentageSearch request);
        string GetConnectionString();
        Task<List<StatusSrvrTransactionResponse>> GetServerTransactionAsync(ServerTransactionSearch request);        
        #endregion
    }
}

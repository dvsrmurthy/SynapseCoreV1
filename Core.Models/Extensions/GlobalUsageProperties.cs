using Core.Models.Dtos.CommonDtos;
using Core.Models.Dtos.Responses.Synapse.Account;
using Core.Models.Dtos.Responses.Synapse.SMSCSettings;
using System;
using System.Collections.Generic;


namespace Core.Models.Extensions
{
    public class GlobalUsageProperties
    {
        public List<Roles> Roles { get; set; }
        public List<DivisionTable> DivisionTable { get; set; }
        public List<CustomerList> CustomerList { get; set; }
        public List<CCredits> CCredits { get; set; }
        public List<CampaignsTable> CampaignsTable { get; set; }
        public List<ContactsTable> ContactsTable { get; set; }
        public List<UsersList> UsersList { get; set; }
        public List<SendersList>SendersList { get; set; }
        public List<CountryGlobalTable> CountryGlobalTable { get; set; }
        public List<PreferedCountryList> PreferedCountryList { get; set; }
        public List<PreferedList> PreferedList { get; set; }
        public List<VendorsNameList> VendorsNameList { get; set; }
        public List<AccountManagerList> AccountManagerList { get; set; }
        public List<CustomerAccountList> CustomerAccountList { get; set; }
        public List<MailList> MailList { get; set; }
        public List<MobilityList> MobilityList { get; set; }
        public List<ModuleList> ModuleList { get; set; }
        public List<StagesList> StagesList { get; set; }
        public List<SMPPIDList> SMPPIDList { get; set; }
        public List<OutboundSenderList> OutboundSenderList { get; set; }
        public List<ShortCodeList> ShortCodeList { get; set; }
        public List<MOShortCodeList> MOShortCodeList { get; set; }
        public int AlertTypeId { get; set; }
        public List<CustomerPrefList> CustomerPrefList { get; set; }
        public List<AdminResellers> AdminResellers { get; set; }
        public List<OperatorList> OperatorList { get; set; }
        public List<OperatorListRate> OperatorListRate { get; set; }
        public List<ShortcodeMO> ShortcodeMO { get; set; }
        public List<KeywordMO> KeywordMO { get; set; }
        public List<CampaignsList> CampaignsList { get; set; }
         
        public List<PackageList> PackageList { get; set; }
        //customer
        public List<CustomerViewlist> CustomerViewlist { get; set; }
        //vendor
        public List<VendorViewlist> VendorViewlist { get; set; }

        public List<Vendorlist> Vendorlist { get; set; }

        public ApplicationGlobalVariables ApplicationGlobalVariables { get; set; }

        public List<CampList> CampList { get; set; }

        public List<MOCampList> MOCampList { get; set; }
        public List<SenderList> SenderList { get; set; }

        public List<SurveyMO> SurveyMO { get; set; }
        public List<KeywordList> KeywordList { get; set; }

        public List<SmscList> SmscList { get; set; }
        public int UserType { get; set; }

        public List<GroupList> GroupList { get; set; }
        public List<SMPPUserList> SMPPUserList { get; set; }
        public List<PriviligeList> PriviligeList { get; set; }

        public List<ConnectionList> ConnectionList { get; set; }
        public List<TemplateList> TempalateList { get; set; }
        public List<AllVendorsResponse> allVendorsResponses { get; set; }
        public List<ConnectionListRate> ConnectionListRate { get; set; }
        public List<MenuItem> MenuItemsList { get; set; }    
    }

    public class Roles
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsReseller { get; set; }

        public bool Status { get; set; }

        public int CurrentStatus { get; set; }

        public int CreatedBy { get; set; }

        public string CreatedOn { get; set; }

        public int Level { get; set; }
    }

    public class CustomerList
    {
        public int Id { get; set; }

        public string Name { get; set; }        
    }

    public class CustomerPrefList
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class AdminResellers
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
   

    public class UsersList
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public int ParentId { get; set; }


    }
    public class SubCustomerList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class OperatorList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SendersList
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int Type { get; set; }
    }

    public class CountryGlobalTable
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public int CountryCode { get; set; }
    }

    public class DivisionTable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }
  
        public int CreatedBy { get; set; }

        public string CreatedOn { get; set; }

        public int UpdatedBy { get; set; }

        public string UpdatedOn { get; set; }
    }

    public class CustomeUser
    {
        public LogOnRespons LogOnRespons { get; set; }

        public PreferencesResponse UserPreferences { get; set; }
    }

    public class MenuItem
    {
        public string Name { get; set; }

        public string ArabicName { get; set; }

        public string MenuIcon { get; set; }

        public List<SubMenuItems> ChildMenuItems { get; set; }
    }

    public class SubMenuItems
    {
        public string Name { get; set; }

        public string ArabicName { get; set; }

        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public string AreaName { get; set; }

        public bool IsCheckerRequired { get; set; }
        public int UserRole { get; set; }
        public int RateCardRoleId { get; set; }
        public int ParentCustomerId { get; set; }
    }

    public class CCredits
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PreferedList
    {
        //public int Id { get; set; }
        public string CodeName { get; set; }
        public string IdwithSeriesid { get; set; }
        public int CoountryCode { get; set; }
    }

    public class PreferedCountryList
    {
        public int Countrycode { get; set; }
        public string CountryName { get; set; }
        
    }

    public class CampaignsTable
    {
        public int CampId { get; set; }
        public string CampName { get; set; }
    }

    public class ContactsTable
    {
        public int ListId { get; set; }
        public string ListName { get; set; }
    }

    public class VendorsNameList
    {

        public int Id { get; set; }
        public string VendorName { get; set; }
    }
    public class OperatorListRate
    {
        public int OPRID { get; set; }
        public string OPRNAME { get; set; }
        public int CNTID { get; set; }
        public string CNTNAME { get; set; } 
        public int OPRCNTRY { get; set; }        
    }
    //Added for AccountManagerMap
    public class AccountManagerList
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }
    public class CustomerAccountList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AcmId { get; set; }
        public bool IsAssigned { get; set; }
    }
    public class PackageList {
        public int Id { get; set; }
        public string PackageDetails { get; set; }
    }

    public class MailList
    {
        public int Id { get; set; }
        public string Mailbox { get; set; }
    }


    public class CustomerViewlist
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ParentId { get;set; }

        public int AcmId { get; set; }
    }

    public class VendorViewlist
    {
        public int Id { get; set; }
        public string VendorName { get; set; }
    }
    //vendor

    public class  Vendorlist
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
    public class MobilityList
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    public class ModuleList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class StagesList
    {
        public string NAME { get; set; }
    }
    public class SMPPIDList
    {
        public int UserId { get; set; }
        public string Name { get; set; }
    }
    public class OutboundSenderList
    {
        public int AutoId { get; set; }
        public string OutboundSender { get; set; }
    }
    public class ShortCodeList
    {
        public int Id { get; set; }
        public string Code { get; set; }
    }

    public class MOShortCodeList
    {
        public int Id { get; set; }
        public string Code { get; set; }
    }

    public class CampList
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class MOCampList
    {
        public string Id { get; set; }
        public string CampaignName { get; set; }
        public string WinnerSettingName { get; set; }
    }

    public class SenderList
    {
        public int Id { get; set; }
        public string Code { get; set; }
    }

    public class ShortcodeMO 
    {
        public string Id { get; set; }
        public string Shortcode { get; set; }
        public string CampaignName { get; set; }
    }
    public class KeywordMO
    {
        public string KeyWordId { get; set; }
        public string Name { get; set; }
        public string CampaignName { get; set; }
    }

    public class CampaignsList
    {
        public int Id { get; set; }
        public string CampaignName { get; set; }
    }
    public class SurveyMO
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class KeywordList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SmscList
    {
        public string SmscName { get; set; }
    }

    public class MoCampaignsList
    {
        public int Id { get; set; }
        public string CampaignName { get; set; }
    }

    public class GroupList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }

    public class SMPPUserList
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }
    public class PriviligeList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class BankCardBinList
    {
        public int Id { get; set; }
        public string CardBinNo { get; set; }
    }

    public class ConnectionList
    {
        public int Id { get; set; }
        public string SmscName { get; set; }
    }
    public class ConnectionListRate
    {
        public int Id { get; set; }
        public string SmscName { get; set; }
        public int VendorId { get; set; }
    }
    public class TemplateList
    {
        public int TempID { get; set; }
        public string TemplateName { get; set; }
        //public int UserId { get; set; }
    }
}

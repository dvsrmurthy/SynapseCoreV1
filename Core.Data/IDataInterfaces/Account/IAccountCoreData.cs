using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models.Dtos.CommonDtos;
using Core.Models.Dtos.Responses.Synapse.Account;
using Core.Models.Extensions;
using System.DirectoryServices.AccountManagement;
using Core.Models.Dtos.Responses.Synapse.UserManagement;
using Core.Models.Dtos.Responses.Synapse.SMSCSettings;

namespace Core.Data.IDataInterfaces.Account
{
    public interface IAccountCoreData
    {
        Task<LogOnRespons> AuthenticateUser(LogOnRequest request);

        Task<PreferencesResponse> GetAllPreferencesAsync(ReUsableRequest request);

        Task<GlobalUsageProperties> GetAllRolesAsync(ReUsableRequest request);

        //Task<GlobalUsageProperties> GetCreditsAsync();
        Task<List<CustomerList>> GetCustomersByAccountManagerId(ReUsableRequest request);

        Task<LogOnRespons> AuthenticateUserLogout(LogOnRequest requst);

        Task<GlobalUsageProperties> GetAllPreferred();
        Task<int> GetDashBoardTypeAsync(ReUsableRequest request);
        Task<int> GetAvailableCreditsAsync(ReUsableRequest request);
        Task<GlobalUsageProperties> GetAllPreferredCountry();

        Task<GlobalUsageProperties> GetAllDivisionsColomnsAsync(ReUsableRequest request);

        Task<GlobalUsageProperties> GetAllUsersAsync();

        Task<GlobalUsageProperties> GetAllSendersAsync();

        //Task<GlobalUsageProperties> GetAllCustPrefList();
        Task<GlobalUsageProperties> GetAllPrefLists(ReUsableRequest request);

        Task<GlobalUsageProperties> GetAdminResellers(ReUsableRequest request);

        // Task<GlobalUsageProperties> GetAllVendorsAsync();

        //Task<GlobalUsageProperties> GetAllVendorsColomnsAsync();

        Task<GlobalUsageProperties> GetAllVendorsRate();

        Task<GlobalUsageProperties> GetAllPackageRate();

        Task<List<CountryGlobalTable>> GetAllCountriesAsync();

        Task<List<ConnectionList>> GetAllSMSCAsync();
        Task<List<ConnectionListRate>> GetAllSMSCAsyncRate();
        //Task<bool> TestUsage();

        Task<GlobalUsageProperties> GetAllCustomerColomnsAsync(ReUsableRequest request);

        Task<GlobalUsageProperties> GetAllCustomerColomnsAsyncRep(ReUsableRequest request);

        Task<GlobalUsageProperties> GetCustomersForRoles(ReUsableRequest request);

        //Task<GlobalUsageProperties> GetAllDivisionsColomnsAsync();
        //Task<GlobalUsageProperties> GetAllDivisionsColomnsAsync(ReUsableRequest request);
        LdapUseDetailsResponse IsUserExists(string userName);

        Task<List<UserMenuItems>> GetUserMenuItemsByUser(ReUsableRequest request);

        Task<ApplicationGlobalVariables> GetAppGobalKeys();
        //For Accountmanger
        Task<GlobalUsageProperties> GetAccountManager();
        Task<List<CustomerAccountList>> GetCustomerAccountdrop(ReUsableRequest request);
        Task<List<CustomerAccountList>> GetCustomerAccount(ReUsableRequest request);

        Task<GlobalUsageProperties> GetAllMailboxAsync();
        Task<GlobalUsageProperties> GetModule();
        Task<GlobalUsageProperties> GetStages();
        Task<GlobalUsageProperties> GetMobility();
        Task<List<MOShortCodeList>> GetAllMOShortcode();
        Task<GlobalUsageProperties> GetSMPPIDAsync();
        Task<GlobalUsageProperties> GetShortCode();
        Task<GlobalUsageProperties> GetOutboundSender();



        //customers
        Task<GlobalUsageProperties> GetReportcustomer(ReUsableRequest request);
        //Task<GlobalUsageProperties> GetReportcustomer();

        //vendor
        Task<GlobalUsageProperties> GetVendor();
        Task<GlobalUsageProperties> GetAllStatusBoardCustomers(ReUsableRequest request);
        Task<GlobalUsageProperties> GetAllOperatorsRate();


    }
}

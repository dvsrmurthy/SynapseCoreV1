using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClientHTTPConsuming.Utilities;
using Core.Models.Dtos.CommonDtos;
using Core.Models.Dtos.Responses.Synapse.Account;
using Core.Models.Extensions;
using UriBuilder = ClientHTTPConsuming.Utilities.UriBuilder;
using Core.Models.Dtos.Responses.Synapse.Analytics;
using Core.Models.Dtos.Requests.Synapse.Analytics;
using Core.Models.Dtos.Responses.Synapse.Customers;
using Core.Models.Dtos.Requests.Synapse.UserCampaigns;
using Core.Models.Dtos.Responses.Synapse.UserCampaigns;
using Core.Models.Dtos.Requests.Synapse.SMSCSettings;
using Core.Models.Dtos.Responses.Synapse.SMSCSettings;
using Synapse.Web.Models;
using Core.Models.Dtos.Responses.Synapse.StatusMonitor;
using Core.Models.Dtos.Requests.Synapse.StatusMonitor;
using System.Data;

namespace Synapse.Web.Helpers.SecureAccess
{
    public class AuthenticateSecurityClient : DisposeBaseClass
    {
        private IConfiguration _configuration;        
        public AuthenticateSecurityClient()
        {
            _configuration = AppConfiguration.Configuration;
        }        
        public string? BaseServiceHostUrl
        {
            get
            {
                return !string.IsNullOrEmpty(_configuration["BaseServiceHostUrl"])
                    ? _configuration["BaseServiceHostUrl"]
                    : "http://localhost/APIServices/";
            }
        }

        public UriBuilder GetUriBuilderForServiceMethod(string suffix)
        {
            return new UriBuilder(BaseServiceHostUrl + suffix);
        }

        public LogOnRespons AuthenticateUser(LogOnRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("AuthenticateUser");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            var response = client.Post<LogOnRequest, LogOnRespons>(request);
            return response;
        }
        public LogOnRespons AuthenticateUserForgotPassword(LogOnRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("AuthenticateUserForgotPassword");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<LogOnRequest, LogOnRespons>(request);
        }
        public IpWhiteListResponse IpWhitelistByUser(IpWhiteListRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("IpWhitelistByUser");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<IpWhiteListRequest, IpWhiteListResponse>(request);
        }
        public LogOnRespons VerifyOTP(LogOnRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("VerifyOTP");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<LogOnRequest, LogOnRespons>(request);
        }

        public async Task<List<MobileLengthValidationResponse>> ValidateMobileNums(int senderId)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("ValidateMobileNums");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return
                client.Post<ReUsableRequest, List<MobileLengthValidationResponse>>(new ReUsableRequest
                {
                    SenderId = senderId
                });
        }
        public async Task<string> OTPQSMS(InsertQSMSOnRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("OTPQSMS");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<InsertQSMSOnRequest, string>(request);
        }

        public async Task<string> GetRadioData(InsertQSMSOnRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetRadioData");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<InsertQSMSOnRequest, string>(request);
        }

        public LogOnRespons AuthenticateUserLogout(LogOnRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("AuthenticateUserLogout");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<LogOnRequest, LogOnRespons>(request);
        }

        public async Task<DashBoardAnalyticsResponse> GetDashBoardAnalytics(DashBoardAnalyticsRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetDashBoardAnalyticsAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<DashBoardAnalyticsRequest, DashBoardAnalyticsResponse>(request);
        }
        public async Task<SMSMOResponse> GetSMSMOAnalytics(SMSMOAnalyticsRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetSMSMOAnalyticsAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<SMSMOAnalyticsRequest, SMSMOResponse>(request);
        }
        public async Task<DashBoardAnalyticsResponse> GetDashBoardModules(DashBoardAnalyticsRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetDashBoardModulesAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<DashBoardAnalyticsRequest, DashBoardAnalyticsResponse>(request);
        }
        public async Task<DashBoardAnalyticsResponse> GetDashBoardSucessRatio(DashBoardAnalyticsRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetDashBoardSucessRatioAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<DashBoardAnalyticsRequest, DashBoardAnalyticsResponse>(request);
        }
        public async Task<DashBoardAnalyticsResponse> GetDashBoardPullSmses(DashBoardAnalyticsRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetDashBoardPullSmsesAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<DashBoardAnalyticsRequest, DashBoardAnalyticsResponse>(request);
        }
        public async Task<DashBoardAnalyticsResponse> GetDashBoardThroughPut(DashBoardAnalyticsRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetDashBoardThroughPutAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<DashBoardAnalyticsRequest, DashBoardAnalyticsResponse>(request);
        }
        public async Task<DashBoardAnalyticsResponse> GetDashBoardCampaignActivities(DashBoardAnalyticsRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetDashBoardCampaignActivitiesAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<DashBoardAnalyticsRequest, DashBoardAnalyticsResponse>(request);
        }
        public async Task<DashBoardAnalyticsResponse> GetDashBoardSMSC(DashBoardAnalyticsRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetDashBoardSMSCAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<DashBoardAnalyticsRequest, DashBoardAnalyticsResponse>(request);
        }
        public async Task<DashBoardAnalyticsResponse> GetDashBoardWorldMap(DashBoardAnalyticsRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetDashBoardWorldMapAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<DashBoardAnalyticsRequest, DashBoardAnalyticsResponse>(request);
        }


        public bool UpdateSmsAnalyticsAsync(ReUsableRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("UpdateSmsAnalyticsAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<ReUsableRequest, bool>(request);
        }

        public PreferencesResponse GetAllPreferences(int custid)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetAllPreferencesAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            var empty = string.Empty;
            return client.Post<ReUsableRequest, PreferencesResponse>(new ReUsableRequest { CustomerId = custid });
        }

        public async Task<CustomerAppPreferencesResponse> GetCustomerAppPreferencesAsync(ReUsableRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetCustomerAppPreferencesAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<ReUsableRequest, CustomerAppPreferencesResponse>(request);
        }

        public async Task<GlobalUsageProperties> GetCreditsAsync()
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetCreditsAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            var empty = string.Empty;
            return client.Get<GlobalUsageProperties>(new object());
        }

        public async Task<GlobalUsageProperties> GetAllRollsAsync(int customerId)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetAllRolesAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            var empty = string.Empty;
            return client.Post<ReUsableRequest, GlobalUsageProperties>(new ReUsableRequest { CustomerId = customerId });
        }



        public async Task<ApplicationGlobalVariables> GetAppGobalKeysAsync()
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetAllGlobalKeysAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            var empty = string.Empty;
            return client.Get<ApplicationGlobalVariables>(new object());
        }

        public async Task<GlobalUsageProperties> GetAllDivisionsColomnsAsync(int customerId)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetAllDivisionsColomnsAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            var empty = string.Empty;
            //return client.Get<GlobalUsageProperties>(new ReUsableRequest { CustomerId = customerId });

            return client.Post<ReUsableRequest, GlobalUsageProperties>(new ReUsableRequest { CustomerId = customerId });
        }

        public async Task<GlobalUsageProperties> GetAllCustomerColomnsAsync(ReUsableRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetAllCustomerColomnsAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            var empty = string.Empty;
            return client.Post<ReUsableRequest, GlobalUsageProperties>(request);
        }
        public async Task<GlobalUsageProperties> GetAllStatusBoardCustomers(ReUsableRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetAllStatusBoardCustomers");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            var empty = string.Empty;
            return client.Post<ReUsableRequest, GlobalUsageProperties>(request);
        }
        public async Task<List<CustomerList>> GetCustomersByAccountMgrIdAsync(ReUsableRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetCustomersByAccountMgrIdAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            var empty = string.Empty;
            return client.Post<ReUsableRequest, List<CustomerList>>(request);
        }
        public async Task<List<UsersList>> GetUsersByCustomerIdAsync(ReUsableRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetUserByCustomerIdAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<ReUsableRequest, List<UsersList>>(request);
        }
        public async Task<GlobalUsageProperties> GetAllUsersAsync()
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetAllUsersAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            var empty = string.Empty;
            return client.Get<GlobalUsageProperties>(new object());
        }


        public async Task<GlobalUsageProperties> GetAllPreferred()
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetAllPreferred");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            var empty = string.Empty;
            return client.Get<GlobalUsageProperties>(new object());
        }


        public async Task<GlobalUsageProperties> GetAllPreferredCountry()
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetAllPreferredCountry");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            var empty = string.Empty;
            return client.Get<GlobalUsageProperties>(new object());
        }

        public async Task<GlobalUsageProperties> GetAllSendersAsync()
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetAllSendersAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            var empty = string.Empty;
            return client.Get<GlobalUsageProperties>(new object());
        }

        public async Task<GlobalUsageProperties> GetAllMOShortcode()
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetAllMOShortcode");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            var empty = string.Empty;
            return client.Get<GlobalUsageProperties>(new object());
        }

        public async Task<int> GetDashBoardTypeAsync(int UserId)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetDashBoardTypeAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            var empty = string.Empty;
            return client.Post<ReUsableRequest, int>(new ReUsableRequest { UserId = UserId });
        }

        public async Task<int> GetAvailableCreditsAsync(int CustId)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetAvailableCreditsAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            var empty = string.Empty;
            return client.Post<ReUsableRequest, int>(new ReUsableRequest { CustomerId = CustId });
        }

        //public async Task<GlobalUsageProperties> GetAllMailboxAsync()
        //{
        //    var uriBuilder = GetUriBuilderForServiceMethod("GetAllMailboxAsync");
        //    var factory = new RestClientFactory();
        //    var client = factory.GetJsonRestRequest(uriBuilder);
        //    var empty = string.Empty;
        //    return client.Get<GlobalUsageProperties>(new object());
        //}

        public async Task<GlobalUsageProperties> GetAllVendorsRate()
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetAllVendorsRate");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            var empty = string.Empty;
            return client.Get<GlobalUsageProperties>(new object());
        }

        public async Task<GlobalUsageProperties> GetAllPackageRate()
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetAllPackageRate");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            var empty = string.Empty;
            return client.Get<GlobalUsageProperties>(new object());
        }

        public async Task<GlobalUsageProperties> GetAllPackageByVendorID()
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetAllPackageByVendorID");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            var empty = string.Empty;
            return client.Get<GlobalUsageProperties>(new object());
        }

        public async Task<List<UserMenuItems>> GetUserMenuItems(int userId)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetMenuByUserAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<ReUsableRequest, List<UserMenuItems>>(new ReUsableRequest { UserId = userId });
        }

        public async Task<List<CountryGlobalTable>> GetAllCountriesAsync()
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetAllCountriesAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Get<List<CountryGlobalTable>>(new object());
        }
        //Added for MapAccountManager DropDown
        public async Task<GlobalUsageProperties> GetAccountManager()
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetAccountManager");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Get<GlobalUsageProperties>(new object());
        }

        public async Task<List<CustomerAccountList>> GetCustomerAccount(ReUsableRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetCustomerAccount");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<ReUsableRequest, List<CustomerAccountList>>(request);
        }
        //reports

        public async Task<GlobalUsageProperties> GetCountryAccount()
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetCountryAccount");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Get<GlobalUsageProperties>(new object());
        }

        public async Task<GlobalUsageProperties> GetOperatorAccount()
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetOperatorAccount");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Get<GlobalUsageProperties>(new object());
        }


        public async Task<GlobalUsageProperties> GetReportcustomer(int CustId, int UserId, int roleid)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetReportcustomer");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            var empty = string.Empty;
            //return client.Get<GlobalUsageProperties>(new object());

            return client.Post<ReUsableRequest, GlobalUsageProperties>(new ReUsableRequest { CustomerId = CustId, UserId = UserId, RoleId = roleid });
        }

        public async Task<GlobalUsageProperties> GetVendor()
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetVendor");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Get<GlobalUsageProperties>(new object());
        }

        public async Task<List<ShortcodeMO>> GetShortcodeByUserid(ReUsableRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetShortcodeByUserid");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<ReUsableRequest, List<ShortcodeMO>>(request);
        }
        public async Task<List<StatusCustUserResponse>> GetCustomerUserAsync(CustUserSearch request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetCustomerUserAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<CustUserSearch, List<StatusCustUserResponse>>(request);
        }
        public async Task<List<PromoSummaryResponse>> GetPromoSummaryAsync(PromoSummarySearch request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetPromoSummaryAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<PromoSummarySearch, List<PromoSummaryResponse>>(request);
        }
        public async Task<List<StatusMapSender>> GetMapSenderAsync(MapSenderSearch request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetMapSenderAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<MapSenderSearch, List<StatusMapSender>>(request);
        }
        public async Task<List<StatusSMSCResponse>> GetSMSCMasterAsync(SMSCSearch request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetSMSCMasterAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<SMSCSearch, List<StatusSMSCResponse>>(request);
        }
        public async Task<List<StatusCustomerResponse>> GetCustomerMasterAsync(CustomerSearch request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetCustomerMasterAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<CustomerSearch, List<StatusCustomerResponse>>(request);
        }
        public async Task<List<StatusUserResponse>> GetUsersMasterAsync(UserSearch request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetUsersMasterAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<UserSearch, List<StatusUserResponse>>(request);
        }
        public async Task<List<UsersList>> GetUsersByCustomerIdAsyncRep(ReUsableRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetUsersByCustomerIdAsyncRep");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<ReUsableRequest, List<UsersList>>(request);
        }
        public async Task<List<SendersList>> GetSenderbyUserId(ReUsableRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetSenderbyUserId");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<ReUsableRequest, List<SendersList>>(request);
        }
        public async Task<OperatorResponse> GetDlrPercentageAsync(DLRPercentageSearch request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetDlrPercentageAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<DLRPercentageSearch, OperatorResponse>(request);
        }
        public async Task<CountryResponse> GetDlrPercentageCAsync(DLRPercentageSearch request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetDlrPercentageCAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<DLRPercentageSearch, CountryResponse>(request);
        }
        public string? GetConnectionString(DLRPercentageSearch request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetConnectionString");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<DLRPercentageSearch, string>(request);
        }
        public async Task<List<StatusServerTransactionRpt>> GetServerTransactionAsync(StatusServerTransactionRpt request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetServerTransactionAsync");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<StatusServerTransactionRpt, List<StatusServerTransactionRpt>>(request);
        }
    }
}
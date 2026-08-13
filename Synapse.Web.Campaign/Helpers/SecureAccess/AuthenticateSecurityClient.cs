using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientHTTPConsuming.Utilities;
using Core.Models.Dtos.CommonDtos;
using Core.Models.Dtos.Requests.Synapse.AlertsManager;
using Core.Models.Dtos.Requests.Synapse.MailBox;
using Core.Models.Dtos.Requests.Synapse.ManageMobilityCenter;
using Core.Models.Dtos.Requests.Synapse.UserCampaigns;
using Core.Models.Dtos.Requests.Synapse.UserContacts;
using Core.Models.Dtos.Responses.Synapse.AlertsManager;
using Core.Models.Dtos.Responses.Synapse.MailBox;
using Core.Models.Dtos.Responses.Synapse.ManageMobilityCenter;
using Core.Models.Dtos.Responses.Synapse.UserCampaigns;
using Core.Models.Dtos.Responses.Synapse.UserContacts;
using Core.Models.Extensions;
using Microsoft.Extensions.Configuration;
using UriBuilder = ClientHTTPConsuming.Utilities.UriBuilder;

namespace Synapse.Web.CampaignPlugin.Helpers.SecureAccess
{
    public class AuthenticateSecurityClient : DisposeBaseClass
    {
        private readonly IConfiguration _configuration;
        public AuthenticateSecurityClient()
        { }
        public AuthenticateSecurityClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string? GetConfiguration(string param)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Sets look-up folder to application directory
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
            return configuration[param].ToString();
        }
        public string? BaseServiceHostUrl
        {
            get
            {
                return !string.IsNullOrEmpty(GetConfiguration("BaseServiceHostUrl")?.ToString())
                    ? GetConfiguration("BaseServiceHostUrl")?.ToString()
                    : "http://localhost/APIServices/";
            }
        }

        public UriBuilder GetUriBuilderForServiceMethod(string suffix)
        {
            return new UriBuilder(BaseServiceHostUrl + suffix);
        }

        public async Task<List<GetgGSMCharsQSMSCampOnResponse>> GetgGSMCharsQSMSCamp()
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetgGSMCharsQSMSCamp");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Get<List<GetgGSMCharsQSMSCampOnResponse>>(new object());
        }
        public async Task<List<ShowGridQuickOnResponse>> ShowGridQuick(ShowGridQuickOnRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("ShowGridQuick");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<ShowGridQuickOnRequest, List<ShowGridQuickOnResponse>>(request);
        }
        public async Task<List<ShowGridContactsOnResponse>> ShowGridContacts(ShowGridContactsOnRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("ShowGridContacts");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<ShowGridContactsOnRequest, List<ShowGridContactsOnResponse>>(request);
        }
        public async Task<List<LoadSenderIDCampaignsOnResponse>> loadSenderIDCampaigns(LoadSenderIDCampaignsOnRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("loadSenderIDCampaigns");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<LoadSenderIDCampaignsOnRequest, List<LoadSenderIDCampaignsOnResponse>>(request);
        }

        public async Task<List<LoadNationalityCampaignsOnResponse>> loadNationalityCampaigns(LoadNationalityCampaignsOnRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("loadNationalityCampaigns");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<LoadNationalityCampaignsOnRequest, List<LoadNationalityCampaignsOnResponse>>(request);
        }

        public async Task<List<LoadCityCampaignsOnResponse>> loadCityCampaigns(LoadCityCampaignsOnRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("loadCityCampaigns");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<LoadCityCampaignsOnRequest, List<LoadCityCampaignsOnResponse>>(request);
        }

        public async Task<List<LoadIncomegroupCampaignsOnResponse>> loadIncomegroupCampaigns(LoadIncomegroupCampaignsOnRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("loadIncomegroupCampaigns");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<LoadIncomegroupCampaignsOnRequest, List<LoadIncomegroupCampaignsOnResponse>>(request);
        }

        public async Task<List<LoadTemplateCampaignsOnResponse>> LoadTemplateCampaigns(LoadTemplateCampaignsOnRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("LoadTemplateCampaigns");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<LoadTemplateCampaignsOnRequest, List<LoadTemplateCampaignsOnResponse>>(request);
        }
        public async Task<List<GetTemplateDetailsResponse>> ShowGridTemplateDetails(GetTemplateDetailsRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("ShowGridTemplateDetails");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<GetTemplateDetailsRequest, List<GetTemplateDetailsResponse>>(request);
        }

        public async Task<List<gettemplatecolumnsresponse>> ShowTemplateMapColumns(GetTemplateDetailsRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("ShowTemplateMapColumns");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<GetTemplateDetailsRequest, List<gettemplatecolumnsresponse>>(request);
        } 
        public async Task<List<CampainTimingsLoadCampOnResponse>> LoadCampaignTypes(CampainTimingsLoadCampOnRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("LoadCampaignTypes");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<CampainTimingsLoadCampOnRequest, List<CampainTimingsLoadCampOnResponse>>(request);
        }
        public async Task<string> InsertQSMS(InsertQSMSOnRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("InsertQSMS");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<InsertQSMSOnRequest, string>(request);
        }
        public async Task<int> CheckerUpdateQuickSMS(CheckerUpdateQSMSOnRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("CheckerUpdateQuickSMS");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<CheckerUpdateQSMSOnRequest, int>(request);
        }
        public async Task<List<GetGroupsContactsOnResponse>> PopulateGroups(GetGroupsContactsOnRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("PopulateGroups");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<GetGroupsContactsOnRequest, List<GetGroupsContactsOnResponse>>(request);
        }
        public async Task<List<LoadSenderByCategoryResponse>> LoadSenderByCategory(LoadSenderByCategory request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("LoadSenderByCategory");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<LoadSenderByCategory, List<LoadSenderByCategoryResponse>>(request);
        }
        public async Task<string> InsertBulkSMS(InsertBulkSMSOnRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("InsertBulkSMS");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<InsertBulkSMSOnRequest, string>(request);
        }
        public async Task<string> InsertCustomSMSActualCredits(InsertBulkSMSOnRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("InsertCustomSMSActualCredits");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<InsertBulkSMSOnRequest, string>(request);
        }
        public async Task<string> InsertTestSMSCamp(InsertTestSMSOnRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("InsertTestSMSCamp");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<InsertTestSMSOnRequest, string>(request);
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

        public async Task<List<CampaignTimingsOnResponse>> MessageTimings(CampaignTimingsOnRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("MessageTimings");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<CampaignTimingsOnRequest, List<CampaignTimingsOnResponse>>(request);
        }

        public async Task<GetStageCountsOnResponse> GetCampStageCounts(GetStageCountsOnRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetCampStageCounts");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<GetStageCountsOnRequest, GetStageCountsOnResponse>(request);
        }
        public async Task<int> SetCampaignEvents(SetCampEventsOnRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("SetCampaignEvents");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<SetCampEventsOnRequest, int>(request);
        }

        public async Task<bool> ValidateCampaignName(ReUsableRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("ValidateCampaignName");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<ReUsableRequest, bool>(request);
        }

        public async Task<GroupContactsMain> GetGroupByContacts(ReUsableRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("GetGroupByContacts");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            var empty = string.Empty;
            return client.Post<ReUsableRequest, GroupContactsMain>(request);
        }

        public async Task<DndNonDndNumbers> DndNumberCheck(string str)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("DndNumberCheck");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return
                client.Post<ReUsableRequest, DndNonDndNumbers>(new ReUsableRequest
                {
                    MobileNo = str
                });
        }

        public async Task<string> CampaignStatusChange(string name)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("CampaignStatusChange");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<ReUsableRequest, string>(new ReUsableRequest { Name = name });
        }

        public async Task<int> Externaldbcount(ExternalDB request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("Externaldbcount");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<ExternalDB, int>(request);
        }

        public async Task<List<MobileNos>> Externaldbfilter(string income,string nationality,string city,string gender,string campcount,string fromrange,string torange)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("Externaldbfilter");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return
                client.Post<ReUsableRequest, List<MobileNos>>(new ReUsableRequest
                {
                    IncomeGroup=income,Nationality=nationality,City=city,Gender=gender,CampaignCount=campcount,FromRange=fromrange,ToRange=torange
                });
        }

        public async Task<string> InsertExternalDB(InsertBulkSMSOnRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("InsertExternalDB");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<InsertBulkSMSOnRequest, string>(request);
        }
        //Added By Murty
        public async Task<string> UpdateTestSMSCredits(InsertTestSMSOnRequest request)
        {
            var uriBuilder = GetUriBuilderForServiceMethod("UpdateTestSMSCredits");
            var factory = new RestClientFactory();
            var client = factory.GetJsonRestRequest(uriBuilder);
            return client.Post<InsertTestSMSOnRequest, string>(request);
        }
        //Added By Murty

    }
}
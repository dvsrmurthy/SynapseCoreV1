using Core.Models.Dtos.Responses.HMS.HealthAlerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Dtos.Requests.HMS.HealthAlerts;
using Core.Models.Dtos.Responses.HMS.HealthAlerts.AdminOnResponse;
using Core.Models.Dtos.Requests.HMS.HealthAlerts.AdminOnRequest;
using Core.Models.Dtos.Requests.HMS.HealthAlerts.UserOnRequest;
using Core.Models.Dtos.Responses.HMS.HealthAlerts.UserOnResponse;


namespace Core.Data.IDataInterfaces.HMS
{
    public interface IHmsCoreData
    {
        #region Health Alerts
        #region CustomerTriggers       
        Task<List<CustomerOnResponse>> GetCustomers(CustomerOnRequest Request);
        Task<List<TransMsgTypesOnResponse>> GetTransMsgTypes(TransMsgTypesOnRequest Request);
        Task<List<SelectedTransMsgTypesOnResponse>> GetSelectedTransMsgTypes(SelectedTransMsgTypesOnRequest Request);
        Task<List<GetCustomerTriggersOnResponse>> GetCustomerTriggers(GetCustomerTriggersOnRequest Request);
        Task<int> SetCustomerTriggersChangeStatus(CustomerTriggerChangeStatusOnRequest Request);
        Task<List<CustomerPerferenceCountOnRespone>> GetCustomerPerferenceCount(CustomerPerferenceCountOnRequest Request);
        Task<List<CustomerExistingTriggersCountOnResponse>> GetCustomerExistingTriggersCount(CustomerExistingTriggersCountOnRequest Request);
        Task<int> SetCustomerTriggers(SetCustomerTriggersOnRequest Request);
        Task<int> DeleteExistingCustomerTriggers(DeleteExistingCustomerTriggersOnRequest Request);
        Task<int> UpdateCustomeTriggers(UpdateCustomeTriggersOnRequest Request);
        #endregion
        #region AlerTypes
        Task<List<AlertTypesOnResponse>> GetTransactionTypes(GetAlertTypesRequest Request);
        Task<int> SetTransactionTypes(AlertTypesOnRequest Request);
        Task<int> RemoveSegments(RemoveSegmentsOnrequest Request);
        Task<int> ImportSegments(ImportSegmentsListOnRequest Request);
        Task<List<GetSegmentsOnResponse>> GetSegments(GetSegmentsOnRequest Request);
        #endregion            
        #region ConfigAlerts
        Task<List<ConfigAlertsOnResponse>> GetUsers(ConfigAlertsOnRequest Request);
        Task<List<SegmentsConfigOnResponse>> GetSegmentsConfig(SegmentsConfigOnRequest Request);
        Task<List<SegmentsSelectedOnResponse>> GetSegmentsSelected(SegmentsSelectedOnRequest Request);
        Task<List<SenderIdsOnResponse>> GetSenderIds(SenderIdsOnRequest Request);
        Task<List<TransTypesCustomerOnResponse>> GetTransTypesCustomer(TransTypeCustomerOnRequest Request);
        Task<List<SegmentsforTemplateOnResponse>> GetSegmentsforTemplate(SegmentsforTemplateOnRequest Request);
        Task<int> DeletePlaceholders(DeletePlaceHoldersOnRequest Request);
        Task<int> SetPlaceHolders(SetPlaceholdersOnRequest Request);
        Task<List<SegmentForTransTypeOnResponse>> GetSegmentForTransType(SegmentForTransTypeOnRequest Request);
        Task<List<FilePathsAlertTypeOnResponse>> GetFilePathsAlertType(FilePathsAlertTypeOnRequest Request);
        Task<int> SetAlertTemplates(AlertTemplatesOnRequest Request);
        Task<List<GetSegmentsbyAutoIdOnResponse>> GetSegmentsbyAutoId(GetSegmentsbyAutoIdOnRequest Request);
        Task<List<GetAlertTemplatedetOnResponse>> GetAlertTemplateDet(GetAlertTemplatedetOnRequest Request);
        #endregion
        #endregion

       
    }
}


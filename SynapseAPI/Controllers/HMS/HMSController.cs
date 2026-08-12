using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Models.Dtos.Responses.HMS.HealthAlerts;
using Core.Models.Dtos.Requests.HMS.HealthAlerts;
using Core.Models.Dtos.Responses.HMS.HealthAlerts.AdminOnResponse;
using Core.Models.Dtos.Requests.HMS.HealthAlerts.AdminOnRequest;
using Core.Models.Dtos.Requests.HMS.HealthAlerts.UserOnRequest;
using Core.Models.Dtos.Responses.HMS.HealthAlerts.UserOnResponse;
using System.Threading.Tasks;
using Core.Models.Helpers;

namespace APIServices.Controllers.HMS
{
    public class HMSController : ServicesBaseController
    {
        #region Health Alerts
        #region CustomerTriggers
        /// <summary>
        /// Method Name : GetCustomers
        /// Created By : CH Rajeswari
        /// Created On : 29/09/2016
        /// Purpose : To get all the data of Customer Triggers
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>   
        [HttpPost]
        [Route("GetCustomers")]
        public async Task<List<CustomerOnResponse>> GetCustomers(CustomerOnRequest Request)
        {
           
            return await _contextHmsCore.GetCustomers(Request);
        }
        /// <summary>
        /// Method Name : GetTransMsgTypes
        /// Created By : CH Rajeswari
        /// Created On : 29/09/2016
        /// Purpose : To get all the data of Transaction Message Types
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>   
        [HttpPost]
        [Route("GetTransMsgTypes")]
        public async Task<List<TransMsgTypesOnResponse>> GetTransMsgTypes(TransMsgTypesOnRequest Request)
        {
                var result = await _contextHmsCore.GetTransMsgTypes(Request);
                Logger.InfoFormat("Entered in GetTransMsgTypes", result);
                return result;
           
        }
        /// <summary>
        /// Method Name : GetSelectedTransMsgTypes
        /// Created By : CH Rajeswari
        /// Created On : 29/09/2016
        /// Purpose : To get selected the data of Transaction Message Types of particular customer
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>   
        [HttpPost]
        [Route("GetSelectedTransMsgTypes")]
        public async Task<List<SelectedTransMsgTypesOnResponse>> GetSelectedTransMsgTypes(SelectedTransMsgTypesOnRequest Request)
        {
            var result = await _contextHmsCore.GetSelectedTransMsgTypes(Request);
            Logger.InfoFormat("Entered in GetSelectedTransMsgTypes", result);
            return result;
            
        }
        /// <summary>
        /// Method Name : GetCustomerTriggers
        /// Created By : CH Rajeswari
        /// Created On : 29/09/2016
        /// Purpose : To get all the data of Customer Triggers
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>  
        [HttpPost]
        [Route("GetCustomerTriggers")]
        public async Task<List<GetCustomerTriggersOnResponse>> GetCustomerTriggers(GetCustomerTriggersOnRequest Request)
        {
            var result = await _contextHmsCore.GetCustomerTriggers(Request);
            Logger.InfoFormat("GetCustomerTriggers", result);
            return result;
        }
        /// <summary>
        /// Method Name : GetCustomerTriggersChangeStatus
        /// Created By : CH Rajeswari
        /// Created On : 29/09/2016
        /// Purpose : To change the Status of Customer Triggers
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>  
        [HttpPost]
        [Route("SettCustomerTriggerChangeStatus")]
        public async Task<int> SetCustomerTriggersChangeStatus(CustomerTriggerChangeStatusOnRequest Request)
        {
            var responsedata = await _contextHmsCore.SetCustomerTriggersChangeStatus(Request);
            Logger.InfoFormat("SetCustomerTriggersChangeStatus", responsedata);
            return responsedata;
        }
        /// <summary>
        /// Method Name : GetCustomerPerferenceCount
        /// Created By : CH Rajeswari
        /// Created On : 30/09/2016
        /// Purpose : To get the Preference count of Customer 
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>  
        [HttpPost]
        [Route("GetCustomerPerferenceCount")]
        public async Task<List<CustomerPerferenceCountOnRespone>> GetCustomerPerferenceCount(CustomerPerferenceCountOnRequest Request)
        {
            var result=await _contextHmsCore.GetCustomerPerferenceCount(Request);
            Logger.InfoFormat("GetCustomerPerferenceCount", result);
            return result;
        }
        /// <summary>
        /// Method Name : GetCustomerExistingTriggersCount
        /// Created By : CH Rajeswari
        /// Created On : 30/09/2016
        /// Purpose : To get the existing triggers count of Customer 
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>  
        [HttpPost]
        [Route("GetCustomerExistingTriggersCount")]
        public async Task<List<CustomerExistingTriggersCountOnResponse>> GetCustomerExistingTriggersCount(CustomerExistingTriggersCountOnRequest Request)
        {
            var result=await _contextHmsCore.GetCustomerExistingTriggersCount(Request);
            Logger.InfoFormat("GetCustomerExistingTriggersCount", result);
            return result;
        }
        /// <summary>
        /// Method Name : SetCustomerTriggers
        /// Created By : CH Rajeswari
        /// Created On : 30/09/2016
        /// Purpose : To add the customer triggers 
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>  
        [HttpPost]
        [Route("SetCustomerTriggers")]
        public async Task<int> SetCustomerTriggers(SetCustomerTriggersOnRequest Request)
        {
            var result = await _contextHmsCore.SetCustomerTriggers(Request);
            Logger.InfoFormat("SetCustomerTriggers", result);
            return result;
        }
        /// <summary>
        /// Method Name : DeleteExistingCustomerTriggers
        /// Created By : CH Rajeswari
        /// Created On : 30/09/2016
        /// Purpose : To delete the existing triggers of customers
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>  
        [HttpPost]
        [Route("DeleteExistingCustomerTriggers")]
        public async Task<int> DeleteExistingCustomerTriggers(DeleteExistingCustomerTriggersOnRequest Request)
        {
            var result = await _contextHmsCore.DeleteExistingCustomerTriggers(Request);
            Logger.InfoFormat("DeleteExistingCustomerTriggers", result);
            return await _contextHmsCore.DeleteExistingCustomerTriggers(Request);
        }
        /// <summary>
        /// Method Name : UpdateCustomeTriggers
        /// Created By : CH Rajeswari
        /// Created On : 30/09/2016
        /// Purpose : To update the existing triggers of customers
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary> 
        [HttpPost]
        [Route("UpdateCustomeTriggers")]
        public async Task<int> UpdateCustomeTriggers(UpdateCustomeTriggersOnRequest Request)
        {
            var result=await _contextHmsCore.UpdateCustomeTriggers(Request);
            Logger.InfoFormat("UpdateCustomeTriggers", result);
            return result;
        }
        #endregion
        #region AlertTypes
        /// <summary>
        /// Method Name : GetTransactionTypes
        /// Created By : CH Rajeswari
        /// Created On : 03/10/2016
        /// Purpose : To get all the transaction types 
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>   
        [HttpPost]
        [Route("GetTransactionTypes")]
        public async Task<List<AlertTypesOnResponse>> GetTransactionTypes(GetAlertTypesRequest Request)
        {
            var result = await _contextHmsCore.GetTransactionTypes(Request);
            Logger.InfoFormat("GetTransactionTypes", result);
            return result;
        }
        /// <summary>
        /// Method Name : SetTransactionTypes
        /// Created By : CH Rajeswari
        /// Created On : 03/10/2016
        /// Purpose : To add the transaction types 
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>   
        [HttpPost]
        [Route("SetTransactionTypes")]
        public async Task<int> SetTransactionTypes(AlertTypesOnRequest Request)
        {
            var result = await _contextHmsCore.SetTransactionTypes(Request);
            Logger.InfoFormat("SetTransactionTypes", result);
            return result;
        }
        /// <summary>
        /// Method Name : RemoveSegments
        /// Created By : CH Rajeswari
        /// Created On : 03/10/2016
        /// Purpose : To remove all the segments of particular transaction types 
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary> 
        [HttpPost]
        [Route("RemoveSegments")]
        public async Task<int> RemoveSegments(RemoveSegmentsOnrequest Request)
        {
            var result = await _contextHmsCore.RemoveSegments(Request);
            Logger.InfoFormat("RemoveSegments", result);
            return result;
        }
        /// <summary>
        /// Method Name : ImportSegments
        /// Created By : CH Rajeswari
        /// Created On : 12/12/2016
        /// Purpose : To import all the segments of particular transaction types 
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary> 
        [HttpPost]
        [Route("Importsegments")]
        public async Task<int> ImportSegments(ImportSegmentsListOnRequest Request)
        {
            Logger.Info("ImportSegments");
            //if(Request.Id > 0)
            //{
                var result = await _contextHmsCore.ImportSegments(Request);
                Logger.InfoFormat("ImportSegments", result);
                return result;
           // }
           
        }
        /// <summary>
        /// Method Name : ImportSegments
        /// Created By : CH Rajeswari
        /// Created On : 13/12/2016
        /// Purpose : To get all the segments of particular transaction types 
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary> 
        [HttpPost]
        [Route("GetSegments")]
        public async Task<List<GetSegmentsOnResponse>> GetSegments(GetSegmentsOnRequest Request)
        {
            var result = await _contextHmsCore.GetSegments(Request);
            Logger.InfoFormat("GetSegments", result);
            return result;
        }
        #endregion       
        #region ConfigAlerts
        /// <summary>
        /// Method Name : GetUsers
        /// Created By : CH Rajeswari
        /// Created On : 06/10/2016
        /// Purpose : To get all the data of Users in Synapse Admin Application
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary> 
        [HttpPost]
        [Route("GetUsers")]
        public async Task<List<ConfigAlertsOnResponse>> GetUsers(ConfigAlertsOnRequest Request)
        {
            var result =await _contextHmsCore.GetUsers(Request);
            Logger.InfoFormat("GetUsers", result);
            return result;
        }
        /// <summary>
        /// Method Name : GetSegmentsConfig
        /// Created By : CH Rajeswari
        /// Created On : 06/10/2016
        /// Purpose : To get all the Segments
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>   
        [HttpPost]
        [Route("GetSegmentsConfig")]
        public async Task<List<SegmentsConfigOnResponse>> GetSegmentsConfig(SegmentsConfigOnRequest Request)
        {
            var result = await _contextHmsCore.GetSegmentsConfig(Request);
            Logger.InfoFormat("GetSegmentsConfig", result);
            return result;
        }
        /// <summary>
        /// Method Name : GetSegmentsSelected
        /// Created By : CH Rajeswari
        /// Created On : 07/10/2016
        /// Purpose : To get selected segments for the particular segments ids
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>  
        [HttpPost]
        [Route("GetSegmentsSelected")]
        public async Task<List<SegmentsSelectedOnResponse>> GetSegmentsSelected(SegmentsSelectedOnRequest Request)
        {
            var result=await _contextHmsCore.GetSegmentsSelected(Request);
            Logger.InfoFormat("GetSegmentsSelected", result);
            return result;
        }
        /// <summary>
        /// Method Name : GetSenderIds
        /// Created By : CH Rajeswari
        /// Created On : 12/10/2016
        /// Purpose : To get SenderIds for the particular User Id
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>   
        [HttpPost]
        [Route("GetSenderIds")]
        public async Task<List<SenderIdsOnResponse>> GetSenderIds(SenderIdsOnRequest Request)
        {
            var result = await _contextHmsCore.GetSenderIds(Request);
            Logger.InfoFormat("GetSenderIds", result);
            return result;
        }
        /// <summary>
        /// Method Name : GetTransTypesCustomer
        /// Created By : CH Rajeswari
        /// Created On : 12/10/2016
        /// Purpose : To get Transaction Details for the particular Trans Id
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>   
        [HttpPost]
        [Route("GetTransTypesCustomer")]
        public async Task<List<TransTypesCustomerOnResponse>> GetTransTypesCustomer(TransTypeCustomerOnRequest Request)
        {
            var result = await _contextHmsCore.GetTransTypesCustomer(Request);
            Logger.InfoFormat("GetTransTypesCustomer", result);
            return await _contextHmsCore.GetTransTypesCustomer(Request);
        }
        /// <summary>
        /// Method Name : GetSegmentsforTemplate
        /// Created By : CH Rajeswari
        /// Created On : 12/10/2016
        /// Purpose : To get Segments Details for the particular Template
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary> 
        [HttpPost]
        [Route("GetSegmentsforTemplate")]
        public async Task<List<SegmentsforTemplateOnResponse>> GetSegmentsforTemplate(SegmentsforTemplateOnRequest Request)
        {
            var result = await _contextHmsCore.GetSegmentsforTemplate(Request);
            Logger.InfoFormat("GetSegmentsforTemplate", result);
            return result;
        }
        /// <summary>
        /// Method Name : GetSegmentsforTemplate
        /// Created By : CH Rajeswari
        /// Created On : 13/10/2016
        /// Purpose : To delete placeholders for the particular TransTypeId
        /// Modified By : 
        /// Modified On :   
        /// Details of Modification :  
        /// </summary> 
        [HttpPost]
        [Route("DeletePlaceholders")]
        public async Task<int> DeletePlaceholders(DeletePlaceHoldersOnRequest Request)
        {
            var result=await _contextHmsCore.DeletePlaceholders(Request);
            Logger.InfoFormat("DeletePlaceholders", result);
            return result;
        }
        /// <summary>
        /// Method Name : GetSegmentsforTemplate
        /// Created By : CH Rajeswari
        /// Created On : 13/10/2016
        /// Purpose : To insert placeholders details
        /// Modified By : 
        /// Modified On :   
        /// Details of Modification :  
        /// </summary>
        [HttpPost]
        [Route("SetPlaceHolders")]
        public async Task<int> SetPlaceHolders(SetPlaceholdersOnRequest Request)
        {
            var result=await _contextHmsCore.SetPlaceHolders(Request);
            Logger.InfoFormat("SetPlaceHolders", result);
            return result;
        }
        /// <summary>
        /// Method Name : GetSegmentForTransType
        /// Created By : CH Rajeswari
        /// Created On : 14/10/2016
        /// Purpose : To get segments for Trans Type details
        /// Modified By : 
        /// Modified On :   
        /// Details of Modification :  
        /// </summary> 
        [HttpPost]
        [Route("GetSegmentForTransType")]
        public async Task<List<SegmentForTransTypeOnResponse>> GetSegmentForTransType(SegmentForTransTypeOnRequest Request)
        {
            var result = await _contextHmsCore.GetSegmentForTransType(Request);
            Logger.InfoFormat("GetSegmentForTransType", result);
            return result;
        }
        /// <summary>
        /// Method Name : GetFilePathsAlertType
        /// Created By : CH Rajeswari
        /// Created On : 14/10/2016
        /// Purpose : To get FilePaths for Alerttype
        /// Modified By : 
        /// Modified On :   
        /// Details of Modification :  
        /// </summary> 
        [HttpPost]
        [Route("GetFilePathsAlertType")]
        public async Task<List<FilePathsAlertTypeOnResponse>> GetFilePathsAlertType(FilePathsAlertTypeOnRequest Request)
        {
            var result = await _contextHmsCore.GetFilePathsAlertType(Request);
            Logger.InfoFormat("GetSegmentForTransType", result);
            return result;
        }
        /// <summary>
        /// Method Name : GetFilePathsAlertType
        /// Created By : CH Rajeswari
        /// Created On : 14/10/2016
        /// Purpose : To get FilePaths for Alerttype
        /// Modified By : 
        /// Modified On :   
        /// Details of Modification :  
        /// </summary> 
        [HttpPost]
        [Route("SetAlertTemplates")]
        public async Task<int> SetAlertTemplates(AlertTemplatesOnRequest Request)
        {
            var result = await _contextHmsCore.SetAlertTemplates(Request);
            Logger.InfoFormat("SetAlertTemplates", result);
            return result;
        }
        /// <summary>
        /// Method Name : GetFilePathsAlertType
        /// Created By : CH Rajeswari
        /// Created On : 06/01/2017
        /// Purpose : To get Segments Details for SegmentId
        /// Modified By : 
        /// Modified On :   
        /// Details of Modification :  
        /// </summary> 
        [HttpPost]
        [Route("GetSegmentsbyAutoId")]
        public async Task<List<GetSegmentsbyAutoIdOnResponse>> GetSegmentsbyAutoId(GetSegmentsbyAutoIdOnRequest Request)
        {
            var result = await _contextHmsCore.GetSegmentsbyAutoId(Request);
            Logger.InfoFormat("GetSegmentsbyAutoId", result);
            return result;
        }
          /// <summary>
        /// Method Name : GetFilePathsAlertType
        /// Created By : CH Rajeswari
        /// Created On : 06/01/2017
        /// Purpose : To get ALert Template Details 
        /// Modified By : 
        /// Modified On :   
        /// Details of Modification :  
        /// </summary> 
        [HttpPost]
        [Route("GetAlertTemplateDet")]
        public async Task<List<GetAlertTemplatedetOnResponse>> GetAlertTemplateDet(GetAlertTemplatedetOnRequest Request)
        {
            var result = await _contextHmsCore.GetAlertTemplateDet(Request);
            Logger.InfoFormat("GetAlertTemplateDet", result);
            return result;
        }
        #endregion
        #endregion
        #region Default
        //// GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
        #endregion
    }
}
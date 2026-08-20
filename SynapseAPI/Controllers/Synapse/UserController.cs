using Core.Models.Dtos.Requests.Synapse.UserGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Models.Dtos.Requests.Synapse.UserMoKeyWordConfig;
using Core.Models.Dtos.Responses.Synapse.UserMoKeyWordConfig;
using Core.Models.Dtos.Requests.Synapse.UserMoCampaignConfiguration;
using Core.Models.Dtos.Requests.Synapse.UserCampaigns;
using Core.Models.Dtos.Requests.Synapse.UserContacts;
using Core.Models.Extensions;
using Core.Models.Dtos.Requests.Synapse.UserDND;
using Core.Models.Dtos.Requests.Synapse.UserMoInbox;
using Core.Models.Dtos.Responses.Synapse.UserMoInbox;
using System.Data;
using APIServices.Filters;
using Core.Models.Helpers;
using Core.Models.Dtos.Requests.Synapse.DBAlerts;
using Core.Models.Dtos.CommonDtos;
using System.Web;
using Core.Models.Dtos.Requests.Synapse.ManageMobilityCenter;

namespace SynapseAPI.Controllers
{
    [ApiController]
    [Route("")]
    public class UserController : ServicesBaseController
    {
        #region  Phone Book

        #region Group

        [HttpPost]
        [Route("ShowGridGroups")]
        public async Task<IActionResult> ShowGridGroups([FromBody] ShowGridGroupsOnRequests request)
        {
            //if (request.ID != 0)
            //{
            //    var response = await _contextSynapseCore.ShowGridGroup(request);

            //    return Ok(response);
            //}
            var responseData = await _contextSynapseCore.ShowGridGroups(request);

            return Ok(responseData);
        }

        [HttpPost]
        [Route("SaveGroups")]
        public async Task<IActionResult> InsertGroups([FromBody] SaveGroupsOnRequests request)
        {

            var responseData = await _contextSynapseCore.InsertGroups(request);

            return Ok(responseData);
        }

        [HttpPost]
        [Route("ApproveUserGroup")]
        public async Task<IActionResult> ApproveGroup([FromBody] ApproveUserGroupOnRequest request)
        {
            var responseData = await _contextSynapseCore.ApproveUserGroup(request);
            return Ok(responseData);
        }

        [HttpPost]
        [Route("RejectUserGroup")]
        public async Task<IActionResult> RejectUserGroup([FromBody] ApproveUserGroupOnRequest request)
        {
            var responseData = await _contextSynapseCore.RejectUserGroup(request);
            return Ok(responseData);
        }

        [HttpPost]
        [Route("ChangeStatusGroups")]
        public async Task<IActionResult> ChangeStatusGroups([FromBody] ReUsableRequest request)
        {
            if (request.GrpId == null)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.ChangeStatusGroups(request);
            return Ok(response);
        }

        //[HttpPost]
        //[Route("ChangeStatus")]
        //public async Task<IActionResult> ChangeStatusGroups([FromBody] ChangeStatusOnRequests request)
        //{
        //    var responseData = await _contextSynapseCore.ChangeStatusGroups(request);
        //    return Ok(responseData);
        //}

        //[HttpPost]
        //[Route("DeleteGroups")]
        //public async Task<IActionResult> DeleteGroups([FromBody] DeleteGroupsOnRequests request)
        //{
        //    var responseData = await _contextSynapseCore.DeleteGroups(request);
        //    return Ok(responseData);
        //}

        #endregion

        #region Contacts

        /// <summary>
        /// Method Name : PopulateGroups
        /// Created By : G.Murali
        /// Created On : 13/09/2016
        /// Purpose : To get all the groups of a particular user
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>  
        [HttpPost]
        [Route("PopulateGroups")]
        public async Task<IActionResult> PopulateGroups(GetGroupsContactsOnRequest request)
        {
            var response = await _contextSynapseCore.PopulateGroups(request);
            return Ok(response);
        }
        /// <summary>
        /// Method Name : ShowGridContacts
        /// Created By : G.Murali
        /// Created On : 14/09/2016
        /// Purpose : To get all or single record(s) of contacts based on usergroupid or searchvalue 
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>  
        [HttpPost]
        [Route("ShowGridContacts")]
        public async Task<IActionResult> ShowGridContacts(ShowGridContactsOnRequest request)
        {
            if (request.CREATEDBY != 0 && request.USERGROUPID == 0)
            {
                var response = await _contextSynapseCore.ShowGridContacts(request);
                return Ok(response);

            }
            if (request.USERGROUPID != 0)
            {
                var response = await _contextSynapseCore.ShowContactForEdit(request);
                return Ok(response);
            }
            return Ok();
        }
        /// <summary>
        /// Method Name : InsertContacts
        /// Created By : G.Murali
        /// Created On : 14/09/2016
        /// Purpose : To insert a new Contact or update old Contact based on same contact present in DB or not
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary> 
        [HttpPost]
        [Route("InsertContacts")]
        public async Task<IActionResult> InsertContacts(InsertContactsOnRequest request)
        {
            if (request.CREATEDBY != 0 && request.MOBILE != "")
            {
                var response = await _contextSynapseCore.InsertContacts(request);
                return Ok(response);
            }
            return Ok();
        }
        /// <summary>
        /// Method Name : ChangeStatusContacts
        /// Created By : G.Murali
        /// Created On : 15/09/2016
        /// Purpose : To change the status of a contact as Activate or Deactivate(Delete)
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary> 
        [HttpPost]
        [Route("ChangeStatusContacts")]
        public async Task<IActionResult> ChangeStatusContacts(ChangeStatusContactsOnRequest request)
        {
            if (request.UPDATEDBY != 0 && request.CONTGRPID != 0)
            {
                var response = await _contextSynapseCore.ChangeStatusContacts(request);
                return Ok(response);
            }
            return Ok();
        }
        [HttpPost]
        [Route("DeleteContacts")]
        public async Task<IActionResult> DeleteContacts(DeleteContactsOnRequest request)
        {
            if (request.CONTACTIDS != "" && request.GROUPIDS != "")
            {
                var response = await _contextSynapseCore.DeleteContacts(request);
                return Ok(response);
            }
            return Ok();
        }


        /// <summary>
        /// Method Name : ImportContacts
        /// Created By : G.Murali
        /// Created On : 19/09/2016
        /// Purpose : To insert Contacts into DB from Notepad or Excel file
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary> 
        [HttpPost]
        //[TimeoutFilter(600000)]
        [Route("ImportContacts")]
        public async Task<IActionResult> ImportContacts(ImportContactsCSVOnRequest request)
        {
            Logger.Info("ServiceExecutionStart");
            try
            {
                if (request.FILEPATH != "" && request.GROUPIDS != "" && request.CREATEDBY != 0)
                {
                    var response = await _contextSynapseCore.ImportContacts(request);
                    Logger.Info("ServiceExecutionEnd");
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return Ok();
        }
        /// <summary>
        /// Method Name : ExportContacts
        /// Created By : G.Murali
        /// Created On : 19/09/2016
        /// Purpose : To Export Contacts into Notepad or Excel file from DB
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary> 
        [HttpPost]
        [Route("ExportContacts")]
        public async Task<IActionResult> ExportContacts(ExportContactsOnRequest request)
        {
            try
            {
                if (request.CREATEDBY != 0)
                {
                    var response = await _contextSynapseCore.ExportContacts(request);
                    return new JsonResult(response);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return Ok();
        }

        #endregion

        #region DND

        [HttpGet]
        [Route("ShowGridDND")]
        public async Task<IActionResult> ShowGridDND(ShowGridDNDOnRequest request)
        {
            if (request.USERID != 0 && request.CUSTOMERID != 0)
            {
                var response = await _contextSynapseCore.ShowGridDND(request);
                return Ok(response);
            }
            return Ok();
        }

        #endregion

        #endregion

        #region MoKeyWordConfig

        /// <summary>
        /// Method Name : ShowGridMoKeyWordConfig
        /// Created By : G.Murali
        /// Created On : 09/09/2016
        /// Purpose : To get all or single record(s) of KeyWord based on KeyID supplied
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>      
        [HttpGet]
        [Route("ShowGridMoKeyWordConfig")]
        public async Task<IActionResult> ShowGridMoKeyWordConfig([FromBody] MoKeyWordConfigOnRequest request)
        {
            if (request.UserID != 0 && request.KeyID == 0)
            {
                var response = await _contextSynapseCore.ShowGridMoKeyWordConfig(request);
                return Ok(response);
            }
            if (request.KeyID != 0)
            {
                var singleRespose = await _contextSynapseCore.ShowGridMoKeyWordConfigForEdit(request);
                return Ok(singleRespose);
            }
            return Ok();
        }

        //[HttpGet]
        //[Route("GetKeywordsOnloadAsync")]
        //public async Task<IActionResult> GetKeywordsOnloadAsync()
        //{
        //    var response = await _contextSynapseCore.GetKeywordsOnloadAsync();
        //    return Ok(response);
        //}
        /// <summary>
        /// Method Name : InsertMoKeyWordConfig
        /// Created By : G.Murali
        /// Created On : 12/09/2016
        /// Purpose : To insert a new KeyWord or update old KeyWord based on KeyID supplied
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>
        [HttpPost]
        [Route("InsertMoKeyWordConfig")]
        public async Task<IActionResult> InsertMoKeyWordConfig([FromBody]InsertMoKeyWordConfigOnRequest request)
        {
            var response = await _contextSynapseCore.InsertMoKeyWordConfig(request);
            return Ok(response);
        }
        /// <summary>
        /// Method Name : ChangeStatusMoKeyWordConfig
        /// Created By : G.Murali
        /// Created On : 12/09/2016
        /// Purpose : To chnage the status of a selectd KeyWord(Activate or Deativate)
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>
        [HttpPost]
        [Route("ChangeStatusMoKeyWordConfig")]
        public async Task<IActionResult> ChangeStatusMoKeyWordConfig([FromBody]ChangeStatusMoKeyWordConfigOnRequest request)
        {
            if (request.KeyID != 0)
            {
                var response = await _contextSynapseCore.ChangeStatusMoKeyWordConfig(request);
                return Ok(response);
            }
            return Ok();
        }



        //MOKeyword
        [HttpPost]
        [Route("GetMOKeyword")]
        public async Task<IActionResult>GetMOKeyword([FromBody]InsertMoKeyWordConfigOnRequest request)
        {
            if (request.KeyID == null )
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetMOKeyword(request);
            return Ok(response);
        }


        [HttpPost]
        [Route("VerifyIskeyExistedOrNotAsync")]
        public async Task<IActionResult> VerifyIskeyExistedOrNotAsync([FromBody] ReUsableRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Keyword))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.ValidateIskeyExistedOrNot(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("KeyActiveStatuChange")]
        public async Task<IActionResult> KeyActiveStatuChange([FromBody] ReUsableRequest request)
        {
            if (request.Kid < 0)           
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.KeyActiveStatuChange(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("SaveMOKeyword")]
        public async Task<IActionResult> SaveMOKeyword([FromBody]InsertMoKeyWordConfigOnRequest request)
        {
            var response = await _contextSynapseCore.SaveMOKeyword(request);
            return Ok(response);
        }

        #endregion

        #region ShortcodeAnalytic

        [HttpPost]
        [Route("GetShortcodeByUserid")]
        public async Task<IActionResult> GetShortcodeByUserid([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetShortcodeByUserid(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ShortcodeAnalytic")]
        public async Task<IActionResult> ShortcodeAnalytic([FromBody] ShortcodeAnalyticRequest request)
        {
            if (request.UserId == 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.ShortcodeAnalytic(request);
            return Ok(response);
        }
        #endregion

        #region MOWinner

        [HttpPost]
        [Route("GetMOWinner")]
        public async Task<IActionResult> GetMOWinner([FromBody]WinnerRequest request)
        {
            if (request.Id == null)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetMOWinner(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetMOWinnerReport")]
        public async Task<IActionResult> GetMOWinnerReport([FromBody]WinnerReportRequest request)
        {
            if (request.CampaignId == null)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetMOWinnerReport(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("SaveMOWinner")]
        public async Task<IActionResult> SaveMOWinner([FromBody]WinnerRequest request)
        {
            var response = await _contextSynapseCore.SaveMOWinner(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetCampaignByCustomerId")]
        public async Task<IActionResult> GetCampaignByCustomerId([FromBody] ReUsableRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CustId))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetCampaignByCustomerId(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetWinnerCampaignByCustomerId")]
        public async Task<IActionResult> GetWinnerCampaignByCustomerId([FromBody] ReUsableRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CustId))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetWinnerCampaignByCustomerId(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("LoadCampaignByWinnersettingId")]
        public async Task<IActionResult> LoadCampaignByWinnersettingId([FromBody] ReUsableRequest request)
        {
            if (request.CampaignId < 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.LoadCampaignByWinnersettingId(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("MOWinnerStatusChange")]
        public async Task<IActionResult> MOWinnerStatusChange([FromBody] ReUsableRequest request)
        {
            if (request.Kid < 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.MOWinnerStatusChange(request);
            return Ok(response);
        }

        #endregion

        #region MoCampaignConfiguration

        #region MoCampaign
        [HttpGet]
        [Route("ShowGridMoCampaigns")]
        public async Task<IActionResult> ShowGridMoCampaigns([FromBody] GetMoCampaignNamesOnRequest request)
        {

            if (request.NID != 0)
            {
                var response = await _contextSynapseCore.ViewMoCampaignNames(request);

                return Ok(response);
            }
            return Ok();

        }

        [HttpGet]
        [Route("GetMoCampaigns")]
        public async Task<IActionResult> GetMoCampaigns([FromBody] ShowMoCampaignOnRequest request)
        {
            if (request.USERID != 0)
            {
                var responseData = await _contextSynapseCore.ShowMoCampaigns(request);

                return Ok(responseData);
            }
            return Ok();
        }

        [HttpPost]
        [Route("InsertMoCampaigns")]
        public async Task<IActionResult> InsertMoCampaigns([FromBody] SaveMoCamapignOnRequest request)
        {

            var responseData = await _contextSynapseCore.InsertMoCampaigns(request);

            return Ok(responseData);
        }

        //code added on 21092017

        [HttpPost]
        [Route("GetAllMoCampaign")]
        public async Task<IActionResult> GetAllMoCampaign([FromBody]MoCampaignConfigRequest request)
        {
            if (request.MOCampaignID == null)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetAllMoCampaign(request);
            return Ok(response);
        }


        [HttpPost]
        [Route("SaveMOCampaignConfig")]
        public async Task<IActionResult> SaveMOCampaignConfig([FromBody] MoCampaignConfigRequest request)
        {

            var responseData = await _contextSynapseCore.SaveMOCampaignConfig(request);

            return Ok(responseData);
        }

        [HttpPost]
        [Route("ChangeStatusMoCampaign")]
        public async Task<IActionResult> ChangeStatusMoCampaign([FromBody]  ChangeStatusMoCampaignsOnRequest request)
        {
            var responseData = await _contextSynapseCore.ChangeStatusMoCampaigns(request);
            return Ok(responseData);
        }

        [HttpGet]
        [Route("BindSenderIDs")]
        public async Task<IActionResult> BindSenderIDs([FromBody] LoadSenderIDsOnRequest request)
        {

            if (request.USERID == 0)
            {
                var response = await _contextSynapseCore.BindSenderIDs(request);

                return Ok(response);
            }
            var responsedata = await _contextSynapseCore.BindSenderIDs(request);
            return Ok(responsedata);

        }

        [HttpGet]
        [Route("BindSMSCs")]
        public async Task<IActionResult> BindSMSCs([FromBody] LoadSMSCSOnRequest request)
        {

            if (request.USERID == 0)
            {
                var response = await _contextSynapseCore.BindSMSCs(request);

                return Ok(response);
            }
            var responsedata = await _contextSynapseCore.BindSMSCs(request);
            return Ok(responsedata);

        }

        [HttpGet]
        [Route("BindKeywords")]
        public async Task<IActionResult> BindKeywords([FromBody] LoadKeywordsOnRequest request)
        {

            if (request.USERID == 0)
            {
                var response = await _contextSynapseCore.BindKeywords(request);

                return Ok(response);
            }
            var responsedata = await _contextSynapseCore.BindKeywords(request);
            return Ok(responsedata);

        }

        [HttpPost]
        [Route("GetUsersByCustomerMOIdAsync")]
        public async Task<IActionResult> GetUsersByCustomerMOIdAsync([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetUsersByCustomerMOIdAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetUsersByCustomerMOIdAsyncRep")]
        public async Task<IActionResult> GetUsersByCustomerMOIdAsyncRep([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetUsersByCustomerMOIdAsyncRep(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetShortcodeByUserAsync")]
        public async Task<IActionResult> GetShortcodeByUserAsync([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetShortcodeByUserAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetMOShortcodeByUserAsync")]
        public async Task<IActionResult> GetMOShortcodeByUserAsync([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetMOShortcodeByUserAsync(request);
            return Ok(response);
        }

        //[HttpPost]
        //[Route("GetSenderByUserasync")]
        //public async Task<IActionResult> GetSenderByUserasync([FromBody] ReUsableRequest request)
        //{
        //    var response = await _contextSynapseCore.GetSenderByUserasync(request);
        //    return Ok(response);
        //}

        [HttpGet]
        [Route("GetSenderbyUserIds")]
        public async Task<IActionResult> GetSenderbyUserIds([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetSenderbyUserIds(request);
            return Ok(response);
        }

        
        [HttpPost]
        [Route("GetSenderByUserIdcamp")]
        public async Task<IActionResult> GetSenderByUserIdcamp([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetSenderByUserIdcamp(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetSMPPUserByUserasync")]
        public async Task<IActionResult> GetSMPPUserByUserasync([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetSMPPUserByUserasync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetKeywordByUserasync")]
        public async Task<IActionResult> GetKeywordByUserasync([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetKeywordByUserasync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetOutboundByUsersAsync")]
        public async Task<IActionResult> GetOutboundByUsersAsync([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetOutboundByUsersAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetCustomerByMoUsersLookUp")]
        public async Task<IActionResult> GetCustomerByMoUsersLookUp([FromBody]MoCampaignSerchRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchText))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetCustomerByMoUsersLookUp(request);
            return Ok(response);
        }


        [HttpPost]
        [Route("GetUserByMoUsersLookUp")]
        public async Task<IActionResult> GetUserByMoUsersLookUp([FromBody]MoCampaignSerchRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchText))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetUserByMoUsersLookUp(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("MOCampaignActiveStatusChange")]
        public async Task<IActionResult> MOCampaignActiveStatusChange([FromBody] ReUsableRequest request)
        {
            if (request.MoCampId < 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.MOCampaignActiveStatusChange(request);
            return Ok(response);
        }
        #endregion

        #region MoReply

        [HttpPost]
        [Route("ShowGridMoReply")]
        public async Task<IActionResult> ShowGridMoReply([FromBody]ShowMoReplyOnRequest request)
        {
            if (request.NID != 0)
            {
                var response = await _contextSynapseCore.ViewMoReply(request);

                return Ok(response);
            }
            var responseData = await _contextSynapseCore.ShowMoReplyDetails(request);

            return Ok(responseData);
        }

        [HttpPost]
        [Route("SaveMoReply")]
        public async Task<IActionResult> InsertMoReply([FromBody] SaveMoReplyOnRequest request)
        {

            var responseData = await _contextSynapseCore.InsertMoReply(request);

            return Ok(responseData);
        }



        #endregion

        #region Mo Forward

        [HttpPost]
        [Route("ShowGridMoForward")]
        public async Task<IActionResult> ShowGridMoForward([FromBody]ShowMoForwardOnRequest request)
        {
            if (request.NID != 0)
            {
                var response = await _contextSynapseCore.ViewMoforward(request);

                return Ok(response);
            }
            var responseData = await _contextSynapseCore.ShowMoForwardDetails(request);

            return Ok(responseData);
        }

        [HttpPost]
        [Route("SaveMoForward")]
        public async Task<IActionResult> InsertMoForward([FromBody] SaveMoForwardOnRequest request)
        {

            var responseData = await _contextSynapseCore.InsertMoForward(request);

            return Ok(responseData);
        }

        [HttpPost]
        [Route("DeleteMoForward")]
        public async Task<IActionResult> DeleteMoForward([FromBody]DeleteMoForwardOnRequest request)
        {
            if (request.CAMPID != 0)
            {
                var response = await _contextSynapseCore.DeleteMoForward(request);

                return Ok(response);
            }
            return Ok();
        }


        #endregion

        #region Mo SmppForward
        [HttpPost]
        [Route("ShowMoSmppForward")]
        public async Task<IActionResult> ShowMoSmppForward([FromBody] ShowMoSmppForwardOnRequest request)
        {
            if (request.STRSEARCH != "")
            {
                var response = await _contextSynapseCore.ShowMoSmppForwardDetails(request);

                return Ok(response);
            }
            if (request.USERID != 0)
            {
                var responseData = await _contextSynapseCore.ViewMoSmppForward(request);

                return Ok(responseData);
            }
            return Ok();
        }

        [HttpPost]
        [Route("SaveMoSmppForward")]
        public async Task<IActionResult> InsertMoSmppForward([FromBody] SaveMoSmppForwardOnRequest request)
        {
            if (request.CREATEDBY != 0)
            {
                var responseData = await _contextSynapseCore.InsertMoSmppForward(request);
                return Ok(responseData);
            }
            return Ok();
        }

        [HttpPost]
        [Route("ChangeStatusMoSmppForward")]
        public async Task<IActionResult> ChangeStatusMoSmppForward([FromBody]  ChangeStatusMoSmppOnRequest request)
        {
            if (request.UPDATEDBY != 0)
            {
                var responseData = await _contextSynapseCore.ChangeStatusMoSmppForward(request);
                return Ok(responseData);
            }
            return Ok();
        }


        #endregion


        #endregion

        #region MoInbox     
        [HttpPost]
        [Route("GetMoInBox")]
        public async Task<IActionResult>GetMoInbox([FromBody] MoInboxOnRequest request)
        {
            if(request.UserId > 0)
            {
                var responseData = await _contextSynapseCore.MoInboxShowGrid(request);
                return Ok(responseData);
            }
            return Ok();
        }
        #endregion        

        #region for MoForward Box
        [HttpPost]
        [Route("GetMoForwardBox")]
        public async Task<IActionResult>GetMoForwardbox([FromBody] MoForwardOnRequest request)
        {
            if(request.UserId > 0)
            {
                var responseData = await _contextSynapseCore.MoForwardGrid(request);
                return Ok(responseData);
            }
            return Ok();
        }
        #endregion for MoForward Box

        #region for MoSentBox
        [HttpPost]
        [Route("GetMoSentBox")]
        public async Task<IActionResult> GetMoSentBox([FromBody] MoSentBoxOnrequest request)
        {
            if (request.UserId > 0)
            {
                var responseData = await _contextSynapseCore.MoSentboxShowGrid(request);
                return Ok(responseData);
            }
            return Ok();
        }
        #endregion for MoSentBox

        #region MoSurvey

        [HttpPost]
        [Route("VerifyIsSurveyExisted")]
        public async Task<IActionResult> VerifyIsSurveyExisted([FromBody] ReUsableRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Surveyname))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.VerifyIsSurveyExisted(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetMoSurvey")]
        public async Task<IActionResult> GetMoSurvey([FromBody]  MOsurveyRequest request)
        {
            var response = await _contextSynapseCore.GetMoSurvey(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetSendersByMoUsersLookUp")]
        public async Task<IActionResult> GetSendersByMoUsersLookUp([FromBody]MoSenderGetRequest request)
        {
            //if (request.UserId)
            //{
            //    return BadRequest();
            //}
            var response = await _contextSynapseCore.GetSendersByMoUsersLookUp(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetCampByUserIdAsync")]
        public async Task<IActionResult> GetCampByUserIdAsync([FromBody] MOCampaignbyuserrequest request)
        {
            var response = await _contextSynapseCore.GetCampByUserIdAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("SetMOActivities")]
        public async Task<IActionResult> SetMOActivities(Mosurveystatus request)
        {
            if (request.CampID != 0)
            {
                var response = await _contextSynapseCore.SetMOActivities(request);
                return Ok(response);
            }
            return Ok();
        }


        #endregion

        #region CampaignsORQuick

        [HttpGet]
        [Route("GetgGSMCharsQSMSCamp")]
        public async Task<IActionResult> GetgGSMCharsQSMSCamp()
        {
            var response = await _contextSynapseCore.GetgGSMCharsQSMSCamp();
            return Ok(response);
        }

        [HttpPost]
        [Route("ShowGridQuick")]
        public async Task<IActionResult> ShowGridQuick(ShowGridQuickOnRequest request)
        {
            var response = await _contextSynapseCore.ShowGridQuick(request);
            return Ok(response);
        }
        /// <summary>
        /// Method Name : ShowGridCampaigns
        /// Created By : G.Murali
        /// Created On : 13/09/2016
        /// Purpose : To get all the data of campaigns
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>      
        //[HttpGet]
        //[Route("ShowGridCampaigns")]
        //public async Task<IActionResult> ShowGridCampaigns(ShowGridCampaignsOnRequest request)
        //{
        //    var response = await _contextSynapseCore.ShowGridCampaigns(request);
        //    return Ok(response);
        //}
        //[HttpPost]
        //[Route("IsUnicodeCharsFound")]
        //public async Task<IActionResult> IsUnicodeCharsFound()
        //{
        //    var response = await _contextSynapseCore.IsUnicodeCharsFound();
        //    return Ok(response);
        //}
        //[HttpPost]
        //[Route("GetRolesPriviliges")]
        //public async Task<IActionResult> GetRolesPriviliges(RolesPriviligesOnRequest request)
        //{
        //    var response = await _contextSynapseCore.GetRolesPriviliges(request);
        //    return Ok(response);
        //}
        //[HttpPost]
        //[Route("GetLoadCampaignType")]
        //public async Task<IActionResult> GetLoadCampaignType(LoadCampaignTypeOnRequest request)
        //{
        //    var response = await _contextSynapseCore.GetLoadCampaignType(request);
        //    return Ok(response);
        //}
        /// <summary>
        /// Method Name : loadSenderIDCampaigns
        /// Created By : G.Murali
        /// Created On : 22/09/2016
        /// Purpose : To get all the SenderIDs of a user
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary> 
        [HttpPost]
        [Route("loadSenderIDCampaigns")]
        public async Task<IActionResult> loadSenderIDCampaigns(LoadSenderIDCampaignsOnRequest request)
        {
            if (request.USERID != 0)
            {
                var response = await _contextSynapseCore.loadSenderIDCampaigns(request);
                return Ok(response);
            }
            return Ok();
        }

        [HttpPost]
        [Route("loadNationalityCampaigns")]
        public async Task<IActionResult> loadNationalityCampaigns(LoadNationalityCampaignsOnRequest request)
        {
            var response = await _contextSynapseCore.loadNationalityCampaigns(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("loadCityCampaigns")]
        public async Task<IActionResult> loadCityCampaigns(LoadCityCampaignsOnRequest request)
        {
            var response = await _contextSynapseCore.loadCityCampaigns(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("loadIncomegroupCampaigns")]
        public async Task<IActionResult> loadIncomegroupCampaigns(LoadIncomegroupCampaignsOnRequest request)
        {
            var response = await _contextSynapseCore.loadIncomegroupCampaigns(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("LoadTemplateCampaigns")]
        public async Task<IActionResult> LoadTemplateCampaigns(LoadTemplateCampaignsOnRequest request)
        {
            if (request.CUSTID != 0 && request.USERID != 0)
            {
                var response = await _contextSynapseCore.LoadTemplateCampaigns(request);
                return Ok(response);
            }
            return Ok();
        }

        [HttpPost]
        [Route("InsertQSMS")]
        public async Task<IActionResult> InsertQSMS(InsertQSMSOnRequest request)
        {
            if (request.CreatedBy != 0 && request.SenderID != 0)
            {
                var response = await _contextSynapseCore.InsertQSMS(request);
                return Ok(response);
            }
            return Ok();
        }

        [HttpPost]
        [Route("OTPQSMS")]
        public async Task<IActionResult> OTPQSMS(InsertQSMSOnRequest request)
        {
            if (request.CreatedBy != 0 && request.SenderID != 0)
            {
                var response = await _contextSynapseCore.OTPQSMS(request);
                return Ok(response);
            }
            return Ok();
        }

        [HttpPost]
        [Route("GetRadioData")]
        public async Task<IActionResult> GetRadioData(InsertQSMSOnRequest request)
        {
            if (request.Medata != "0")
            {
                var response = await _contextSynapseCore.GetRadioData(request);
                return Ok(response);
            }
            return Ok();
        }

        [HttpPost]
        [Route("CheckerUpdateQuickSMS")]
        public async Task<IActionResult> CheckerUpdateQuickSMS(CheckerUpdateQSMSOnRequest Request)
        {
            if (Request.QSMSID != 0)
            {
                var response = await _contextSynapseCore.CheckerUpdateQuickSMS(Request);
                return Ok(response);
            }
            return Ok();
        }
        [HttpPost]
        [Route("InsertBulkSMS")]
        public async Task<IActionResult> InsertBulkSMS(InsertBulkSMSOnRequest request)
        {
            if (request.CustomerID != 0 && request.CreatedBy != 0 && Convert.ToInt32(request.SenderID) != 0)
            {
                var response = await _contextSynapseCore.InsertBulkSMS(request);
                return Ok(response);
            }
            return Ok();
        }
        [HttpPost("LoadSenderByCategory")]        
        public async Task<IActionResult> LoadSenderByCategory(LoadSenderByCategory request)
        {
            // allow reading the body multiple times
            HttpContext.Request.EnableBuffering();

            HttpContext.Request.Body.Position = 0;
            if (request.userId != 0 && request.category != string.Empty)
            {
                var response = await _contextSynapseCore.LoadSenderByCategory(request);
                return Ok(response);
            }
            return Ok();
        }
        [HttpPost]
        [Route("InsertCustomSMSActualCredits")]
        public async Task<IActionResult> InsertCustomSMSActualCredits(InsertBulkSMSOnRequest request)
        {
            try
            {

                var response = await _contextSynapseCore.InsertCustomSMSActualCredits(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
               Logger.ErrorFormat("InsertCustomSMSActualCredits :: Error :: {0}", ex.ToString());
            }
            return Ok();
        }     

        [HttpPost]
        [Route("InsertMOSMS")]
        public async Task<IActionResult> InsertMOSMS(MoSureveySaveRequest request)
        {
            if (request.CustomerID != 0 && request.CreatedBy != 0)
            {
                var response = await _contextSynapseCore.InsertMOSurvey(request);
                return Ok(response);
            }
            return Ok();
        }
        [HttpPost]
        [Route("InsertTestSMSCamp")]
        public async Task<IActionResult> InsertTestSMSCamp(InsertTestSMSOnRequest request)
        {
            if (request.InsertTestRecords != null && request.TotalCredReq != 0)
            {
                var response = await _contextSynapseCore.InsertTestSMSCamp(request);
                return Ok(response);
            }
            return Ok();
        }
        //Added by Murty
        [HttpPost]
        [Route("UpdateTestSMSCredits")]
        public async Task<IActionResult> UpdateTestSMSCredits(InsertTestSMSOnRequest request)
        {
            if (request.InsertTestRecords != null && request.TotalCredReq != 0)
            {
                var response = await _contextSynapseCore.UpdateTestSMSCredits(request);
                return Ok(response);
            }
            return Ok();
        }
        //Added By Murty
        [HttpPost]
        [Route("GetCampStageCounts")]
        public async Task<IActionResult> GetCampStageCounts(GetStageCountsOnRequest request)
        {
            if (request.CampID != 0 && request.StageIDs != "")
            {
                var response = await _contextSynapseCore.GetCampStageCounts(request);
                return Ok(response);
            }
            return Ok();
        }
        [HttpPost]
        [Route("SetCampaignEvents")]
        public async Task<IActionResult> SetCampaignEvents(SetCampEventsOnRequest request)
        {
            if (request.CampID != 0 && request.StageIDs != "")
            {
                var response = await _contextSynapseCore.SetCampaignEvents(request);
                return Ok(response);
            }
            return Ok();
        }


        [HttpPost]
        [Route("ValidateCampaignName")]
        public async Task<IActionResult> ValidateCampaignName(ReUsableRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.CustomerName))
            {
                var response = await _contextSynapseCore.ValidateCampaignName(request);
                return Ok(response);
            }
            return Ok();
        }

        [HttpPost]
        [Route("GetGroupByContacts")]
        public async Task<IActionResult> GetGroupByContacts(ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetGroupByContacts(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("CampaignStatusChange")]
        public async Task<IActionResult> CampaignStatusChange(ReUsableRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                var response = await _contextSynapseCore.CampaignStatusChange(request);
                return Ok(response);
            }
            return Ok();
        }

        [HttpPost]
        [Route("Externaldbcount")]
        public async Task<IActionResult> Externaldbcount(ExternalDB request)
        {
            var response = await _contextSynapseCore.Externaldbcount(request);
            return Ok(response);

        }

        [HttpPost]
        [Route("Externaldbfilter")]
        public async Task<IActionResult> Externaldbfilter(ReUsableRequest request)
        {
            var response = await _contextSynapseCore.Externaldbfilter(request);
            return Ok(response);

        }

        [HttpPost]
        [Route("InsertExternalDB")]
        public async Task<IActionResult> InsertExternalDB(InsertBulkSMSOnRequest request)
        {
            if (request.CustomerID != 0 && request.CreatedBy != 0 && Convert.ToInt32(request.SenderID) != 0)
            {
                var response = await _contextSynapseCore.InsertExternalDB(request);
                return Ok(response);
            }
            return Ok();
        }

        #endregion

        #region DB Alerts

        //[HttpPost]
        //[Route("GetDBAlerts")]
        //public async Task<IActionResult> GetDBAlerts([FromBody]  DBAlertsRequest request)
        //{
        //    var response = await _contextSynapseCore.GetDBAlerts(request);
        //    return Ok(response);
        //}

       

        #endregion

        [HttpPost]
        [Route("MessageTimings")]
        public async Task<IActionResult> MessageTimings(CampaignTimingsOnRequest request)
        {
            try
            {
                var response = await _contextSynapseCore.ShowGridCampaignTimings(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return Ok();
        }
    }
}

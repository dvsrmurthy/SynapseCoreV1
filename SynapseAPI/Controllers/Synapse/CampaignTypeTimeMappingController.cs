using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Models.Dtos.Requests.Synapse.ManageMobilityCenter;
using Core.Models.Dtos.CommonDtos;

namespace SynapseAPI.Controllers
{
    public class CampaignTypeTimeMappingController : ServicesBaseController
    {
        /// <summary>
        /// Method Name : ShowGridCampaignTimings
        /// Created By : G.Murali
        /// Created On : 
        /// Purpose : To get all  records of CampaignTimings
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>
        [HttpPost]
        [Route("ShowGridCampaignTimings")]
        public async Task<IActionResult> ShowGridCampaignTimings(CampaignTimingsOnRequest request)
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
        /// <summary>
        /// Method Name : LoadCampaignTypes
        /// Created By : G.Murali
        /// Created On : 
        /// Purpose : To get all  records of CampaignTypes 
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>
        [HttpPost]
        [Route("LoadCampaignTypes")]
        public async Task<IActionResult> LoadCampaignTypes(CampainTimingsLoadCampOnRequest request)
        {
            try
            {
                var response = await _contextSynapseCore.LoadCampaignTypes(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return Ok();
        }

        [HttpPost]
        [Route("ValidateMobileNums")]
        public async Task<IActionResult> ValidateMobileNums(ReUsableRequest request)
        {
            try
            {
                var response = await _contextSynapseCore.ValidateMobileNums(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return Ok();
        }


        [HttpPost]
        [Route("DndNumberCheck")]
        public async Task<IActionResult> DndNumberCheck(ReUsableRequest request)
        {
            try
            {
                var response = await _contextSynapseCore.DndNumberCheck(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return Ok();
        }
        /// <summary>
        /// Method Name : InsertCampTimings
        /// Created By : G.Murali
        /// Created On : 
        /// Purpose : To Insert new CampaignTimings
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>
        [HttpPost]
        [Route("InsertCampTimings")]
        public async Task<IActionResult> InsertCampTimings(CampainTimingsInsertCampOnRequest request)
        {
            try
            {
                if (request.CAMPTYPEID != 0 && request.FROMTIME != "" && request.TOTIME != "")
                {
                    var response = await _contextSynapseCore.InsertCampTimings(request);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            { var error = ex.Message; }
            return Ok();

        }
        /// <summary>
        /// Method Name : ChangestatusCampTimings
        /// Created By : G.Murali
        /// Created On : 
        /// Purpose : To Change the status of selected CampaignTiming
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>
        [HttpPost]
        [Route("ChangeStatusCampTimings")]
        public async Task<IActionResult> ChangeStatusCampTimings(CampaignTimingsChangeStatusOnRequest request)
        {
            try
            {
                if (request.CAMPID != "")
                {
                    var response = await _contextSynapseCore.ChangestatusCampTimings(request);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            { var error = ex.Message; }
            return Ok();
        }
        /// <summary>
        /// Method Name : CheckerUpdateCampaignTimings
        /// Created By : G.Murali
        /// Created On : 
        /// Purpose : To Approve or Reject of selected CampaignTiming
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>
        [HttpPost]
        [Route("CheckerUpdateCampaignTimings")]
        public async Task<IActionResult> CheckerUpdateCampaignTimings(CheckerUpdateCampaignTimingsOnRequest request)
        {
            if (request.CAMPID != 0 && request.UPDATEDBY != 0)
            {
                var response = await _contextSynapseCore.CheckerUpdateCampaignTimings(request);
                return Ok(response);
            }
            return Ok();
        }




        //status -startl
        [HttpPost]
        [Route("AINMobilityAsynch")]
        public async Task<IActionResult> AINCustomerAsynch([FromBody] ReUsableRequest request)
        {
            if (request.CustomerId < 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.AINMobilityAsynch(request);
            return Ok(response);
        }

        //status -end





    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Models.Dtos.CommonDtos;
using Core.Models.Dtos.Requests.Synapse.SMPP;

namespace SynapseAPI.Controllers
{
    public class SMPPController : ServicesBaseController
    {
        # region SMPPSENDERID
        [HttpPost]
        [Route("GetSMPPSenderAsync")]
        public async Task<IActionResult> GetSMPPSenderAsync([FromBody] GetSmppRequest request)
        {
            if (request.nSMPPSendetID <= 0 && request.nuserid <= 0 && request.strSender == null && request.nStatus <= 0)
            {
                return NotFound();
            }
            var response = await _contextSynapseCore.GetSMPPSenderAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("SetSMPPSenderAsync")]
        public async Task<IActionResult> SetSMPPSenderAsync([FromBody] SetSmppRequest request)
        {
            if(
                request.nUseId <=0 &&
                request.strGWSender == null &&
                request.strOutBoundSender == null &&
                request.strShCode == null &&
                request.strRemarks == null && 
                request.nSMPPSEnderstatus <=0 && 
                request.NSMPPID <=0 && 
                request.Stage == null &&
                request.MBCID <=0 &&
                request.ModuleID <=0 &&
                request.UserName == null)
            {
                return NotFound();
            }
            var response = await _contextSynapseCore.SetSMPPSenderAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetSmppById")]
        public async Task<IActionResult> GetSmppById([FromBody]  SmppIdReq request)
        {
            var response = await _contextSynapseCore.GetSmppById(request);
            return Ok(response);
        }
        #endregion

        # region SMPPMaster
        /// <summary>
        /// Method Name : ShowGridSMPPMaster
        /// Created By : 
        /// Created On : 
        /// Purpose : To get all records of SMPPMaster
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>
        [HttpPost]
        [Route("ShowGridSMPPMaster")]
        public async Task<IActionResult> ShowGridSMPPMaster(SMPPMasterOnRequest request)
        {
            var response = await _contextSynapseCore.ShowGridSMPPMaster(request);
                return Ok(response);
        }
        /// <summary>
        /// Method Name : GetUsersSMPPMaster
        /// Created By : 
        /// Created On : 
        /// Purpose : To get all Active Users
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>
        [HttpPost]
        [Route("GetUsersSMPPMaster")]
        public async Task<IActionResult> GetUsersSMPPMaster(GetUsersSMPPMasterOnRequest request)
        {
            var response = await _contextSynapseCore.GetUsersSMPPMaster(request);
            return Ok(response);
        }
        /// <summary>
        /// Method Name : GetCustomerSMPPMaster
        /// Created By : 
        /// Created On : 
        /// Purpose : To get all Active Customers
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>
        [HttpPost]
        [Route("GetCustomerSMPPMaster")]
        public async Task<IActionResult> GetCustomerSMPPMaster(GetCustomerSMPPMasterOnRequest request)
        {
            var response = await _contextSynapseCore.GetCustomerSMPPMaster(request);
            return Ok(response);
        }
        /// <summary>
        /// Method Name : InsertSMPPMaster
        /// Created By : 
        /// Created On : 
        /// Purpose : To Save or Update SMPPMaster details for selected users
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>
        [HttpPost]
        [Route("InsertSMPPMaster")]
        public async Task<IActionResult> InsertSMPPMaster(InsertSMPPMasterOnRequest request)
        {
            var response = await _contextSynapseCore.InsertSMPPMaster(request);
            return Ok(response);
        }
        /// <summary>
        /// Method Name : ChangeStatusSMPPMaster
        /// Created By : 
        /// Created On : 
        /// Purpose : To change status of SMPPMaster details of selected user
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>
        [HttpPost]
        [Route("ChangeStatusSMPPMaster")]
        public async Task<IActionResult> ChangeStatusSMPPMaster(ChangeStatusSMPPMasterOnRequest request)
        {
            var response = await _contextSynapseCore.ChangeStatusSMPPMaster(request);
            return Ok(response);
        }
        /// <summary>
        /// Method Name : CheckerUpdateSMPPMaster
        /// Created By : 
        /// Created On : 
        /// Purpose : To Approve or Reject of SMPPMaster details for selected user
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>
        [HttpPost]
        [Route("CheckerUpdateSMPPMaster")]
        public async Task<IActionResult> CheckerUpdateSMPPMaster(CheckerUpdateSMPPMasterOnRequest request)
        {
            var response = await _contextSynapseCore.CheckerUpdateSMPPMaster(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetInstanceSMPP")]
        public async Task<IActionResult> GetInstanceSMPP([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetInstanceSMPP(request);
            return Ok(response);
        }

        #endregion 

        #region SMPPIPAllocation
        /// <summary>
        /// Method Name : GetSMPPMasterIPAllocation
        /// Created By : 
        /// Created On : 
        /// Purpose : To get SMPPMaster details of selected user
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>
        [HttpPost]
        [Route("GetSMPPMasterIPAllocation")]
        public async Task<IActionResult> GetSMPPMasterIPAllocation(GetSmppMasterIPAllocationOnRequest request)
        {
            if (request.USERID != 0 && request.CUSTID != 0)
            {
                var response = await _contextSynapseCore.GetSMPPMasterIPAllocation(request);
                return Ok(response);
            }
            return Ok();
        }
        /// <summary>
        /// Method Name : GetSMPPIPAllocation
        /// Created By : 
        /// Created On : 
        /// Purpose : To get all IPs of selected user
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>
        [HttpPost]
        [Route("GetSMPPIPAllocation")]
        public async Task<IActionResult> GetSMPPIPAllocation(GetSmppIPAllocationOnRequest request)
        {
            if (request.CUSTID!=0 && request.USERID!=0&&request.SMPPID!=0)
            {
                var response = await _contextSynapseCore.GetSMPPIPAllocation(request);
                return Ok(response);
            }
            return Ok();
        }
        /// <summary>
        /// Method Name : InsertSMPPIPAllocation
        /// Created By : 
        /// Created On : 
        /// Purpose : To Save IP details for selected user and Approve or Reject IP details of selected
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>
        [HttpPost]
        [Route("InsertSMPPIPAllocation")]
        public async Task<IActionResult> InsertSMPPIPAllocation(SetSmppIPAllocationOnRequest request)
        {
            if (request.SMPPIPS.Count>0)
            {
                var response = await _contextSynapseCore.InsertSMPPIPAllocation(request);
                return Ok(response);
            }
            return Ok();
        }
        //[HttpPost]
        //[Route("CheckerUpdateSMPPIP")]
        //public async Task<IActionResult> CheckerUpdateSMPPIP(CheckerUpdateSMPPIPOnRequest request)
        //{
        //    if (request.AUTOID!= 0)
        //    {
        //        var response = await _contextSynapseCore.CheckerUpdateSMPPIP(request);
        //        return Ok(response);
        //    }
        //    return Ok();
        //}

        #endregion
    }
}

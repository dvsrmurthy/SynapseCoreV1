using Core.Models.Dtos.Requests.Synapse.SMSCSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Models.Dtos.CommonDtos;

namespace SynapseAPI.Controllers
{
    public class TestSMSCController : ServicesBaseController
    {

        //[HttpPost]
        //[Route("GetTestSMSCByIdAsync")]
        //public async Task<IActionResult> GetTestSMSCByIdAsync([FromBody] TestSMSCRequest request)
        //{
          
        //    if (request.nID <= 0 &&  request.nAddedBy <=0 &&  request.nStatus <=0 &&  request.nTestSMSC <=0 && request.RequestedPage )
        //    {
        //        return BadRequest();
        //    }
        //    var response = await _contextSynapseCore.GetTestSMSCByIdAsync(request);
        //    return Ok(response);
        //}

        [HttpPost]
        [Route("GetTestSMSCByIdAsync")]
        public async Task<IActionResult> GetTestSMSCByIdAsync([FromBody]  TestSMSCRequest request)
        {
            var response = await _contextSynapseCore.GetTestSMSCByIdAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ADDTestSMSCByUser")]
        public async Task<IActionResult> AddTestSMSCByUser([FromBody] AddSMSRequest request)
        {

            if (
                request.QSMSID <= 0 &&
                request.nSenderID <= 0 &&
                request.nLangId == null &&
                request.strMsg == null &&
                request.nCharCount <= 0 &&
                request.nCreditsUsed <= 0 &&
                request.nDLR <= 0 &&
                request.nAddedBy <= 0 &&
                request.Sender == null &&
                request.Module == null &&
              //  request.Stage == null &&
                request.nTestSMSC <= 0 &&
                request.nReturn <= 0 &&
                request.nId <= 0 &&
                request.EventType <=0 &&
                request.Currentstatus <=0 &&
                request.strMobiles ==  null &&   
                request.updatedby <=0

                )
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.AddTestSMSCByUser(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("CheckerTestSMSC")]
        public async Task<IActionResult> CheckerTestSMSC([FromBody] CheckTestSMSC request)
        {
            if (request.QuicksmsId <= 0 &&
                string.IsNullOrWhiteSpace(request.RejectNote))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.CheckerTestSMSC(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetSenderbyUserIdTest")]
        public async Task<IActionResult> GetSenderbyUserIdTest([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetSenderbyUserIdTest(request);
            return Ok(response);
        }

        #region VendorMaster

        [HttpPost]
        [Route("GetAllVendorsAsync")]
        public async Task<IActionResult> GetAllVendorsAsync([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetAllVendorsAsync(request);
            return Ok(response);
        }
        
        [HttpPost]
        [Route("SetVendorsAsync")]
        public async Task<IActionResult> SetVendors([FromBody] SetVendorsRequest request)
        {
            var response = await _contextSynapseCore.SetVendors(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetVendoeByVendorIdAsync")]
        public async Task<IActionResult> GetVendoeByVendorId([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetVendoeByVendorId(request);
            return Ok(response);
        }
        
        [HttpPost]
        [Route("ApproveOrRejectVendorAsync")]
        public async Task<IActionResult> ApproveOrRejectVendorAsync([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.ApproveOrRejectVendorAsync(request);
            return Ok(response);
        }
        #endregion
        
        #region Map SMSC

        [HttpPost]
        [Route("GetAllMapSenderDetailsAsync")]
        public async Task<IActionResult> GetAllMapSenderDetailsAsync([FromBody] SmscTableRequest request)
        {
            var response = await _contextSynapseCore.GetAllMapSenderDetailsAsync(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetAllRoutesAsync")]
        public async Task<IActionResult> GetAllRoutesAsync([FromBody] SMSCRoutes request)
        {
            var response = await _contextSynapseCore.GetAllRoutesAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("SaveOrUpdateMapSenderAsync")]
        public async Task<IActionResult> SaveOrUpdateMapSenderAsync([FromBody] MapSenderRequest request)
        {
            var response = await _contextSynapseCore.SaveOrUpdateMapSender(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ChangeStatusMS")]
        public async Task<IActionResult> ChangeStatusMS([FromBody] ReUsableRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserIds))
            {
                return BadRequest();
            }
            var response = _contextSynapseCore.ChangeStatusMS(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ApproveOrRejectMapSenderAsync")]
        public async Task<IActionResult> ApproveOrRejectMapSenderAsync([FromBody] MapSenderRequest request)
        {
            var response = await _contextSynapseCore.ApproveOrRejectMapSender(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetShortCodesnRoutesAsync")]
        public async Task<IActionResult> GetShortCodesnRoutesAsync([FromBody] ReUsableRequest request)
        {
            if (request.UserId<=0 && request.CustomerId <=0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetShortCodesnRoutes(request);
            return Ok(response);
        }
        #endregion
    }
}

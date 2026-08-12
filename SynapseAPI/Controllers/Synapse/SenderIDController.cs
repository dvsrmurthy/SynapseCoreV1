using Core.Models.Dtos.CommonDtos;
using Core.Models.Dtos.Requests.Synapse.SMSCSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SynapseAPI.Controllers
{
    public class SenderIDController : ServicesBaseController
    {
        [HttpPost]
        [Route("GetAllSenderByIdAsync")]
        public async Task<IActionResult> GetAllSenderByIdAsync([FromBody] SenderIDRequest Request)
        {
            if (Request.NSENDERID <= 0 && Request.NSTATUS <= 0 && Request.nCreatedby <= 0)
            {
                return BadRequest();
            }
            var Response = await _contextSynapseCore.GetAllSenderByIdAsync(Request);
            return Ok(Response);
        }
        [HttpPost]
        [Route("GetAllSenderByIdAsyncSearch")]
        public async Task<IActionResult> GetAllSenderByIdAsyncSearch([FromBody] SenderIDRequest Request)
        {            
            var Response = await _contextSynapseCore.GetAllSenderByIdAsyncSearch(Request);
            return Ok(Response);
        }


        //code added 20-09-2016 add/edit senderidshort codes
        [HttpPost]
        [Route("AddEditSenderIDShortCode")]
        public async Task<IActionResult> AddEditSenderIDShortCode([FromBody] AddEditSender request)
        {
            if (
                request.sType <= 0 &&
                request.sCode == null &&
                request.sId < 0 
                )
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.AddEditSenderIDShortCode(request);
            return Ok(response);
        }



        //status -startl
        [HttpPost]
        [Route("AINSenderIDShortCodeAsynch")]
        public async Task<IActionResult> AINSenderIDShortCodeAsynch([FromBody] ReUsableRequest request)
        {
            if (request.CustomerId < 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.AINSenderIDShortCodeAsynch(request);
            return Ok(response);
        }

        //status -end


        [HttpPost]
        [Route("SIDSCCheck")]
        public async Task<IActionResult> SIDSCCheck([FromBody] AorRSIDSC request)
        {
            if (request.id <= 0 &&
                string.IsNullOrWhiteSpace(request.rejectnote))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.SIDSCCheck(request);
            return Ok(response);
        }






    }
}

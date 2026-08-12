using Core.Models.Dtos.Requests.Synapse.MailBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SynapseAPI.Controllers
{
    public class UserMailBoxMappingController : ServicesBaseController
    {
        [HttpPost]
        [Route("GetUserMappingMailAsync")]
        public async Task<IActionResult> GetUserMappingMailAsync([FromBody] UserMailBoxMappingRequest request)
        {
            if (
                request.NUSERMAILBOXMAPPINGID < 0 &&
                request.NSTATUS <= 0
                )
            {
                return NotFound();
            }
            var responce = await _contextSynapseCore.GetUserMappingMailAsync(request);
            return Ok(responce);
        }

        [HttpPost]
        [Route("SetUserMailboxAsync")]
        public async Task<IActionResult> SetUserMailboxAsync([FromBody] InsertUserMailboxMappingRequest request)
        {
            if (
                request.NCUSTOMERID <= 0 &&
                request.NUSERID <= 0 &&
                request.NSENDERID <= 0 &&
                request.NMAILBOXID<=0 &&
                request.STRUSERNAME == null &&
                request.NADDEDBY == null
                )
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.SetUserMailboxAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetUserbyId")]
        public async Task<IActionResult> GetUserbyId([FromBody] UserbyCustomerIdReq request)
        {
            var response = await _contextSynapseCore.GetUserbyId(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetSenderbyId")]
        public async Task<IActionResult> GetSenderbyId([FromBody] SenderbyUserIdReq request)
        {
            var response = await _contextSynapseCore.GetSenderbyId(request);
            return Ok(response);
        }

        //[HttpPost]
        //[Route("CheckerUserMailbox")]
        //public async Task<IActionResult> CheckerUserMailbox([FromBody] UserMailconfigReq request)
        //{
        //    if (request.ID <= 0 &&
        //        string.IsNullOrWhiteSpace(request.REJECTNOTE))
        //    {
        //        return BadRequest();
        //    }
        //    var responce = await _contextSynapseCore.CheckerUserMailbox(request);
        //    return Ok(responce);
        //}
    }

    
}

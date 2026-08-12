using Core.Models.Dtos.Requests.Synapse.AdminOperation;
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
    public class UnlockUserController : ServicesBaseController
    {
        [HttpPost]
        [Route("GetAllUnlockByIdAsync")]
        public async Task<IActionResult> GetAllUnlockByIdAsync([FromBody] UnlockUserRequest request)
        {
            if (request.USERID < 0 && request.UPDATEDBY < 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetAllUnlockByIdAsync(request);
            return Ok(response);
        }


        [HttpPost]
        [Route("UpdateLockUserByUserId")]
        public async Task<IActionResult> UpdateLockUserByUserId([FromBody] UpdateLockStatus request)
        {
            if (request.UserId <= 0 && request.UpdatedBy <= 0 && request.EventType <= 0 && request.ReturnValue <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.UpdateLockUserByUserId(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ApproveUnlockUserAsync")]
        public async Task<IActionResult> ApproveUnlockUserAsync([FromBody] UpdateLockStatus request)
        {
            if (request.UserId <= 0 && request.Functionalstatus<=0 && !string.IsNullOrWhiteSpace(request.RejectNote))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.ApproveUnlockUserAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("RejectUnlockUserAsync")]
        public async Task<IActionResult> RejectUnlockUserAsync([FromBody] UpdateLockStatus request)
        {
            if (request.UserId <= 0 && request.Functionalstatus <= 0 && !string.IsNullOrWhiteSpace(request.RejectNote))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.RejectUnlockUserAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetPreferenceValue")]
        public async Task<IActionResult> GetPreferenceValue([FromBody] ChangePasswordRequest request)
        {
            var result = await _contextSynapseCore.GetPreferenceValue(request);
            return Ok(result);
        }

        [HttpPost]
        [Route("ValidatePassAsync")]
        public async Task<IActionResult> ValidatePassAsync([FromBody]ReUsableRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.IsValidPassAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ChangePassAsync")]
        public async Task<IActionResult> ChangePassAsync([FromBody]ReUsableRequest request)
        {
            if (request.UserId <= 0 && string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.UpdatekPassByUserId(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("UnlockStatus")]
        public async Task<IActionResult> UnlockStatus([FromBody] ReUsableRequest request)
        {
            if (request.CustomerId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.UnlockStatus(request);
            return Ok(response);
        }
    }
}

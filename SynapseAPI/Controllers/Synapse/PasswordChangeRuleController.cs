using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Models.Dtos.CommonDtos;
using Core.Models.Dtos.Requests.Synapse.SecurityManagement;

namespace SynapseAPI.Controllers
{
    public class PasswordChangeRuleController : ServicesBaseController
    {

        [HttpPost]
        [Route("Setpasswordpreference")]
        public async Task<IActionResult> Setpasswordpreference([FromBody] PasswordPreferenceRequest request)
        {
            var result = await _contextSynapseCore.Setpasswordpreference(request);
            return Ok(result);
        }

        [HttpPost]
        [Route("SetTwoFactorRules")]
        public async Task<IActionResult> SetTwoFactorRules([FromBody] PasswordPreferenceRequest request)
        {
            var result = await _contextSynapseCore.SetTwoFactorRules(request);
            return Ok(result);
        }

        [HttpPost]
        [Route("GetPasswordPreference")]
        public async Task<IActionResult> GetPasswordPreference([FromBody] GetPasswordPreferenceRequest request)
        {
            var result = await _contextSynapseCore.GetPasswordPreference(request);
            return Ok(result);
        }

        [HttpPost]
        [Route("UpdateUserPasswordAsync")]
        public async Task<IActionResult> UpdateUserPasswordAsync([FromBody] ReUsableRequest request)
        {
            var result = await _contextSynapseCore.UpdateUserPassword(request);
            return Ok(result);
        }

        [HttpPost]
        [Route("CheckerPasswordChangeRules")]
        public async Task<IActionResult> CheckerPasswordChangeRules([FromBody] PSRCheckerRequest request)
        {
            if (request.Id <= 0 &&
                string.IsNullOrWhiteSpace(request.RejectNote))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.CheckerPasswordChangeRules(request);
            return Ok(response);
        }
    }
}

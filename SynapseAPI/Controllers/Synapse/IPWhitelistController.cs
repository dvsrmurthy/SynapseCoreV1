using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Models.Dtos.Requests.Synapse.UserManagement;
using Core.Models.Dtos.CommonDtos;

namespace SynapseAPI.Controllers
{
    public class IPWhitelistController : ServicesBaseController 
    {
        [HttpPost]
        [Route("GetUserIPWhiteList")]
        public async Task<IActionResult> GetUserIPWhiteList([FromBody] GetUserIPwhiteListRequest request)
        {
            var result = await _contextSynapseCore.GetIPWhiteList(request);
            return Ok(result);
        }


        [HttpPost]
        [Route("SetUserIPWhiteList")]
        public async Task<IActionResult> SetUserIPWhiteList([FromBody] SetUserIPwhiteListRequest request)
        {
            var response = await _contextSynapseCore.SetIPWhiteList(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ChangeStatusIP")]
        public async Task<IActionResult> ChangeStatusIP([FromBody] ReUsableRequest request)
        {

            if (request.UserId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.ChangeStatusIP(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetHTTPUsersByCustomerIdAsync")]
        public async Task<IActionResult> GetHTTPUsersByCustomerIdAsync([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetHTTPUsersByCustomerIdAsync(request);
            return Ok(response);
        }

    }
}

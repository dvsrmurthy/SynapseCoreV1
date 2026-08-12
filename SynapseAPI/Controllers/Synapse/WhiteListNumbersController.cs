using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Models.Dtos.Requests.Synapse.AdminOperation;
using Core.Models.Helpers;
using System.Threading.Tasks;
using Core.Models.Dtos.CommonDtos;
using Microsoft.AspNetCore.Mvc;

namespace SynapseAPI.Controllers
{
    public class WhiteListNumbersController : ServicesBaseController
    {
        [HttpPost]
        [Route("GetAllWhiteListNumberstAsync")]
        public async Task<IActionResult> GetAllWhiteListNumberstAsync([FromBody] WhiteListNumbersRequest request)
        {
            if (
                request.nCustomerId <= 0 &&
                request.nUserId <= 0 &&                
                request.strMobileNo == null &&                
                request.nStatus == null                
                )
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetAllWhiteListNumberstAsync(request);
            return Ok(response);
        }
    }
}

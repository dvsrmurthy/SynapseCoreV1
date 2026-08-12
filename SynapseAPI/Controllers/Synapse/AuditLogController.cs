using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
//using Microsoft.AspNetCore.Mvc;
using Core.Models.Dtos.Requests.Synapse.AdminOperation;
using Core.Models.Dtos.Responses.Synapse.AdminOperation;
using System.Threading.Tasks;
using Core.Models.Dtos.CommonDtos;
using Microsoft.AspNetCore.Mvc;

namespace SynapseAPI.Controllers
{
    public class AuditLogController : ServicesBaseController
    {
        [HttpPost]
        [Route("GetAuditlogAsync")]
        public async Task<IActionResult> GetAuditlogAsync([FromBody] AuditLogsRequest request)
        {
            var result = await _contextSynapseCore.GetAuditlogAsync(request);
            return Ok(result);

        }

        [HttpPost]
        [Route("GetDetailedAuditlogAsync")]
        public async Task<IActionResult> GetDetailedAuditlogAsync([FromBody] AuditLogsRequest request)
        {
            var result = await _contextSynapseCore.GetDetailedAuditlogAsync(request);
            return Ok(result);

        }
       
        [HttpPost]
        [Route("GetpriviligesbyCustomerId")]
        public async Task<IActionResult> GetpriviligesbyCustomerId([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetpriviligesbyCustomerId(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetUsersByCustomerIdAsyncString")]
        public async Task<IActionResult> GetUsersByCustomerIdAsyncString([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetUsersByCustomerIdAsyncString(request);
            return Ok(response);
        } 
    }
}

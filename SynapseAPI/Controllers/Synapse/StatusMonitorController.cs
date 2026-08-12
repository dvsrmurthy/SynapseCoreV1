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
using Core.Models.Dtos.Requests.Synapse.StatusMonitor;
using System.Data;
using Core.Models.Dtos.Responses.Synapse.StatusMonitor;

namespace SynapseAPI.Controllers
{
    public class StatusMonitorController : ServicesBaseController
    {
        [HttpPost]
        [Route("GetCustomerUserAsync")]
        public async Task<IActionResult> GetCustomerUserAsync([FromBody] CustUserSearch request)
        {
            var response = await _contextSynapseCore.GetCustomerUserAsync(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetPromoSummaryAsync")]
        public async Task<IActionResult> GetPromoSummaryAsync([FromBody] PromoSummarySearch request)
        {
            var response = await _contextSynapseCore.GetPromoSummaryAsync(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetMapSenderAsync")]
        public async Task<IActionResult> GetMapSenderAsync([FromBody] MapSenderSearch request)
        {
            var response = await _contextSynapseCore.GetMapSenderAsync(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetSMSCMasterAsync")]
        public async Task<IActionResult> GetSMSCMasterAsync([FromBody] SMSCSearch request)
        {
            var response = await _contextSynapseCore.GetSMSCMasterAsync(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetCustomerMasterAsync")]
        public async Task<IActionResult> GetCustomerMasterAsync([FromBody] CustomerSearch request)
        {
            var response = await _contextSynapseCore.GetCustomerMasterAsync(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetUsersMasterAsync")]
        public async Task<IActionResult> GetUsersMasterAsync([FromBody] UserSearch request)
        {
            var response = await _contextSynapseCore.GetUsersMasterAsync(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetDlrPercentageAsync")]
        public async Task<IActionResult> GetDlrPercentageAsync([FromBody] DLRPercentageSearch request)
        {
            var response = await _contextSynapseCore.GetDlrPercentageAsync(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetDlrPercentageCAsync")]
        public async Task<IActionResult> GetDlrPercentageCAsync([FromBody] DLRPercentageSearch request)
        {
            var response = await _contextSynapseCore.GetDlrPercentageCAsync(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetConnectionString")]
        public string? GetConnectionString([FromBody] DLRPercentageSearch request)
        {
            var response = _contextSynapseCore.GetConnectionString();
            return response;
        }
        [HttpPost]
        [Route("GetServerTransactionAsync")]
        public async Task<IActionResult> GetServerTransactionAsync([FromBody] ServerTransactionSearch request)
        {
            var response = await _contextSynapseCore.GetServerTransactionAsync(request);
            return Ok(response);
        }
    }
}

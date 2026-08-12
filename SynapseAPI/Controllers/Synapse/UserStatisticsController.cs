using Core.Models.Dtos.Requests.Synapse.SMSCSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Models.Extensions;



namespace SynapseAPI.Controllers
{
    public class UserStatisticsController:ServicesBaseController
    {
        [HttpPost]
        [Route("ShowINTLSenderDetails")]
        public async Task<IActionResult> ShowINTLSenderDetails([FromBody] GetGBLSenderDetailsRequest request)
        {
            var response = await _contextSynapseCore.ShowINTLSenderDetails(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("SHOWINTLUSERSTATDETAILS")]
        public async Task<IActionResult> SHOWINTLUSERSTATDETAILS([FromBody] GETINTLUSERSTATDETAILSREquest request)
        {
            var response = await _contextSynapseCore.SHOWINTLUSERSTATDETAILS(request);
            return Ok(response);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Models.Dtos.Requests.Synapse.SMSCSettings;

namespace SynapseAPI.Controllers
{
    public class SMSCSwitchOverController : ServicesBaseController
    {
        [HttpPost]
        [Route("GetAllSMSCSwitchAsync")]
        public async Task<IActionResult> GetAllSMSCSwitchAsync([FromBody] SMSCSwithRequest request)
        {
            if (request.Id <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetAllSMSCSwitchAsync(request);
            return Ok(response);
        }

         [HttpPost]
         [Route("GetAllSMSCRoute")]
        public async Task<IActionResult> GetAllSMSCRoute([FromBody] SMSCSwithRequest request)
        {
            if (request.Id <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetAllSMSCRoute(request);
            return Ok(response);
        }        

        [HttpPost]
        [Route("UpdateSMSCSwitchOverasync")]
        public async Task<IActionResult> UpdateSMSCSwitchOverasync([FromBody] SMSCSwithRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FromSMSC))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.UpdateSMSCSwitchOverasync(request);
            return Ok(response);
        }
    }
}

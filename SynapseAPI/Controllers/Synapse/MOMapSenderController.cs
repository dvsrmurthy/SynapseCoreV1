using Core.Models.Dtos.Requests.Synapse.SMSCSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SynapseAPI.Controllers
{
    public class MOMapSenderController : ServicesBaseController
    {
        [HttpPost]
        [Route("GetAllMOMapSenderByIdAsync")]
        public async Task<IActionResult> GetAllMOMapSenderByIdAsync([FromBody] MOMapSenderRequest request)
        {
            if (
                string.IsNullOrWhiteSpace(request.ID)
                )
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetAllMOMapSenderByIdAsync(request);
            return Ok(response);
        }


        [HttpPost]
        [Route("AddMOMapSenderAsync")]
        public async Task<IActionResult> AddMOMapSenderAsync([FromBody] MOMapSenderRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.ID) &&
                string.IsNullOrWhiteSpace(request.NCUSTID) &&
                string.IsNullOrWhiteSpace(request.NUSERID) &&
                string.IsNullOrWhiteSpace(request.NSID) &&
                string.IsNullOrWhiteSpace(request.DispShortCode)  &&
                string.IsNullOrWhiteSpace(request.ShortCodeType) &&
                request.Status <= 0 &&
                request.NCREATEDBY <= 0

                )
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.AddMOMapSenderAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("MOShortcodeStatusChange")]
        public async Task<IActionResult> MOShortcodeStatusChange([FromBody] MOMapSenderRequest request)
        {
            if (
                string.IsNullOrWhiteSpace(request.ID)
                )
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.MOShortcodeStatusChange(request);
            return Ok(response);
        }

    }
}

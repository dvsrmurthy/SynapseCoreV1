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
    public class MailBoxController : ServicesBaseController
    {

        [HttpPost]
        [Route("GetAllMailAsync")]
        public async Task<IActionResult> GetAllMailAsync([FromBody] MailBoxConfigurationRequest request)
        {
            if (
                request.NMAILBOXID < 0 &&
                request.NSTATUS <= 0
                )
            {
                return NotFound();
            }
            var responce = await _contextSynapseCore.GetAllMailAsync(request);
            return Ok(responce);
        }
        [HttpPost]
        [Route("SetAllMailAsync")]
        public async Task<IActionResult> SetAllMailAsync([FromBody] AddMailBoxConfiguration request)
        {
            if (
                request.STRHOST == null &&
                request.STRMAILBOX == null &&
                request.STRPASSWORD == null &&
                request.NPORT < 0 &&
                //request.NSSL < 0 &&
                request.NFREQUENCY < 0 &&
                request.NMAILTYPE < 0 &&
                request.NSTATUS < 0 &&
                request.NADDEDBY < 0 &&
                request.NRETVAL < 0 &&
                request.NMBID < 0

                )
            {
                return BadRequest();
            }
            var responce = await _contextSynapseCore.SetAllMailAsync(request);
            return Ok(responce);

        }

        [HttpPost]
        [Route("CheckerMailConfig")]
        public async Task<IActionResult> CheckerMailConfig([FromBody] CheckerMailconfigRequest request)
        {
            if (request.ID <= 0 &&
                string.IsNullOrWhiteSpace(request.REJECTNOTE))
            {
                return BadRequest();
            }
            var responce = await _contextSynapseCore.CheckerMailConfig(request);
            return Ok(responce);
        }
      
    }
}

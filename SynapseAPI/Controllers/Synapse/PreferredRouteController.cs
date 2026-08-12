using Core.Models.Dtos.Requests.Synapse.SMSCSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SynapseAPI.Controllers
{
    public class PreferredRouteController : ServicesBaseController
    {
        [HttpPost]
        [Route("GetAllPreferredByIdAsync")]
        public async Task<IActionResult> GetAllPreferredByIdAsync([FromBody] PreferedRouteRequest request)
        {
            if (
                request.NID <=0 &&
                request.NROUTEID <=0 &&
                request.NCOUNTRYCODE <=0 &&
                request.NCUSTID <=0 &&
                request.STRUSERID <=0 &&
                request.NSERIESID <=0 &&
                request.NSTATUS  &&
                request.NCREATEDBY <=0 &&
                request.requestedby== null
                
                )
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetAllPreferredByIdAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("AddPreferedByIdAsync")]
        public async Task<IActionResult> AddPreferedByIdAsync([FromBody] AddPreferedRouteReq request)
        {
            if (request.NROUTEID <= 0 &&
                request.NCUSTOMERID <= 0 &&
                string.IsNullOrWhiteSpace(request.NUSERID) &&
                request.STRCOUNTRYCODE !=string.Empty &&
                request.NSERIES <= 0 &&
                request.NCREATEDBY <= 0 &&
                request.EventType <= 0 &&
                request.CurrentStatus <= 0 &&
                request.UPDATEDBY <= 0 &&
                request.NRETVAL <= 0

                )
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.AddPreferedByIdAsync(request);
            return Ok(response);
        }


        [HttpPost]
        [Route("CheckerPreferedRouteUser")]
        public async Task<IActionResult> CheckerPreferedRouteUser([FromBody] CheckerPreferedRoute request)
        {
            if (request.Id <= 0 &&
                string.IsNullOrWhiteSpace(request.Rejectnote))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.CheckerPreferedRouteUser(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus([FromBody]PreferedStatus request)
        {
            if (string.IsNullOrWhiteSpace(request.CustomerId))
            {
                return BadRequest();
            }
            var response = _contextSynapseCore.ChangeStatus(request);
            return Ok(response);
        }
    }
}

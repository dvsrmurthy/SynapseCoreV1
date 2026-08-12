using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Models.Dtos.CommonDtos;
using Core.Models.Dtos.Requests.Synapse.AdminOperation;

namespace SynapseAPI.Controllers
{
    public class HlrLookUpController : ServicesBaseController
    {
        [HttpPost]
        [Route("GetHlrLookUpAsync")]
        public async Task<IActionResult> GetHlrLookUpAsync([FromBody]HlrLookupRequest request)
        {
            if (request.CustomerId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetHlrLookUpAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetHlrLookUpRuleByCustomer")]
        public async Task<IActionResult> GetHlrLookUpRuleByCustomer([FromBody]ReUsableRequest request)
        {
            if (request.CustomerId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetAllHlrLookUpsByCustomerAync(request);
            return Ok(response);
        }


        [HttpPost]
        [Route("GetUsersByCustomerLookUpAsync")]
        public async Task<IActionResult> GetUsersByCustomerLookUpAsync([FromBody]ReUsableRequest request)
        {
            if (request.CustomerId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetUsersByCustomerLookUpAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetSendersByUsersLookUpAsync")]
        public async Task<IActionResult> GetSendersByUsersLookUpAsync([FromBody]ReUsableRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserIds))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetSendersByUsersLookUpAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("SaveHlrLookUpResult")]
        public async Task<IActionResult> SaveHlrLookUpResult([FromBody]List<HlrLookupRequest> request)
        {
            if (request.Count <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.SaveHlrLookUpResult(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("HlrLookupStatus")]
        public async Task<IActionResult> HlrLookupStatus([FromBody] ReUsableRequest request)
        {
            if (request.CustomerId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.HlrLookupStatus(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("CheckerHlrLookup")]
        public async Task<IActionResult> CheckerHlrLookup([FromBody] HlrLookupRequest request)
        {
            if (request.Id <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.CheckerHlrLookup(request);
            return Ok(response);

        }


        [HttpPost]
        [Route("SaveAppConfig")]
        public async Task<IActionResult> SaveAppConfig(ApplicationConfigurationReq request)
        {
            if (request.Interval <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.SaveAppConfig(request);
            return Ok(response);
        }
    }
}
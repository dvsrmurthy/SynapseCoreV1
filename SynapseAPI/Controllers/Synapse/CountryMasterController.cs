using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Models.Dtos.CommonDtos;
using Core.Models.Dtos.Requests.Synapse.SMSCSettings;

namespace SynapseAPI.Controllers
{
    public class CountryMasterController : ServicesBaseController
    {
        [HttpPost]
        [Route("GetAllCountryByIdAsync")]
        public async Task<IActionResult> GetAllCountryByIdAsync([FromBody] CountryDetails Request)
        {
            if(Request.STRSEARCH == null && Request.NSEARCH<=0 && Request.nCreatedBy<=0)
            {
                return BadRequest();
            }
            var Response = await _contextSynapseCore.GetAllCountryByIdAsync(Request);
            return Ok(Response);
        }

        [HttpPost]
        [Route("CountryActiveStatuChangeAsync")]
        public async Task<IActionResult> RoleActiveStatuChangeAsync([FromBody] ReUsableRequest request)
        {
            if (request.ParentId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.CountryActiveStatusChange(request);
            return Ok(response);
        }

        //code added  20-09-2016 add/edit country codes
        [HttpPost]
        [Route("AddEditCountryMaster")]
        public async Task<IActionResult> AddEditCountryMaster([FromBody] AddEditCountry request)
        {
            if (
                //request.StrCountry == null &&
                //request.StrCode == null &&
                //request.AddUpdateUser <= 0 &&
                //request.CountryID <= 0

                request.StrCountry == null &&
                request.CountryCode == null &&
                
                Convert.ToInt32(request.CountryCode) <= 0
                )
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.AddEditCountryMaster(request);
            return Ok(response);
        }


        [HttpPost]
        [Route("ApproveOrRejectCountryAsync")]
        public async Task<IActionResult> ApproveOrRejectCountryAsync([FromBody] ApproveOrRejectRequestCountry request)
        {
            if (
                request.ISDCode <= 0
                )
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.ApproveOrRejectCountry(request);
            return Ok(response);
        }
    }
}

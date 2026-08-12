using Core.Models.Dtos.Requests.Synapse.CreditsManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SynapseAPI.Controllers
{
    public class CreditsManagementController : ServicesBaseController
    {
        [HttpPost]
        [Route("GetAllCreditsByIdAsync")]
        public async Task<IActionResult> GetAllCreditsByIdAsync([FromBody] CustomerCreditsRequest request)
        {
            if (request.NID <= 0  && request.NCUSTID <= 0 && request.NCREATEDBY <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetAllCreditsByIdAsync(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetAllCreditsBySearchAsync")]
        public async Task<IActionResult> GetAllCreditsBySearchAsync([FromBody] CustomerCreditsRequest request)
        {            
            var response = await _contextSynapseCore.GetAllCreditsBySearchAsync(request);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomerCredits()
        {
            var response = await _contextSynapseCore.GetAllCustomers();
            return Ok(response);
        }

        [HttpPost]
        [Route("GetCustomerDetails")]
        public async Task<IActionResult> GetCustomerDetails([FromBody] CustomerCreditsRequest request)
        {
            var response = await _contextSynapseCore.GetCustDetails(request);
                return Ok(response);
        }

        [HttpPost]
        [Route("SaveCustomerCredits")]
        public async Task<IActionResult> SetCustomerCredits([FromBody] SaveCustomerCreditsOnRequest request)
        {
            var response = await _contextSynapseCore.SaveCustomerCredits(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ApproveCustomerCredits")]
        public async Task<IActionResult> ApproveCustomerCredits([FromBody] ApproveCustomerCreditOnRequest request)
        {
            var response = await _contextSynapseCore.ApproveCustomerCredits(request);
            return Ok(response);
        }


        [HttpPost]
        [Route("RejectCustomerCredits")]
        public async Task<IActionResult> RejectCustomerCredits([FromBody] ApproveCustomerCreditOnRequest request)
        {
            var response = await _contextSynapseCore.RejectCustomerCredits(request);
            return Ok(response);
        }

        

        //[HttpPost]
        //[Route("SaveCustomerCredits")]
        //public async Task<IActionResult> SetCustomerCredits([FromBody] ShowGridCustomerCreditsOnRequest request)
        //{
        //    var response = await _contextSynapseCore.SaveCustomerCredits(request);
        //    return Ok(response);
        //}

        //[HttpPost]
        //[Route("SetCustomerCredits")]
        //public async Task<IActionResult> SetCustomerCredits([FromBody] ShowGridCustomerCreditsOnRequest request)
        //{
        //    var response = await _contextSynapseCore.SetCustomerCredits(request);
        //    return Ok(response);
        //}

        //[HttpPost]
        //[Route("UpdateCustomerCredits")]
        //public async Task<IActionResult> UpdateCustomerCredits([FromBody] ShowGridCustomerCreditsOnRequest request)
        //{
        //    var response = await _contextSynapseCore.UpdateAvailableCredits(request);
        //    return Ok(response);
        //}
    }
}
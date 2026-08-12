using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Models.Extensions;
using Core.Models.Dtos.Requests.Synapse.SMSCSettings;
using Core.Models.Dtos.Responses.Synapse.SMSCSettings;
using Core.Models.Dtos.CommonDtos;

namespace SynapseAPI.Controllers
{
    public class SMSCMasterController : ServicesBaseController
    {

        [HttpPost]
        [Route("ShowGridSMSCMasterDetails")]
        public async Task<IActionResult> ShowGridSMSCMasterDetails([FromBody] GetSMSCINTLDetailsRequest request)
        {
            var response = await _contextSynapseCore.ShowGridSMSCMasterDetails(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetVendors")]
        public async Task<IActionResult> GetVendors([FromBody] GetINTLVendorsRequest request)
        {
            var response = await _contextSynapseCore.GetVendors(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetInstance")]
        public async Task<IActionResult> GetInstance([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetInstance(request);
            return Ok(response);
        }

        //[HttpPost]
        //[Route("GetCountryCodes")]
        //public async Task<IActionResult> GetCountryCodes([FromBody] GetCountryCodesRequest request)
        //{
        //    var response = await _contextSynapseCore.GetCountryCodes(request);
        //    return Ok(response);
        //}

        //[HttpPost]
        //[Route("GetOperators")]
        //public async Task<IActionResult> GetOperators([FromBody] GetOperatorsRequest request)
        //{
        //    var response = await _contextSynapseCore.GetOperators(request);
        //    return Ok(response);
        //}

        //[HttpPost]
        //[Route("GetActiveStages")]
        //public async Task<IActionResult> GetActiveStages([FromBody] GetRouteStagesRequest request)
        //{
        //    var response = await _contextSynapseCore.GetActiveStages(request);
        //    return Ok(response);
        //}

        [HttpPost]
        [Route("ShowSMSCDetailsForedit")]
        public async Task<IActionResult> ShowSMSCDetailsForedit([FromBody] GetConnectionsRequest request)
        {

            var response = await _contextSynapseCore.ShowSMSCDetailsForedit(request);

            return Ok(response);
        }

        [HttpPost]
        [Route("CheckerUpdateSMSCMaster")]
        public async Task<IActionResult> CheckerUpdateSMSCMaster([FromBody] CheckerUpdateUserSMSCMasterRequest request)
        {

            var response = await _contextSynapseCore.CheckerUpdateSMSCMaster(request);

            return Ok(response);
        }

        [HttpPost]
        [Route("GetIntlSMSCId")]
        public async Task<IActionResult> GetIntlSMSCId([FromBody] GetIntlSMSCIdRequest request)
        {

            var response = await _contextSynapseCore.GetIntlSMSCId(request);

            return Ok(response);
        }

        //[HttpPost]
        //[Route("InsertSMSCINTLDetails")]
        //public async Task<IActionResult> InsertSMSCINTLDetails([FromBody]SetSMSCINTLDetailsRequest request)
        //{
        //    var response = await _contextSynapseCore.InsertSMSCINTLDetails(request);
        //    return Ok(response);
        //}

        [HttpPost]
        [Route("ChangeStatusSMSCINTL")]
        public async Task<IActionResult> ChangeStatusSMSCINTL([FromBody]UpdateSMSCINTLStatusRequest request)
        {
            if (request.strSMSCIds != "")
            {
                var response = _contextSynapseCore.ChangeStatusSMSCINTL(request);
                return Ok(response);
            }
            return Ok();
        }

        //[HttpPost]
        //[Route("InsertSMSCIntlConnectionDetailsHTTP")]
        //public async Task<IActionResult> InsertSMSCIntlConnectionDetailsHTTP([FromBody]SetConnectionsHTTPRequest request)
        //{
        //    var response = await _contextSynapseCore.InsertSMSCIntlConnectionDetailsHTTP(request);
        //    return Ok(response);
        //}

        [HttpPost]
        [Route("InsertSMSCIntlConnectionDetailsSMPP")]
        public async Task<IActionResult> InsertSMSCIntlConnectionDetailsSMPP([FromBody]SetConnectionsSMPPRequest request)
        {
            var response = await _contextSynapseCore.InsertSMSCIntlConnectionDetailsSMPP(request);
            return Ok(response);
        }


        //code added  Ketha on 07082017  one extra action call to verify the connection to route dependancy check
        //check the dependancy for the connection on routes    value 3
        [HttpPost]
        [Route("CheckDependancyConectionRoute")]
        public async Task<IActionResult> CheckDependancyConectionRoute([FromBody]SetConnectionsSMPPRequest request)
        {
            var response = await _contextSynapseCore.CheckDependancyConectionRoute(request);
            return Ok(response);
        }


        [HttpPost]
        [Route("GetRateCard")]
        public async Task<IActionResult> GetRateCard([FromBody]  RateCardRequest request)
        {
            var response = await _contextSynapseCore.GetRateCard(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetRateCardHistoryDownload")]
        public async Task<IActionResult> GetRateCardHistoryDownload([FromBody] RateCardRequest request)
        {
            var response = await _contextSynapseCore.GetRateCardHistory(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetRateCardSearch")]
        public async Task<IActionResult> GetRateCardSearch([FromBody] RateCardRequest request)
        {
            var response = await _contextSynapseCore.GetRateCardSearch(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetRateCardHistory")]
        public async Task<IActionResult> GetRateCardHistory([FromBody] RateCardRequest request)
        {
            var response = await _contextSynapseCore.GetRateCardHistory(request);
            return Ok(response);
        }

        //[HttpPost]
        //[Route("Getpackagebyid")]
        //public async Task<IActionResult> Getpackagebyid([FromBody]  PackagesbyVIdReq request)
        //{
        //    var response = await _contextSynapseCore.Getpackagebyid(request);
        //    return Ok(response);
        //}



        [HttpPost]
        [Route("AddRateCardByIdAsync")]
        public async Task<IActionResult> AddRateCardByIdAsync([FromBody] InsertRateCardRequest request)
        {            
            var response = await _contextSynapseCore.AddRateCardByIdAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("CheckerRateCard")]
        public async Task<IActionResult> CheckerRateCard([FromBody] CheckerRateCardRequest request)
        {
            if (request.ID <= 0 &&
                string.IsNullOrWhiteSpace(request.REJECTNOTE))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.CheckerRateCard(request);
            return Ok(response);
        }

        //[HttpPost]
        //[Route("ShowINTLSenderDetails")]
        //public async Task<IActionResult> ShowINTLSenderDetails([FromBody] GetGBLSenderDetailsRequest request)
        //{
        //    var response = await _contextSynapseCore.ShowINTLSenderDetails(request);
        //    return Ok(response);
        //}

        //[HttpPost]
        //[Route("SHOWINTLUSERSTATDETAILS")]
        //public async Task<IActionResult> SHOWINTLUSERSTATDETAILS([FromBody] GETINTLUSERSTATDETAILSREquest request)
        //{
        //    var response = await _contextSynapseCore.SHOWINTLUSERSTATDETAILS(request);
        //    return Ok(response);
        //}

        [HttpPost]
        [Route("RatecardStatus")]
        public async Task<IActionResult> RatecardStatus([FromBody] ReUsableRequest request)
        {
            if (request.CustomerId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.RatecardStatus(request);
            return Ok(response);
        }


        [HttpPost]
        [Route("VendorStautsChange")]
        public async Task<IActionResult> VendorStautsChange([FromBody] ReUsableRequest request)
        {
            if (request.CustomerId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.VendorStautsChange(request);
            return Ok(response);
        }
    }
}
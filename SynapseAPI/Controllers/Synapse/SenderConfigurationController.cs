using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Models.Extensions;
using System.Threading.Tasks;
using Core.Models.Dtos.Requests.Synapse.SMSCSettings;
using Core.Models.Dtos.CommonDtos;

namespace SynapseAPI.Controllers
{
    public class SenderConfigurationController : ServicesBaseController
    {
        [HttpPost]
        [Route("InsertSenderConfiguration")]
        public async Task<IActionResult> InsertSenderConfiguration([FromBody] SenderConfigurationRequest request)
        {
            var response = await _contextSynapseCore.InsertSenderConfiguration(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetCardbinbyBankName")]
        public async Task<IActionResult> GetCardbinbyBankName([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetCardbinbyBankName(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetSenderConfigurationDetails")]
        public async Task<IActionResult> GetSenderConfigurationDetails([FromBody] GetSenderConfigurationDetailsRequest request)
        {
            var response = await _contextSynapseCore.GetSenderConfigurationDetails(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ShowSenderConfigurationDetailsForedit")]
        public async Task<IActionResult> ShowSenderConfigurationDetailsForedit([FromBody] GetSenderConfigurationDetailsRequest request)
        {
            var response = await _contextSynapseCore.ShowSenderConfigurationDetailsForedit(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ChangeSenderConfigurationStatus")]
        public async Task<IActionResult> ChangeSenderConfigurationStatus([FromBody] GetSenderConfigurationDetailsRequest request)
        {
            var response = await _contextSynapseCore.ChangeSenderConfigurationStatus(request);
            return Ok(response);
        }

        //[HttpPost]
        //[Route("CheckerUpdateSenderConfiguration")]
        //public async Task<IActionResult> CheckerUpdateSenderConfiguration([FromBody] ApproveRejectSenderConfigurationOnRequest request)
        //{
        //    var response = await _contextSynapseCore.CheckerUpdateSenderConfiguration(request);
        //    return Ok(response);
        //}
    }
}
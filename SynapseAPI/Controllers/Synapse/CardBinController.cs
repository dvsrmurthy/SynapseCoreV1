using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Models.Extensions;
using Core.Models.Dtos.Requests.Synapse.AlertsManager;
using Core.Models.Dtos.Responses.Synapse.AlertsManager;
using System.Threading.Tasks;

namespace SynapseAPI.Controllers
{
    public class CardBinController : ServicesBaseController
    {
        [HttpPost]
        [Route("InsertCardBinCreation")]
        public async Task<IActionResult> InsertCardBinCreation([FromBody] CardBinOnRequest request)
        {
            var response = await _contextSynapseCore.InsertCardBinCreation(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetCardBinInformationDetails")]
        public async Task<IActionResult> GetCardBinInformationDetails([FromBody] GetCardBinDetailsRequest request)
        {
            var response = await _contextSynapseCore.GetCardBinInformationDetails(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ChangeCardBinStatus")]
        public async Task<IActionResult> ChangeCardBinStatus([FromBody] GetCardBinDetailsRequest request)
        {
            var response = await _contextSynapseCore.ChangeCardBinStatus(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ShowCardBinInformationDetailsForedit")]
        public async Task<IActionResult> ShowCardBinInformationDetailsForedit([FromBody] GetCardBinDetailsRequest request)
        {
            var response = await _contextSynapseCore.ShowCardBinInformationDetailsForedit(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ApproveCardBinDetails")]
        public async Task<IActionResult> ApproveCardBinDetails([FromBody] ApproveRejectCardBinDetails request)
        {
            var response = await _contextSynapseCore.ApproveCardBinDetails(request);
            return Ok(response);
        }
    }
}
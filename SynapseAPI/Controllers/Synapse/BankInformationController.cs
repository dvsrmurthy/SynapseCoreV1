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
    public class BankInformationController : ServicesBaseController
    {
        [HttpPost]
        [Route("InsertBankCreation")]
        public async Task<IActionResult> InsertBankCreation([FromBody] BankInformationOnRequest request)
        {
            var response = await _contextSynapseCore.InsertBankCreation(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ShowBankInformationDetailsForedit")]
        public async Task<IActionResult> ShowBankInformationDetailsForedit([FromBody] GetBankInformationDetailsRequest request)
        {
            var response = await _contextSynapseCore.ShowBankInformationDetailsForedit(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetBankInformationDetails")]
        public async Task<IActionResult> GetBankInformationDetails([FromBody] GetBankInformationDetailsRequest request)
        {
            var response = await _contextSynapseCore.GetBankInformationDetails(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ChangeBankInformationStatus")]
        public async Task<IActionResult> ChangeBankInformationStatus([FromBody] GetBankInformationDetailsRequest request)
        {
            var response = await _contextSynapseCore.ChangeBankInformationStatus(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ApproveBankInformationCreation")]
        public async Task<IActionResult> ApproveBankInformationCreation([FromBody] ApproveRejectBankInformationDetailsCreation request)
        {
            var response = await _contextSynapseCore.ApproveBankInformationCreation(request);
            return Ok(response);
        }
	}
}
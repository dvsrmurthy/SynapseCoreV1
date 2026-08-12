using Core.Models.Dtos.CommonDtos;
using Core.Models.Dtos.Requests.Synapse.AlertsManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SynapseAPI.Controllers
{
    public class BusinessRulesController : ServicesBaseController
    {
        //
        // GET: /BusinessRules/
        [HttpPost]
        [Route("GetBusinessProfiles")]
        public async Task<IActionResult> GetBusinessProfiles([FromBody] GetBusinessProfilesOnRequest request)
        {
            var response = await _contextSynapseCore.GetBusinessProfiles(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("TestStatement")]
        public async Task<IActionResult> TestStatement([FromBody] TestStatementOnRequest request)
        {
            var response = await _contextSynapseCore.TestStatement(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("SaveOrUpdateBusinessRules")]
        public async Task<IActionResult> SaveOrUpdateBusinessRules([FromBody] InsertOrUpdateBusinessOnRequest request)
        {
            var response = await _contextSynapseCore.SaveOrUpdateBusinessRules(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("EMailOrSMSSettings")]
        public async Task<IActionResult> EMailOrSMSSettings(ReUsableRequest request)
        {
            var response = await _contextSynapseCore.EMailOrSMSSettings(request);
            return Ok(response);
        }




        [HttpPost]
        [Route("ShowGridBusinessRules")]
        public async Task<IActionResult> ShowGridBusinessRules([FromBody] GetBusinessRulesOnRequest request)
        {
            var response = await _contextSynapseCore.ShowGridBusinessRules(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("ChangeStatusBusinessRule")]
        public async Task<IActionResult> ChangeStatusBusinessRule([FromBody] StatusUpdatedOnRequest request)
        {
            var response = await _contextSynapseCore.ChangeStatusBusinessRule(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("ApproveOrRejectBusinessRule")]
        public async Task<IActionResult> ApproveOrRejectBusinessRule([FromBody] ApproveBusinessRuleOnRequest request)
        {
            var response = await _contextSynapseCore.ApproveOrRejectBusinessRule(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("TestStatementView")]
        public async Task<IActionResult> TestStatementView([FromBody] TestStatementOnRequest request)
        {
            var response = await _contextSynapseCore.TestStatementView(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetBankDetails")]
        public async Task<IActionResult> GetBankDetails([FromBody] GetBankInformationDetailsRequest request)
        {
            var response = await _contextSynapseCore.GetBankDetails(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("CheckAlertForScheduleOnOff")]
        public async Task<IActionResult> CheckAlertForScheduleOnOff(ReUsableRequest request)
        {
            var response = await _contextSynapseCore.CheckAlertForScheduleOnOff(request);
            return Ok(response);
        }
	}
}
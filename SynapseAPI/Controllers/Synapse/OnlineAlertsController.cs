using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Models.Extensions;
using Core.Models.Dtos.Requests.Synapse.AlertsManager;
using Core.Models.Dtos.Responses.Synapse.AlertsManager;
using Core.Models.Dtos.Requests.Synapse.SMSCSettings;
using Core.Models.Dtos.CommonDtos;

namespace SynapseAPI.Controllers
{
    public class OnlineAlertsController : ServicesBaseController
    {
        [HttpPost]
        [Route("InsertOnlineCreation")]
        public async Task<IActionResult> InsertOnlineCreation([FromBody] SetOnlineAlertsRequest request)
        {
            var response = await _contextSynapseCore.InsertOnlineCreation(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetBusinessRules")]
        public async Task<IActionResult> GetBusinessRules([FromBody] GetBusinessRulesRequest request)
        {
            var response = await _contextSynapseCore.GetBusinessRules(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetSenderDetails")]
        public async Task<IActionResult> GetSenderDetails([FromBody] GetSenderRequest request)
        {
            var response = await _contextSynapseCore.GetSenderDetails(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetTemplateDetails")]
        public async Task<IActionResult> GetTemplateDetails([FromBody] GetTemplatesRequest request)
        {
            var response = await _contextSynapseCore.GetTemplateDetails(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetTemplateDetailsByTempId")]
        public async Task<IActionResult> GetTemplateDetailsByTempId([FromBody] GetTemplatesRequest request)
        {
            var response = await _contextSynapseCore.GetTemplateDetailsByTempId(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetOnlineAlertsDetails")]
        public async Task<IActionResult> GetOnlineAlertsDetails([FromBody] DBAlertsRequest request)
        {
            var response = await _contextSynapseCore.GetOnlineAlertsDetails(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ChangeOnlineAlertsStatus")]
        public async Task<IActionResult> ChangeOnlineAlertsStatus([FromBody] ChangeOnlineAlertsStatusRequest request)
        {
            var response = await _contextSynapseCore.ChangeOnlineAlertsStatus(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ShowOnlineAlertsDetailsForedit")]
        public async Task<IActionResult> ShowOnlineAlertsDetailsForedit([FromBody] GetOnlineAlertsDetailsRequest request)
        {
            var response = await _contextSynapseCore.ShowOnlineAlertsDetailsForedit(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("CheckerUpdateOnlineAlerts")]
        public async Task<IActionResult> CheckerUpdateOnlineAlerts([FromBody] ApproveRejectAlertsCreation request)
        {
            var response = await _contextSynapseCore.CheckerUpdateOnlineAlerts(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetDBQueryOnlineAlerts")]
        public async Task<IActionResult> GetDBQueryOnlineAlerts([FromBody] GetBusinessRulesRequest request)
        {
            var response = await _contextSynapseCore.GetDBQueryOnlineAlerts(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetPreviewDetailsOnlineAlerts")]
        public async Task<IActionResult> GetPreviewDetailsOnlineAlerts([FromBody] GetBusinessRulesRequest request)
        {
            var response = await _contextSynapseCore.GetPreviewDetailsOnlineAlerts(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetEmailTemplateDetails")]
        public async Task<IActionResult> GetEmailTemplateDetails([FromBody] GetTemplatesRequest request)
        {
            var response = await _contextSynapseCore.GetEmailTemplateDetails(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetTemplateByUser")]
        public async Task<IActionResult> GetTemplateByUser([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetTemplateByUser(request);
            return Ok(response);
        }
	}
}
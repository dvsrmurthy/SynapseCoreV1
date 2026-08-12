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

namespace SynapseAPI.Controllers
{
    public class OfflineAlertsController : ServicesBaseController
    {
        [HttpPost]
        [Route("InsertOfflineCreation")]
        public async Task<IActionResult> InsertOfflineCreation([FromBody] SetOfflineAlertsRequest request)
        {
            var response = await _contextSynapseCore.InsertOfflineCreation(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetBusinessOfflineRules")]
        public async Task<IActionResult> GetBusinessOfflineRules([FromBody] GetBusinessOfflineRulesRequest request)
        {
            var response = await _contextSynapseCore.GetBusinessOfflineRules(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("CheckerUpdateOfflineAlerts")]
        public async Task<IActionResult> CheckerUpdateOfflineAlerts([FromBody] ApproveRejectOfflineAlerts request)
        {
            var response = await _contextSynapseCore.CheckerUpdateOfflineAlerts(request);
            return Ok(response);
        }

        //[HttpPost]
        //[Route("GetSenderDetails")]
        //public async Task<IActionResult> GetSenderDetails([FromBody] GetSenderRequest request)
        //{
        //    var response = await _contextSynapseCore.GetSenderDetails(request);
        //    return Ok(response);
        //}

        //[HttpPost]
        //[Route("GetTemplateDetails")]
        //public async Task<IActionResult> GetTemplateDetails([FromBody] GetTemplatesRequest request)
        //{
        //    var response = await _contextSynapseCore.GetTemplateDetails(request);
        //    return Ok(response);
        //}

        //[HttpPost]
        //[Route("GetTemplateDetailsByTempId")]
        //public async Task<IActionResult> GetTemplateDetailsByTempId([FromBody] GetTemplatesRequest request)
        //{
        //    var response = await _contextSynapseCore.GetTemplateDetailsByTempId(request);
        //    return Ok(response);
        //}

        [HttpPost]
        [Route("GetOfflineAlertsDetails")]
        public async Task<IActionResult> GetOfflineAlertsDetails([FromBody] GetOfflineAlertsRequest request)
        {
            var response = await _contextSynapseCore.GetOfflineAlertsDetails(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ChangeOfflineAlertsStatus")]
        public async Task<IActionResult> ChangeOfflineAlertsStatus([FromBody] ChangeOfflineAlertsStatusRequest request)
        {
            var response = await _contextSynapseCore.ChangeOfflineAlertsStatus(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ShowOfflineAlertsDetailsForedit")]
        public async Task<IActionResult> ShowOfflineAlertsDetailsForedit([FromBody] GetOfflineAlertsRequest request)
        {
            var response = await _contextSynapseCore.ShowOfflineAlertsDetailsForedit(request);
            return Ok(response);
        }
    }
}
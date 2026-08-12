using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Models.Extensions;
using Core.Models.Dtos.Requests.Synapse.AlertsManager;
using Core.Models.Dtos.Responses.Synapse.AlertsManager;
using Core.Models.Dtos.CommonDtos;

namespace SynapseAPI.Controllers
{
    public class TemplatesCreationController : ServicesBaseController
    {
        # region Templates
        [HttpPost]
        [Route("ShowGridTemplateDetails")]
        public async Task<IActionResult> ShowGridTemplateDetails([FromBody] GetTemplateDetailsRequest request)
        {
            var response = await _contextSynapseCore.ShowGridTemplateDetails(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ShowTemplateMapColumns")]
        public async Task<IActionResult> ShowTemplateMapColumns([FromBody] GetTemplateDetailsRequest request)
        {
            var response = await _contextSynapseCore.ShowTemplateMapColumns(request);
            return Ok(response);
        }   

        [HttpPost]
        [Route("ChangeTemplateStatus")]
        public async Task<IActionResult> ChangeTemplateStatus([FromBody] ChangeTemplateStatusRequest request)
        {
            var response = await _contextSynapseCore.ChangeTemplateStatus(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("CheckerUpdateTemplates")]
        public async Task<IActionResult> CheckerUpdateTemplates([FromBody] ApproveRejectTemplateCreation request)
        {
            var response = await _contextSynapseCore.CheckerUpdateTemplates(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("InsertTemplateCreation")]
        public async Task<IActionResult> InsertTemplateCreation([FromBody] SetTemplatesRequest request)
        {
            var response = await _contextSynapseCore.InsertTemplateCreation(request);
            return Ok(response);
        }



        [HttpPost]
        [Route("ShowTemplateDetailsForedit")]
        public async Task<IActionResult> ShowTemplateDetailsForedit([FromBody] GetTemplateDetailsRequest request)
        {
            var response = await _contextSynapseCore.ShowTemplateDetailsForedit(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("CheckTemplateAlertForScheduleOnOff")]
        public async Task<IActionResult> CheckAlertForScheduleOnOff(ReUsableRequest request)
        {
            var response = await _contextSynapseCore.CheckAlertForScheduleOnOff(request);
            return Ok(response);
        }

        # endregion

        # region UserMappings
        [HttpPost]
        [Route("ShowGridTemplateUserMapDetails")]
        public async Task<IActionResult> ShowGridTemplateUserMapDetails([FromBody] GetTemplateUserMapDetailsRequest request)
        {
            var response = await _contextSynapseCore.ShowGridTemplateUserMapDetails(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ShowGridTemplateUserMapDetailsForEdit")]
        public async Task<IActionResult> ShowGridTemplateUserMapDetailsForEdit([FromBody] GetTemplateUserMapDetailsRequest request)
        {
            var response = await _contextSynapseCore.ShowGridTemplateUserMapDetailsForEdit(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ChangeTemplateUserMapStatus")]
        public async Task<IActionResult> ChangeTemplateUserMapStatus([FromBody] ChangeTemplateUserMapStatusRequest request)
        {
            var response = await _contextSynapseCore.ChangeTemplateUserMapStatus(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("CheckerTemplateUserMap")]
        public async Task<IActionResult> CheckerTemplateUserMap([FromBody] ApproveRejectTemplateCreation request)
        {
            var response = await _contextSynapseCore.CheckerTemplateUserMap(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ShowCustomersDetails")]
        public async Task<IActionResult> ShowCustomersDetails([FromBody] GetCustomersDetailsRequest request)
        {
            var response = await _contextSynapseCore.ShowCustomersDetails(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ShowUsersDetails")]
        public async Task<IActionResult> ShowUsersDetails([FromBody] GetUsersDetailsRequest request)
        {
            var response = await _contextSynapseCore.ShowUsersDetails(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("InsertTemplateUserMapping")]
        public async Task<IActionResult> InsertTemplateUserMapping([FromBody] SetTemplateUserMappingRequest request)
        {
            var response = await _contextSynapseCore.InsertTemplateUserMapping(request);
            return Ok(response);
        }
        #endregion
    }
}
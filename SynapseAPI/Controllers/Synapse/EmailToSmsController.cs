using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Models.Dtos.CommonDtos;
using Core.Models.Dtos.Requests.Synapse.EmailToSms;


namespace SynapseAPI.Controllers
{
    public class EmailToSmsController : ServicesBaseController
    {
        [HttpPost]
        [Route("GetMailServerSettings")]
        public async Task<IActionResult> GetMailServerSettings([FromBody]MailServerSettingsRequest request)
        {
            if (request.Id == null)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetMailServerSettings(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("SaveMailServerSettings")]
        public async Task<IActionResult> SaveMailServerSettings([FromBody]MailServerSettingsRequest request)
        {
            var response = await _contextSynapseCore.SaveMailServerSettings(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("MailServerSettingsStatusChange")]
        public async Task<IActionResult> MailServerSettingsStatusChange([FromBody] ReUsableRequest request)
        {
            if (request.EmailId < 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.MailServerSettingsStatusChange(request);
            return Ok(response);
        }


        [HttpPost]
        [Route("GetEmailToSMS")]
        public async Task<IActionResult> GetEmailToSMS([FromBody]EmailToSmsRequest request)
        {
            if (request.ID == null)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetEmailToSMS(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("SaveEmailToSMS")]
        public async Task<IActionResult> SaveEmailToSMS([FromBody]EmailToSmsRequest request)
        {
            var response = await _contextSynapseCore.SaveEmailToSMS(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("EmailToSMSStatusChange")]
        public async Task<IActionResult> EmailToSMSStatusChange([FromBody] ReUsableRequest request)
        {
            if (request.EmailId < 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.EmailToSMSStatusChange(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetSendersByUsersEId")]
        public async Task<IActionResult> GetSendersByUsersEId([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetSendersByUsersEId(request);
            return Ok(response);
        }

        #region Email Template

        [HttpPost]
        [Route("GetEmailTemplate")]
        public async Task<IActionResult> GetEmailTemplate([FromBody]EmailTemplateRequest request)
        {
            if (request.Id == null)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetEmailTemplate(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("VerifyIsTemplateExistedOrNotAsync")]
        public async Task<IActionResult> VerifyIsTemplateExistedOrNotAsync([FromBody] ReUsableRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.TempName))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.VerifyIsTemplateExistedOrNotAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("SaveEmailTemplate")]
        public async Task<IActionResult> SaveEmailTemplate([FromBody]EmailTemplateRequest request)
        {
            var response = await _contextSynapseCore.SaveEmailTemplate(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("EmailTemplateStatusChange")]
        public async Task<IActionResult> EmailTemplateStatusChange([FromBody] ReUsableRequest request)
        {
            if (request.EmailId < 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.EmailTemplateStatusChange(request);
            return Ok(response);
        }
        #endregion

        # region SMStoEmail

        [HttpPost]
        [Route("GetSMSToEmail")]
        public async Task<IActionResult> GetSMSToEmail([FromBody]  GetSMSToEmailRequest request)
        {
            var response = await _contextSynapseCore.GetSMSToEmail(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetSendersByUsersEmail")]
        public async Task<IActionResult> GetSendersByUsersEmail([FromBody]GetMOSMSToEmailRequest request)
        {
            var response = await _contextSynapseCore.GetSendersByUsersEmail(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetMOCampaignbyUserId")]
        public async Task<IActionResult> GetMOCampaignbyUserId([FromBody]GetMOSMSToEmailRequest request)
        {
            var response = await _contextSynapseCore.GetMOCampaignbyUserId(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetMOTemplatebyUserId")]
        public async Task<IActionResult> GetMOTemplatebyUserId([FromBody]GetMOSMSToEmailRequest request)
        {
            var response = await _contextSynapseCore.GetMOTemplatebyUserId(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetMailByUsers")]
        public async Task<IActionResult> GetMailByUsers([FromBody]GetMOSMSToEmailRequest request)
        {
            var response = await _contextSynapseCore.GetMailByUsers(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("SaveSMStoEmail")]
        public async Task<IActionResult> SaveSMStoEmail([FromBody]SaveSMSToEmailRequest request)
        {
            var response = await _contextSynapseCore.SaveSMStoEmail(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ChangeStatusSE")]
        public async Task<IActionResult> ChangeStatusSE([FromBody] ReUsableRequest request)
        {

            if (request.UserId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.ChangeStatusSE(request);
            return Ok(response);
        }

        #endregion

        [HttpPost]
        [Route("GetUsersBySMTPCustomerAsync")]
        public async Task<IActionResult> GetUsersBySMTPCustomerAsync([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetUsersBySMTPCustomerAsync(request);
            return Ok(response);
        }
    }
}

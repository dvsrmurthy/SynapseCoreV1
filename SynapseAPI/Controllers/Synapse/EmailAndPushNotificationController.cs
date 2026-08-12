using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Models.Dtos.CommonDtos;
using Core.Models.Dtos.Requests.Synapse.EmailAndPushNotifications;

namespace SynapseAPI.Controllers
{
    public class EmailAndPushNotificationController : ServicesBaseController
    {
        #region Email

        #region Registered Email

        [HttpPost]
        [Route("GetRegisteredEmails")]
        public async Task<IActionResult> GetRegisteredEmails([FromBody]ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetRegisteredEmails(request);
            return Ok(response);

        }

        [HttpPost]
        [Route("SaveOrUpdateRegisteredFromMail")]
        public async Task<IActionResult> SaveOrUpdateRegisteredFromMail([FromBody]RegisterEmailRequest request)
        {
            var response = await _contextSynapseCore.SaveOrUpdateRegisteredFromMail(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ValidateFromMail")]
        public async Task<IActionResult> ValidateFromMail([FromBody]RegisterEmailRequest request)
        {
            var response = await _contextSynapseCore.ValidateFromMail(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ChangeStatusRegEmail")]
        public async Task<IActionResult> ChangeStatusRegEmail([FromBody] RegisterEmailRequest request)
        {
            var response = await _contextSynapseCore.ChangeStatusRegEmail(request);
            return Ok(response);
        }

        #endregion

        #region Map Registered Email

        [HttpPost]
        [Route("GetMapRegisteredEmails")]
        public async Task<IActionResult> GetMapRegisteredEmails([FromBody]ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetMapRegisteredEmails(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("SaveOrUpdateMapRegisterMail")]
        public async Task<IActionResult> SaveOrUpdateMapRegisterMail([FromBody]MapRegisterEmailRequest request)
        {
            var response = await _contextSynapseCore.SaveOrUpdateMapRegisterMail(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ChangeStatusMapEmail")]
        public async Task<IActionResult> ChangeStatusMapEmail([FromBody] MapRegisterEmailRequest request)
        {
            var response = await _contextSynapseCore.ChangeStatusMapEmail(request);
            return Ok(response);
        }

        #endregion

        #region Email Campaign

        [HttpPost]
        [Route("SaveOrUpdateEmailCampaign")]
        public async Task<IActionResult> SaveOrUpdateEmailCampaign([FromBody]EmailCampaignPDb request)
        {
            var response = await _contextSynapseCore.SaveOrUpdateEmailCampaign(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetEmailCampaigns")]
        public async Task<IActionResult> GetEmailCampaigns([FromBody]EmailCampaignRequest request)
        {
            var response = await _contextSynapseCore.GetEmailCampaigns(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("SetPNCampActivities")]
        public async Task<IActionResult> SetPNCampActivities(EmailCampaignPDb request)
        {
            if (request.Id != 0)
            {
                var response = await _contextSynapseCore.SetPNCampActivities(request);
                return Ok(response);
            }
            return Ok();
        }

        #endregion

        #region EmailAnalysis

        [HttpPost]
        [Route("GetEmailAnalysis")]
        public async Task<IActionResult> GetEmailAnalysis([FromBody]EmailAnalysisRequest request)
        {
            var response = await _contextSynapseCore.GetEmailAnalysis(request);
            return Ok(response);

        }

        [HttpPost]
        [Route("GetEmailAnalysisSummary")]
        public async Task<IActionResult> GetEmailAnalysisSummary([FromBody]EmailAnalysisRequest request)
        {
            var response = await _contextSynapseCore.GetEmailAnalysisSummary(request);
            return Ok(response);

        }

        [HttpPost]
        [Route("GetEmailAnalysisDetailed")]
        public async Task<IActionResult> GetEmailAnalysisDetailed([FromBody]EmailAnalysisRequest request)
        {
            var response = await _contextSynapseCore.GetEmailAnalysisDetailed(request);
            return Ok(response);

        }


        #endregion

        #endregion

        #region Push Notifications

        #region Register PushNotification

        [HttpPost]
        [Route("GetRegisterPushNotificationCollection")]
        public async Task<IActionResult> GetRegisterPushNotificationCollection([FromBody]RegisterPushNotificationRequest request)
        {
            var response = await _contextSynapseCore.GetRegisterPushNotificationCollection(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("SaveOrUpdateRegisterPushNotifications")]
        public async Task<IActionResult> SaveOrUpdateRegisterPushNotifications([FromBody]RegisterPushNotificationRequest request)
        {
            var response = await _contextSynapseCore.SaveOrUpdateRegisterPushNotifications(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ChangeStatusAppReg")]
        public async Task<IActionResult> ChangeStatusAppReg([FromBody] RegisterPushNotificationRequest request)
        {
            var response = await _contextSynapseCore.ChangeStatusAppReg(request);
            return Ok(response);
        }

        #endregion

        #region PushNotificationAnalysis

        [HttpPost]
        [Route("GetPNAnalysis")]
        public async Task<IActionResult> GetPNAnalysis([FromBody]PNAnalysisRequest request)
        {
            var response = await _contextSynapseCore.GetPNAnalysis(request);
            return Ok(response);

        }

        [HttpPost]
        [Route("GetPNAnalysisSummary")]
        public async Task<IActionResult> GetPNAnalysisSummary([FromBody]PNAnalysisRequest request)
        {
            var response = await _contextSynapseCore.GetPNAnalysisSummary(request);
            return Ok(response);

        }

        [HttpPost]
        [Route("GetPNAnalysisDetailed")]
        public async Task<IActionResult> GetPNAnalysisDetailed([FromBody]PNAnalysisRequest request)
        {
            var response = await _contextSynapseCore.GetPNAnalysisDetailed(request);
            return Ok(response);

        }


        #endregion

        #region Push Notification Campaign

        //[HttpPost]
        //[Route("GetSecretKeyAndLabels")]
        //public async Task<IActionResult> GetSecretKeyAndLabels([FromBody]ReUsableRequest request)
        //{
        //    var response = await _contextSynapseCore.GetSecretKeyAndLabels(request);
        //    return Ok(response);
        //}

        #endregion

        #endregion
    }
}

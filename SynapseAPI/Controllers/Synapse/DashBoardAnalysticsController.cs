using Core.Models.Dtos.Requests.Synapse.Analytics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Models.Dtos.CommonDtos;

namespace SynapseAPI.Controllers
{
    public class DashBoardAnalysticsController : ServicesBaseController
    {
        [HttpPost]
        [Route("GetSMSMOAnalyticsAsync")]
        public async Task<IActionResult> GetSMSMOAnalyticsAsync([FromBody] SMSMOAnalyticsRequest request)
        {
            var response = await _contextSynapseCore.GetSMSMOAnalyticsAsync(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetDashBoardAnalyticsAsync")]
        public async Task<IActionResult> GetDashBoardAnalyticsAsync([FromBody] DashBoardAnalyticsRequest request)
        {
            var response = await _contextSynapseCore.GetDashBoardAnalyticsAsync(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetDashBoardModulesAsync")]
        public async Task<IActionResult> GetDashBoardModulesAsync([FromBody] DashBoardAnalyticsRequest request)
        {
            var response = await _contextSynapseCore.GetDashBoardModulesAsync(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetDashBoardSucessRatioAsync")]
        public async Task<IActionResult> GetDashBoardSucessRatioAsync([FromBody] DashBoardAnalyticsRequest request)
        {
            var response = await _contextSynapseCore.GetDashBoardSucessRatioAsync(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetDashBoardPullSmsesAsync")]
        public async Task<IActionResult> GetDashBoardPullSmsesAsync([FromBody] DashBoardAnalyticsRequest request)
        {
            var response = await _contextSynapseCore.GetDashBoardPullSmsesAsync(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetDashBoardThroughPutAsync")]
        public async Task<IActionResult> GetDashBoardThroughPutAsync([FromBody] DashBoardAnalyticsRequest request)
        {
            var response = await _contextSynapseCore.GetDashBoardThroughPutAsync(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetDashBoardCampaignActivitiesAsync")]
        public async Task<IActionResult> GetDashBoardCampaignActivitiesAsync([FromBody] DashBoardAnalyticsRequest request)
        {
            var response = await _contextSynapseCore.GetDashBoardCampaignActivitiesAsync(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetDashBoardSMSCAsync")]
        public async Task<IActionResult> GetDashBoardSMSCAsync([FromBody] DashBoardAnalyticsRequest request)
        {
            var response = await _contextSynapseCore.GetDashBoardSMSCAsync(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetDashBoardWorldMapAsync")]
        public async Task<IActionResult> GetDashBoardWorldMapAsync([FromBody] DashBoardAnalyticsRequest request)
        {
            var response = await _contextSynapseCore.GetDashBoardWorldMapAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("UpdateSmsAnalyticsAsync")]
        public async Task<IActionResult> UpdateSmsAnalyticsAsync([FromBody] ReUsableRequest request)
        {
            if (request.SmsId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.UpdateSmsForAnalytics(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetShortcodeByUser")]
        public async Task<IActionResult> GetShortcodeByUser([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetShortcodeByUserid(request);
            return Ok(response);
        }
    }
}
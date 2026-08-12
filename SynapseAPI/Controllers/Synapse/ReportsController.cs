using Core.Models.Dtos.CommonDtos;
using Core.Models.Dtos.Requests.Synapse.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SynapseAPI.Controllers
{
    public class ReportsController : ServicesBaseController
    {
        [HttpPost]
        [Route("GetSmstrackingAsync")]
        public async Task<IActionResult> GetSmstrackingAsync([FromBody] SmsTrackingRequest request)
        {
            if
                (
                request.FromDate == null &&
                request.ToDate == null &&
                request.userId == null &&
                request.Senderid == null &&
                request.CustomerId == null &&
                request.mobileno == null &&
                request.messageid == null &&
                 request.country != null &&
                 request.Operator == null &&
                 request.status == null &&
                request.Return < 0
                )
            {
                return NotFound();
            }
            var responce = await _contextSynapseCore.GetSmstrackingAsync(request);
            return Ok(responce);
        }

        [HttpPost]
        [Route("GetSMSQueryAsync")]
        public async Task<IActionResult> GetSMSQueryAsync([FromBody] SmsQueryRequest request)
        {
            if
                (
                request.FromDate == null &&
                request.ToDate == null &&
                request.userId == null &&
                request.Senderid == null &&
                request.CustomerId == null &&
                request.mobileno == null &&
                request.messageid == null &&
                 request.country != null &&
                 request.Operator == null &&
                 request.status == null &&
                request.Return < 0
                )
            {
                return NotFound();
            }
            var responce = await _contextSynapseCore.GetSMSQueryAsync(request);
            return Ok(responce);
        }
        [HttpPost]
        [Route("GetSMSQuerySummary")]
        public async Task<IActionResult> GetSMSQuerySummary([FromBody] SmsQueryRequest request)
        {
            if
                (
                request.FromDate == null &&
                request.ToDate == null &&
                request.userId == null &&
                request.Senderid == null &&
                request.CustomerId == null &&
                request.mobileno == null &&
                request.messageid == null &&
                 request.country != null &&
                 request.Operator == null &&
                 request.status == null &&
                request.Return < 0
                )
            {
                return NotFound();
            }
            var responce = await _contextSynapseCore.GetSMSQuerySummary(request);
            return Ok(responce);
        }
        [HttpPost]
        [Route("GetSMSQueryDetailed")]
        public async Task<IActionResult> GetSMSQueryDetailed([FromBody] SmsQueryRequest request)
        {
            if
                (
                request.FromDate == null &&
                request.ToDate == null &&
                request.userId == null &&
                request.Senderid == null &&
                request.CustomerId == null &&
                request.mobileno == null &&
                request.messageid == null &&
                 request.country != null &&
                 request.Operator == null &&
                 request.status == null &&
                request.Return < 0
                )
            {
                return NotFound();
            }
            var responce = await _contextSynapseCore.GetSMSQueryDetailed(request);
            return Ok(responce);
        }
        [HttpPost]
        [Route("GetObjectData")]
        public async Task<IActionResult> GetObjectData([FromBody] SmsQueryRequest request)
        {
            if
                (
                request.FromDate == null &&
                request.ToDate == null &&
                request.userId == null &&
                request.Senderid == null &&
                request.CustomerId == null &&
                request.mobileno == null &&
                request.messageid == null &&
                 request.country != null &&
                 request.Operator == null &&
                 request.status == null &&
                request.Return < 0
                )
            {
                return NotFound();
            }
            var responce = await _contextSynapseCore.GetObjectData(request);
            return Ok(responce);
        }
        [HttpPost]
        [Route("GetQuickSMSQueryAsync")]
        public async Task<IActionResult> GetQuickSMSQueryAsync([FromBody] SmsQueryRequest request)
        {
            if
                (
                request.userId == null &&
                request.Senderid == null &&
                request.CustomerId == null &&
                request.mobileno == null &&
                request.messageid == null &&
                request.Return < 0
                )
            {
                return NotFound();
            }
            var responce = await _contextSynapseCore.GetQuickSMSQueryAsync(request);
            return Ok(responce);
        }


        [HttpPost]
        [Route("GetAdminReportViewDetailsAsync")]
        public async Task<IActionResult> GetAdminReportViewDetailsAsync([FromBody] SmsTrafficRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.AccountManagerId))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetSmsTrafficOfMonthAsync(request);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetAllActiveAccountManagersAsync")]
        public async Task<IActionResult> GetAllActiveAccountManagersAsync()
        {
            var response = await _contextSynapseCore.GetAllActiveAccountManagersAsync();
            return Ok(response);
        }

        //customer view
        [HttpPost]
        [Route("GetCustomerReportViewDetailsAsync")]
        public async Task<IActionResult> GetCustomerReportViewDetailsAsync([FromBody] CustomerViewrequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CustomerId))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetCustomerSmsTrafficOfMonthAsync(request);
            return Ok(response);
        }


        //AccountManager

        [HttpPost]
        [Route("GetAccountManagerTrafficOfMonth")]
        public async Task<IActionResult> GetAccountManagerTrafficOfMonth([FromBody] SmsTrafficRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CustomerId))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetAccountManagerTrafficOfMonthAsync(new AccountManagerrequest { CustomerId = request.CustomerId, Country = request.Country, FromDate = request.FromDate, UserId=request.Userid, ToDate = request.ToDate, Operator = request.Operator, SenderId = request.SenderId });//GetSmsTrafficOfMonthAsync(request);
            return Ok(response);
        }

        //vendor
        [HttpPost]
        [Route("GetVendorSmsTrafficOfMonth")]
        public async Task<IActionResult> GetVendorSmsTrafficOfMonth([FromBody] Vendorrequest request)
        {
            if (string.IsNullOrWhiteSpace(request.VendorId))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetVendorSmsTrafficOfMonthAsync(request);
            return Ok(response);
        }


        [HttpPost]
        [Route("GetSubCustomersByCustomerIdAsync")]
        public async Task<IActionResult> GetSubCustomersByCustomerIdAsync([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetSubCustomersByCustomerIdAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetUserByCustomerIdAsync")]
        public async Task<IActionResult> GetUserByCustomerIdAsync([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetUsersByCustomerIdAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetUsersByCustomerIdAsyncRep")]
        public async Task<IActionResult> GetUsersByCustomerIdAsyncRep([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetUsersByCustomerIdAsyncRep(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetSenderbyUserId")]
        public async Task<IActionResult> GetSenderbyUserId([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetSenderbyUserId(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetSenderbyUserIds")]
        public async Task<IActionResult> GetSenderbyUserIds([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetSenderbyUserIds(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetCountrybyUserId")]
        public async Task<IActionResult> GetCountrybyUserId([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetCountrybyUserId(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetOperatorbyUserId")]
        public async Task<IActionResult> GetOperatorbyUserId([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetOperatorbyUserId(request);
            return Ok(response);
        }


        [HttpPost]
        [Route("GetOperatorbyCountry")]
        public async Task<IActionResult> GetOperatorbyCountry([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetOperatorbyCountry(request);
            return Ok(response);
        }


        [HttpPost]
        [Route("GetVendorbyUserId")]
        public async Task<IActionResult> GetVendorbyUserId([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetVendorbyUserId(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetUsersByVendorIdAsync")]
        public async Task<IActionResult> GetUsersByVendorIdAsync([FromBody] ReUsableRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.VendorIds))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetUsersByVendorIdAsync(request);
            return Ok(response);
        }



        //finance

        [HttpPost]
        [Route("GetFinanceReportViewDetailsAsync")]
        public async Task<IActionResult> GetFinanceReportViewDetailsAsync([FromBody] FinanceViewRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FromDate))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetFinanceViewAsync(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetHlrReportViewDetailsAsync")]
        public async Task<IActionResult> GetHlrReportViewDetailsAsync([FromBody] HlrRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.strFromDate))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetHlrReportViewDetailsAsync(request);
            return Ok(response);
        }
        //vendorfinanceview

        [HttpPost]
        [Route("GetFinanceVendorViewDetailsAsync")]
        public async Task<IActionResult> GetFinanceVendorViewDetailsAsync([FromBody] VendorFinanceviewrequest request)
        {
            if (string.IsNullOrWhiteSpace(request.vendor))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetVendorFinanceViewAsync(request);
            return Ok(response);
        }

        //customerfinance
        [HttpPost]
        [Route("GetFinanceCustomerReportViewDetailsAsync")]
        public async Task<IActionResult> GetFinanceCustomerReportViewDetailsAsync([FromBody] CustomerFinanceviewrequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CustomerId))
            {
               // return BadRequest();
                request.CustomerId = "1";
            }
            var response = await _contextSynapseCore.GetCustomerFinanceViewAsync(request);
            return Ok(response);
        }


        [HttpPost]
        [Route("GetCustomersByAccountMgrIdAsync")]
        public async Task<IActionResult> GetCustomersByAccountMgrIdAsync([FromBody] ReUsableRequest request)
        {
            if (request.AccountManagerId <= 0)
            {
                // return BadRequest();
                request.CustomerId = 1;
            }
            var response = await _contextAccountCore.GetCustomersByAccountManagerId(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetCampaignByDay")]
        public async Task<IActionResult> GetCampaignByDay([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetCampaignByDay(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetCampaignDetailedAsync")]
        public async Task<IActionResult> GetCampaignDetailedAsync([FromBody] CampaignDetailedRequest request)
        {
            if(request.CampId == null)
            {
                return NotFound();
            }
            var responce = await _contextSynapseCore.GetCampaignDetailedAsync(request);
            return Ok(responce);
        }




        [HttpPost]
        [Route("GetCampaignSummaryAsync")]
        public async Task<IActionResult> GetCampaignSummaryAsync([FromBody] CampaignSummaryRequest request)
        {
            if (request.CampId == null)
            {
                return NotFound();
            }
            var responce = await _contextSynapseCore.GetCampaignSummaryAsync(request);
            return Ok(responce);
        }

        [HttpPost]
        [Route("GetCampaignSecondSummaryAsync")]
        public async Task<IActionResult> GetCampaignSecondSummaryAsync([FromBody] CampaignSummaryRequest request)
        {
            if (request.CampId == null)
            {
                return NotFound();
            }
            var responce = await _contextSynapseCore.GetCampaignSecondSummaryAsync(request);
            return Ok(responce);
        }

        [HttpPost]
        [Route("GetMISReportAsync")]
        public async Task<IActionResult> GetMISReportAsync([FromBody] MISReportRequest request)
        {
            if (request.Customer == null)
            {
                return NotFound();
            }
            var responce = await _contextSynapseCore.GetMISReportAsync(request);
            return Ok(responce);
        }

        [HttpPost]
        [Route("GetMoDetailedAsync")]
        public async Task<IActionResult> GetMoDetailedAsync([FromBody] MoDetailedRequest request)
        {
            if (request.UserId  == null)
            {
                return NotFound();
            }
            var responce = await _contextSynapseCore.GetMoDetailedAsync(request);
            return Ok(responce);
        }

        [HttpPost]
        [Route("GetMoSummaryAsync")]
        public async Task<IActionResult> GetMoSummaryAsync([FromBody] MoSummaryRequest request)
        {
            if (request.UserId == null)
            {
                return NotFound();
            }
            var responce = await _contextSynapseCore.GetMoSummaryAsync(request);
            return Ok(responce);
        }

        [HttpPost]
        [Route("GetMoDetailedRCAsync")]
        public async Task<IActionResult> GetMoDetailedRCAsync([FromBody] MoDetailedRequest request)
        {
            if (request.UserId == null)
            {
                return NotFound();
            }
            var responce = await _contextSynapseCore.GetMoDetailedRCAsync(request);
            return Ok(responce);
        }
        [HttpPost]
        [Route("GetKeywordsByUser")]
        public async Task<IActionResult> GetKeywordsByUser([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetKeywordsByUser(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetShortcodesByUser")]
        public async Task<IActionResult> GetShortcodesByUser([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetShortcodesByUser(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetSenderWiseAsync")]
        public async Task<IActionResult> GetSenderWiseAsync([FromBody] SenderWiseRequest request)
        {
            if (request.Customer == null)
            {
                return NotFound();
            }
            var responce = await _contextSynapseCore.GetSenderWiseAsync(request);
            return Ok(responce);
        }


        [HttpPost]
        [Route("GetMoSurveyAsync")]
        public async Task<IActionResult> GetMoSurveyAsync([FromBody] MOSuyveyRequest request)
        {
            if (request.SurveyId == null)
            {
                return NotFound();
            }
            var responce = await _contextSynapseCore.GetMoSurveyAsync(request);
            return Ok(responce);
        }

        [HttpPost]
        [Route("GetSurveyByDay")]
        public async Task<IActionResult> GetSurveyByDay([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetSurveyByDay(request);
            return Ok(response);
        }

        //BusinessRuleReport
        [HttpPost]
        [Route("GetBusinessRuleReportsAsync")]
        public async Task<IActionResult> GetBusinessRuleReportsAsync([FromBody] BusinessRuleReportRequest request)
        {

            var responce = await _contextSynapseCore.GetBusinessRuleReportsAsync(request);
            return Ok(responce);
        }

        [HttpPost]
        [Route("GetBusinessRuleReportSecondResultSetAsync")]
        public async Task<IActionResult> GetBusinessRuleReportSecondResultSetAsync([FromBody] BusinessRuleReportRequest request)
        {

            var responce = await _contextSynapseCore.GetBusinessRuleReportSecondResultSetAsync(request);
            return Ok(responce);
        }

        [HttpPost]
        [Route("GetUsersByCustomerIdString")]
        public async Task<IActionResult> GetUsersByCustomerIdAsyncString([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetUsersByCustomerIdAsyncString(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetDownloadDlrWiseAsync")]
        public async Task<IActionResult> GetDownloadDlrWiseAsync([FromBody] DownloadDlrRequest request)
        {
            if (request.UserName == null)
            {
                return NotFound();
            }
            var responce = await _contextSynapseCore.GetDownloadDlrWiseAsync(request);
            return Ok(responce);
        }

        [HttpPost]
        [Route("GetExternalCampaignAsync")]
        public async Task<IActionResult> GetExternalCampaignAsync([FromBody] ExternalCampaignRequest request)
        {
            var responce = await _contextSynapseCore.GetExternalCampaignAsync(request);
            return Ok(responce);
        }
    }
}
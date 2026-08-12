using Core.Models.Dtos.Requests.Synapse.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Models.Dtos.CommonDtos;
using Core.Models.Dtos.Responses.Synapse.SecurityManagement;

namespace SynapseAPI.Controllers
{
    public class UserManagementController : ServicesBaseController
    {
        [HttpPost]
        [Route("GetAllUsersByIdAsync")]
        public async Task<IActionResult> GetAllUsersByIdAsync([FromBody] GetUsersRequest request)
        {
            if (request.NUSERID <= 0 && request.NSTATUS <= 0 && request.NCUSTID <= 0 && request.NCREATEDBY <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetAllUsersByIdAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetUserByIdAsync")]
        public async Task<IActionResult> GetUserByIdAsync([FromBody] GetUsersRequest request)
        {
            if (request.NUSERID <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetUserByIdAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("UpdateUserByUserId")]
        public async Task<IActionResult> UpdateUserByUserId([FromBody] UpdateUserRequest request)
        {
            if (
                request.FirstName == null && 
                request.MiddleName == null && 
                request.LastName == null && 
                request.UserName == null &&
                request.Password == null &&
                request.SessionsCount <= 0 &&
                request.MobileNo == null &&
                request.Mail == null &&
                request.CustomerId <= 0 &&
                request.divisionid <= 0 &&
                request.RoleId <= 0 &&
                request.Createdby <= 0              
                
                
                )
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.UpdateUserByUserId(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ChangeStatusUC")]
        public async Task<IActionResult> ChangeStatusUC([FromBody] ReUsableRequest request)
        {

            if (request.UserId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.ChangeStatusUC(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ApproveUserAsync")]
        public async Task<IActionResult> ApproveUserAsync([FromBody] ApproveUserRequest request)
        {
            if (request.UserId <= 0 && request.CurrentStatus <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.ApproveUserAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("RejectUserAsync")]
        public async Task<IActionResult> RejectUserAsync([FromBody] ApproveUserRequest request)
        {
            if (request.UserId <= 0 && request.CurrentStatus <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.RejectUserAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ValidateIsUserExistedOrNotAsync")]
        public async Task<IActionResult> ValidateIsUserExistedOrNotAsync([FromBody] ReUsableRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.ValidateIsUserExistedOrNot(request);
            return Ok(response);
        }
        
        #region Account Manager

        [HttpPost]
        [Route("GetAccountManagersAsync")]
        public async Task<IActionResult> GetAccountManagersAsync([FromBody] ReUsableRequest request)
        {
            if (request.AccountManagerId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetAccountManagersAsync();
            return Ok(response);
        }

        [HttpPost]
        [Route("CreateAccountManagersAsync")]
        public async Task<IActionResult> CreateAccountManagersAsync([FromBody] AccountManagersResponse request)
        {
            if (string.IsNullOrWhiteSpace(request.FirstName) && string.IsNullOrWhiteSpace(request.LastName))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.CreateOrUpdateAccountManagerAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ApproveOrRejectAccountManagerAsync")]
        public async Task<IActionResult> ApproveOrRejectAccountManagerAsync([FromBody] ReUsableRequest request)
        {
            if (request.AccountManagerId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.ApproveOrRejectAccountManager(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("AccountStatusChange")]
        public async Task<IActionResult> AccountStatusChange([FromBody] ReUsableRequest request)
        {
            if (request.CustomerId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.AccountStatusChange(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetUsersByCustomerIdAsync")]
        public async Task<IActionResult> GetUsersByCustomerIdAsync([FromBody] ReUsableRequest request)
        {
            var response = await _contextSynapseCore.GetUsersByCustomerIdAsync(request);
            return Ok(response);
        }
        #endregion
    }
}

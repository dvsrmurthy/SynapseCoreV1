using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Models.Dtos.CommonDtos;
using Core.Models.Dtos.Requests.Synapse.SecurityManagement;

namespace SynapseAPI.Controllers
{
    public class RoleCreationController : ServicesBaseController
    {
        [HttpPost]
        [Route ("GetAllRolesByIdAsync")]
        public async Task <IActionResult> GetAllRolesByIdAsync([FromBody]RolesCreationRequest request)
        {
            //if (request.nroleid <= 0)
            //{
            //    return NotFound();
            //}
            var response = await _contextSynapseCore.GetAllRolesByIdAsync(request);
            return Ok(response);
        }


        [HttpPost]
        [Route("UpdateRolesByUserId")]
        public async Task<IActionResult> UpdateRolesByUserId([FromBody] EditRolesCreation request)
        {
            if (

                request.Name== null &&
                request.Description == null &&
                request.CreatedBy < 1
               
                )
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.UpdateRolesByUserId(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetAllFeaturesAsync")]
        public async Task<IActionResult> GetAllFeaturesAsync([FromBody] ReUsableRequest request)
        {
            if (request.RoleId < 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetAllPrivilagesAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("RoleActiveStatuChangeAsync")]
        public async Task<IActionResult> RoleActiveStatuChangeAsync([FromBody] ReUsableRequest request)
        {
            if (request.RoleId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.RoleActiveStatusChange(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("DepartmentActiveStatuChangeAsync")]
        public async Task<IActionResult> DepartmentActiveStatuChangeAsync([FromBody] ReUsableRequest request)
        {
            if (request.DepId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.DepartmentActiveStatusChange(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetAllprivilagesByCustomerAsync")]
        public async Task<IActionResult> GetAllprivilagesByCustomerAsync([FromBody] ReUsableRequest request)
        {
            if (request.CustomerId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetAllprivilagesByCustomerAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("SetFeaturesByRoleId")]
        public async Task<IActionResult> SetFeaturesByRoleId([FromBody] RolePrivilageRequest request)
        {
            if (request.PrivilageIds.Length == 0 && request.RoleId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.SetRolePrivilagesByRoleId(request);
            return Ok(response);
        }


        [HttpPost]
        [Route("CheckerRole")]
        public async Task<IActionResult> CheckerRole([FromBody] EditRolesCreation request)
        {
            if (request.RoleId <= 0)
            {
                return BadRequest();
            }
            if (request.command.Equals("Reject", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(request.RejectNote))
                {
                    return BadRequest();
                }
            }
            var response = await _contextSynapseCore.CheckerRole(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetFeatuePrivilagesSetup")]
        public async Task<IActionResult> GetFeatuePrivilagesSetup([FromBody] ReUsableRequest request)
        {
            if (request.CustomerId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.buildFeaturePrivilagesSetup(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("SetFeatureprivilagesSetypAysnc")]
        public async Task<IActionResult> SetFeatureprivilagesSetyp([FromBody] ReUsableRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FeatureIds) && request.CustomerId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.SetFeatureprivilagesSetyp(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ActiveOrInactiveFeatureprivilagesSetypAysnc")]
        public async Task<IActionResult> ActiveOrInactiveFeatureprivilagesSetypAysnc([FromBody] ReUsableRequest request)
        {
            if (request.SubFeatureId <= 0 && request.CustomerId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.ActiveOrInActiveFeature(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("VerifyIsRoleExistedOrNotAsync")]
        public async Task<IActionResult> VerifyIsRoleExistedOrNotAsync([FromBody] ReUsableRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.RoleName))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.ValidateIsRoleExistedOrNot(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetAllDepartmentsAsync")]
        public async Task<IActionResult> GetAllDepartmentsAsync([FromBody] ReUsableRequest request)
        {            
            var response = await _contextSynapseCore.GetAllDepartments(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("SaveOrUpdateDepartmentAsync")]
        public async Task<IActionResult> SaveOrUpdateDepartmentAsync([FromBody] DepartemntsRequest request)
        {
            var response = await _contextSynapseCore.SaveOrUpdateDepartment(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("IsDepartmentExistedByNameAsync")]
        public async Task<IActionResult> IsDepartmentExistedByNameAsync([FromBody] ReUsableRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.DeptName))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.IsDepartmentExistedByName(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ApproveOrRejectDepotAsync")]
        public async Task<IActionResult> ApproveOrRejectDepotAsync([FromBody] ReUsableRequest request)
        {
            if (request.DepId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.ApproveOrReject(request);
            return Ok(response);
        }
    }
}

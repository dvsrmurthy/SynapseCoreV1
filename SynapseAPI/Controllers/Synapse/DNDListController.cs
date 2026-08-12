using Core.Models.Dtos.Requests.Synapse.AdminOperation;
using Core.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Models.Dtos.CommonDtos;
using Microsoft.AspNetCore.Mvc;

namespace SynapseAPI.Controllers
{
    public class DNDListController : ServicesBaseController
    {
        [HttpPost]
        [Route("GetAllDNDListAsync")]
        public async Task<IActionResult>GetAllDNDListAsync([FromBody] DNDListRequest request)
        {
            if (
                request.nCustomerId <= 0 &&
                request.nUserId <= 0 &&
                request.nDNDId <= 0 &&
                request.strMobileNo == null &&
                request.strName == null &&
                request.nStatus == null &&
                request.bSearch <= 0
                )
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetAllDNDListAsync(request);
            return Ok(response);
        }


        [HttpPost]
        [Route("InsertDNDList")]
        public async Task<IActionResult> InsertDNDList([FromBody] InsertDNDList request)
        {
            if (
                
                request.strMobileNo == null &&
                request.strName == null &&
                request.nUserId <= 0 &&
                request.nCustomerId <= 0 &&
                request.CreatedBy <=0 &&
                request.Custname<=0
                )
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.InsertDNDList(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ImportDNDS")]
        public async Task<IActionResult> ImportDNDS(ImportDNDRequest request)
        {
            Logger.Info("ServiceExecutionStart");
            if (string.IsNullOrWhiteSpace(request.FILEPATH) && request.Id <=0 && request.Createdby == 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.ImportDNDS(request);
            Logger.Info("ServiceExecutionEnd");
            return Ok(response);
        }


        [HttpPost]
        [Route("ExportDNDS")]
        public async Task<IActionResult> ExportDNDS(ExportDNDReq request)
        {
            try
            {
                if (request.CreatedBy != 0)
                {
                    var response = await _contextSynapseCore.ExportDNDS(request);
                    return new JsonResult(response);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return Ok();
        }

        [HttpPost]
        [Route("FileDBCheckDND")]
        public async Task<IActionResult> FileDBCheckDND(StatusUpdateDNDList request)
        {

            if (!string.IsNullOrWhiteSpace(request.MobileNo))
            {
                var response = await _contextSynapseCore.FileDBCheckDND(request);
                return Ok(response);
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("ChangeStatusDND")]
        public async Task<IActionResult> ChangeStatusDND([FromBody] ReUsableRequest request)
        {

            if (request.UserId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.ChangeStatusDND(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("CheckerDNDList")]
        public async Task<IActionResult> CheckerDNDList([FromBody] StatusUpdateDNDList request)
        {
            if (request.id != "")
            {
                var response = await _contextSynapseCore.CheckerDNDList(request);
                return Ok(response);
            }
            return Ok();
        }

        [HttpPost]
        [Route("GetAllWhiteListAsync")]
        public async Task<IActionResult> GetAllWhiteListAsync([FromBody] WhitelistRequest request)
        {
            //if (request.strMobileNo == null && request.nStatus == null)
            //{
            //    return BadRequest();
            //}
            var response = await _contextSynapseCore.GetAllWhiteListAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("WhitelistStatus")]
        public async Task<IActionResult> WhitelistStatus([FromBody] ReUsableRequest request)
        {
            if (request.UserId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.WhitelistStatus(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("InsertWhitelist")]
        public async Task<IActionResult> InsertWhitelist([FromBody] InsertWhitelist request)
        {
            if (request.strMobileNo == null && request.CreatedBy <=0 )
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.InsertWhitelist(request);
            return Ok(response);
        }

        //[HttpPost]
        //[Route("ExportWhitelist")]
        //public async Task<IActionResult> ExportWhitelist(ExportWhitelistRequest request)
        //{
        //    try
        //    {
        //        if (request.CreatedBy != 0)
        //        {
        //            var response = await _contextSynapseCore.ExportWhitelist(request);
        //            return Json(response);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //    }
        //    return Ok();
        //}

        [HttpPost]
        [Route("ImportWhitelistNumbers")]
        public async Task<IActionResult> ImportWhitelistNumbers(ImportWlistnoRequest request)
        {
            Logger.Info("ServiceExecutionStart");
            if (string.IsNullOrWhiteSpace(request.FILEPATH) && request.Id <= 0 && request.Createdby == 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.ImportWhitelistNumbers(request);
            Logger.Info("ServiceExecutionEnd");
            return Ok(response);
        }
    }
}

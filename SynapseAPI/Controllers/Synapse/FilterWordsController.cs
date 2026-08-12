using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Models.Dtos.Requests.Synapse.AdminOperation;
using Core.Models.Dtos.Responses.Synapse.AdminOperation;
using Core.Models.Helpers;
using Core.Models.Dtos.CommonDtos;
using APIServices.Filters;

namespace SynapseAPI.Controllers
{
    public class FilterWordsController : ServicesBaseController
    {

        [HttpPost]
        [Route("GetFilterWords")]
        public async Task<IActionResult> GetFilterWords([FromBody]  FilterWordsReq request)
        {
            var response = await _contextSynapseCore.GetFilterWords(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("AddFilterWordById")]
        public async Task<IActionResult> AddFilterWordById([FromBody] SetFWReq request)
        {
            if (
                //request.nId <= 0 &&
                request.strWord == null &&
                request.strReplaceWord == null
                //request.nStatus <= 0 &&
                //request.CreatedBy <= 0 &&
                //request.CurrentStatus <= 0 &&
                //request.strDupliWords == null 
                //request.nReturn <= 0 
                )
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.AddFilterWordById(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("ImportFilterWords")]
        public async Task<IActionResult> ImportFilterWords(ImportFWReq request)
        {
            Logger.Info("ServiceExecutionStart");
            if (request.CREATEDBY != 0)
            {
                //request.FILEPATH = @"D:\ENBD\projects\Contacts11.xls";
                var response = await _contextSynapseCore.ImportFilterWords(request);
                Logger.Info("ServiceExecutionEnd");
                return Ok(response);
            }

            return Ok();
        }
        [HttpPost]
        [Route("FiterDBCheck")]
        public async Task<IActionResult> FiterDBCheck(CheckerFilterWordsRequest request)
        {

            if (!string.IsNullOrWhiteSpace(request.Word))
            {
                var response = await _contextSynapseCore.FiterDBCheck(request);
                return Ok(response);
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("ChangeStatusFW")]
        public async Task<IActionResult> ChangeStatusFW([FromBody] ReUsableRequest request)
        {

            if (request.UserId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.ChangeStatusFW(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("CheckerFilterWords")]
        public async Task<IActionResult> CheckerFilterWords([FromBody] CheckerFilterWordsRequest request)
        {
            if (request.ID != "")
            {
                var response = await _contextSynapseCore.CheckerFilterWords(request);
                return Ok(response);
            }
            return Ok();

        }

        [HttpPost]
        [Route("ImportExternalDBS")]
        public async Task<IActionResult> ImportExternalDBS(ExternalDBRequest request)
        {
            if (request.Createdby != 0)
            {
                var response = await _contextSynapseCore.ImportExternalDBS(request);
                return Ok(response);
            }
            return Ok();
        }

        [HttpPost]
        [Route("GetExternalDBGrid")]
        public async Task<IActionResult> GetExternalDBGrid([FromBody]  ExternalDBRequest request)
        {
            var response = await _contextSynapseCore.GetExternalDBGrid(request);
            return Ok(response);
        }

    }


}

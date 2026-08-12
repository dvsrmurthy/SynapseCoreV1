using Core.Models.Dtos.Requests.Synapse.SMSCSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Models.Dtos.CommonDtos;
  
namespace SynapseAPI.Controllers
{
    public class OperatorsController : ServicesBaseController
    {
        [HttpPost]
        [Route("GetAllOperatorsByIDAsync")]
        public async Task<IActionResult> GetAllOperatorsByIDAsync([FromBody] OperatorsRequest request)
        {
            if (request.NID <= 0 && request.STRCOUNTRY != null && request.NSTATUS <= 0 && request.nCreatedby <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetAllOperatorsByIDAsync(request);
                return Ok(response);

        }



        [HttpPost]
        [Route("GetAllOperatorSeries")]
        public async Task<IActionResult> GetAllOperatorSeries([FromBody] int request)
        {
            if (request <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetAllOperatorSeries(request);
            return Ok(response);

        }




        [HttpPost]
        [Route("OperatorAddEdit")]
        public async Task<IActionResult> AddEditOperatorMethod([FromBody] AddEditOperator request)
        {
            if (
                request.OPRNAME == null 
                )
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.OperatorAddEdit(request);
            return Ok(response);
        }



        [HttpPost]
        [Route("SeriesDuplicaeCheck")]
        public async Task<IActionResult> SeriesDuplicaeCheck([FromBody]  ReUsableRequest request)
        {
            if (
               request.SmsId < 0
                )
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.SeriesDuplicaeCheck(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("BulkSeriesAdd")]
        public async Task<IActionResult> BulkSeriesAddAsync([FromBody]  DtoBulkOperatorsMainRequest request)
        {
            if (request.UserId < 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.BulkSeriesAdd(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ChangeStatusOP")]
        public async Task<IActionResult> ChangeStatusOP([FromBody] ReUsableRequest request)
        {

            if (request.UserId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.ChangeStatusOP(request);
            return Ok(response);
        }

        //code added on 30032017
        [HttpPost]
        [Route("DeleteOperatorLegs")]
        public async Task<IActionResult> DeleteOperatorLegs([FromBody] ReUsableRequest request)
        {

            if (request.ParentId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.DeleteOperatorLegs(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ApproveOrRejectOperatorAsync")]
        public async Task<IActionResult> ApproveOrRejectOperatorAsync([FromBody] ApproveOrRejectRequest request)
        {
            if (
                request.Id <= 0
                )
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.ApproveOrRejectOperator(request);
            return Ok(response);
        }

    }
}

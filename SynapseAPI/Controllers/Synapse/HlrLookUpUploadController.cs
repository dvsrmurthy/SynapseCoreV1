
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Models.Dtos.Requests.Synapse.HlrLookup;

namespace SynapseAPI.Controllers
{
    public class HlrLookUpUploadController : ServicesBaseController
    {
        [HttpPost]
        [Route("GetAllHlrLookupUpload")]
        public async Task<IActionResult> GetAllHlrLookupUpload([FromBody] HlrLookupRequestUpload request)
        {
            var response = await _contextSynapseCore.GetAllHlrLookupUpload(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("SaveHlrLookupUpload")]
        public async Task<IActionResult> SaveHlrLookupUpload([FromBody] SaveHlrLookupRequestUpload request)
        {
            var response = await _contextSynapseCore.SaveHlrLookupUpload(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetAllCountryByIdHlrAsync")]
        public async Task<IActionResult> GetAllCountryByIdHlrAsync([FromBody] CountryDetailsHlr request)
        {
            if (request.Strsearch == null && request.Nsearch <= 0 && request.NCreatedBy <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetAllCountryByIdHlrAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ChangeStatusHlrLookupUpload")]
        public async Task<IActionResult> ChangeStatusHlrLookupUpload([FromBody] StatusHlrUpdatedOnRequest request)
        {
            var response = await _contextSynapseCore.ChangeStatusHlrLookupUpload(request);
            return Ok(response);
        }
	}
}
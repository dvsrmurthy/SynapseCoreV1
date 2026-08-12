using Core.Models.Dtos.Requests.Synapse.AdminOperation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
//using Microsoft.AspNetCore.Mvc;

namespace SynapseAPI.Controllers
{
    public class ApplicationConfigurationController : ServicesBaseController
    {

        [HttpPost]
        [Route("GetAppconfig")]
        public async Task<IActionResult> GetAppconfig([FromBody] ApplicationConfigurationReq request)
        {
            var result = await _contextSynapseCore.GetAppConfig(request);
            return Ok(result);

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Models.Extensions;
using Core.Models.Dtos.Requests.Synapse.AlertsManager;
using Core.Models.Dtos.Responses.Synapse.AlertsManager;

namespace SynapseAPI.Controllers
{
    public class ProfileCreationController : ServicesBaseController
    {
        //
        // GET: /ProfileCreation/
        //public ActionResult Index()
        //{
        //    return View();
        //}
        #region ProfileCreation
        [HttpPost]
        [Route("SProfileConnectionCheck")]
        public async Task<IActionResult> SProfileConnectionCheck([FromBody] TestConnectionOnRequest Request)
        {
            Core.Models.Helpers.Logger.Info("SProfileConnectionCheck Service Invoked: ");

            try
            {
                var response = _contextSynapseCore.TestConnections(Request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Core.Models.Helpers.Logger.ErrorFormat("SProfileConnectionCheck error : {0}, full trace: {1}", ex.Message, ex.StackTrace);
            }
            return Ok();

        }
        [HttpPost]
        [Route("Getdefultdbs")]
        public IActionResult Getdefultdbs([FromBody] TestConnectionOnRequest Request)
        {
            var response = _contextSynapseCore.Getdefultdbs(Request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetProfiles")]
        public async Task<IActionResult> GetProfiles([FromBody] GetProfilesOnRequest Request)
        {
            var response = await _contextSynapseCore.GetProfiles(Request);
            return Ok(response);
        }
        [HttpPost]
        [Route("SaveProfiles")]
        public async Task<IActionResult> SaveProfiles(ProfilesCreationOnRequest Request)
        {
            var response =await  _contextSynapseCore.SaveProfiles(Request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetEditProfiles")]
        public async Task<IActionResult> GetEditProfiles(GetEditProfileOnRequest Request)
        {
            var response = await _contextSynapseCore.GetEditProfiles(Request);
            return Ok(response);
        }
        [HttpPost]
        [Route("UpdateProfileStatus")]
        public async Task<IActionResult> UpdateProfileStatus(UpdateProfileStatusOnRequest Request)
        {
            var response = await _contextSynapseCore.UpdateProfileStatus(Request);
            return Ok(response);
        }
        [HttpPost]
        [Route("CheckerUpdateProfiles")]
        public async Task<IActionResult> CheckerUpdateProfiles(ApproveRejectProfileCreation Request)
        {
            var response = await _contextSynapseCore.CheckerUpdateProfiles(Request);
            return Ok(response);
        }
        #endregion
    }
}
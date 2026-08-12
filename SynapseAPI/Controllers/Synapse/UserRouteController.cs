using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Models.Dtos.Requests.Synapse.SMSCSettings;

namespace SynapseAPI.Controllers
{
    public class UserRouteController : ServicesBaseController
    {
        /// <summary>
        /// Method Name : ShowGridUserroute
        /// Created By : G.Murali
        /// Created On : 
        /// Purpose : To get all the UserRoutes
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>  
        [HttpPost]
        [Route("ShowGridUserroute")]
        public async Task<IActionResult> ShowGridUserroute(UserRouteOnRequest request)
        {
            if (request.CREATEDBY != 0)
            {
                var response = await _contextSynapseCore.ShowGridUserRoute(request);
                return Ok(response);

            }            
            return Ok();
        }
        /// <summary>
        /// Method Name : ChangeStatusUserRoute
        /// Created By : G.Murali
        /// Created On : 
        /// Purpose : To Change the status of selected routes
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>  
        //[HttpPost]
        //[Route("ChangeStatusUserRoute")]
        //public async Task<IActionResult> ChangeStatusUserRoute(CheckDefaultRouteOnRequest request)
        //{
        //    if (request.ROUTEIDS != ""&&request.ROUTEID!=0&&request.VENDORID!=0&&request.SMSCID!=0)
        //    {
        //        var response = await _contextSynapseCore.ChangeStatusUserRoute(request);
        //        return Ok(response);

        //    }
        //    return Ok();
        //}

        [HttpPost]
        [Route("ChangeStatusUserRoute")]
        public async Task<IActionResult> ChangeStatusUserRoute(CheckDefaultRouteOnRequest request)
        {
            if (request.ROUTEIDS != "")
            {
                var response = _contextSynapseCore.ChangeStatusUserRoute(request);
                return Ok(response);

            }
            return Ok();
        }
        /// <summary>
        /// Method Name : BindSeriesUserRoute
        /// Created By : G.Murali
        /// Created On : 
        /// Purpose : To get all Series of selected operators
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>  
        [HttpPost]
        [Route("BindSeriesUserRoute")]
        public async Task<IActionResult> BindSeriesUserRoute(BindSeriesUserRouteOnRequest request)
        {
            var response = await _contextSynapseCore.BindSeriesUserRoute(request);
            return Ok(response);
        }
        /// <summary>
        /// Method Name : GetVendorsUserRoute
        /// Created By : G.Murali
        /// Created On : 
        /// Purpose : To get all Vendors
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>  
        [HttpPost]
        [Route("GetVendorsUserRoute")]
        public async Task<IActionResult> GetVendorsUserRoute(GetVendorsUserRouteOnRequest request)
        {
            if (request.CREATEDBY != 0)
            {
                var response = await _contextSynapseCore.GetVendorsUserRoute(request);
                return Ok(response);

            }
            return Ok();
        }
        /// <summary>
        /// Method Name : GetSMSCUserRoute
        /// Created By : G.Murali
        /// Created On : 
        /// Purpose : To get all SMSCs of selected Vendor
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>  
        [HttpPost]
        [Route("GetSMSCUserRoute")]
        public async Task<IActionResult> GetSMSCUserRoute(GetSMSCUserRouteOnRequest request)
        {
            if (request.VENDORID != 0)
            {
                var response = await _contextSynapseCore.GetSMSCUserRoute(request);
                return Ok(response);

            }
            return Ok();
        }
        /// <summary>
        /// Method Name : GetCountriesUserRoute
        /// Created By : G.Murali
        /// Created On : 
        /// Purpose : To get all Countries
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>  
        [HttpGet]
        [Route("GetCountriesUserRoute")]
        public async Task<IActionResult> GetCountriesUserRoute()
        {
            var response = await _contextSynapseCore.GetCountriesUserRoute();
                return Ok(response);
        }
        /// <summary>
        /// Method Name : GetCountriesUserRoute
        /// Created By : G.Murali
        /// Created On : 
        /// Purpose : To get all Operators
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>  
        [HttpGet]
        [Route("GetOperatorsUserRoute")]
        public async Task<IActionResult> GetOperatorsUserRoute()
        {
            var response = await _contextSynapseCore.GetOperatorsUserRoute();
            return Ok(response);
        }
        /// <summary>
        /// Method Name : InsertRouteUserRoute
        /// Created By : G.Murali
        /// Created On : 
        /// Purpose : To Save or Update Route
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>  
        [HttpPost]
        [Route("InsertRouteUserRoute")]
        public async Task<IActionResult> InsertRouteUserRoute(InsertRouteUserRouteOnRequest request)
        {           
            if (request.SMSCID != "" && request.VENDORID != 0 && request.COUNTRYCODE != "")
            {
                var response = await _contextSynapseCore.InsertRouteUserRoute(request);
                return Ok(response);
            }
            return Ok();
        }
        /// <summary>
        /// Method Name : CheckerUpdateUserRoute
        /// Created By : G.Murali
        /// Created On : 
        /// Purpose : To Approve or Reject Route
        /// Modified By : 
        /// Modified On :
        /// Details of Modification :  
        /// </summary>
        [HttpPost]
        [Route("CheckerUpdateUserRoute")]
        public async Task<IActionResult> CheckerUpdateUserRoute(CheckerUpdateUserRouteOnRequest request)
        {
            if (request.ROUTEID!="" && request.UPDATEDBY != 0)
            {
                var response = await _contextSynapseCore.CheckerUpdateUserRoute(request);
                return Ok(response);
            }
            return Ok();
        }
    }
}
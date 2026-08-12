
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Models.Dtos.CommonDtos;
using Core.Models.Dtos.Requests.Synapse.Customers;
using Core.Models.Dtos.Responses.Synapse.Customers;

namespace SynapseAPI.Controllers
{    
    #region Customers Creation

    public class CustomersController : ServicesBaseController
    {
        [HttpPost]
        [Route("GetAllCustomersByIDAsync")]
        public async Task<IActionResult> GetAllCustomersByIdAsync([FromBody] ReUsableRequest request)
        {
            //if (request.CustomerId <= 0 && request.Createdby <= 0)
            //{
            //    return BadRequest();
            //}
            var response = await _contextSynapseCore.GetAllCustomersByIDAsync(request);
            return Ok(response);

        }

        [HttpPost]
        [Route("IsCustomerNameExistedAsync")]
        public async Task<IActionResult> IsCustomerExisted([FromBody] ReUsableRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CustomerName))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.IsCustomerNameExists(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("CreateCustomerAsync")]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CustomerCreationRequest request)
        {
            if (!ValidateRequest(request))
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.CreateCustomerAsync(request);
            return Ok(response);
        }

        //status -start
        [HttpPost]
        [Route("AINCustomerAsynch")]
        public async Task<IActionResult> AINCustomerAsynch([FromBody] ReUsableRequest request)
        {
            if (request.CustomerId < 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.AINCustomerAsynch(request);
            return Ok(response);
        }
        
        //status -end

        private bool ValidateRequest(CustomerCreationRequest request)
        {
            return !string.IsNullOrWhiteSpace(request.Name) && !string.IsNullOrWhiteSpace(request.Address1) &&
                 //  !string.IsNullOrWhiteSpace(request.City) && !string.IsNullOrWhiteSpace(request.State) &&
                //   !string.IsNullOrWhiteSpace(request.Country) && !string.IsNullOrWhiteSpace(request.Pin) &&
                   !string.IsNullOrWhiteSpace(request.Mobile) && !string.IsNullOrWhiteSpace(request.Email) &&
                   request.CumtomerType > 0 ;
        }

        [HttpPost]
        [Route("ApproveOrRejectCustomerAsync")]
        public async Task<IActionResult> ApproveOrRejectCustomerAsync([FromBody] ReUsableRequest request)
        {
            if (request.CustomerId <= 0 && request.CurrentStatus <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.ApproveOrRejectCustomer(request);
            return Ok(response);
        }
    }

    #endregion

    //Divisions
    public class DivisionsController : ServicesBaseController
    {
        [HttpPost]
        [Route("GetAllDivisionsByIDAsync")]
        public async Task<IActionResult> GetAllDivisionsByIdAsync([FromBody] DivisionsRequest request)
        {
            if (request.NDIVID <= 0 && request.nCreatedby <= 0 && request.NSTATUS <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetAllDivisionsByIDAsync(request);
            return Ok(response);

        }

    }

    //Map Divisions
    public class MapDivisionsController : ServicesBaseController
    {
        [HttpPost]
        [Route("GetAllMapDivsByIDAsync")]
        public async Task<IActionResult> GetAllMapDivsByIdAsync([FromBody] MapDivisionsRequest request)
        {
            if (request.NDIVDETID < 0 && request.NDIVID < 0 && request.NSTATUS < 0 && request.NCUSTID < 0 && request.nCreatedby < 0 )
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetAllMapDivsByIDAsync(request);
            return Ok(response);

        }

    }

    #region Customer App Preferences
    
    public class CustomerAppPreferencesController : ServicesBaseController
    {
        [HttpPost]
        [Route("GetCustomerAppPreferencesAsync")]
        public async Task<IActionResult> GetCustomerAppPreferencesAsync([FromBody] ReUsableRequest request)
        {
            if (request.CustomerId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.BuildCustomerAppPreferences(request);
            return Ok(response);

        }

        [HttpPost]
        [Route("GetCustomerAppPreferencesGrid")]
        public async Task<IActionResult> GetCustomerAppPreferencesGrid([FromBody] ReUsableRequest request)
        {
            if (request.CustomerId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.GetCustomerAppPreferencesGrid(request);
            return Ok(response);
        }

        

        //[HttpPost]
        //[Route("GetAllCustPrefList")]
        //public async Task<IActionResult> GetAllCustPrefList([FromBody] ReUsableRequest request)
        //{
        //    if (request.CustomerId <= 0)
        //    {
        //        return BadRequest();
        //    }
        //    var response = await _contextSynapseCore.BuildCustomerAppPreferences(request);
        //    return Ok(response);

        //}

        [HttpPost]
        [Route("SaveCustomerAppPreferencesAsync")]
        public async Task<IActionResult> SaveCustomerAppPreferencesAsync([FromBody] CustomerAppPreferencesResponse request)
        {
            if (request.CustomerId <= 0 && request.NumberOfGroups <= 0 && request.NumberOfContacts <= 0 &&
                request.CreditType <= 0 && request.FilterByColumn <= 0 &&
                request.GridSize <= 0 && request.ConsiderMargin <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.SaveCustomerAppPreferences(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("ApproveOrRejectCustomerAppPreferencesAsync")]
        public async Task<IActionResult> ApproveOrRejectCustomerAppPreferencesAsync([FromBody] ReUsableRequest request)
        {
            if (request.CustomerAppPreferenceId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.UpdateApproveOrRejectCustomerAppPreferences(request);
            return Ok(response);
        }
          #endregion
    }
    #region Map Account Manager
    public class AccountManagerMapController : ServicesBaseController
    {

        [HttpPost]
        [Route("UpdateAccount")]
        public async Task<IActionResult> UpdateAccount([FromBody] MapAccountRequest request)
        {
            if (request.Actmgrid <= 0 && request.customerId != null)
            {
                return BadRequest();
            }
            var response = await _contextSynapseCore.UpdateAccount(request);
            return Ok(response);
        }

    }

    #endregion

}

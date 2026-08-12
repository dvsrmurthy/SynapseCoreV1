using APIServices.Helpers;
using Core.Data.IDataInterfaces.Account;
using Core.Models.Dtos.CommonDtos;
using Core.Models.Dtos.Requests.Synapse.MailBox;
using Core.Models.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace SynapseAPI.Controllers
{
    [ApiController]
    [Route("")]
    public class AccountController : ServicesBaseController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountCoreData _contextAccountCore;
        public AccountController(ILogger<AccountController> logger, IAccountCoreData accountCoreData)
        {
            _logger = logger; _contextAccountCore = accountCoreData;
        }
        [HttpPost("AuthenticateUser")]        
        public async Task<IActionResult> AuthenticateUserAsync([FromBody] LogOnRequest requst)
        {
            Logger.InfoFormat("Authentication Started :: {0}", requst.UserName);
            if (string.IsNullOrWhiteSpace(requst.UserName) && string.IsNullOrWhiteSpace(requst.Password))
            {
                return BadRequest();
            }
            var responseData = await _contextAccountCore.AuthenticateUser(requst);
            return Ok(responseData);
        }

        [HttpPost]
        [Route("AuthenticateUserLogout")]
        public async Task<IActionResult> AuthenticateUserLogout([FromBody] LogOnRequest requst)
        {
            if (requst.UserId <= 0)
            {
                return BadRequest();
            }
            var responseData = await _contextAccountCore.AuthenticateUserLogout(requst);

            return Ok(responseData);
        }

        [HttpPost]
        [Route("GetAllPreferencesAsync")]
        public async Task<IActionResult> GetAllPreferencesAsync([FromBody] ReUsableRequest request)
        {
            var response = await _contextAccountCore.GetAllPreferencesAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetAllRolesAsync")]
        public async Task<IActionResult> GetAllRolesAsync(ReUsableRequest request)
        {
            if (request.CustomerId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextAccountCore.GetAllRolesAsync(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetAllPreferred")]
        public async Task<IActionResult> GetAllPreferred()
        {
            var response = await _contextAccountCore.GetAllPreferred();
            return Ok(response);
        }

        [Route("GetDashBoardTypeAsync")]
        [HttpPost]
        public async Task<IActionResult> GetDashBoardTypeAsync(ReUsableRequest request)
        {
            var response = await _contextAccountCore.GetDashBoardTypeAsync(request);
            return Ok(response);
        }
        [Route("GetAvailableCreditsAsync")]
        [HttpPost]
        public async Task<IActionResult> GetAvailableCreditsAsync(ReUsableRequest request)
        {
            var response = await _contextAccountCore.GetAvailableCreditsAsync(request);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetAllPreferredCountry")]
        public async Task<IActionResult> GetAllPreferredCountry()
        {
            var response = await _contextAccountCore.GetAllPreferredCountry();
            return Ok(response);
        }

        [HttpPost]
        [Route("GetAllCustPrefList")]
        public async Task<IActionResult> GetAllCustPrefList(ReUsableRequest request)
        {
            var response = await _contextAccountCore.GetAllPrefLists(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetAdminResellers")]
        public async Task<IActionResult> GetAdminResellers(ReUsableRequest request)
        {
            var response = await _contextAccountCore.GetAdminResellers(request);
            return Ok(response);
        }


        [HttpPost]
        [Route("GetAllDivisionsColomnsAsync")]
        public async Task<IActionResult> GetAllDivisionsColomnsAsync(ReUsableRequest request)
        {
            var response = await _contextAccountCore.GetAllDivisionsColomnsAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetAllVendorsRate")]
        public async Task<IActionResult> GetAllVendorsRate()
        {
            var response = await _contextAccountCore.GetAllVendorsRate();
            return Ok(response);
        }
        [HttpPost]
        [Route("GetAllOperatorsRate")]
        public async Task<IActionResult> GetAllOperatorsRate()
        {
            var response = await _contextAccountCore.GetAllOperatorsRate();
            return Ok(response);
        }
        [HttpPost]
        [Route("GetAllPackageRate")]
        public async Task<IActionResult> GetAllPackageRate()
        {
            var response = await _contextAccountCore.GetAllPackageRate();
            return Ok(response);
        }

        [HttpPost]
        [Route("GetAllCustomerColomnsAsync")]
        public async Task<IActionResult> GetAllCustomerColomnsAsync([FromBody] ReUsableRequest request)
        {
            if (request.ParentId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextAccountCore.GetAllCustomerColomnsAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetAllCustomerColomnsAsyncRep")]
        public async Task<IActionResult> GetAllCustomerColomnsAsyncRep([FromBody] ReUsableRequest request)
        {
            if (request.ParentId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextAccountCore.GetAllCustomerColomnsAsyncRep(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetCustomersForRoles")]
        public async Task<IActionResult> GetCustomersForRoles([FromBody] ReUsableRequest request)
        {
            if (request.ParentId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextAccountCore.GetCustomersForRoles(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("GetAllGlobalKeysAsync")]
        public async Task<IActionResult> GetAllGlobalKeysAsync()
        {
            var response = await _contextAccountCore.GetAppGobalKeys();
            return Ok(response);
        }
        [HttpGet]
        [Route("GetAllUsersAsync")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var response = await _contextAccountCore.GetAllUsersAsync();
            return Ok(response);
        }
        [HttpGet]
        [Route("GetAllSendersAsync")]
        public async Task<IActionResult> GetAllSendersAsync()
        {
            var response = await _contextAccountCore.GetAllSendersAsync();
            return Ok(response);
        }
        [HttpPost]
        [Route("GetAllCountriesAsync")]
        public async Task<IActionResult> GetAllCountriesAsync()
        {
            var response = await _contextAccountCore.GetAllCountriesAsync();
            return Ok(response);
        }
        [HttpPost]
        [Route("GetAllSMSCAsync")]
        public async Task<IActionResult> GetAllSMSCAsync()
        {
            var response = await _contextAccountCore.GetAllSMSCAsync();
            return Ok(response);
        }
        [HttpPost]
        [Route("GetAllSMSCAsyncRate")]
        public async Task<IActionResult> GetAllSMSCAsyncRate()
        {
            var response = await _contextAccountCore.GetAllSMSCAsyncRate();
            return Ok(response);
        }

        [HttpPost]
        [Route("GetMenuByUserAsync")]
        public async Task<IActionResult> GetMenuByUserAsync([FromBody] ReUsableRequest request)
        {
            var response = await _contextAccountCore.GetUserMenuItemsByUser(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("VerifyLdapUserAsync")]
        public async Task<IActionResult> VerifyLdapUserAsync([FromBody] ReUsableRequest request)
        {
            var response = _contextAccountCore.IsUserExists(request.UserName);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetAllMailboxAsync")]
        public async Task<IActionResult> GetAllMailboxAsync([FromBody] ReUsableRequest request)
        {
            var response = await _contextAccountCore.GetAllMailboxAsync();
            return Ok(response);
        }
        [HttpPost]
        [Route("GetAccountManager")]
        public async Task<IActionResult> GetAccountManager()
        {
            var response = await _contextAccountCore.GetAccountManager();
            return Ok(response);
        }

        [Route("GetCustomerAccount")]
        [HttpPost]
        public async Task<IActionResult> GetCustomerAccount([FromBody]ReUsableRequest request)
        {
            var response = await _contextAccountCore.GetCustomerAccount(request);
            return Ok(response);
        }

        [Route("GetCustomerAccountdrop")]
        [HttpPost]
        public async Task<IActionResult> GetCustomerAccountdrop([FromBody]ReUsableRequest request)
        {
            var response = await _contextAccountCore.GetCustomerAccountdrop(request);
            return Ok(response);
        }
        //reports
        [Route("GetReportcustomer")]
        [HttpPost]
        public async Task<IActionResult> GetReportcustomer(ReUsableRequest request)
        {
            var responce = await _contextAccountCore.GetReportcustomer(request);
            return Ok(responce);
        }
        //vendor
        [Route("GetVendor")]
        [HttpGet]
        public async Task<IActionResult> GetVendor()
        {
            var responce = await _contextAccountCore.GetVendor();
            return Ok(responce);
        }
        [HttpPost]
        [Route("GetMobility")]
        public async Task<IActionResult> GetMobility()
        {
            var response = await _contextAccountCore.GetMobility();
            return Ok(response);
        }
        [HttpPost]
        [Route("GetAllMOShortcode")]
        public async Task<IActionResult> GetAllMOShortcode()
        {
            var response = await _contextAccountCore.GetAllMOShortcode();
            return Ok(response);
        }
        [HttpPost]
        [Route("GetModule")]
        public async Task<IActionResult> GetModule()
        {
            var response = await _contextAccountCore.GetModule();
            return Ok(response);
        }
        [HttpPost]
        [Route("GetStages")]
        public async Task<IActionResult> GetStages()
        {
            var response = await _contextAccountCore.GetStages();
            return Ok(response);
        }
        [HttpPost]
        [Route("GetSMPPIDAsync")]
        public async Task<IActionResult> GetSMPPIDAsync()
        {
            var response = await _contextAccountCore.GetSMPPIDAsync();
            return Ok(response);
        }
        [HttpPost]
        [Route("GetOutboundSender")]
        public async Task<IActionResult> GetOutboundSender()
        {
            var response = await _contextAccountCore.GetOutboundSender();
            return Ok(response);
        }
        [HttpPost]
        [Route("GetShortCode")]
        public async Task<IActionResult> GetShortCode()
        {
            var response = await _contextAccountCore.GetShortCode();
            return Ok(response);
        }

        [HttpPost]
        [Route("GetMOShortcodeAsync")]
        public async Task<IActionResult> GetMOShortcodeAsync()
        {
            var response = await _contextAccountCore.GetAllMOShortcode();
            return Ok(response);
        }

        [HttpPost]
        [Route("GetAllStatusBoardCustomers")]
        public async Task<IActionResult> GetAllStatusBoardCustomers([FromBody] ReUsableRequest request)
        {
            if (request.ParentId <= 0)
            {
                return BadRequest();
            }
            var response = await _contextAccountCore.GetAllStatusBoardCustomers(request);
            return Ok(response);
        }
    }
}

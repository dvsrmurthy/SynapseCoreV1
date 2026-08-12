using System.Configuration;
using Microsoft.AspNetCore.Mvc;
using Core.Data.IDataInterfaces.Account;
using System.Web;
using Core.Data.Data.Account;
using Core.Data.IDataInterfaces.ISynapse;
using Core.Data.Data.Synapse;
using APIServices.Helpers;
using Microsoft.Extensions.Configuration;
using Core.Data;
using SynapseAPI.Controllers;

namespace SynapseAPI.Controllers
{
    [ValidateBasicAuthrioze]
    public class ServicesBaseController : ControllerBase
    {        
        public IAccountCoreData _contextAccountCore { get; set; }       
        public ISynapseCoreData _contextSynapseCore { get; set; }

        public string HL7FileLocation = null;

        public ServicesBaseController()
        {
            _contextSynapseCore = new SynapseCoreData();
            _contextAccountCore = new AccountCoreData();
            IConfiguration config = AppConfigurationAPI.Configuration;
            //HL7FileLocation = config.GetValue<string>("HL7FileLocation");
        }
    }
}

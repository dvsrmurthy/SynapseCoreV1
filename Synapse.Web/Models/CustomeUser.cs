using Core.Models.Dtos.CommonDtos;
using Core.Models.Dtos.Responses.Synapse.Account;

namespace Synapse.Web.Models
{
    public class CustomeUser
    {
        public LogOnRespons LogOnRespons { get; set; }

        public PreferencesResponse UserPreferences { get; set; }
    }
}
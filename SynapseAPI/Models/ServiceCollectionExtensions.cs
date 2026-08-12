using Core.Data.Data.Account;
using Core.Data.Data.Services;
using Core.Data.Data.Synapse;
using Core.Data.IDataInterfaces.Account;
using Core.Data.IDataInterfaces.ISynapse;

namespace SynapseAPI.Models
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSynapseApiServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<IAccountCoreData, AccountCoreData>();

            services.AddScoped<ThirdPartyServiceConsumption>();
            services.AddScoped<ISynapseCoreData, SynapseCoreData>();                
            // Other API registrations

            return services;
        }
    }
}

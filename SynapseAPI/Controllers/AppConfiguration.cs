namespace SynapseAPI.Controllers
{
    public static class AppConfigurationAPI
    {
        public static IConfiguration Configuration { get; private set; } = null!;

        public static void Initialize(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}

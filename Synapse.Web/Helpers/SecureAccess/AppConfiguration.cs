namespace Synapse.Web.Helpers.SecureAccess
{
    public static class AppConfiguration
    {
        public static IConfiguration Configuration { get; private set; } = null!;

        public static void Initialize(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}

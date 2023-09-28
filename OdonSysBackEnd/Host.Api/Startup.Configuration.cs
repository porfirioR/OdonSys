using Utilities.Configurations;

namespace Host.Api
{
    public partial class Startup
    {
        public void AddConfigurations(IServiceCollection services)
        {
            services.Configure<CloudinarySettings>(Configuration.GetSection(CloudinarySettings.ConfigSection));
            services.Configure<DataAccessSettings>(Configuration.GetSection(DataAccessSettings.ConfigSection));
            services.Configure<AuthenticationSettings>(Configuration.GetSection(AuthenticationSettings.ConfigSection));
            services.Configure<AzureB2CSettings>(Configuration.GetSection(AzureB2CSettings.ConfigSection));
            services.Configure<MainConfiguration>(Configuration);
            MainConfiguration = Configuration.Get<MainConfiguration>();
        }
    }
}

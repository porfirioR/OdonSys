using Manager.Administration;

namespace Host.Api
{
    public partial class Startup
    {
        public void InjectServices(IServiceCollection services)
        {
            //Access
            Access.Data.ServiceInjection.ConfigureServices(services);
            Access.File.ServiceInjection.ConfigureServices(services);
            Access.Sql.ServiceInjection.ConfigureServices(services, MainConfiguration);

            //Manager
            ServiceInjection.ConfigureServices(services);
            Manager.Payment.ServiceInjection.ConfigureServices(services);
            Manager.Workspace.ServiceInjection.ConfigureServices(services);
        }
    }
}

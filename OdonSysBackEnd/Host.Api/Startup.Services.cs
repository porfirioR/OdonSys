using Host.Api.Contract.MapBuilders;
using Host.Api.Mapper;
using Manager.Administration;

namespace Host.Api
{
    public partial class Startup
    {
        public void InjectServices(IServiceCollection services)
        {
            services.AddTransient<IClientHostBuilder, ClientHostBuilder>();
            services.AddTransient<IProcedureHostBuilder, ProcedureHostBuilder>();

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

using Host.Api.Contract.MapBuilders;
using Host.Api.Mapper;
using Manager.Administration;

namespace Host.Api;

public partial class Startup
{
    public void InjectServices(IServiceCollection services)
    {
        services.AddTransient<IClientHostBuilder, ClientHostBuilder>();
        services.AddTransient<IOrthodonticHostBuilder, OrthodonticHostBuilder>();
        services.AddTransient<IProcedureHostBuilder, ProcedureHostBuilder>();
        services.AddTransient<IRoleHostBuilder, RoleHostBuilder>();
        services.AddTransient<IUserHostBuilder, UserHostBuilder>();

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

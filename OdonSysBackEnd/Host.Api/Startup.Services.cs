using Microsoft.Extensions.DependencyInjection;

namespace Host.Api
{
    public partial class Startup
    {
        public void InjectServices(IServiceCollection services)
        {
            //Access
            Access.Data.ServiceInjection.ConfigureServices(services);
            
            //Manager
            Manager.Admin.ServiceInjection.ConfigureServices(services);
            Manager.Workspace.ServiceInjection.ConfigureServices(services);
        }
    }
}

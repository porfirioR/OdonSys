using Access.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Host.Api
{
    public partial class Startup
    {
        public void InjectServices(IServiceCollection services)
        {
            //Access
            ServiceInjection.ConfigureServices(services);
            
            //Manager
            Manager.Admin.ServiceInjection.ConfigureServices(services);
            Manager.Procedure.ServiceInjection.ConfigureServices(services);
            Manager.Authentication.ServiceInjection.ConfigureServices(services);    
        }
    }
}

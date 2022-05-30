using Contract.Admin.Clients;
using Manager.Admin.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace Manager.Admin
{
    public class ServiceInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IClientManager, ClientManager>();
        }
    }
}

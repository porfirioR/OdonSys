using Contract.Admin.Clients;
using Contract.Admin.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Manager.Admin
{
    public class ServiceInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IClientManager, ClientManager>();
            services.AddTransient<IUserManager, UserManager>();
        }
    }
}

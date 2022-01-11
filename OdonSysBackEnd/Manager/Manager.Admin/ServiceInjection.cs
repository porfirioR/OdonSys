using Contract.Admin.Clients;
using Contract.Admin.User;
using Manager.Admin.Clients;
using Manager.Admin.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Manager.Admin
{
    public class ServiceInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IClientManager, ClientManager>();
        }
    }
}

using Manager.Admin.Clients;
using Manager.Admin.Users;
using Microsoft.Extensions.DependencyInjection;
using Resources.Contract.Clients;
using Resources.Contract.User;

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

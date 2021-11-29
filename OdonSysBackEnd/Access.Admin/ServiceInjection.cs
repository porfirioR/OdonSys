using Access.Admin.Clients;
using Access.Admin.Users;
using Access.Contract.Auth;
using Access.Contract.Clients;
using Access.Contract.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Access.Admin
{
    public class ServiceInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserDataAccess, UserDataAccess>();
            services.AddTransient<IAuthDataAccess, AuthDataAccess>();
            services.AddTransient<IClientAccess, ClientAccess>();

        }
    }
}

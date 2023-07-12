using Contract.Administration.Clients;
using Contract.Administration.Roles;
using Contract.Administration.Users;
using Manager.Administration.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Manager.Administration
{
    public class ServiceInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IClientManager, ClientManager>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IRoleManager, RoleManager>();

            services.AddTransient<IClientManagerBuilder, ClientManagerBuilder>();
            services.AddTransient<IRoleManagerBuilder, RoleManagerBuilder>();
            services.AddTransient<IUserManagerBuilder, UserManagerBuilder>();
        }
    }
}

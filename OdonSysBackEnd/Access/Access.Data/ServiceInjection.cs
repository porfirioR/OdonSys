using Access.Admin.Access;
using Access.Contract.Auth;
using Access.Contract.Clients;
using Access.Contract.Procedure;
using Access.Contract.Roles;
using Access.Contract.Teeth;
using Access.Contract.Users;
using Access.Data.Access;
using Microsoft.Extensions.DependencyInjection;

namespace Access.Data
{
    public class ServiceInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserDataAccess, UserAccess>();
            services.AddTransient<IAuthAccess, AuthAccess>();
            services.AddTransient<IClientAccess, ClientAccess>();
            services.AddTransient<IProcedureAccess, ProcedureAccess>();
            services.AddTransient<IToothAccess, ToothAccess>();
            services.AddTransient<IRoleAccess, RoleAccess>();
            services.AddHttpContextAccessor();
        }
    }
}

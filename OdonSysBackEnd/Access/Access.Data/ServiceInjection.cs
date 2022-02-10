using Access.Admin;
using Access.Admin.Access;
using Access.Contract.Auth;
using Access.Contract.Clients;
using Access.Contract.Procedure;
using Access.Contract.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Access.Data
{
    public class ServiceInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserDataAccess, UserDataAccess>();
            services.AddTransient<IAuthDataAccess, AuthDataAccess>();
            services.AddTransient<IClientAccess, ClientAccess>();
            services.AddTransient<IProcedureAccess, ProcedureAccess>();

        }
    }
}

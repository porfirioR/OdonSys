using Contract.Workspace.User;
using Manager.Authentication.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Manager.Authentication
{
    public class ServiceInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserManager, UserManager>();
        }
    }
}

using Contract.Workspace.User;
using Manager.Workspace.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Manager.Workspace
{
    public class ServiceInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserManager, UserManager>();
        }
    }
}

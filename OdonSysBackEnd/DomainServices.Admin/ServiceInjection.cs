using DomainServices.Admin.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DomainServices.Admin
{
    public class ServiceInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
        }
    }
}

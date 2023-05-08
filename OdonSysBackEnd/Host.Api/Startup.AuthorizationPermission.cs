using Host.Api.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Host.Api
{
    public partial class Startup
    {
        public void ConfigureAuthorizationHandlers(IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, CheckAuthPermissions>();
            services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>();
        }
    }
}

using Host.Api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Utilities.Enums;

namespace Host.Api
{
    public partial class Startup
    {
        public void ConfigureAuthorizationHandlers(IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, CheckAuthorizationPermissions>();
            var environment = Environment.GetEnvironmentVariable("Environment");
            if (string.Equals(environment, OdonSysEnvironment.Test.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>();
            }
        }
    }
}

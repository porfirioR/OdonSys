using Access.Admin.Mapper;
using Manager.Admin.Mapper;
using Microsoft.Extensions.DependencyInjection;
using OdonSys.Api.Main.Mapper;

namespace OdonSysBackEnd
{
    public partial class Startup
    {
        public void ConfigureMappings(IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(UserHostProfile),
                typeof(UserManagerProfile),
                typeof(UserDataAccessProfile)
            );
        }
    }
}

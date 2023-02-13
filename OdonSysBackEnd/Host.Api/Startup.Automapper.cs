using Access.Admin.Mapper;
using Host.Api.Mapper;
using Manager.Admin.Mapper;
using Manager.Workspace.Procedures;
using Microsoft.Extensions.DependencyInjection;

namespace Host.Api
{
    public partial class Startup
    {
        public void ConfigureMappings(IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(UserHostProfile),
                typeof(UserManagerProfile),
                typeof(UserDataAccessProfile),
                typeof(ProcedureHostProfile),
                typeof(ProcedureManagerProfile),
                typeof(ProcedureAccessProfile)
            );
        }
    }
}

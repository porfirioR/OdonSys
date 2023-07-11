using Host.Api.Mapper;
using Manager.Administration.Mapper;
using Manager.Workspace.Procedures;

namespace Host.Api
{
    public partial class Startup
    {
        public void ConfigureMappings(IServiceCollection services)
        {
            services.AddAutoMapper(
                //typeof(ToothAccessProfile),

                typeof(ProcedureManagerProfile),
                typeof(UserManagerProfile),

                typeof(ProcedureHostProfile),
                typeof(UserHostProfile)
            );
        }
    }
}

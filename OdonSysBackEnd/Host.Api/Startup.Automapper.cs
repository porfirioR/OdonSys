using Host.Api.Mapper;

namespace Host.Api
{
    public partial class Startup
    {
        public void ConfigureMappings(IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(ProcedureHostProfile),
                typeof(UserHostProfile)
            );
        }
    }
}

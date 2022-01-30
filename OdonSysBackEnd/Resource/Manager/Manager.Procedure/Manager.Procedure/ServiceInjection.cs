using Contract.Procedure.Procedures;
using Manager.Procedure.Procedures;
using Microsoft.Extensions.DependencyInjection;

namespace Manager.Procedure
{
    public class ServiceInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IProcedureManager, ProcedureManager>();
        }
    }
}

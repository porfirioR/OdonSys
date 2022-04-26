using Contract.Procedure.Procedures;
using Contract.Procedure.Teeth;
using Manager.Procedure.Procedures;
using Manager.Procedure.Teeth;
using Microsoft.Extensions.DependencyInjection;

namespace Manager.Procedure
{
    public class ServiceInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IProcedureManager, ProcedureManager>();
            services.AddTransient<IToothManager, ToothManager>();
        }
    }
}

using Contract.Workspace.Procedures;
using Contract.Workspace.Teeth;
using Manager.Workspace.Procedures;
using Manager.Workspace.Teeth;
using Microsoft.Extensions.DependencyInjection;

namespace Manager.Workspace
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

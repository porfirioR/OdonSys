using Contract.Workspace.Files;
using Contract.Workspace.Procedures;
using Contract.Workspace.Teeth;
using Manager.Workspace.Files;
using Manager.Workspace.Procedures;
using Manager.Workspace.Teeth;
using Microsoft.Extensions.DependencyInjection;

namespace Manager.Workspace
{
    public class ServiceInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFileManager, FileManager>();
            services.AddTransient<IProcedureManagerBuilder, ProcedureManagerBuilder>();
            services.AddTransient<IProcedureManager, ProcedureManager>();
            services.AddTransient<IToothManagerBuilder, ToothManagerBuilder>();
            services.AddTransient<IToothManager, ToothManager>();
        }
    }
}

using Contract.Workspace.Files;
using Contract.Workspace.Procedures;
using Manager.Workspace.Files;
using Manager.Workspace.Mapper;
using Manager.Workspace.Procedures;
using Microsoft.Extensions.DependencyInjection;

namespace Manager.Workspace
{
    public class ServiceInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFileManager, FileManager>();
            services.AddTransient<IProcedureManager, ProcedureManager>();
            services.AddTransient<IProcedureManagerBuilder, ProcedureManagerBuilder>();
            //services.AddTransient<IToothManager, ToothManager>();
        }
    }
}

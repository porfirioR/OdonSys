using Contract.Workspace.Files;
using Contract.Workspace.Orthodontics;
using Contract.Workspace.Procedures;
using Contract.Workspace.Teeth;
using Manager.Workspace.Files;
using Manager.Workspace.Orthodontics;
using Manager.Workspace.Procedures;
using Manager.Workspace.Teeth;
using Microsoft.Extensions.DependencyInjection;

namespace Manager.Workspace;

public class ServiceInjection
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IProcedureManagerBuilder, ProcedureManagerBuilder>();
        services.AddTransient<IToothManagerBuilder, ToothManagerBuilder>();
        services.AddTransient<IOrthodonticManagerBuilder, OrthodonticManagerBuilder>();

        services.AddTransient<IFileManager, FileManager>();
        services.AddTransient<IProcedureManager, ProcedureManager>();
        services.AddTransient<IToothManager, ToothManager>();
        services.AddTransient<IOrthodonticManager, OrthodonticManager>();
    }
}

using Access.File.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace Access.File;

public class ServiceInjection
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IUploadFileStorage, UploadFileStorage>();
    }
}

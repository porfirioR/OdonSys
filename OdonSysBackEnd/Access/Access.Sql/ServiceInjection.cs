using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Utilities.Configurations;

namespace Access.Sql
{
    public class ServiceInjection
    {
        public static void ConfigureServices(IServiceCollection services, MainConfiguration mainConfiguration)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(mainConfiguration.ConnectionStrings.DefaultConnection, sqlServerOptionsAction: sqlOptions => sqlOptions.EnableRetryOnFailure());
            });
        }
    }
}

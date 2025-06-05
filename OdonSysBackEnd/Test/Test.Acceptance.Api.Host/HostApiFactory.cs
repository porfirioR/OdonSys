using Access.Sql;
using Host.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Utilities.Enums;

namespace AcceptanceTest.Host.Api;

public class HostApiFactory : WebApplicationFactory<Startup>
{

    private readonly string _connectionString = $"Server=(local);Database=OdonSys;Integrated Security=True;MultipleActiveResultSets=False;TrustServerCertificate=True;";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("Environment", OdonSysEnvironment.Development.ToString().ToLowerInvariant());
        builder.ConfigureServices(services =>
        {
            var dbDescriptor = services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<DataContext>));
            if (dbDescriptor != null)
            {
                services.Remove(dbDescriptor);
            }
            services.AddDbContext<DataContext>(options => options.UseSqlServer(_connectionString));

            //var dbServerCache = services.SingleOrDefault(d => d.ServiceType == typeof(SqlServerCacheOptions));
            //if (dbServerCache != null)
            //{
            //    services.Remove(dbDescriptor);
            //}
            //services.AddDistributedSqlServerCache(o =>
            //{
            //    o.ConnectionString = connectionString;
            //    o.SchemaName = "dbo";
            //    o.TableName = "Cache";
            //});
        });

        base.ConfigureWebHost(builder);
    }
}

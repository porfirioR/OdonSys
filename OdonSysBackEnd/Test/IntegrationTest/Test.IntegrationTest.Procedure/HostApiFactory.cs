using Access.Sql;
using Host.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace DT.Host.Api.Acceptance.Test
{
    public class HostApiFactory : WebApplicationFactory<Startup> {
        
        private readonly string connectionString = "Server=(local);Database=DigitalTrustDocuments;Integrated Security=True;MultipleActiveResultSets=False";
        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DataContext>));
                if (dbDescriptor != null)
                {
                    services.Remove(dbDescriptor);
                }
                services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

                var dbServerCache = services.SingleOrDefault(d => d.ServiceType == typeof(SqlServerCacheOptions));
                if (dbServerCache != null)
                {
                    services.Remove(dbDescriptor);
                }
                services.AddDistributedSqlServerCache(o =>
                {
                    o.ConnectionString = connectionString;
                    o.SchemaName = "dbo";
                    o.TableName = "Cache";
                });
            });

            base.ConfigureWebHost(builder);
        }
    }
}

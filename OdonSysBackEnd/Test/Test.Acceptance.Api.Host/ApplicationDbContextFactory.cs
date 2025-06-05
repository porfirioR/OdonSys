using Access.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AcceptanceTest.Host.Api;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>()
                        .UseSqlServer($"Server=(local);Database=OdonSys;Integrated Security=True;MultipleActiveResultSets=False;TrustServerCertificate=True;");

        return new DataContext(optionsBuilder.Options, null);
    }
}

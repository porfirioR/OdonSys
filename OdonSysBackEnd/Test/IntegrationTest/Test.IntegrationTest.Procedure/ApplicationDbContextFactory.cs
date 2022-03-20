using Access.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DT.Host.Api.Acceptance.Test
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>()
                            .UseSqlServer("Server=(local);Database=DigitalTrustDocuments;Integrated Security=True;MultipleActiveResultSets=False");

            return new DataContext(optionsBuilder.Options);
        }
    }
}

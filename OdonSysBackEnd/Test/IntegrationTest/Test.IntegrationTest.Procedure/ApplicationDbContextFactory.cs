using Access.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Utilities.Enums;

namespace AcceptanceTest.Host.Api
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>()
                            .UseSqlServer($"Server=(local);Database={Configuration.DataBase};Integrated Security=True;MultipleActiveResultSets=False");

            return new DataContext(optionsBuilder.Options);
        }
    }
}

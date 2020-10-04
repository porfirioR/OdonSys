using Microsoft.EntityFrameworkCore;
using OdonSys.Storage.Sql.Configurations;
using OdonSys.Storage.Sql.Entities;

namespace OdonSys.Storage.Sql
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}

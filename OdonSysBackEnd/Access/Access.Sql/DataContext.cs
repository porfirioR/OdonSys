using Access.Sql.Configurations;
using Access.Sql.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utilities;

namespace Access.Sql
{
    public class DataContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DataContext(DbContextOptions<DataContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorRoles> DoctorRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Tooth> Teeth { get; set; }
        public DbSet<ProcedureTooth> ProcedureTeeth { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new DoctorConfiguration());
            modelBuilder.ApplyConfiguration(new DoctorRolesConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new DoctorClientConfiguration());
            modelBuilder.ApplyConfiguration(new ProcedureConfiguration());
            modelBuilder.ApplyConfiguration(new ProcedureToothConfiguration());
            modelBuilder.ApplyConfiguration(new ToothConfiguration());
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity &&
                                                        (x.State == EntityState.Added ||
                                                        x.State == EntityState.Modified ||
                                                        x.State == EntityState.Deleted)).ToList();
            // Need specific list since the change tracker updates the state after save changes.
            var addedEntites = entities.Where(x => x.Entity is BaseEntity && x.State == EntityState.Added).ToList();
            var transactionId = Guid.NewGuid();
            var username = SetUserContext(entities.Where(x => x.State != EntityState.Deleted));

            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }
        private string SetUserContext(IEnumerable<EntityEntry> entities)
        {
            var username = _httpContextAccessor?.HttpContext?.User?.FindFirst(Claims.UserName)?.Value ?? "api";
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).UserCreated = username;
                }
                if (entity.State == EntityState.Modified)
                {
                    // Explicitly set UserCreated and DateCreated to Modified false in case something else in the flow sets this.
                    Entry((BaseEntity)entity.Entity).Property(p => p.UserCreated).IsModified = false;
                    Entry((BaseEntity)entity.Entity).Property(p => p.DateCreated).IsModified = false;
                }

                ((BaseEntity)entity.Entity).UserUpdated = username;
                ((BaseEntity)entity.Entity).DateModified = DateTime.Now;
            }
            return username;
        }
    }
}

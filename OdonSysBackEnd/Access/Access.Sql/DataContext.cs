using Access.Sql.Configurations;
using Access.Sql.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<UserClient> UserClients { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Tooth> Teeth { get; set; }
        public DbSet<ClientProcedure> ClientProcedures { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<InvoiceDetailTooth> InvoiceDetailTeeth { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<FileStorage> FileStorages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new ClientProcedureConfiguration());
            modelBuilder.ApplyConfiguration(new FileStorageConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceDetailConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceDetailToothConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new ProcedureConfiguration());
            modelBuilder.ApplyConfiguration(new ToothConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserClientConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRolesConfiguration());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity &&
                                                        (x.State == EntityState.Added ||
                                                        x.State == EntityState.Modified ||
                                                        x.State == EntityState.Deleted)).ToList();

            SetUserAndDateTimeContext(entities.Where(x => x.State != EntityState.Deleted));
            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }

        private void SetUserAndDateTimeContext(IEnumerable<EntityEntry> entities)
        {
            var user = _httpContextAccessor?.HttpContext?.User;
            var userName = user.FindFirst(Claims.UserName)?.Value;
            if (string.IsNullOrEmpty(userName))
            {
                userName = Helper.GetUsername(user.FindFirst(Claims.NameAadB2C)?.Value, user.FindFirst(Claims.SurnameAadB2C)?.Value);
            }
            var username = userName ?? "api";
            foreach (var entity in entities)
            {
                var baseEntity = (BaseEntity)entity.Entity;
                if (entity.State == EntityState.Added)
                {
                    baseEntity.UserCreated = username;
                }
                if (entity.State == EntityState.Modified)
                {
                    // Explicitly set UserCreated and DateCreated to Modified false in case something else in the flow sets this.
                    Entry(baseEntity).Property(x => x.UserCreated).IsModified = false;
                    Entry(baseEntity).Property(x => x.DateCreated).IsModified = false;
                }

                baseEntity.UserUpdated = username;
                baseEntity.DateModified = DateTime.Now;
            }
        }
    }
}

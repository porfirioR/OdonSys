using Access.Contract.Roles;
using Access.Sql;
using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore;

namespace Access.Data.Access
{
    internal sealed class RoleAccess : IRoleAccess
    {
        private readonly DataContext _context;
        private readonly IRoleDataAccessBuilder _roleDataAccessBuilder;

        public RoleAccess(DataContext context, IRoleDataAccessBuilder roleDataAccessBuilder)
        {
            _context = context;
            _roleDataAccessBuilder = roleDataAccessBuilder;
        }

        public async Task<RoleAccessModel> CreateAccessAsync(CreateRoleAccessRequest accessRequest)
        {
            var entity = _roleDataAccessBuilder.MapCreateRoleAccessRequestToRole(accessRequest);
            _context.Roles.Add(entity);
            await _context.SaveChangesAsync();
            return _roleDataAccessBuilder.MapRoleToRoleAccessModel(entity);
        }

        public async Task<IEnumerable<RoleAccessModel>> GetAllAccessAsync()
        {
            var entities = await _context.Roles
                                        .Include(x => x.RolePermissions)
                                        .AsNoTracking()
                                        .ToListAsync();

            var accessModelList = entities.Select(_roleDataAccessBuilder.MapRoleToRoleAccessModel);
            return accessModelList;
        }

        public async Task<RoleAccessModel> GetRoleByCodeAccessAsync(string code)
        {
            var entity = await GetRoleByCodeAsync(code);
            var accessModel = _roleDataAccessBuilder.MapRoleToRoleAccessModel(entity);
            return accessModel;
        }

        public async Task<IEnumerable<RoleAccessModel>> GetRolesByUserIdAsync(string userId)
        {
            var userRoles = await _context.UserRoles
                                .Include(x => x.Role)
                                .Where(x => x.UserId == new Guid(userId))
                                .ToListAsync();

            var roles = userRoles.Any() ?
                userRoles.Select(x => _roleDataAccessBuilder.MapRoleToRoleAccessModel(x.Role)) :
                throw new ArgumentException($"Usuario Id: {userId}");

            return roles;
        }

        public async Task<RoleAccessModel> UpdateAccessAsync(UpdateRoleAccessRequest accessRequest)
        {
            var entity = await GetRoleByCodeAsync(accessRequest.Code);
            var entityPermissions = entity.RolePermissions.Select(x => x.Name);
            var persistPermissions = entity.RolePermissions.Where(x => accessRequest.Permissions.Contains(x.Name));
            var permissions = accessRequest.Permissions
                                    .Where(x => !entityPermissions.Contains(x))
                                    .Select(x => new Permission { Id = Guid.NewGuid(), Name = x, RoleId = entity.Id, Active = true });

            permissions = permissions.Concat(persistPermissions);
            entity = _roleDataAccessBuilder.MapUpdateRoleAccessRequestToRole(accessRequest, entity);
            entity.RolePermissions = permissions.ToList();
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var accessModel = _roleDataAccessBuilder.MapRoleToRoleAccessModel(entity);
            return accessModel;
        }

        private async Task<Role> GetRoleByCodeAsync(string code)
        {
            var entity = await _context.Set<Role>()
                            .Include(x => x.RolePermissions)
                            .SingleOrDefaultAsync(x => x.Code == code);

            return entity ?? throw new KeyNotFoundException($"code {code}");
        }
    }
}

using Access.Contract.Roles;
using Access.Sql;
using Access.Sql.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Access.Data.Access
{
    internal class RoleAccess : IRoleAccess
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public RoleAccess(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<RoleAccessModel> CreateAccessAsync(CreateRoleAccessRequest accessRequest)
        {
            var entity = _mapper.Map<Role>(accessRequest);
            _context.Roles.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<RoleAccessModel>(entity);
        }

        public async Task<IEnumerable<RoleAccessModel>> GetAllAccessAsync()
        {
            var entities = await _context.Roles
                                        .Include(x => x.RolePermissions)
                                        .AsNoTracking()
                                        .ToListAsync();
            var respose = _mapper.Map<IEnumerable<RoleAccessModel>>(entities);
            return respose;
        }

        public async Task<RoleAccessModel> GetRoleByCodeAccessAsync(string code)
        {
            var entity = await GetRoleByCodeAsync(code);
            var respose = _mapper.Map<RoleAccessModel>(entity);
            return respose;
        }

        public async Task<RoleAccessModel> UpdateAccessAsync(UpdateRoleAccessRequest accessRequest)
        {
            var entity = await GetRoleByCodeAsync(accessRequest.Code);
            var entityPermissions = entity.RolePermissions.Select(x => x.Name);
            var persistPermissions = entity.RolePermissions.Where(x => accessRequest.Permissions.Contains(x.Name));
            var permissions = accessRequest.Permissions.Where(x => !entityPermissions.Contains(x)).Select(x => new Permission { Id = Guid.NewGuid(), Name = x, RoleId = entity.Id, Active = true });
            permissions = permissions.Concat(persistPermissions);
            entity = _mapper.Map(accessRequest, entity);
            entity.RolePermissions = permissions.ToList();
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var respose = _mapper.Map<RoleAccessModel>(entity);
            return respose;
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

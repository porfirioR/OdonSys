using Access.Contract.Roles;
using Access.Sql;
using Access.Sql.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            _context.Entry(entity).State = EntityState.Added;
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
            entity = _mapper.Map(accessRequest, entity);
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

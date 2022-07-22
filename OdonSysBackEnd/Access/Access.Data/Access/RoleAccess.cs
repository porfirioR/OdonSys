using Access.Contract.Roles;
using Access.Sql;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Task<RoleAccessModel> CreateAccessAsync(CreateRoleAccessRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RoleAccessModel>> GetAllAccessAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RoleAccessModel> GetRoleByCodeAccessAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task<RoleAccessModel> UpdateAccessAsync(UpdateRoleAccessModel request)
        {
            throw new NotImplementedException();
        }
    }
}

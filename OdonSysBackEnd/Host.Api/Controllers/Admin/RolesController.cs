using AutoMapper;
using Contract.Admin.Roles;
using Host.Api.Models.Auth;
using Host.Api.Models.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Host.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : OdonSysBaseController
    {
        private readonly IMapper _mapper;
        private readonly IRoleManager _roleManager;

        public RolesController(IMapper mapper, IRoleManager clientManager)
        {
            _mapper = mapper;
            _roleManager = clientManager;
        }

        [HttpPost]
        [Authorize(Policy = Policy.CanManageRole)]
        public async Task<RoleModel> Create([FromBody] CreateRoleApiRequest apiRequest)
        {
            var request = _mapper.Map<CreateRoleRequest>(apiRequest);
            var model = await _roleManager.CreateAsync(request);
            return model;
        }

        [HttpPut]
        [Authorize(Policy = Policy.CanManageRole)]
        public async Task<RoleModel> Update([FromBody] UpdateRoleApiRequest apiRequest)
        {
            var request = _mapper.Map<UpdateRoleRequest>(apiRequest);
            var model = await _roleManager.UpdateAsync(request);
            return model;
        }

        [HttpGet]
        [Authorize(Policy = Policy.CanAccessRole)]
        public async Task<IEnumerable<RoleModel>> GetAll()
        {
            var model = await _roleManager.GetAllAsync();
            return model;
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Policy.CanAccessRole)]
        public async Task<RoleModel> GetRoleByCode(string code)
        {
            var model = await _roleManager.GetRoleByCodeAsync(code);
            return model;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<string>> GetPermissionsByRoles()
        {
            var roles = Roles;
            var model = await _roleManager.GetPermissonsByRolesAsync(roles);
            return model;
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<PermissionModel> GetPermissions()
        {
            var model = _roleManager.GetAllPermissions();
            return model;
        }
    }
}

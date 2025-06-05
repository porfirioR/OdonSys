using Contract.Administration.Roles;
using Contract.Administration.Users;
using Host.Api.Contract.Authorization;
using Host.Api.Contract.MapBuilders;
using Host.Api.Contract.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Host.Api.Controllers.Administration;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public sealed class RolesController : OdonSysBaseController
{
    private readonly IRoleManager _roleManager;
    private readonly IRoleHostBuilder _roleHostBuilder;
    private readonly IUserManager _userManager;

    public RolesController(IRoleManager roleManager, IRoleHostBuilder roleHostBuilder, IUserManager userManager)
    {
        _roleManager = roleManager;
        _roleHostBuilder = roleHostBuilder;
        _userManager = userManager;
    }

    [HttpPost]
    [Authorize(Policy = Policy.CanManageRole)]
    public async Task<RoleModel> Create([FromBody] CreateRoleApiRequest apiRequest)
    {
        var request = _roleHostBuilder.MapCreateRoleApiRequestToCreateRoleRequest(apiRequest);
        var model = await _roleManager.CreateAsync(request);
        return model;
    }

    [HttpPut]
    [Authorize(Policy = Policy.CanManageRole)]
    public async Task<RoleModel> Update([FromBody] UpdateRoleApiRequest apiRequest)
    {
        var request = _roleHostBuilder.MapUpdateRoleApiRequestToUpdateRoleRequest(apiRequest);
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

    [HttpGet("{code}")]
    [Authorize(Policy = Policy.CanAccessRole)]
    public async Task<RoleModel> GetRoleByCode([FromRoute] string code)
    {
        var model = await _roleManager.GetRoleByCodeAsync(code);
        return model;
    }

    [HttpGet("permissions-role")]
    public async Task<IEnumerable<string>> GetPermissionsByRoleCodes()
    {
        var id = string.IsNullOrEmpty(UserId) ? await _userManager.GetInternalUserIdByExternalUserIdAsync(UserIdAadB2C) : UserId;
        var model = await _roleManager.GetPermissionsByUserIdAsync(id);
        return model;
    }

    [HttpGet("permissions")]
    public IEnumerable<PermissionModel> GetPermissions()
    {
        var model = _roleManager.GetAllPermissions();
        return model;
    }
}

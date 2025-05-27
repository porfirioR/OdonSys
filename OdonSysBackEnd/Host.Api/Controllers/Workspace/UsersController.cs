using Contract.Administration.Users;
using Host.Api.Contract.Authorization;
using Host.Api.Contract.Error;
using Host.Api.Contract.MapBuilders;
using Host.Api.Contract.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Host.Api.Controllers.Workspace;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public sealed class UsersController : OdonSysBaseController
{
    private readonly IUserManager _userManager;
    private readonly IUserHostBuilder _userHostBuilder;

    public UsersController(IUserManager userManager, IUserHostBuilder userHostBuilder)
    {
        _userManager = userManager;
        _userHostBuilder = userHostBuilder;
    }

    [HttpPost("approve/{id}")]
    [Authorize(Policy = Policy.CanApproveDoctor)]
    public async Task<UserModel> ApproveNewUser([FromRoute] string id)
    {
        var model = await _userManager.ApproveNewUserAsync(id);
        return model;
    }

    [HttpGet]
    [Authorize(Policy = Policy.CanAccessDoctor)]
    public async Task<IEnumerable<DoctorModel>> GetAll()
    {
        var response = await _userManager.GetAllAsync();
        return response;
    }

    [HttpPatch("{id}")]
    [Authorize(Policy = Policy.CanModifyVisibilityDoctor)]
    public async Task<DoctorModel> PatchDoctor([FromRoute] string id, [FromBody] JsonPatchDocument<UpdateDoctorRequest> patchDoctor)
    {
        if (patchDoctor == null) throw new Exception(JsonConvert.SerializeObject(new ApiException(400, "Valor inválido", "No puede estar vacío.")));
        var updateDoctorRequest = _userHostBuilder.MapDoctorModelToUpdateDoctorRequest(await _userManager.GetByIdAsync(id));
        patchDoctor.ApplyTo(updateDoctorRequest);
        if (!ModelState.IsValid)
        {
            throw new Exception(JsonConvert.SerializeObject(new ApiException(400, "Valor inválido", "Valor inválido.")));
        }
        var model = await _userManager.UpdateAsync(updateDoctorRequest);
        return model;
    }

    // TODO hard delete if not associated with patients and other references

    [HttpPost("user-roles")]
    [Authorize(Policy = Policy.CanAssignDoctorRoles)]
    public async Task<IEnumerable<string>> UpdateUserRoles([FromBody] UserRolesApiRequest apiRequest)
    {
        var request = new UserRolesRequest(apiRequest.UserId, apiRequest.Roles);
        var roles = await _userManager.SetUserRolesAsync(request);
        return roles;
    }
}

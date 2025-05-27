using Contract.Workspace.Orthodontics;
using Host.Api.Contract.Authorization;
using Host.Api.Contract.MapBuilders;
using Host.Api.Contract.Orthodontics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Host.Api.Controllers.Workspace;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public sealed class OrthodonticsController : OdonSysBaseController
{
    private readonly IOrthodonticManager _orthodonticManager;
    private readonly IOrthodonticHostBuilder _orthodonticHostBuilder;

    public OrthodonticsController(IOrthodonticManager orthodonticManager, IOrthodonticHostBuilder orthodonticHostBuilder)
    {
        _orthodonticManager = orthodonticManager;
        _orthodonticHostBuilder = orthodonticHostBuilder;
    }

    [HttpPut("{id}")]
    [Authorize(Policy = Policy.CanUpdateOrthodontic)]
    public async Task<OrthodonticModel> Update([FromRoute] string id, [FromBody] OrthodonticApiRequest apiRequest)
    {
        var request = _orthodonticHostBuilder.MapOrthodonticApiRequestToOrthodonticRequest(apiRequest, id);
        var model = await _orthodonticManager.UpsertOrthodontic(request);
        return model;
    }

    [HttpPost]
    [Authorize(Policy = Policy.CanCreateOrthodontic)]
    public async Task<OrthodonticModel> Create([FromBody] OrthodonticApiRequest apiRequest)
    {
        var request = _orthodonticHostBuilder.MapOrthodonticApiRequestToOrthodonticRequest(apiRequest, null);
        var model = await _orthodonticManager.UpsertOrthodontic(request);
        return model;
    }

    [HttpGet]
    [Authorize(Policy = Policy.CanAccessAllOrthodontics)]
    public async Task<IEnumerable<OrthodonticModel>> GetAll()
    {
        var models = await _orthodonticManager.GetAllAsync();
        return models;
    }

    [HttpGet("patient-orthodontics/{clientId}")]
    [Authorize(Policy = Policy.CanAccessOrthodontic)]
    public async Task<IEnumerable<OrthodonticModel>> GetPatientOrthodonticsById([FromRoute] string clientId)
    {
        var models = await _orthodonticManager.GetAllByClientIdAsync(clientId);
        return models;
    }
}

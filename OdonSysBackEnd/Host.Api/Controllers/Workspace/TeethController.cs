using Contract.Workspace.Teeth;
using Host.Api.Contract.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Host.Api.Controllers.Workspace
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public sealed class TeethController : ControllerBase
    {
        private readonly IToothManager _toothManager;

        public TeethController(IToothManager toothManager)
        {
            _toothManager = toothManager;
        }

        [HttpGet]
        [Authorize(Policy = Policy.CanAccessTeeth)]
        public async Task<IEnumerable<ToothModel>> GetAll()
        {
            var modelList = await _toothManager.GetAllAsync();
            return modelList;
        }
    }
}

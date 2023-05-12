using Contract.Workspace.Teeth;
using Host.Api.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Host.Api.Controllers.Workspace
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TeethController : ControllerBase
    {
        private readonly IToothManager _toothManager;

        public TeethController(IToothManager toothManager)
        {
            _toothManager = toothManager;
        }

        [HttpGet]
        [Authorize(Policy = Policy.CanAccessProcedure)]
        public async Task<IEnumerable<ToothModel>> GetAll()
        {
            var response = await _toothManager.GetAllAsync();
            return response;
        }
    }
}

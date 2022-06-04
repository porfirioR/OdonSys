using Contract.Procedure.Teeth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<IEnumerable<ToothModel>> GetAll()
        {
            var response = await _toothManager.GetAllAsync();
            return response;
        }
    }
}

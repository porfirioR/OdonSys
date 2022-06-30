using Contract.Authentication.User;
using Contract.Workspace.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Host.Api.Controllers.Workspace
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UsersController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("approve/{id}")]
        public async Task<UserModel> ApproveNewUser([FromRoute] string id)
        {
            var model = await _userManager.ApproveNewUserAsync(id);
            return model;
        }

        [HttpGet]
        public async Task<IEnumerable<DoctorModel>> GetAll()
        {
            var response = await _userManager.GetAllAsync();
            return response;
        }

        [HttpPut("deactivate/{id}")]
        public async Task<DoctorModel> Deactivate([FromRoute] string id)
        {
            return await _userManager.DeactivateRestoreAsync(id);
        }

        [HttpPut("activate/{id}")]
        public async Task<DoctorModel> Activate([FromRoute] string id)
        {
            return await _userManager.DeactivateRestoreAsync(id);
        }
        // TODO hard delete if not associated with patients and other references
    }
}

using Contract.Administration.Clients;
using Contract.Administration.Users;
using Host.Api.Contract.Authorization;
using Host.Api.Contract.Clients;
using Host.Api.Contract.MapBuilders;
using Host.Api.Contract.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Host.Api.Controllers.Workspace
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public sealed class DoctorsController : ControllerBase
    {
        private readonly IClientManager _clientManager;
        private readonly IUserManager _userManager;
        private readonly IUserHostBuilder _userHostBuilder;

        public DoctorsController(
            IClientManager clientManager,
            IUserManager userManager,
            IUserHostBuilder userHostBuilder
        )
        {
            _clientManager = clientManager;
            _userManager = userManager;
            _userHostBuilder = userHostBuilder;
        }

        [HttpPost]
        [Authorize(Policy = Policy.CanAssignClient)]
        public async Task<IEnumerable<ClientModel>> AssignClientToUser([FromBody] AssignClientApiRequest apiRequest)
        {
            var user = new AssignClientRequest(apiRequest.UserId, apiRequest.ClientId);
            var model = await _clientManager.AssignClientToUser(user);
            return model;
        }

        [HttpPut]
        [Authorize(Policy = Policy.CanUpdateDoctor)]
        public async Task<DoctorModel> Update([FromBody] UpdateDoctorApiRequest apiRequest)
        {
            var user = _userHostBuilder.MapUpdateDoctorApiRequestToUpdateDoctorRequest(apiRequest);
            var model = await _userManager.UpdateAsync(user);
            return model;
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Policy.CanAccessDoctor)]
        public async Task<DoctorModel> GetById([FromRoute] string id)
        {
            var doctorModel = await _userManager.GetByIdAsync(id);
            return doctorModel;
        }

    }
}

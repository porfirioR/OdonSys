using AutoMapper;
using Contract.Admin.Clients;
using Contract.Admin.Users;
using Host.Api.Models.Auth;
using Host.Api.Models.Clients;
using Host.Api.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Host.Api.Controllers.Workspace
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IClientManager _clientManager;
        private readonly IUserManager _userManager;

        public DoctorsController(
            IMapper mapper,
            IClientManager clientManager,
            IUserManager userManager)
        {
            _mapper = mapper;
            _clientManager = clientManager;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize(Policy = Policy.CanAssignClient)]
        public async Task<IEnumerable<ClientModel>> AssignClientToDoctor([FromBody] AssignClientApiRequest apiRequest)
        {
            var user = new AssignClientRequest(apiRequest.UserId, apiRequest.ClientId);
            var model = await _clientManager.AssignClientToDoctor(user);
            return model;
        }

        [HttpPut]
        [Authorize(Policy = Policy.CanUpdateDoctor)]
        public async Task<DoctorModel> Update([FromBody] UpdateDoctorApiRequest apiRequest)
        {
            var user = _mapper.Map<UpdateDoctorRequest>(apiRequest);
            var response = await _userManager.UpdateAsync(user);
            return response;
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Policy.CanAccessDoctor)]
        public async Task<DoctorModel> GetById([FromRoute] string id)
        {
            var response = await _userManager.GetByIdAsync(id);
            return response;
        }

    }
}

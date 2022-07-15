using AutoMapper;
using Contract.Admin.Clients;
using Contract.Workspace.User;
using Host.Api.Models.Clients;
using Host.Api.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<ClientModel> AssignClientToDoctor([FromBody] AssignClientApiRequest apiRequest)
        {
            var user = _mapper.Map<CreateClientRequest>(apiRequest);
            var model = await _clientManager.CreateAsync(user);
            return model;
        }

        [HttpPut]
        public async Task<DoctorModel> Update([FromBody] UpdateDoctorApiRequest apiRequest)
        {
            var user = _mapper.Map<UpdateDoctorRequest>(apiRequest);
            var response = await _userManager.UpdateAsync(user);
            return response;
        }

        [HttpGet("{id}")]
        public async Task<DoctorModel> GetById([FromRoute] string id)
        {
            var response = await _userManager.GetByIdAsync(id);
            return response;
        }

    }
}

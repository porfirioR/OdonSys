using AutoMapper;
using Contract.Authentication.User;
using Contract.Workspace.User;
using Host.Api.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Host.Api.Controllers.Workspace
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;

        public UserController(IMapper mapper, IUserManager userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost("approve/{id}")]
        public async Task<DoctorModel> ApproveNewUser([FromRoute] string id)
        {
            var model = await _userManager.ApproveNewUserAsync(id);
            return model;
        }

        [HttpDelete("{id}")]
        public async Task<UserModel> Deactivate([FromRoute] string id)
        {
            var response = await _userManager.DeactivateAsync(id);
            return response;
        }

        [HttpGet]
        public async Task<IEnumerable<DoctorModel>> GetAll()
        {
            var response = await _userManager.GetAllAsync();
            return response;
        }

        [HttpGet("{id}")]
        public async Task<DoctorModel> GetById([FromRoute] string id)
        {
            var response = await _userManager.GetByIdAsync(id);
            return response;
        }

        [HttpPut]
        public async Task<DoctorModel> Update([FromBody] UpdateDoctorApiRequest apiRequest)
        {
            var user = _mapper.Map<UpdateDoctorRequest>(apiRequest);
            var response = await _userManager.UpdateAsync(user);
            return response;
        }

    }
}

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
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;
        public UsersController(IMapper mapper, IUserManager userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpDelete("{id}")]
        public async Task<UserModel> Deactivate(string id)
        {
            var response = await _userManager.DeactivateAsync(id);
            return response;
        }

        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetAll()
        {
            var response = await _userManager.GetAllAsync();
            return response;
        }

        [HttpGet("{id}")]
        public async Task<UserModel> GetById(string id)
        {
            var response = await _userManager.GetByIdAsync(id);
            return response;
        }

        [HttpPut]
        public async Task<UserModel> Update([FromBody] UpdateUserApiRequest userDTO)
        {
            var user = _mapper.Map<UpdateUserRequest>(userDTO);
            var response = await _userManager.UpdateAsync(user);
            return response;
        }

        [HttpPost("approve/{id}")]
        public async Task<UserModel> ApproveNewUser([FromRoute] string id)
        {
            var model = await _userManager.ApproveNewUserAsync(id);
            return model;
        }

    }
}

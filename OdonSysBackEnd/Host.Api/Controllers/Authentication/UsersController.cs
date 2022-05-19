using AutoMapper;
using Contract.Authentication.User;
using Host.Api.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Host.Api.Controllers.Authentication
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

        [HttpPost]
        public async Task<UserModel> RegisterUser([FromBody] RegisterUserApiRequest apiRequest)
        {
            var user = _mapper.Map<RegisterUserRequest>(apiRequest);
            var model = await _userManager.RegisterUserAsync(user);
            return model;
        }

        [HttpDelete("{id}")]
        public async Task<UserModel> Delete(string id)
        {
            var response = await _userManager.Delete(id);
            return response;
        }

        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetAll()
        {
            var response = await _userManager.GetAll();
            return response;
        }

        [HttpGet("{id}")]
        public async Task<UserModel> GetById(string id)
        {
            var response = await _userManager.GetById(id);
            return response;
        }

        [HttpPut]
        public async Task<UserModel> Update([FromBody] UpdateUserApiRequest userDTO)
        {
            var user = _mapper.Map<UpdateUserRequest>(userDTO);
            var response = await _userManager.Update(user);
            return response;
        }

    }
}

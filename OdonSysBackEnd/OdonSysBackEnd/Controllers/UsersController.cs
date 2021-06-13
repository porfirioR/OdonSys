using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OdonSysBackEnd.Models.Users;
using Resources.Contract.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdonSysBackEnd.Controllers
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
        public async Task<ActionResult<UserModel>> Create([FromBody] CreateUserApiRequest apiRequest)
        {
            var user = _mapper.Map<CreateUserRequest>(apiRequest);
            var model = await _userManager.Create(user);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserModel>> Delete(string id)
        {
            var response = await _userManager.Delete(id);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAll()
        {
            var response = await _userManager.GetAll();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetById(string id)
        {
            var response = await _userManager.GetById(id);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<UserModel>> Update([FromBody] UpdateUserApiRequest userDTO)
        {
            var user = _mapper.Map<UpdateUserRequest>(userDTO);
            var response = await _userManager.Update(user);
            return Ok(response);
        }

    }
}

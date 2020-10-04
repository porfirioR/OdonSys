using Microsoft.AspNetCore.Mvc;
using OdonSys.Api.Main.DTO.Users;
using OdonSys.Api.Main.Flow.Interfaces;

namespace OdonSys.Api.Main.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserFlow _userFlow;

        public UsersController(IUserFlow userFlow)
        {
            _userFlow = userFlow;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userFlow.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(string id)
        {
            var user = _userFlow.GetById(id);
            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserDTO userDTO)
        {
            var response = _userFlow.CreateAsync(userDTO);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UpdateUserDTO userDTO)
        {
            var response = _userFlow.UpdateAsync(userDTO);
            return Ok(response);
        }

        [HttpPatch("id")]
        public IActionResult ActiveUser(string id, [FromBody] UpdateUserDTO userDTO)
        {
            var response = _userFlow.UpdateAsync(userDTO);
            return Ok(response);
        }
    }
}

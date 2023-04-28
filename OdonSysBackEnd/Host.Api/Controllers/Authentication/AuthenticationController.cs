using AutoMapper;
using Contract.Admin.Auth;
using Contract.Admin.Users;
using Host.Api.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Host.Api.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;

        public AuthenticationController(IMapper mapper, IUserManager userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<AuthModel> Login([FromHeader] LoginApiRequest loginApiRequest)
        {
            var model = await _userManager.LoginAsync(loginApiRequest.Authorization);
            return model;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<AuthModel> Register([FromBody] RegisterUserApiRequest apiRequest)
        {
            var register = _mapper.Map<RegisterUserRequest>(apiRequest);
            var model = await _userManager.RegisterUserAsync(register);
            return model;
        }

    }
}

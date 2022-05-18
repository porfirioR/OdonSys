using AutoMapper;
using Contract.Authentication.User;
using Host.Api.Models.Users;
using Microsoft.AspNetCore.Mvc;
using OdonSysBackEnd.Models.Auth;
using System.Threading.Tasks;

namespace Host.Api.Controllers
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

        [HttpPost("login")]
        public async Task<UserModel> Login([FromBody] LoginApiRequest loginApiRequest)
        {
            var login = _mapper.Map<LoginRequest>(loginApiRequest);
            var model = await _userManager.Login(login);
            return model;
        }

        [HttpPost("register")]
        public async Task<UserModel> Register([FromBody] RegisterUserApiRequest apiRequest)
        {
            var login = _mapper.Map<LoginRequest>(apiRequest);
            var model = await _userManager.Login(login);
            return model;
        }

    }
}

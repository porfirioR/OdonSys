using Access.Contract.Users;
using Contract.Administration.Authentication;
using Contract.Administration.Users;
using Host.Api.Contract.Authorization;
using Host.Api.Contract.MapBuilders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Utilities;

namespace Host.Api.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class AuthenticationController : OdonSysBaseController
    {
        private readonly IUserManager _userManager;
        private readonly IUserHostBuilder _userHostBuilder;

        public AuthenticationController(IUserManager userManager, IUserHostBuilder userHostBuilder)
        {
            _userManager = userManager;
            _userHostBuilder = userHostBuilder;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<AuthenticationModel> Login([FromHeader] LoginApiRequest loginApiRequest)
        {
            var model = await _userManager.LoginAsync(loginApiRequest.Authorization);
            return model;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<AuthenticationModel> Register([FromBody] RegisterUserApiRequest apiRequest)
        {
            var register = _userHostBuilder.MapRegisterUserApiRequestToRegisterUserRequest(apiRequest);
            var model = await _userManager.RegisterUserAsync(register);
            return model;
        }

        [Authorize]
        [HttpPost("register-user")]
        public async Task<UserModel> RegisterAzureAdB2C()
        {
            var userId = UserIdAadB2C;
            var userModel = await _userManager.RegisterUserAsync(userId);
            return userModel;
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<UserModel> GetUserProfile()
        {
            var userId = UserIdAadB2C;
            var model = await _userManager.GetUserFromGraphApiByIdAsync(userId);
            return model;
        }

        [Authorize]
        [HttpGet("logout")]
        public LogoutModel Logout()
        {
            var user = Username;
            var success = _userManager.RemoveAllClaims(User);
            return new LogoutModel(success, user);
        }

    }
}

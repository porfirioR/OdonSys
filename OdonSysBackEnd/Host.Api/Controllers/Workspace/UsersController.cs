using AutoMapper;
using Contract.Authentication.User;
using Contract.Workspace.User;
using Host.Api.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Host.Api.Controllers.Workspace
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UsersController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("approve/{id}")]
        public async Task<UserModel> ApproveNewUser([FromRoute] string id)
        {
            var model = await _userManager.ApproveNewUserAsync(id);
            return model;
        }

        [HttpGet]
        public async Task<IEnumerable<DoctorModel>> GetAll()
        {
            var response = await _userManager.GetAllAsync();
            return response;
        }
        // TODO hard delete if not associated with patients and other references
    }
}

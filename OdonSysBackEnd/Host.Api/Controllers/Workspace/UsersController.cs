using AutoMapper;
using Contract.Admin.Users;
using Host.Api.Models.Auth;
using Host.Api.Models.Error;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
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
        private readonly IMapper _mapper;

        public UsersController(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost("approve/{id}")]
        [Authorize(Policy = Policy.CanApproveDoctor)]
        public async Task<UserModel> ApproveNewUser([FromRoute] string id)
        {
            var model = await _userManager.ApproveNewUserAsync(id);
            return model;
        }

        [HttpGet]
        [Authorize(Policy = Policy.CanAccessDoctor)]
        public async Task<IEnumerable<DoctorModel>> GetAll()
        {
            var response = await _userManager.GetAllAsync();
            return response;
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = Policy.CanDeleteDoctor)]
        public async Task<DoctorModel> PatchDoctor([FromRoute] string id, [FromBody] JsonPatchDocument<UpdateDoctorRequest> patchDoctor)
        {
            if (patchDoctor == null) throw new Exception(JsonConvert.SerializeObject(new ApiException(400, "Valor invalido", "No puede ser null.")));
            var updateDoctorRequest = _mapper.Map<UpdateDoctorRequest>(await _userManager.GetByIdAsync(id));
            patchDoctor.ApplyTo(updateDoctorRequest);
            if (!ModelState.IsValid)
            {
                throw new Exception(JsonConvert.SerializeObject(new ApiException(400, "Valor invalido", "Valor invalido.")));
            }
            var model = await _userManager.UpdateAsync(updateDoctorRequest);
            return model;
        }
        // TODO hard delete if not associated with patients and other references
    }
}

using AutoMapper;
using Contract.Admin.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Host.Api.Controllers.Workspace
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDataController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IClientManager _clientManager;
        public UserDataController(IMapper mapper, IClientManager clientManager)
        {
            _mapper = mapper;
            _clientManager = clientManager;
        }
    }
}

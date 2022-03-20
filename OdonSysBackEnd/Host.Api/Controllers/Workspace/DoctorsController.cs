using AutoMapper;
using Contract.Admin.Clients;
using Host.Api.Models.Clients;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Host.Api.Controllers.Workspace
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IClientManager _clientManager;
        public DoctorsController(IMapper mapper, IClientManager clientManager)
        {
            _mapper = mapper;
            _clientManager = clientManager;
        }

        [HttpPost]
        public async Task<ClientModel> AssignClientToDoctor([FromBody] AssignClientApiRequest apiRequest)
        {
            var user = _mapper.Map<CreateClientRequest>(apiRequest);
            var model = await _clientManager.CreateAsync(user);
            return model;
        }
    }
}

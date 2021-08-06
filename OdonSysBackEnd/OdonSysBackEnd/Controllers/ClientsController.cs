using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OdonSysBackEnd.Models.Clients;
using Resources.Contract.Clients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdonSysBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IClientManager _clientManager;
       
        public ClientsController(IMapper mapper, IClientManager userManager)
        {
            _mapper = mapper;
            _clientManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<ClientAdminModel>> Create([FromBody] CreateClientApiRequest apiRequest)
        {
            var user = _mapper.Map<CreateClientRequest>(apiRequest);
            var model = await _clientManager.Create(user);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _clientManager.Delete(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientAdminModel>>> GetAll()
        {
            var response = await _clientManager.GetAll();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientAdminModel>> GetById(string id)
        {
            var response = await _clientManager.GetById(id);
            return Ok(response);
        }

        //[HttpPut]
        //public async Task<ActionResult<ClientAdminModel>> Update([FromBody] UpdateClientApiRequest userDTO)
        //{
        //    var user = _mapper.Map<UpdateClientRequest>(userDTO);
        //    var response = await _clientManager.Update(user);
        //    return Ok(response);
        //}
    }
}

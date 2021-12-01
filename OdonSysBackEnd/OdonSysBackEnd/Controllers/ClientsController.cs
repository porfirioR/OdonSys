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
        public async Task<ClientModel> Create([FromBody] CreateClientApiRequest apiRequest)
        {
            var user = _mapper.Map<CreateClientRequest>(apiRequest);
            var model = await _clientManager.CreateAsync(user);
            return model;
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _clientManager.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<ClientModel>> GetAll()
        {
            var response = await _clientManager.GetAllAsync();
            return response;
        }

        [HttpGet("{id}")]
        public async Task<ClientModel> GetById(string id)
        {
            var response = await _clientManager.GetByIdAsync(id);
            return response;
        }

        [HttpPut]
        public async Task<ClientModel> Update([FromBody] UpdateClientApiRequest apiRequest)
        {
            var user = _mapper.Map<UpdateClientRequest>(apiRequest);
            var response = await _clientManager.UpdateAsync(user);
            return response;
        }
    }
}

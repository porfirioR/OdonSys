using AutoMapper;
using Contract.Procedure.Procedures;
using Microsoft.AspNetCore.Mvc;

namespace Host.Api.Controllers.Procedure
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMapper _mapper;
        IProcedureManager _procedureManager;
        public ValuesController(IMapper mapper, IProcedureManager procedureManager)
        {
            _mapper = mapper;
            _procedureManager = procedureManager;
        }
    }
}

using Access.Contract.Teeth;
using AutoMapper;
using Contract.Procedure.Teeth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager.Procedure.Teeth
{
    internal class ToothManager : IToothManager
    {
        private readonly IToothAccess _toothAccess;
        private readonly IMapper _mapper;
        public ToothManager(IToothAccess procedureAccess, IMapper mapper)
        {
            _toothAccess = procedureAccess;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ToothModel>> GetAllAsync()
        {
            var accessResponse = await _toothAccess.GetAllAsync();
            return _mapper.Map<IEnumerable<ToothModel>>(accessResponse);
        }
    }
}

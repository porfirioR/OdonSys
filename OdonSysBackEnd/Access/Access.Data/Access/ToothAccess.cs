using Access.Contract;
using Access.Contract.Teeth;
using Access.Sql;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Access.Data.Access
{
    internal class ToothAccess : IToothAccess
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public ToothAccess(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<ToothAccessResponse>> GetAllAsync()
        {
            var entities = await _context.Teeth.AsNoTracking().ToListAsync();
            var respose = _mapper.Map<IEnumerable<ToothAccessResponse>>(entities);
            return respose;
        }
    }
}

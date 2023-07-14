using Access.Contract.Teeth;
using Access.Sql;

namespace Access.Data.Access
{
    internal sealed class ToothAccess : IToothAccess
    {
        private readonly DataContext _context;
        public ToothAccess(DataContext context)
        {
            _context = context;
        }

        //public async Task<IEnumerable<ToothAccessResponse>> GetAllAsync()
        //{
        //    var entities = await _context.Teeth.AsNoTracking().ToListAsync();
        //    var respose = _mapper.Map<IEnumerable<ToothAccessResponse>>(entities);
        //    return respose;
        //}
    }
}

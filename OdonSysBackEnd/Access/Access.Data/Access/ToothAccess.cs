using Access.Contract.Teeth;
using Access.Sql;
using Microsoft.EntityFrameworkCore;

namespace Access.Data.Access
{
    internal sealed class ToothAccess : IToothAccess
    {
        private readonly DataContext _context;
        private readonly IToothDataAccessBuilder _toothAccessBuilder;

        public ToothAccess(DataContext context, IToothDataAccessBuilder toothAccessBuilder)
        {
            _context = context;
            _toothAccessBuilder = toothAccessBuilder;
        }

        public async Task<IEnumerable<ToothAccessModel>> GetAllAsync()
        {
            var accessModelList = await _context.Teeth
                                            .AsNoTrackingWithIdentityResolution()
                                            .Select(x => _toothAccessBuilder.MapToothToToothAccessModel(x))
                                            .ToListAsync();

            return accessModelList;
        }
    }
}

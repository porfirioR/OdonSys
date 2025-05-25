using Access.Contract.Orthodontics;
using Access.Sql;

namespace Access.Data.Access
{
    internal class OrthodonticAccess : IOrthodonticAccess
    {
        private readonly DataContext _context;
        private readonly IOrthodonticDataAccessMapper _orthodonticDataAccessBuilder;

        public OrthodonticAccess(DataContext context, IOrthodonticDataAccessMapper orthodonticDataAccessBuilder)
        {
            _context = context;
            _orthodonticDataAccessBuilder = orthodonticDataAccessBuilder;

        }

        public Task<IEnumerable<OrthodonticAccessModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrthodonticAccessModel> GetAllByClientIdAsync(string clientId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrthodonticAccessModel>> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<OrthodonticAccessModel> UpsertOrthodontic(OrthodonticAccessRequest accessRequest)
        {
            throw new NotImplementedException();
        }
    }
}

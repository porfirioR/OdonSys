using Access.Contract.ClientProcedure;
using Access.Sql;
using Access.Sql.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Access.Data.Access
{
    public class ClientProcedureAccess : IClientProcedureAccess
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public ClientProcedureAccess(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ClientProcedureAccessModel> CreateClientProcedureAsync(CreateClientProcedureAccessRequest accessRequest)
        {
            var entity = _mapper.Map<ClientProcedure>(accessRequest);
            accessRequest.Urls.ToList().ForEach(url => _context.Files.Add(new FileStorage { Url = url }));

            _context.ClientProcedures.Add(entity);
            await _context.SaveChangesAsync();
            var result = new ClientProcedureAccessModel(
                entity.Id.ToString(),
                entity.ProcedureId.ToString(),
                entity.UserClientId.ToString()
            );
            return result;
        }

        public async Task<IEnumerable<ClientProcedureAccessModel>> GetClientProceduresByUserClientIdAsync(IEnumerable<Guid> userClientIds)
        {
            var entities = await _context.ClientProcedures
                            .AsNoTracking()
                            .Where(x => userClientIds.Contains(x.UserClientId)).ToListAsync();
            var respose = entities.Select(x => new ClientProcedureAccessModel(
                x.Id.ToString(),
                x.ProcedureId.ToString(),
                x.UserClientId.ToString()));
            return respose;
        }

        public async Task<ClientProcedureAccessModel> UpdateClientProcedureAsync(UpdateClientProcedureAccessRequest accessRequest)
        {
            var entity = await _context.ClientProcedures
                            .FirstAsync(x => x.UserClientId == new Guid(accessRequest.UserClientId) && x.ProcedureId == new Guid(accessRequest.ProcedureId));
            entity = _mapper.Map(accessRequest, entity);
            await _context.SaveChangesAsync();
            var result = new ClientProcedureAccessModel(
                entity.Id.ToString(),
                entity.ProcedureId.ToString(),
                entity.UserClientId.ToString()
            );
            return result;
        }

        public async Task<bool> CheckExistsClientProcedureAsync(string userClientId, string procedureId)
        {
            var entity = await _context.ClientProcedures
                            .SingleOrDefaultAsync(x => x.UserClientId == new Guid(userClientId) && x.ProcedureId == new Guid(procedureId));
            return entity != null;
        }

        public async Task<bool> CheckExistsClientProcedureAsync(string clientProcedureId)
        {
            var entity = await _context.ClientProcedures
                            .SingleOrDefaultAsync(x => x.Id == new Guid(clientProcedureId));
            return entity != null;
        }
    }
}

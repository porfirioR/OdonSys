using Access.Contract.Clients;
using Access.Sql;
using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore;

namespace Access.Data.Access
{
    internal sealed class ClientAccess : IClientAccess
    {
        private readonly DataContext _context;
        private readonly IClientDataBuilder _clientDataBuilder;
        public ClientAccess(DataContext context, IClientDataBuilder clientDataBuilder)
        {
            _context = context;
            _clientDataBuilder = clientDataBuilder;
        }

        public async Task<ClientAccessModel> CreateClientAsync(CreateClientAccessRequest accessRequest)
        {
            var entity = _clientDataBuilder.MapCreateClientAccessRequestToClient(accessRequest);
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return _clientDataBuilder.MapClientToClientAccessModel(entity);
        }

        public async Task<ClientAccessModel> DeleteAsync(string id)
        {
            // TODO: CHANGE TO HARD DELETE, SOFT DELETE IS ON UPDATE CLIENT
            var entity = await _context.Clients.SingleOrDefaultAsync(x => x.Id == new Guid(id));
            entity.Active = false;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _clientDataBuilder.MapClientToClientAccessModel(entity);
        }

        public async Task<IEnumerable<ClientAccessModel>> GetAllAsync()
        {
            var entities = await _context.Clients
                                    .Include(x => x.UserClients).ThenInclude(x => x.User)
                                    .AsNoTracking()
                                    .ToListAsync();
            var modelList = entities.Select(_clientDataBuilder.MapClientToClientAccessModel);
            return modelList;
        }

        public async Task<ClientAccessModel> GetByIdAsync(string id)
        {
            var entity = await GetClientByIdAsync(id);
            var accessModel = _clientDataBuilder.MapClientToClientAccessModel(entity);
            return accessModel;
        }

        public async Task<IEnumerable<ClientAccessModel>> GetClientsByUserIdAsync(string userId, string userName)
        {
            var entities = await _context.Clients
                            .Include(x => x.UserClients)
                            .ThenInclude(x => x.User)
                            .Where(x => x.UserClients.Any(y => y.UserId == new Guid(userId)) || x.UserCreated == userName).ToListAsync();
            var accessModel = entities.Select(_clientDataBuilder.MapClientToClientAccessModel);
            return accessModel;
        }

        public async Task<ClientAccessModel> UpdateClientAsync(UpdateClientAccessRequest accessRequest)
        {
            var entity = await GetClientByIdAsync(accessRequest.Id);
            entity = _clientDataBuilder.MapUpdateClientAccessRequestToClient(accessRequest, entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var accessModel = _clientDataBuilder.MapClientToClientAccessModel(entity);
            return accessModel;
        }

        public async Task<ClientAccessModel> GetByDocumentAsync(string document)
        {
            var entity = await _context.Set<Client>()
                            .SingleOrDefaultAsync(x => x.Document == document);
            var respose = _clientDataBuilder.MapClientToClientAccessModel(entity);
            return respose;
        }

        public async Task<IEnumerable<ClientAccessModel>> AssignClientToUserAsync(AssignClientAccessRequest accessRequest)
        {
            var entity = _clientDataBuilder.MapAssignClientAccessRequestToUserClient(accessRequest);
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return await GetClientsByUserIdAsync(accessRequest.UserId, entity.UserCreated);
        }

        private async Task<Client> GetClientByIdAsync(string id)
        {
            var entity = await _context.Set<Client>()
                            .SingleOrDefaultAsync(x => x.Id == new Guid(id));
            return entity ?? throw new KeyNotFoundException($"id {id}");
        }
    }
}

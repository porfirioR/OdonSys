using Access.Contract.Clients;
using Access.Sql;
using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore;

namespace Access.Data.Access
{
    internal sealed class ClientAccess : IClientAccess
    {
        private readonly DataContext _context;
        private readonly IClientDataAccessBuilder _clientAccessDataBuilder;
        public ClientAccess(DataContext context, IClientDataAccessBuilder clientDataAccessBuilder)
        {
            _context = context;
            _clientAccessDataBuilder = clientDataAccessBuilder;
        }

        public async Task<ClientAccessModel> CreateClientAsync(CreateClientAccessRequest accessRequest)
        {
            var entity = _clientAccessDataBuilder.MapCreateClientAccessRequestToClient(accessRequest);
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return _clientAccessDataBuilder.MapClientToClientAccessModel(entity);
        }

        public async Task<ClientAccessModel> DeleteAsync(string id)
        {
            // TODO: CHANGE TO HARD DELETE, SOFT DELETE IS ON UPDATE CLIENT
            var entity = await _context.Clients.SingleOrDefaultAsync(x => x.Id == new Guid(id));
            entity.Active = false;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _clientAccessDataBuilder.MapClientToClientAccessModel(entity);
        }

        public async Task<IEnumerable<ClientAccessModel>> GetAllAsync()
        {
            var entities = await _context.Clients
                                    .Include(x => x.UserClients).ThenInclude(x => x.User)
                                    .AsNoTracking()
                                    .ToListAsync();
            var modelList = entities.Select(_clientAccessDataBuilder.MapClientToClientAccessModel);
            return modelList;
        }

        public async Task<ClientAccessModel> GetByIdAsync(string id)
        {
            var entity = await GetClientByIdAsync(id);
            var accessModel = _clientAccessDataBuilder.MapClientToClientAccessModel(entity);
            return accessModel;
        }

        public async Task<IEnumerable<ClientAccessModel>> GetClientsByUserIdAsync(string userId, string userName)
        {
            var user = await _context.Users.FirstAsync(x => x.Id == new Guid(userId));
            var entities = await _context.Clients
                            .Include(x => x.UserClients)
                            .ThenInclude(x => x.User)
                            .Where(x => x.UserClients.Any(y => y.UserId == user.Id) || x.UserCreated == userName).ToListAsync();
            var accessModel = entities.Select(_clientAccessDataBuilder.MapClientToClientAccessModel);
            return accessModel;
        }

        public async Task<ClientAccessModel> UpdateClientAsync(UpdateClientAccessRequest accessRequest)
        {
            var entity = await GetClientByIdAsync(accessRequest.Id);
            entity = _clientAccessDataBuilder.MapUpdateClientAccessRequestToClient(accessRequest, entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var accessModel = _clientAccessDataBuilder.MapClientToClientAccessModel(entity);
            return accessModel;
        }

        public async Task<ClientAccessModel> GetByDocumentAsync(string document)
        {
            var entity = await _context.Set<Client>()
                            .SingleOrDefaultAsync(x => x.Document == document);
            var respose = _clientAccessDataBuilder.MapClientToClientAccessModel(entity);
            return respose;
        }

        public async Task<IEnumerable<ClientAccessModel>> AssignClientToUserAsync(AssignClientAccessRequest accessRequest)
        {
            var user = await _context.Users.FirstAsync(x => x.Id == new Guid(accessRequest.UserId) || x.ExternalUserId == accessRequest.UserId);
            var entity = _clientAccessDataBuilder.MapAssignClientAccessRequestToUserClient(accessRequest);
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

        public async Task<bool> IsDuplicateEmailAsync(string email, string id = null)
        {
            if (id != null)
            {
                var existClientWithEmail = await _context.Clients
                                                    .FirstOrDefaultAsync(x => x.Id != new Guid(id) && !string.IsNullOrEmpty(x.Email) && x.Email == email);
                return existClientWithEmail != null;
            }
            var client = await _context.Clients
                                    .FirstOrDefaultAsync(x => !string.IsNullOrEmpty(x.Email) && x.Email == email);
            return client != null;
        }

        public async Task<bool> IsDuplicateDocumentAsync(string document, string id)
        {
            var client = await _context.Clients
                                    .FirstOrDefaultAsync(x => x.Id != new Guid(id) && x.Document == document);
            return client != null;
        }
    }
}

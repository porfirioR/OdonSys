using Access.Contract.Clients;
using Access.Sql;
using Access.Sql.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Access.Data.Access
{
    internal class ClientAccess : IClientAccess
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public ClientAccess(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ClientAccessModel> CreateClientAsync(CreateClientAccessRequest accessRequest)
        {
            var entity = _mapper.Map<Client>(accessRequest);
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return _mapper.Map<ClientAccessModel>(entity);
        }

        public async Task<ClientAccessModel> DeleteAsync(string id)
        {
            // TODO: CHANGE TO HARD DELETE, SOFT DELETE IS ON UPDATE CLIENT
            var entity = await _context.Clients.SingleOrDefaultAsync(x => x.Id == new Guid(id));
            entity.Active = false;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _mapper.Map<ClientAccessModel>(entity);
        }

        public async Task<IEnumerable<ClientAccessModel>> GetAllAsync()
        {
            var entities = await _context.Clients.AsNoTracking().ToListAsync();
            var respose = _mapper.Map<IEnumerable<ClientAccessModel>>(entities);
            return respose;
        }

        public async Task<ClientAccessModel> GetByIdAsync(string id)
        {
            var entity = await GetClientByIdAsync(id);
            var respose = _mapper.Map<ClientAccessModel>(entity);
            return respose;
        }

        public async Task<IEnumerable<ClientAccessModel>> GetClientsByDoctorIdAsync(string id, string userName)
        {
            var entities = await _context.Clients
                                .Include(x => x.DoctorsClients)
                                .ThenInclude(x => x.Doctor)
                                .Where(x => x.DoctorsClients.Any(y => y.DoctorId == new Guid(id)) || x.UserCreated == userName).ToListAsync();
            var respose = _mapper.Map<IEnumerable<ClientAccessModel>>(entities);
            return respose;
        }

        public async Task<ClientAccessModel> UpdateClientAsync(UpdateClientAccessRequest accessRequest)
        {
            var entity = await GetClientByIdAsync(accessRequest.Id);
            entity = _mapper.Map(accessRequest, entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var respose = _mapper.Map<ClientAccessModel>(entity);
            return respose;
        }

        private async Task<Client> GetClientByIdAsync(string id)
        {
            var entity = await _context.Set<Client>()
                            .SingleOrDefaultAsync(x => x.Id == new Guid(id));
            return entity ?? throw new KeyNotFoundException($"id {id}");
        }

        public async Task<ClientAccessModel> GetByDocumentAsync(string document)
        {
            var entity = await _context.Set<Client>()
                            .SingleOrDefaultAsync(x => x.Document == document);
            var respose = _mapper.Map<ClientAccessModel>(entity);
            return respose;
        }

    }
}

using Access.Contract.Clients;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sql;
using Sql.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Access.Admin.Access
{
    internal class ClientAccess : IClientAccess
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public ClientAccess(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ClientAccessResponse> CreateClientAsync(CreateClientAccessRequest accessRequest)
        {
            var entity = _mapper.Map<Client>(accessRequest);
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return _mapper.Map<ClientAccessResponse>(entity);
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _context.Clients.SingleOrDefaultAsync(x => x.Id == new Guid(id));
            entity.Active = false;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ClientAccessResponse>> GetAllAsync()
        {
            var entities = await _context.Clients.AsNoTracking().Where(x => x.Active).ToListAsync();
            var respose = _mapper.Map<IEnumerable<ClientAccessResponse>>(entities);
            return respose;
        }

        public async Task<ClientAccessResponse> GetByIdAsync(string id)
        {
            var entity = await _context.Clients.AsNoTracking().SingleOrDefaultAsync(x => x.Active && x.Id == new Guid(id));
            var respose = _mapper.Map<ClientAccessResponse>(entity);
            return respose;
        }

        public async Task<ClientAccessResponse> UpadateClientAsync(UpdateClientAccessRequest accessRequest)
        {
            var entity = await _context.Clients.SingleOrDefaultAsync(x => x.Active && x.Id == new Guid(accessRequest.Id));
            entity = _mapper.Map(accessRequest, entity);
            await _context.SaveChangesAsync();
            var respose = _mapper.Map<ClientAccessResponse>(entity);
            return respose;
        }
    }
}

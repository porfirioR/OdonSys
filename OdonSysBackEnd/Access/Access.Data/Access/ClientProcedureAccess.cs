using Access.Contract.ClientProcedure;
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
            _context.ClientProcedures.Add(entity);
            await _context.SaveChangesAsync();
            var result = new ClientProcedureAccessModel(
                entity.ProcedureId.ToString(),
                entity.UserClientId.ToString(),
                entity.Status,
                entity.Price,
                entity.Anesthesia);
            return result;
        }

        public async Task<IEnumerable<ClientProcedureAccessModel>> GetClientProceduresByUserClientIdAsync(string userClientId)
        {
            var entities = await _context.ClientProcedures
                            .Where(x => x.UserClientId == new Guid(userClientId)).ToListAsync();
            var respose = entities.Select(x => new ClientProcedureAccessModel(
                x.ProcedureId.ToString(),
                x.UserClientId.ToString(),
                x.Status,
                x.Price,
                x.Anesthesia));
            return respose;
        }

        public async Task<ClientProcedureAccessModel> UpdateClientProcedureAsync(UpdateClientProcedureAccessRequest accessRequest)
        {
            var entity = await _context.ClientProcedures
                            .FirstAsync(x => x.UserClientId == new Guid(accessRequest.UserClientId) && x.ProcedureId == new Guid(accessRequest.ProcedureId));
            entity.Price = accessRequest.Price;
            await _context.SaveChangesAsync();
            var result = new ClientProcedureAccessModel(
                entity.ProcedureId.ToString(),
                entity.UserClientId.ToString(),
                entity.Status,
                entity.Price,
                entity.Anesthesia);
            return result;
        }

        public async Task<bool> CheckExistsUserProcedureAsync(string userId, string procedureId)
        {
            var entity = await _context.ClientProcedures.SingleOrDefaultAsync(x => x.UserClientId == new Guid(userId) && x.ProcedureId == new Guid(procedureId));
            return entity != null;
        }
    }
}

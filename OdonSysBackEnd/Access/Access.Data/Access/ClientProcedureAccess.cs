using Access.Contract.ClientProcedure;
using Access.Contract.Procedure;
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
    internal class ClientProcedureAccess : IClientProcedureAccess
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public ClientProcedureAccess(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ProcedureAccessModel> CreateUserProcedureAsync(UpsertUserProcedureAccessRequest accessRequest)
        {
            var entity = _mapper.Map<ClientProcedure>(accessRequest);
            _context.ClientProcedures.Add(entity);
            await _context.SaveChangesAsync();
            var procedure = await GetByIdAsync(accessRequest.ProcedureId, true);
            procedure.Price = entity.Price;
            return procedure;
        }

        public async Task<ProcedureAccessModel> DeleteUserProcedureAsync(string userId, string procedureId)
        {
            var entity = await _context.ClientProcedures.SingleOrDefaultAsync(x => x.UserClientId == new Guid(userId) && x.ProcedureId == new Guid(procedureId));
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            var procedure = await GetByIdAsync(procedureId, true);
            return _mapper.Map<ProcedureAccessModel>(procedure);
        }

        public async Task<IEnumerable<ProcedureAccessModel>> GetProceduresByUserIdAsync(string id)
        {
            var entities = await _context.ClientProcedures
                            .Include(x => x.Procedure)
                            .ThenInclude(x => x.ProcedureTeeth)
                            .Include(x => x.Procedure).ThenInclude(x => x.ClientProcedures)
                            .AsNoTracking()
                            .Where(x => x.UserClientId == new Guid(id)).ToListAsync();
            var respose = _mapper.Map<IEnumerable<ProcedureAccessModel>>(entities);
            return respose;
        }

        public async Task<ProcedureAccessModel> UpdateUserProcedureAsync(UpsertUserProcedureAccessRequest accessRequest)
        {
            var entity = await _context.ClientProcedures
                            .FirstAsync(x => x.UserClientId == new Guid(accessRequest.UserId) && x.ProcedureId == new Guid(accessRequest.ProcedureId));
            entity.Price = accessRequest.Price;
            await _context.SaveChangesAsync();
            var respose = _mapper.Map<ProcedureAccessModel>(entity);
            return respose;
        }

        public async Task<bool> CheckExistsUserProcedureAsync(string userId, string procedureId)
        {
            var entity = await _context.ClientProcedures.SingleOrDefaultAsync(x => x.UserClientId == new Guid(userId) && x.ProcedureId == new Guid(procedureId));
            return entity != null;
        }
    }
}

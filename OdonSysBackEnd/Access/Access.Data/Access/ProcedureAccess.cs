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
    internal class ProcedureAccess : IProcedureAccess
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public ProcedureAccess(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> CheckExistsUserProcedureAsync(string userId, string procedureId)
        {
            var entity = await _context.UserProcedures.SingleOrDefaultAsync(x => x.UserId == new Guid(userId) && x.ProcedureId == new Guid(procedureId));
            return entity != null;
        }

        public async Task<ProcedureAccessModel> CreateAsync(CreateProcedureAccessRequest accessRequest)
        {
            var entity = _mapper.Map<Procedure>(accessRequest);
            entity.ProcedureTeeth = accessRequest.ProcedureTeeth.Select(x => new ProcedureTooth { ToothId = new Guid(x), ProcedureId = entity.Id }).ToList();
            _context.Procedures.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProcedureAccessModel>(entity);
        }

        public async Task<ProcedureAccessModel> CreateUserProcedureAsync(UpsertUserProcedureAccessRequest accessRequest)
        {
            var entity = _mapper.Map<UserProcedure>(accessRequest);
            _context.UserProcedures.Add(entity);
            await _context.SaveChangesAsync();
            var procedure = await GetByIdAsync(accessRequest.ProcedureId, true);
            procedure.Price = entity.Price;
            return procedure;
        }

        public async Task<ProcedureAccessModel> DeleteAsync(string id)
        {
            var entity = await _context.Procedures.SingleOrDefaultAsync(x => x.Id == new Guid(id));
            entity.Active = false;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _mapper.Map<ProcedureAccessModel>(entity);
        }

        public async Task<ProcedureAccessModel> DeleteUserProcedureAsync(string userId, string procedureId)
        {
            var entity = await _context.UserProcedures.SingleOrDefaultAsync(x => x.UserId == new Guid(userId) && x.ProcedureId == new Guid(procedureId));
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            var procedure = await GetByIdAsync(procedureId, true);
            return _mapper.Map<ProcedureAccessModel>(procedure);
        }

        public async Task<IEnumerable<ProcedureAccessModel>> GetAllAsync()
        {
            var entities = await _context.Procedures.AsNoTracking().ToListAsync();
            var respose = _mapper.Map<IEnumerable<ProcedureAccessModel>>(entities);
            return respose;
        }

        public async Task<ProcedureAccessModel> GetByIdAsync(string id, bool active)
        {
            var entity = active ? await _context.Procedures
                                        .Include(x => x.ProcedureTeeth)
                                        .AsNoTracking()
                                        .SingleOrDefaultAsync(x => x.Active == active && x.Id == new Guid(id)) :
                await _context.Procedures
                            .Include(x => x.ProcedureTeeth)
                            .AsNoTracking()
                            .SingleOrDefaultAsync(x => x.Id == new Guid(id));
            if (entity is null)
            {
                throw new KeyNotFoundException($"id {id}");
            }
            var respose = _mapper.Map<ProcedureAccessModel>(entity);
            return respose;
        }

        public async Task<IEnumerable<ProcedureAccessModel>> GetProceduresByUserIdAsync(string id)
        {
            var entities = await _context.UserProcedures
                            .Include(x => x.Procedure)
                            .ThenInclude(x => x.ProcedureTeeth)
                            .Include(x => x.Procedure).ThenInclude(x => x.UserProcedures)
                            .AsNoTracking()
                            .Where(x => x.UserId == new Guid(id)).ToListAsync();
            var respose = _mapper.Map<IEnumerable<ProcedureAccessModel>>(entities);
            return respose;
        }

        public async Task<ProcedureAccessModel> RestoreAsync(string id)
        {
            var entity = await _context.Procedures.SingleOrDefaultAsync(x => x.Id == new Guid(id) && !x.Active);
            entity.Active = true;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _mapper.Map<ProcedureAccessModel>(entity);
        }

        public async Task<ProcedureAccessModel> UpdateAsync(UpdateProcedureAccessRequest accessRequest)
        {
            var entity = await _context.Procedures
                            .Include(x => x.ProcedureTeeth)
                            .SingleOrDefaultAsync(x => x.Id == new Guid(accessRequest.Id));

            entity.Description = accessRequest.Description;
            entity.Active = accessRequest.Active;
            entity.ProcedureTeeth = accessRequest.ProcedureTeeth.Select(x => new ProcedureTooth { ToothId = new Guid(x), Active = true }).ToList();
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var respose = _mapper.Map<ProcedureAccessModel>(entity);
            return respose;
        }

        public async Task<ProcedureAccessModel> UpdateUserProcedureAsync(UpsertUserProcedureAccessRequest accessRequest)
        {
            var entity = await _context.UserProcedures
                            .FirstAsync(x => x.UserId == new Guid(accessRequest.UserId) && x.ProcedureId == new Guid(accessRequest.ProcedureId));
            entity.Price = accessRequest.Price;
            await _context.SaveChangesAsync();
            var respose = _mapper.Map<ProcedureAccessModel>(entity);
            return respose;
        }

        public async Task<bool> ValidateIdNameAsync(string value)
        {
            var existProcedure = await _context.Procedures.SingleOrDefaultAsync(x => x.Name.Equals(value) || x.Id.Equals(value));
            return existProcedure is null;
        }

        public async Task<IEnumerable<string>> ValidateProcedureTeethAsync(IEnumerable<string> theetIds)
        {
            var procedureTeeth = (await _context.Teeth.ToListAsync()).Select(x => x.Id.ToString());
            var invalidIds = theetIds.Where(x => !procedureTeeth.Contains(x));
            return invalidIds;
        }


    }
}

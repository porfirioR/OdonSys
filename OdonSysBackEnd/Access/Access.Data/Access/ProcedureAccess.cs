using Access.Contract.Procedure;
using Access.Sql;
using Access.Sql.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Access.Admin.Access
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

        public async Task<ProcedureAccessResponse> CreateAsync(CreateProcedureAccessRequest accessRequest)
        {
            var entity = _mapper.Map<Procedure>(accessRequest);
            entity.ProcedureTeeth = accessRequest.ProcedureTeeth.Select(x => new ProcedureTooth { ToothId = new Guid(x), ProcedureId = entity.Id }).ToList();
            _context.Procedures.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProcedureAccessResponse>(entity);
        }

        public async Task<ProcedureAccessResponse> DeleteAsync(string id)
        {
            var entity = await _context.Procedures.SingleOrDefaultAsync(x => x.Id == new Guid(id));
            entity.Active = false;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _mapper.Map<ProcedureAccessResponse>(entity);
        }

        public async Task<IEnumerable<ProcedureAccessResponse>> GetAllAsync()
        {
            var entities = await _context.Procedures.AsNoTracking().ToListAsync();
            var respose = _mapper.Map<IEnumerable<ProcedureAccessResponse>>(entities);
            return respose;
        }

        public async Task<ProcedureAccessResponse> GetByIdAsync(string id, bool active)
        {
            var entity = active ? await _context.Procedures
                                        .Include(x => x.ProcedureTeeth)
                                        .AsNoTracking()
                                        .SingleOrDefaultAsync(x => x.Active == active && x.Id == new Guid(id)) :
                await _context.Procedures
                            .Include(x => x.ProcedureTeeth)
                            .AsNoTracking()
                            .SingleOrDefaultAsync(x => x.Id == new Guid(id));
            var respose = _mapper.Map<ProcedureAccessResponse>(entity);
            return respose;
        }

        public async Task<ProcedureAccessResponse> RestoreAsync(string id)
        {
            var entity = await _context.Procedures.SingleOrDefaultAsync(x => x.Id == new Guid(id) && !x.Active);
            entity.Active = true;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _mapper.Map<ProcedureAccessResponse>(entity);
        }

        public async Task<ProcedureAccessResponse> UpdateAsync(UpdateProcedureAccessRequest accessRequest)
        {
            var entity = await _context.Procedures
                            .Include(x => x.ProcedureTeeth)
                            .SingleOrDefaultAsync(x => x.Id == new Guid(accessRequest.Id));

            entity.Description = accessRequest.Description;
            entity.Active = accessRequest.Active;
            entity.ProcedureTeeth = accessRequest.ProcedureTeeth.Select(x => new ProcedureTooth { ToothId = new Guid(x), Active = true }).ToList();
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var respose = _mapper.Map<ProcedureAccessResponse>(entity);
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

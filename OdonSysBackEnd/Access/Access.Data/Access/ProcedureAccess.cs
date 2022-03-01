using Access.Contract.Procedure;
using Access.Sql;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sql.Entities;
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
            entity.ProcedureTeeth = accessRequest.ProcedureTeeth.Select(x => new ProcedureTooth { ToothId = new Guid(x) });
            _context.Entry(entity).State = EntityState.Added;
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
            var entities = await _context.Procedures.AsNoTracking().Where(x => x.Active).ToListAsync();
            var respose = _mapper.Map<IEnumerable<ProcedureAccessResponse>>(entities);
            return respose;
        }

        public async Task<ProcedureAccessResponse> GetByIdAsync(string id, bool active)
        {
            var entity = await _context.Procedures.AsNoTracking().SingleOrDefaultAsync(x => x.Active == active && x.Id == new Guid(id));
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
                            .SingleOrDefaultAsync(x => x.Active && x.Id == new Guid(accessRequest.Id));
            
            entity = _mapper.Map(accessRequest, entity);
            entity.ProcedureTeeth = accessRequest.ProcedureTeeth.Select(x => new ProcedureTooth { ToothId = new Guid(x) });
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var respose = _mapper.Map<ProcedureAccessResponse>(entity);
            return respose;
        }
    }
}

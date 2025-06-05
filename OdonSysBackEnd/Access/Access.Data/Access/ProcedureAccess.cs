using Access.Contract.Procedures;
using Access.Sql;
using Microsoft.EntityFrameworkCore;

namespace Access.Data.Access;

internal sealed class ProcedureAccess : IProcedureAccess
{
    private readonly DataContext _context;
    private readonly IProcedureDataAccessBuilder _procedureDataAccessBuilder;

    public ProcedureAccess(DataContext context, IProcedureDataAccessBuilder procedureDataAccessBuilder)
    {
        _context = context;
        _procedureDataAccessBuilder = procedureDataAccessBuilder;
    }

    public async Task<ProcedureAccessModel> CreateAsync(CreateProcedureAccessRequest accessRequest)
    {
        var entity = _procedureDataAccessBuilder.MapCreateProcedureAccessRequestToProcedure(accessRequest);
        _context.Procedures.Add(entity);
        await _context.SaveChangesAsync();
        return _procedureDataAccessBuilder.MapProcedureToProcedureAccessModel(entity);
    }

    public async Task<ProcedureAccessModel> DeleteAsync(string id)
    {
        var entity = await _context.Procedures.SingleOrDefaultAsync(x => x.Id == new Guid(id));
        entity.Active = false;
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return _procedureDataAccessBuilder.MapProcedureToProcedureAccessModel(entity);
    }

    public async Task<IEnumerable<ProcedureAccessModel>> GetAllAsync()
    {
        var accessModelList = await _context.Procedures
                            .AsNoTrackingWithIdentityResolution()
                            .Select(x => _procedureDataAccessBuilder.MapProcedureToProcedureAccessModel(x))
                            .ToListAsync();

        return accessModelList;
    }

    public async Task<ProcedureAccessModel> GetByIdAsync(string id, bool active)
    {
        var entity = (active ?
            await _context.Procedures
                        //.Include(x => x.ProcedureTeeth)
                        .AsNoTracking()
                        .SingleOrDefaultAsync(x => x.Active == active && x.Id == new Guid(id)) :
            await _context.Procedures
                        //.Include(x => x.ProcedureTeeth)
                        .AsNoTracking()
                        .SingleOrDefaultAsync(x => x.Id == new Guid(id))) ??
            throw new KeyNotFoundException($"id {id}");

        var accessModel = _procedureDataAccessBuilder.MapProcedureToProcedureAccessModel(entity);
        return accessModel;
    }

    public async Task<ProcedureAccessModel> UpdateAsync(UpdateProcedureAccessRequest accessRequest)
    {
        var entity = await _context.Procedures
                        //.Include(x => x.ProcedureTeeth)
                        .SingleOrDefaultAsync(x => x.Id == new Guid(accessRequest.Id));
        entity = _procedureDataAccessBuilder.MapUpdateProcedureAccessRequestToProcedure(accessRequest, entity);
        //entity.ProcedureTeeth = accessRequest.ProcedureTeeth.Select(x => new ProcedureTooth { ToothId = new Guid(x), Active = true }).ToList();
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        var accessModel = _procedureDataAccessBuilder.MapProcedureToProcedureAccessModel(entity);
        return accessModel;
    }

    public async Task<bool> ValidateIdNameAsync(string value)
    {
        var existProcedure = await _context.Procedures.SingleOrDefaultAsync(x => x.Name.Equals(value) || x.Id.Equals(value));
        return existProcedure is null;
    }

    //public async Task<IEnumerable<string>> ValidateProcedureTeethAsync(IEnumerable<string> theetIds)
    //{
    //    var procedureTeeth = (await _context.Teeth.ToListAsync()).Select(x => x.Id.ToString());
    //    var invalidIds = theetIds.Where(x => !procedureTeeth.Contains(x));
    //    return invalidIds;
    //}
}

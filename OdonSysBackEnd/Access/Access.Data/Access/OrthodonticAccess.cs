using Access.Contract.Clients;
using Access.Contract.Orthodontics;
using Access.Sql;
using Microsoft.EntityFrameworkCore;

namespace Access.Data.Access;

internal sealed class OrthodonticAccess : IOrthodonticAccess
{
    private readonly DataContext _context;
    private readonly IOrthodonticDataAccessBuilder _orthodonticDataAccessBuilder;
    private readonly IClientDataAccessBuilder _clientDataAccessBuilder;

    public OrthodonticAccess(DataContext context, IOrthodonticDataAccessBuilder orthodonticDataAccessBuilder, IClientDataAccessBuilder clientDataAccessBuilder)
    {
        _context = context;
        _orthodonticDataAccessBuilder = orthodonticDataAccessBuilder;
        _clientDataAccessBuilder = clientDataAccessBuilder;
    }

    public async Task<IEnumerable<OrthodonticAccessModel>> GetAllAsync()
    {
        var entities = _context.Orthodontics
            .Include(x => x.Client)
            .AsNoTrackingWithIdentityResolution()
            .OrderByDescending(x => x.DateCreated);

        var accessModels = entities.Select(x => _orthodonticDataAccessBuilder.MapEntityToAccessModel(x, _clientDataAccessBuilder.MapClientToClientAccessModel(x.Client)));
        return await accessModels.ToListAsync();
    }

    public async Task<IEnumerable<OrthodonticAccessModel>> GetAllByClientIdAsync(string clientId)
    {
        var entities = _context.Orthodontics
            .Include(x => x.Client)
            .AsNoTrackingWithIdentityResolution()
            .OrderByDescending(x => x.DateCreated)
            .Where(x => x.Id == new Guid(clientId));

        var accessModels = entities.Select(x => _orthodonticDataAccessBuilder.MapEntityToAccessModel(x, _clientDataAccessBuilder.MapClientToClientAccessModel(x.Client)));
        return await accessModels.ToListAsync();
    }

    public async Task<OrthodonticAccessModel> GetByIdAsync(string id)
    {
        var entity = await _context.Orthodontics
            .Include(x => x.Client)
            .AsNoTrackingWithIdentityResolution()
            .OrderByDescending(x => x.DateCreated)
            .FirstAsync(x => x.Id == new Guid(id));

        var accessModel = _orthodonticDataAccessBuilder.MapEntityToAccessModel(entity, _clientDataAccessBuilder.MapClientToClientAccessModel(entity.Client));
        return accessModel;
    }

    public async Task<OrthodonticAccessModel> UpsertOrthodontic(OrthodonticAccessRequest accessRequest)
    {
        var entity = _orthodonticDataAccessBuilder.MapAccessRequestToEntity(accessRequest);
        if (accessRequest.Id is not null)
        {
            _context.Orthodontics.Update(entity);
        }
        else
        {
            _context.Orthodontics.Add(entity);
        }
        await _context.SaveChangesAsync();
        var accessModel = await GetByIdAsync(entity.Id.ToString());
        return accessModel;
    }
}

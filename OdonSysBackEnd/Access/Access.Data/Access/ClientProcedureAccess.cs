using Access.Contract.ClientProcedures;
using Access.Sql;
using Microsoft.EntityFrameworkCore;

namespace Access.Data.Access;

public sealed class ClientProcedureAccess : IClientProcedureAccess
{
    private readonly DataContext _context;
    private readonly IClientProcedureDataAccessBuilder _clientProcedureDataAccessBuilder;

    public ClientProcedureAccess(DataContext context, IClientProcedureDataAccessBuilder clientProcedureDataAccessBuilder)
    {
        _context = context;
        _clientProcedureDataAccessBuilder = clientProcedureDataAccessBuilder;
    }

    public async Task<ClientProcedureAccessModel> CreateClientProcedureAsync(CreateClientProcedureAccessRequest accessRequest)
    {
        var entity = _clientProcedureDataAccessBuilder.MapCreateClientProcedureAccessRequestToClientProcedure(accessRequest);
        _context.ClientProcedures.Add(entity);
        await _context.SaveChangesAsync();
        var accessModel = _clientProcedureDataAccessBuilder.MapClientProcedureToClientProcedureAccessModel(entity);
        return accessModel;
    }

    public async Task<IEnumerable<ClientProcedureAccessModel>> GetClientProceduresByUserClientIdAsync(IEnumerable<Guid> userClientIds)
    {
        var entities = await _context.ClientProcedures
                        .AsNoTracking()
                        .Where(x => userClientIds.Contains(x.UserClientId)).ToListAsync();
        var accessModel = entities.Select(_clientProcedureDataAccessBuilder.MapClientProcedureToClientProcedureAccessModel);
        return accessModel;
    }

    public async Task<ClientProcedureAccessModel> UpdateClientProcedureAsync(UpdateClientProcedureAccessRequest accessRequest)
    {
        var entity = await _context.ClientProcedures
                        .FirstAsync(x => x.UserClientId == new Guid(accessRequest.UserClientId) && x.ProcedureId == new Guid(accessRequest.ProcedureId));
        entity = _clientProcedureDataAccessBuilder.MapUpdateClientProcedureAccessRequestToClientProcedure(accessRequest, entity);
        await _context.SaveChangesAsync();
        var accessModel = _clientProcedureDataAccessBuilder.MapClientProcedureToClientProcedureAccessModel(entity);
        return accessModel;
    }

    public async Task<bool> CheckExistsClientProcedureAsync(string userClientId, string procedureId)
    {
        var entity = await _context.ClientProcedures
                        .SingleOrDefaultAsync(x => x.UserClientId == new Guid(userClientId) && x.ProcedureId == new Guid(procedureId));
        return entity != null;
    }

    public async Task<bool> CheckExistsClientProcedureAsync(string clientProcedureId)
    {
        var entity = await _context.ClientProcedures
                        .SingleOrDefaultAsync(x => x.Id == new Guid(clientProcedureId));
        return entity != null;
    }

    public async Task<ClientProcedureAccessModel> GetClientProcedureByIdAsync(string clientProcedureId)
    {
        var entity = await _context.ClientProcedures
                        .SingleAsync(x => x.Id == new Guid(clientProcedureId));
        var accessModel = _clientProcedureDataAccessBuilder.MapClientProcedureToClientProcedureAccessModel(entity);
        return accessModel;
    }
}

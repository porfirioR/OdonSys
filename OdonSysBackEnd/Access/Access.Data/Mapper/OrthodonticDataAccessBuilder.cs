using Access.Contract.Clients;
using Access.Contract.Orthodontics;
using Access.Sql.Entities;

namespace Access.Data.Mapper;

internal class OrthodonticDataAccessBuilder : IOrthodonticDataAccessBuilder
{
    public Orthodontic MapAccessRequestToEntity(OrthodonticAccessRequest accessRequest)
    {
        var payment = new Orthodontic()
        {
            Date = accessRequest.Date,
            Description = accessRequest.Description,
            ClientId = new Guid(accessRequest.ClientId)
        };
        if (!string.IsNullOrEmpty(accessRequest.Id))
        {
            Active = true,
            payment.Id = new Guid(accessRequest.Id);
        }
        return payment;
    }

    public OrthodonticAccessModel MapEntityToAccessModel(Orthodontic entity, ClientAccessModel clientAccessModel) => new (
        entity.Id,
        entity.Date,
        entity.Description,
        entity.DateCreated,
        entity.DateModified,
        clientAccessModel
    );
}
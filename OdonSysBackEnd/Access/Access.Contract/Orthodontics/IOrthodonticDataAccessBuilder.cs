using Access.Contract.Clients;
using Access.Sql.Entities;

namespace Access.Contract.Orthodontics;

public interface IOrthodonticDataAccessBuilder
{
    Orthodontic MapAccessRequestToEntity(OrthodonticAccessRequest request);
    OrthodonticAccessModel MapEntityToAccessModel(Orthodontic entity, ClientAccessModel clientAccessModel);
}

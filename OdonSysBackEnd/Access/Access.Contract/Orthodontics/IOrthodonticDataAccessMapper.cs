using Access.Sql.Entities;

namespace Access.Contract.Orthodontics;

public interface IOrthodonticDataAccessMapper
{
    Orthodontic MapAccessRequestToEntity(OrthodonticAccessRequest request);
    OrthodonticAccessModel MapEntityToAccessModel(Orthodontic payment);
}

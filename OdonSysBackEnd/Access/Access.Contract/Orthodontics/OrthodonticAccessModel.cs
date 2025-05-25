namespace Access.Contract.Orthodontics;

public record OrthodonticAccessModel(
    Guid ClientId,
    DateTime Date,
    string Description,
    string Id = null
);
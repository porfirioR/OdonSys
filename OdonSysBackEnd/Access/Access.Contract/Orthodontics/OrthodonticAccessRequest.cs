namespace Access.Contract.Orthodontics;

public record OrthodonticAccessRequest(
    Guid ClientId,
    DateTime Date,
    string Description,
    string Id = null
);
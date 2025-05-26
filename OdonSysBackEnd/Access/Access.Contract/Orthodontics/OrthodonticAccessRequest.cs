namespace Access.Contract.Orthodontics;

public record OrthodonticAccessRequest(
    string ClientId,
    DateTime Date,
    string Description,
    string Id = null
);
namespace Contract.Workspace.Orthodontics;

public record OrthodonticRequest(
    string ClientId,
    DateTime Date,
    string Description,
    string Id = null
);
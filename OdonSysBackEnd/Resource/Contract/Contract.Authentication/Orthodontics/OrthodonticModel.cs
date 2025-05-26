using Contract.Administration.Clients;

namespace Contract.Workspace.Orthodontics;

public record OrthodonticModel(
    Guid Id,
    DateTime Date,
    string Description,
    DateTime DateCreated,
    DateTime DateModified,
    ClientModel Client
);
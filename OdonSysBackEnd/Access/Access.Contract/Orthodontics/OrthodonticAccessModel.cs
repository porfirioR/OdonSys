using Access.Contract.Clients;

namespace Access.Contract.Orthodontics;

public record OrthodonticAccessModel(
    Guid Id,
    DateTime Date,
    string Description,
    DateTime DateCreated,
    DateTime DateModified,
    ClientAccessModel Client
);
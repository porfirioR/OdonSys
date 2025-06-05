namespace Access.Contract.Procedures;

public record ProcedureAccessModel(
    string Id,
    bool Active,
    DateTime DateCreated,
    DateTime DateModified,
    string Name,
    string Description,
    int Price,
    IEnumerable<string> ProcedureTeeth,
    bool XRays
);

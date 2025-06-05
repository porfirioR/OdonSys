namespace Contract.Workspace.Procedures;

public record ProcedureModel
(
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

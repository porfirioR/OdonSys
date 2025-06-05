namespace Contract.Workspace.Procedures;

public record UpdateProcedureRequest
(
    string Id,
    string Description,
    bool Active,
    int Price,
    IEnumerable<string> ProcedureTeeth,
    bool XRays
);

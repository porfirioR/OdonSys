namespace Contract.Workspace.Procedures
{
    public record CreateProcedureRequest
    (
        string Name,
        string Description,
        int Price,
        IEnumerable<string> ProcedureTeeth,
        bool XRays
    );
}

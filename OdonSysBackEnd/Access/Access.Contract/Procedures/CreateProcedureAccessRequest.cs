namespace Access.Contract.Procedures
{
    public record CreateProcedureAccessRequest
    (
        string Name,
        string Description,
        IEnumerable<string> ProcedureTeeth,
        bool XRays,
        int Price
    );
}

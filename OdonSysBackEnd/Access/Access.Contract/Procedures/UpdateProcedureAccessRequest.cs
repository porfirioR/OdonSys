namespace Access.Contract.Procedures;

public record UpdateProcedureAccessRequest(
    string Id,
    string Description,
    bool Active,
    int Price,
    bool XRays
)
{
    public IEnumerable<string> ProcedureTeeth { set; get; }
}

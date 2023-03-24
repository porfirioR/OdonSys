namespace Contract.Workspace.ClientProcedures
{
    public record CreateClientProcedureRequest(
        string UserId,
        string ProcedureId,
        int Price,
        bool Anesthesia
    )
    { }
}

namespace Contract.Workspace.Procedures
{
    public record UpsertUserProcedureRequest(
        string UserId,
        string ProcedureId,
        int Price
    )
    { }
}

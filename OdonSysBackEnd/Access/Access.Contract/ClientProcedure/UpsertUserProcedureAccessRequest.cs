namespace Access.Contract.ClientProcedure
{
    public record UpsertUserProcedureAccessRequest(
        string UserId,
        string ProcedureId,
        int Price
    // ask the dentist what happens when the amount changes? for now can't change if client status is different to canceled or new
    )
    { }
}

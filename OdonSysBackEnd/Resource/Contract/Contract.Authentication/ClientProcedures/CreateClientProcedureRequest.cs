namespace Contract.Workspace.ClientProcedures;

public record CreateClientProcedureRequest(
    string UserId,
    string ClientId,
    string ProcedureId
);

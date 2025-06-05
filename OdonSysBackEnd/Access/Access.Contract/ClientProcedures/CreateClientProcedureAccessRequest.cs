namespace Access.Contract.ClientProcedures;

public record CreateClientProcedureAccessRequest(
    string UserClientId,
    string ProcedureId
);

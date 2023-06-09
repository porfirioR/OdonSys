using Microsoft.AspNetCore.Http;

namespace Contract.Workspace.ClientProcedures
{
    public record CreateClientProcedureRequest(
        string UserId,
        string ClientId,
        string ProcedureId,
        IEnumerable<IFormFile> Files
    );
}

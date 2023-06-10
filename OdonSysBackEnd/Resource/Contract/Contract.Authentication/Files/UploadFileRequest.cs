using Microsoft.AspNetCore.Http;

namespace Contract.Workspace.Files
{
    public record UploadFileRequest(
        string Id,
        IEnumerable<IFormFile> Files,
        string UserId
    );
}

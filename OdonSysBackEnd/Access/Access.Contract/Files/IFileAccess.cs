namespace Access.Contract.Files;

public interface IFileAccess
{
    Task<string> UploadFile(UploadFileAccessRequest accessRequest);
    Task<IEnumerable<string>> UploadFile(IEnumerable<UploadFileAccessRequest> accessRequest);
    Task<IEnumerable<FileAccessModel>> GetFilesByReferenceIdAsync(string referenceId);
}

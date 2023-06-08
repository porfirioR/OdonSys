namespace Access.File.Contract
{
    public interface IFileAccess
    {
        Task<string> UploadAsync(UploadFileAccessRequest accessRequest);
    }
}

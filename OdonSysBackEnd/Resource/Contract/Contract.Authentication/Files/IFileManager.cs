namespace Contract.Workspace.Files
{
    public interface IFileManager
    {
        Task<IEnumerable<string>> UploadFileAsync(UploadFileRequest request);
    }
}

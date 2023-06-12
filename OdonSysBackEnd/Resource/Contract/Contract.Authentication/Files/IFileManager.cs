namespace Contract.Workspace.Files
{
    public interface IFileManager
    {
        Task<IEnumerable<string>> UploadFileAsync(UploadFileRequest request);
        Task<IEnumerable<FileModel>> GetFilesByReferenceIdAsync(string referenceId, bool preview = true);
    }
}

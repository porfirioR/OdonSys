namespace Access.File.Contract
{
    public record UploadFileStorageRequest(
        Stream Stream,
        string FileName,
        bool Private,
        string Folder,
        string DisplayName
    );
}

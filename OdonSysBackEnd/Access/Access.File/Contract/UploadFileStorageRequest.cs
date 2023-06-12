namespace Access.File.Contract
{
    public record UploadFileStorageRequest(
        Stream Stream,
        string Filename,
        bool Private,
        string Folder
    );
}

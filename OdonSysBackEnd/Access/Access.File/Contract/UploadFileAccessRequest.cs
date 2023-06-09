namespace Access.File.Contract
{
    public record UploadFileAccessRequest(
        Stream Stream,
        string Filename,
        bool Private,
        string Folder
    );
}

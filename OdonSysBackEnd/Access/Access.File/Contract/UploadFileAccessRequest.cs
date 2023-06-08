namespace Access.File.Contract
{
    public record UploadFileAccessRequest(
        Stream Stream,
        string Filename,
        string Container,
        bool Private,
        string Folder,
        string Format = "pdf"
    );
}

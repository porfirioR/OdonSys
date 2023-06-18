namespace Access.Contract.Files
{
    public record UploadFileAccessRequest(
        string Name,
        string Url,
        string ReferenceId,
        string Format
    );
}

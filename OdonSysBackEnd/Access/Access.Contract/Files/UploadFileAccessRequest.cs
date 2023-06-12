namespace Access.Contract.Files
{
    public record UploadFileAccessRequest(
        string Url,
        string ReferenceId,
        string Format
    );
}

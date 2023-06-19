namespace Contract.Workspace.Files
{
    public record FileModel(
        string Name,
        string Url,
        string Format,
        DateTime DateCreated,
        string FullUrl
    );
}

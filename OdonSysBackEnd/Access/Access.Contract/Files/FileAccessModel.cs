namespace Access.Contract.Files
{
    public record FileAccessModel(
        string Url,
        string Format,
        DateTime DateCreated
    );
}

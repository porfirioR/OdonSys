namespace Access.Contract.Files;

public record FileAccessModel(
    string Name,
    string Url,
    string Format,
    DateTime DateCreated
);

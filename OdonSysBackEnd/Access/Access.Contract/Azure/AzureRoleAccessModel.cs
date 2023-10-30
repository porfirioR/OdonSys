namespace Access.Contract.Azure
{
    public record AzureRoleAccessModel(
        string Name,
        string Code,
        bool Active
    );
}

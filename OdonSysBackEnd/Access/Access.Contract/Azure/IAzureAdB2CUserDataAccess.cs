namespace Access.Contract.Azure
{
    public interface IAzureAdB2CUserDataAccess
    {
        Task<IEnumerable<UserGraphAccessModel>> GetUsersAsync();
    }
}

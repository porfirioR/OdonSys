namespace Access.Contract.Azure
{
    public interface IAzureAdB2CUserDataAccess
    {
        Task<IEnumerable<UserGraphAccessModel>> GetUsersAsync();
        Task<UserGraphAccessModel> GetUserByIdAsync(string userId);
        Task<string> SetRoleToUserAsync(string userId);
    }
}

using Microsoft.Graph.Models;

namespace Access.Contract.Azure
{
    public interface IAzureAdB2CUserDataAccess
    {
        Task<IEnumerable<UserGraphAccessModel>> GetUsersAsync();
        Task<UserGraphAccessModel> GetUserByIdAsync(string id);
    }
}

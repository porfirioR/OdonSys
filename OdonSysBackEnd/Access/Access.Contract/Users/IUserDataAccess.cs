namespace Access.Contract.Users;

public interface IUserDataAccess
{
    Task<IEnumerable<DoctorDataAccessModel>> GetAllAsync();
    Task<IEnumerable<UserDataAccessModel>> GetAllUserAsync();
    Task<DoctorDataAccessModel> GetByIdAsync(string id);
    Task<string> GetInternalUserIdByExternalIdAsync(string externalId);
    Task<UserDataAccessModel> ApproveNewUserAsync(string id);
    Task<DoctorDataAccessModel> UpdateAsync(UserDataAccessRequest dataAccess);
    Task<UserClientAccessModel> GetUserClientAsync(UserClientAccessRequest accessRequest);
    Task<IEnumerable<UserClientAccessModel>> GetUserClientsByUserIdAsync(string userId);
    Task<UserClientAccessModel> CreateUserClientAsync(UserClientAccessRequest accessRequest);
    Task<IEnumerable<string>> SetUserRolesAsync(UserRolesAccessRequest accessRequest);
}

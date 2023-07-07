using Access.Sql.Entities;

namespace Access.Contract.Users
{
    public interface IUserDataAccessBuilder
    {
        User MapUserDataAccessRequestToUser(UserDataAccessRequest request, User user = null);
        DoctorDataAccessModel MapUserToDoctorDataAccessModel(User user);
        UserDataAccessModel MapUserToUserDataAccessModel(User user);
        UserClientAccessModel MapUserClientToUserClientAccessModel(UserClient userClient);
    }
}

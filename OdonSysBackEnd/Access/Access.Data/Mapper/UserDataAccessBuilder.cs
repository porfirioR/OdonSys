using Access.Contract.Users;
using Access.Sql.Entities;

namespace Access.Data.Mapper
{
    internal class UserDataAccessBuilder : IUserDataAccessBuilder
    {
        public UserClientAccessModel MapUserClientToUserClientAccessModel(UserClient userClient) => new(
            userClient.Id,
            userClient.ClientId,
            userClient.UserId
        );

        public User MapUserDataAccessRequestToUser(UserDataAccessRequest request, User user = null)
        {
            user ??= new User();
            user.Email = string.IsNullOrEmpty(request.Email) ? user.Email : request.Email;
            if (!string.IsNullOrEmpty(request.Id))
            {
                user.Id = new Guid(request.Id);
            }
            user.Name = request.Name;
            user.MiddleName = request.MiddleName;
            user.Surname = request.Surname;
            user.SecondSurname = request.SecondSurname;
            user.Document = request.Document;
            user.Country = request.Country;
            user.Email = user.Email;
            user.Phone = request.Phone;
            return user;
        }

        public DoctorDataAccessModel MapUserToDoctorDataAccessModel(User user) => new(
            user.Id.ToString(),
            user.Name,
            user.MiddleName,
            user.Surname,
            user.SecondSurname,
            user.Document,
            user.Country,
            user.Email,
            user.Phone,
            user.UserName,
            user.Active,
            user.Approved,
            user.UserRoles != null && user.UserRoles.Any() ? user.UserRoles.Select(x => x.Role.Code) : new List<string>()
        );

        public UserDataAccessModel MapUserToUserDataAccessModel(User user) => new(
            user.Id.ToString(),
            user.UserName,
            user.Active,
            user.Approved,
            user.UserRoles != null && user.UserRoles.Any() ? user.UserRoles.Select(x => x.Role.Code) : new List<string>()
        );
    }
}

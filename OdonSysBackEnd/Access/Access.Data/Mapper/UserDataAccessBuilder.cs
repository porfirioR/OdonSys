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

        public User MapUserDataAccessRequestToUser(UserDataAccessRequest request, User entity = null)
        {
            entity ??= new User();
            entity.Email = string.IsNullOrEmpty(request.Email) ? entity.Email : request.Email;
            if (!string.IsNullOrEmpty(request.Id))
            {
                entity.Id = new Guid(request.Id);
            }
            entity.Name = request.Name;
            entity.MiddleName = request.MiddleName;
            entity.Surname = request.Surname;
            entity.SecondSurname = request.SecondSurname;
            entity.Document = request.Document;
            entity.Country = request.Country;
            entity.Email = entity.Email;
            entity.Phone = request.Phone;
            entity.Active = request.Active;
            entity.ExternalUserId = request.ExternalUserId;
            return entity;
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

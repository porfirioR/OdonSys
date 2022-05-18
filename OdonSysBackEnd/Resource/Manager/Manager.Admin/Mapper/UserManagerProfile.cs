using Access.Admin.Access;
using Access.Contract.Users;
using AutoMapper;
using Contract.Authentication.User;

namespace Manager.Admin.Mapper
{
    public class UserManagerProfile : Profile
    {
        public UserManagerProfile()
        {
            CreateMap<RegisterUserRequest, UserDataAccess>();

            CreateMap<UpdateUserRequest, UserDataAccess>();

            CreateMap<UserDataAccessModel, UserModel>();
        }
    }
}

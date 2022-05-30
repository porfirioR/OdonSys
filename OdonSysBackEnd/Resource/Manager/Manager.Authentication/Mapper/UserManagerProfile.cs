using Access.Admin.Access;
using Access.Contract.Users;
using AutoMapper;
using Contract.Authentication.User;
using Contract.Workspace.User;

namespace Manager.Authentication.Mapper
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

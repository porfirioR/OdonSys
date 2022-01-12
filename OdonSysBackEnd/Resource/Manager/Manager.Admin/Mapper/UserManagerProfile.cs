using Access.Admin.Access;
using Access.Contract.Users;
using AutoMapper;
using Contract.Admin.User;

namespace Manager.Admin.Mapper
{
    public class UserManagerProfile : Profile
    {
        public UserManagerProfile()
        {
            CreateMap<CreateUserRequest, UserDataAccess>();

            CreateMap<UpdateUserRequest, UserDataAccess>();

            CreateMap<UserDataAccessModel, UserModel>();
        }
    }
}

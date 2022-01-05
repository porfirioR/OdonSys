using Access.Admin.Access;
using Access.Contract.Users;
using AutoMapper;
using Resources.Contract.User;

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

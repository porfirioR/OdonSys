using Access.Admin.Access;
using Access.Contract.Auth;
using Access.Contract.Users;
using AutoMapper;
using Contract.Authentication.User;
using Contract.Workspace.User;

namespace Manager.Admin.Mapper
{
    public class UserManagerProfile : Profile
    {
        public UserManagerProfile()
        {
            CreateMap<LoginRequest, LoginDataAccess>();

            CreateMap<RegisterUserRequest, UserDataAccess>();

            CreateMap<UpdateDoctorRequest, UserDataAccess>();

            CreateMap<UserDataAccessModel, UserModel>();

            CreateMap<AuthAccessModel, AuthModel>();
        }
    }
}

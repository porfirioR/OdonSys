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

            CreateMap<RegisterUserRequest, UserDataAccessRequest>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<AuthAccessModel, AuthModel>();

            CreateMap<UpdateDoctorRequest, UserDataAccessRequest>();

            CreateMap<UserDataAccessModel, UserModel>();

            CreateMap<DoctorDataAccessModel, DoctorModel>();

        }
    }
}

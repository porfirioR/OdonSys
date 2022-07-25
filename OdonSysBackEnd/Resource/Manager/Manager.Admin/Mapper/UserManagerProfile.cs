using Access.Admin.Access;
using Access.Contract.Auth;
using Access.Contract.Users;
using AutoMapper;
using Contract.Admin.Auth;
using Contract.Admin.User;
using Contract.Admin.Users;

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

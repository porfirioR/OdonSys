using Access.Contract.Authentication;
using Access.Contract.Users;
using AutoMapper;
using Contract.Administration.Authentication;
using Contract.Administration.Users;

namespace Manager.Administration.Mapper
{
    public class UserManagerProfile : Profile
    {
        public UserManagerProfile()
        {

            CreateMap<RegisterUserRequest, UserDataAccessRequest>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<AuthenticationAccessModel, AuthenticationModel>();

            CreateMap<UpdateDoctorRequest, UserDataAccessRequest>();

            CreateMap<UserDataAccessModel, UserModel>();

            CreateMap<DoctorDataAccessModel, DoctorModel>();

        }
    }
}

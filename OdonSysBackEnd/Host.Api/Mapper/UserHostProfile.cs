using AutoMapper;
using Contract.Workspace.User;
using Host.Api.Models.Auth;
using Host.Api.Models.Users;
using OdonSysBackEnd.Models.Auth;

namespace Host.Api.Mapper
{
    public class UserHostProfile : Profile
    {
        public UserHostProfile()
        {
            CreateMap<LoginApiRequest, LoginRequest>();

            CreateMap<RegisterUserApiRequest, RegisterUserRequest>();

            CreateMap<UpdateDoctorApiRequest, UpdateDoctorRequest>().ReverseMap();
        }
    }
}

using AutoMapper;
using Contract.Administration.Users;
using Host.Api.Contract.Authorization;
using Host.Api.Contract.Users;

namespace Host.Api.Mapper
{
    public class UserHostProfile : Profile
    {
        public UserHostProfile()
        {
            CreateMap<RegisterUserApiRequest, RegisterUserRequest>();

            CreateMap<UpdateDoctorApiRequest, UpdateDoctorRequest>().ReverseMap();

            CreateMap<DoctorModel, UpdateDoctorRequest>();
        }
    }
}

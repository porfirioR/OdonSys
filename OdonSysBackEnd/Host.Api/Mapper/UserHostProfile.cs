using AutoMapper;
using Contract.Authentication.User;
using Host.Api.Models.Users;

namespace Host.Api.Mapper
{
    public class UserHostProfile : Profile
    {
        public UserHostProfile()
        {
            CreateMap<RegisterUserApiRequest, RegisterUserRequest>();
            CreateMap<UpdateUserApiRequest, UpdateUserRequest>().ReverseMap();
        }
    }
}

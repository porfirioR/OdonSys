using AutoMapper;
using Contract.Admin.User;
using Host.Api.Models.Users;

namespace Host.Api.Mapper
{
    public class UserHostProfile : Profile
    {
        public UserHostProfile()
        {
            CreateMap<CreateUserApiRequest, CreateUserRequest>().ReverseMap();
            CreateMap<UpdateUserApiRequest, UpdateUserRequest>().ReverseMap();
        }
    }
}

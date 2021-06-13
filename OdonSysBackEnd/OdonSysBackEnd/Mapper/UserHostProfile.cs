using AutoMapper;
using OdonSysBackEnd.Models.Users;
using Resources.Contract.User;

namespace OdonSys.Api.Main.Mapper
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

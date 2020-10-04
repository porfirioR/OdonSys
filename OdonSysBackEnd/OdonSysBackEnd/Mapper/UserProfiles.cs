using AutoMapper;
using OdonSys.Api.Main.DTO.Users;
using OdonSys.Middleware.Users;
using OdonSys.Storage.Sql.Entities;

namespace OdonSys.Api.Main.Mapper
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<CreateUserDTO, CreateUserMiddleware>().ReverseMap();
            CreateMap<CreateUserMiddleware, User>().ReverseMap();
            CreateMap<UpdateUserDTO, UpdateUserMiddleware>().ReverseMap();
            CreateMap<UpdateUserMiddleware, User>().ReverseMap();
        }
    }
}

using Access.Admin.Access;
using Access.Contract.Users;
using AutoMapper;
using Contract.Authentication.User;
using Contract.Workspace.User;

namespace Manager.Workspace.Mapper
{
    public class UserManagerProfile : Profile
    {
        public UserManagerProfile()
        {
            CreateMap<RegisterUserRequest, UserDataAccess>();

            CreateMap<UpdateDoctorRequest, UserDataAccess>();

            CreateMap<UserDataAccessModel, UserModel>();

            CreateMap<DoctorDataAccessModel, DoctorModel>();
        }
    }
}

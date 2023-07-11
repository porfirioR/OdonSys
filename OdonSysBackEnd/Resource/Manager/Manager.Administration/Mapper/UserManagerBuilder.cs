using Access.Contract.Authentication;
using Access.Contract.Users;
using Contract.Administration.Authentication;
using Contract.Administration.Users;

namespace Manager.Administration.Mapper
{
    internal class UserManagerBuilder : IUserManagerBuilder
    {
        public AuthenticationModel MapAuthenticationAccessModelToAuthenticationModel(AuthenticationAccessModel authenticationAccessModel)
        {
            throw new NotImplementedException();
        }

        public DoctorModel MapDoctorDataAccessModelToDoctorModel(DoctorDataAccessModel doctorDataAccessModel)
        {
            throw new NotImplementedException();
        }

        public UserDataAccessRequest MapRegisterUserRequestToUserDataAccessRequest(RegisterUserRequest registerUserRequest)
        {
            throw new NotImplementedException();
        }

        public UserDataAccessRequest MapUpdateDoctorRequestToUserDataAccessRequest(UpdateDoctorRequest updateDoctorRequest)
        {
            throw new NotImplementedException();
        }

        public UserModel MapUserDataAccessModelToUserModel(UserDataAccessModel userDataAccessModel)
        {
            throw new NotImplementedException();
        }
    }
}

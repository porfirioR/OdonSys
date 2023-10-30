using Access.Contract.Authentication;
using Access.Contract.Azure;
using Access.Contract.Users;
using Contract.Administration.Authentication;

namespace Contract.Administration.Users
{
    public interface IUserManagerBuilder
    {
        UserDataAccessRequest MapRegisterUserRequestToUserDataAccessRequest(RegisterUserRequest registerUserRequest);
        AuthenticationModel MapAuthenticationAccessModelToAuthenticationModel(AuthenticationAccessModel authenticationAccessModel);
        UserDataAccessRequest MapUpdateDoctorRequestToUserDataAccessRequest(UpdateDoctorRequest updateDoctorRequest);
        UserModel MapUserDataAccessModelToUserModel(UserDataAccessModel userDataAccessModel);
        DoctorModel MapDoctorDataAccessModelToDoctorModel(DoctorDataAccessModel doctorDataAccessModel);
        DoctorModel MapUserGraphAccessModelToDoctorModel(UserGraphAccessModel userGraphAccessModel);
        UserModel MapUserGraphAccessModelToUserModel(string id, UserGraphAccessModel userGraphAccessModel);
        UserDataAccessRequest MapUserGraphAccessModelToRegisterUserRequest(UserGraphAccessModel userGraphAccessModel);
    }
}

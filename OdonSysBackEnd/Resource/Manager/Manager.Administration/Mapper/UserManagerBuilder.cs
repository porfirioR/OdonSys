using Access.Contract.Authentication;
using Access.Contract.Users;
using Contract.Administration.Authentication;
using Contract.Administration.Users;

namespace Manager.Administration.Mapper
{
    internal class UserManagerBuilder : IUserManagerBuilder
    {
        public AuthenticationModel MapAuthenticationAccessModelToAuthenticationModel(AuthenticationAccessModel authenticationAccessModel) => new(
            MapUserDataAccessModelToUserModel(authenticationAccessModel.User),
            authenticationAccessModel.Token,
            authenticationAccessModel.ExpirationDate,
            authenticationAccessModel.Scheme
        );

        public DoctorModel MapDoctorDataAccessModelToDoctorModel(DoctorDataAccessModel doctorDataAccessModel) => new(
            doctorDataAccessModel.Id,
            doctorDataAccessModel.Name,
            doctorDataAccessModel.MiddleName,
            doctorDataAccessModel.Surname,
            doctorDataAccessModel.SecondSurname,
            doctorDataAccessModel.Document,
            doctorDataAccessModel.Country,
            doctorDataAccessModel.Email,
            doctorDataAccessModel.Phone,
            doctorDataAccessModel.UserName,
            doctorDataAccessModel.Active,
            doctorDataAccessModel.Approved,
            doctorDataAccessModel.Roles
        );

        public UserDataAccessRequest MapRegisterUserRequestToUserDataAccessRequest(RegisterUserRequest registerUserRequest) => new(
            string.Empty,
            registerUserRequest.Name,
            registerUserRequest.MiddleName,
            registerUserRequest.Surname,
            registerUserRequest.SecondSurname,
            registerUserRequest.Document,
            registerUserRequest.Password,
            registerUserRequest.Phone,
            registerUserRequest.Email,
            registerUserRequest.Country,
            false
        );

        public UserDataAccessRequest MapUpdateDoctorRequestToUserDataAccessRequest(UpdateDoctorRequest updateDoctorRequest) => new(
            updateDoctorRequest.Id,
            updateDoctorRequest.Name,
            updateDoctorRequest.MiddleName,
            updateDoctorRequest.Surname,
            updateDoctorRequest.SecondSurname,
            updateDoctorRequest.Document,
            string.Empty,
            updateDoctorRequest.Phone,
            string.Empty,
            updateDoctorRequest.Country,
            updateDoctorRequest.Active
        );

        public UserModel MapUserDataAccessModelToUserModel(UserDataAccessModel userDataAccessModel) => new(
            userDataAccessModel.Id,
            userDataAccessModel.UserName,
            userDataAccessModel.Active,
            userDataAccessModel.Approved,
            userDataAccessModel.Roles
        );
    }
}

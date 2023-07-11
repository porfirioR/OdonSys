﻿using Access.Contract.Authentication;
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
    }
}
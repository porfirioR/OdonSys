using Contract.Administration.Users;
using Host.Api.Contract.Authorization;
using Host.Api.Contract.MapBuilders;
using Host.Api.Contract.Users;

namespace Host.Api.Mapper
{
    internal sealed class UserHostBuilder : IUserHostBuilder
    {
        public UpdateDoctorRequest MapDoctorModelToUpdateDoctorRequest(DoctorModel doctorModel) => new(
            doctorModel.Id,
            doctorModel.Name,
            doctorModel.MiddleName,
            doctorModel.Surname,
            doctorModel.SecondSurname,
            doctorModel.Document,
            doctorModel.Country,
            doctorModel.Phone,
            doctorModel.Active
        );

        public RegisterUserRequest MapRegisterUserApiRequestToRegisterUserRequest(RegisterUserApiRequest registerUserApiRequest) => new(
            registerUserApiRequest.Name,
            registerUserApiRequest.MiddleName,
            registerUserApiRequest.Surname,
            registerUserApiRequest.SecondSurname,
            registerUserApiRequest.Document,
            registerUserApiRequest.Password,
            registerUserApiRequest.Phone,
            registerUserApiRequest.Email,
            registerUserApiRequest.Country
        );

        public UpdateDoctorRequest MapUpdateDoctorApiRequestToUpdateDoctorRequest(UpdateDoctorApiRequest updateDoctorRequest) => new(
            updateDoctorRequest.Id,
            updateDoctorRequest.Name,
            updateDoctorRequest.MiddleName,
            updateDoctorRequest.Surname,
            updateDoctorRequest.SecondSurname,
            updateDoctorRequest.Document,
            updateDoctorRequest.Country,
            updateDoctorRequest.Phone,
            updateDoctorRequest.Active
        );
    }
}

using Contract.Administration.Users;
using Host.Api.Contract.Authorization;
using Host.Api.Contract.Users;

namespace Host.Api.Contract.MapBuilders;

public interface IUserHostBuilder
{
    RegisterUserRequest MapRegisterUserApiRequestToRegisterUserRequest(RegisterUserApiRequest registerUserApiRequest);
    UpdateDoctorRequest MapUpdateDoctorApiRequestToUpdateDoctorRequest(UpdateDoctorApiRequest updateDoctorRequest);
    UpdateDoctorRequest MapDoctorModelToUpdateDoctorRequest(DoctorModel doctorModel);
}

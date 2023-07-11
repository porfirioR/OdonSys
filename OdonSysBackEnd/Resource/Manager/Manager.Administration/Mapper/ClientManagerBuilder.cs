using Access.Contract.Clients;
using Contract.Administration.Clients;
using Contract.Administration.Users;

namespace Manager.Administration.Mapper
{
    internal sealed class ClientManagerBuilder : IClientManagerBuilder
    {
        public ClientModel MapClientAccessModelToClientModel(ClientAccessModel accessModel)
        {
            var doctors = accessModel.Doctors != null && accessModel.Doctors.Any() ?
                accessModel.Doctors.Select(x => new DoctorModel(
                    x.Id,
                    x.Name,
                    x.MiddleName,
                    x.Surname,
                    x.SecondSurname,
                    x.Document,
                    x.Country,
                    x.Email,
                    x.Phone,
                    x.UserName,
                    x.Active,
                    x.Approved,
                    x.Roles
                )) :
                new List<DoctorModel>();
            return new()
            {
                Id = accessModel.Id,
                Active = accessModel.Active,
                DateCreated = accessModel.DateCreated,
                DateModified = accessModel.DateModified,
                Name = accessModel.Name,
                MiddleName = accessModel.MiddleName,
                Surname = accessModel.Surname,
                SecondSurname = accessModel.SecondSurname,
                Document = accessModel.Document,
                Ruc = accessModel.Ruc,
                Country = accessModel.Country,
                Debts = accessModel.Debts,
                Phone = accessModel.Phone,
                Email = accessModel.Email,
                Doctors = doctors
            };
        }

        public CreateClientAccessRequest MapCreateClientRequestToCreateClientAccessRequest(CreateClientRequest request)
        {
            return new CreateClientAccessRequest(
                request.Name,
                request.MiddleName,
                request.Surname,
                request.SecondSurname,
                request.Document,
                request.Ruc,
                request.Phone,
                request.Country,
                false,
                request.Email
            );
        }

        public UpdateClientAccessRequest MapUpdateClientRequestToUpdateClientAccessRequest(UpdateClientRequest request)
        {
            return new UpdateClientAccessRequest(
                request.Id,
                request.Active,
                request.Name,
                request.MiddleName,
                request.Surname,
                request.SecondSurname,
                request.Phone,
                request.Email
            );
        }
    }
}

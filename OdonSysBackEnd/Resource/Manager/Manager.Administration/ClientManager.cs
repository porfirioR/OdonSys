using Access.Contract.Clients;
using Access.Contract.Invoices;
using Contract.Administration.Clients;
using Contract.Administration.Reports;
using Contract.Administration.Users;

namespace Manager.Administration
{
    internal sealed class ClientManager : IClientManager
    {
        private readonly IClientAccess _clientAccess;
        private readonly IClientManagerBuilder _clientManagerBuilder;
        private readonly IInvoiceAccess _invoiceAccess;

        public ClientManager(IClientAccess clientAccess, IClientManagerBuilder clientManagerBuilder, IInvoiceAccess invoiceAccess)
        {
            _clientAccess = clientAccess;
            _clientManagerBuilder = clientManagerBuilder;
            _invoiceAccess = invoiceAccess;
        }

        public async Task<ClientModel> CreateAsync(CreateClientRequest request)
        {
            var accessRequest = _clientManagerBuilder.MapCreateClientRequestToCreateClientAccessRequest(request);
            var accessModel = await _clientAccess.CreateClientAsync(accessRequest);
            var allClientsForCurrentDoctor = await AssignClientToUser(new AssignClientRequest(request.UserId, accessModel.Id));
            var currentDoctor = allClientsForCurrentDoctor.SelectMany(x => x.Doctors).Distinct().First(x => x.Id == request.UserId);
            var model = _clientManagerBuilder.MapClientAccessModelToClientModel(accessModel);
            model.Doctors = new List<DoctorModel>() { currentDoctor };
            return model;
        }

        public async Task<ClientModel> UpdateAsync(UpdateClientRequest request)
        {
            var accessRequest = _clientManagerBuilder.MapUpdateClientRequestToUpdateClientAccessRequest(request);
            var accessModel = await _clientAccess.UpdateClientAsync(accessRequest);
            return _clientManagerBuilder.MapClientAccessModelToClientModel(accessModel);
        }

        public async Task<IEnumerable<ClientModel>> GetAllAsync()
        {
            var accessModel = await _clientAccess.GetAllAsync();
            return accessModel.Select(_clientManagerBuilder.MapClientAccessModelToClientModel);
        }

        public async Task<ClientModel> GetByIdAsync(string id)
        {
            var accessModel = await _clientAccess.GetByIdAsync(id);
            return _clientManagerBuilder.MapClientAccessModelToClientModel(accessModel);
        }

        public async Task<ClientModel> GetByDocumentAsync(string documentId)
        {
            var accessModel = await _clientAccess.GetByDocumentAsync(documentId);
            return _clientManagerBuilder.MapClientAccessModelToClientModel(accessModel);
        }

        public async Task<ClientModel> DeleteAsync(string id)
        {
            var accessModel = await _clientAccess.DeleteAsync(id);
            return _clientManagerBuilder.MapClientAccessModelToClientModel(accessModel);
        }

        public async Task<IEnumerable<ClientModel>> GetClientsByUserIdAsync(string id, string userName)
        {
            var accessModel = await _clientAccess.GetClientsByUserIdAsync(id, userName);
            return accessModel.Select(_clientManagerBuilder.MapClientAccessModelToClientModel);
        }

        public async Task<IEnumerable<ClientModel>> AssignClientToUser(AssignClientRequest request)
        {

            var clients = await GetClientsByUserIdAsync(request.UserId, "");
            var accessModelList = clients.Any(x => x.Id == request.ClientId) ?
                await _clientAccess.GetClientsByUserIdAsync(request.UserId, string.Empty) :
                await _clientAccess.AssignClientToUserAsync(new AssignClientAccessRequest(request.UserId, request.ClientId));
            return accessModelList.Select(_clientManagerBuilder.MapClientAccessModelToClientModel);
        }

        public async Task<bool> IsDuplicateEmailAsync(string email, string id = null)
        {
            return await _clientAccess.IsDuplicateEmailAsync(email, id);
        }

        public async Task<bool> IsDuplicateDocumentAsync(string document, string id)
        {
            return await _clientAccess.IsDuplicateDocumentAsync(document, id);
        }

        public async Task<ClientReportModel> GetReportByIdAsync(string id)
        {
            var clientAccessModel = await _clientAccess.GetByIdAsync(id);
            var invoicesAccessModel = await _invoiceAccess.GetInvoicesByClientIdAsync(id);
            var clientReportModel = new ClientReportModel(
                _clientManagerBuilder.MapClientAccessModelToClientModel(clientAccessModel),
                invoicesAccessModel.Select(x => new ClientInvoiceReportModel(
                    x.Id,
                    x.Total,
                    x.DateCreated,
                    x.InvoiceDetails.Select(y => new InvoiceDetailReportModel(
                        y.Id,
                        y.Procedure,
                        y.ProcedurePrice,
                        y.FinalPrice, y.DateCreated, y.ToothIds
                    ))
                ))
            );
            return clientReportModel;
        }
    }
}

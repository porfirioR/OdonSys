using Access.Contract.Clients;
using Access.Contract.Invoices;
using Access.Sql.Entities;

namespace Access.Data.Mapper
{
    internal class InvoiceDataAccessBuilder : IInvoiceDataAccessBuilder
    {
        public Invoice MapInvoiceAccessRequestToInvoice(InvoiceAccessRequest request)
        {
            var entity = new Invoice()
            {
                InvoiceNumber = request.InvoiceNumber,
                Iva10 = request.Iva10,
                TotalIva = request.TotalIva,
                SubTotal = request.SubTotal,
                Total = request.Total,
                Timbrado = request.Timbrado,
                Status = request.Status,
                ClientId = request.ClientId,
                Active = true
            };
            return entity;
        }

        public InvoiceDetail MapInvoiceDetailAccessRequestToInvoiceDetail(InvoiceDetailAccessRequest request, Invoice entity)
        {
            var invoiceDetail = new InvoiceDetail()
            {
                ClientProcedureId = request.ClientProcedureId,
                ProcedurePrice = request.ProcedurePrice,
                FinalPrice = request.FinalPrice,
                Active = true,
                Id = Guid.NewGuid(),
                InvoiceId = entity.Id
            };
            return invoiceDetail;
        }

        public InvoiceAccessModel GetModel(Invoice entity, IEnumerable<ClientProcedure> clientProcedureEntities)
        {
            var clientFullName = entity.Client != null ?
                                $"{entity.Client.Name} {entity.Client.MiddleName} {entity.Client.Surname} {entity.Client.SecondSurname}" :
                                string.Empty;

            var invoiceDetails = entity.InvoiceDetails is null ?
                new List<InvoiceDetailAccessModel>() :
                entity.InvoiceDetails.Select(x =>
                {
                    var clientProcedure = clientProcedureEntities.FirstOrDefault(y => y.Id == x.ClientProcedureId);
                    return new InvoiceDetailAccessModel(
                        x.Id,
                        x.InvoiceId,
                        clientProcedure is null ? string.Empty : clientProcedure.Procedure.Name,
                        x.ProcedurePrice,
                        x.FinalPrice,
                        x.DateCreated,
                        x.UserCreated
                    );
                });

            return new InvoiceAccessModel(
                entity.Id,
                entity.InvoiceNumber,
                entity.Iva10,
                entity.TotalIva,
                entity.SubTotal,
                entity.Total,
                entity.Timbrado,
                entity.Status,
                entity.ClientId,
                clientFullName,
                entity.DateCreated,
                entity.UserCreated,
                invoiceDetails
            );
        }
    }
}

using Access.Contract.Invoices;
using Access.Sql;
using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore;

namespace Access.Data.Access
{
    internal sealed class InvoiceAccess : IInvoiceAccess
    {
        private readonly DataContext _context;
        private readonly IInvoiceDataAccessBuilder _invoiceDataAccessBuilder;

        public InvoiceAccess(DataContext context, IInvoiceDataAccessBuilder invoiceDataAccessBuilder)
        {
            _context = context;
            _invoiceDataAccessBuilder = invoiceDataAccessBuilder;
        }

        public async Task<InvoiceAccessModel> CreateInvoiceAsync(InvoiceAccessRequest accessRequest)
        {
            var entity = _invoiceDataAccessBuilder.MapInvoiceAccessRequestToInvoice(accessRequest);
            _context.Invoices.Add(entity);
            if (accessRequest.InvoiceDetails != null && accessRequest.InvoiceDetails.Any())
            {
                var invoiceDetails = accessRequest.InvoiceDetails.Select(x => _invoiceDataAccessBuilder.MapInvoiceDetailAccessRequestToInvoiceDetail(x, entity));
                _context.InvoiceDetails.AddRange(invoiceDetails);
            }
            await _context.SaveChangesAsync();
            var clientProcedureIds = entity.InvoiceDetails.Select(x => x.ClientProcedureId);
            var clientProcedureEntities = await GetClientProcedureEntities(clientProcedureIds);
            return _invoiceDataAccessBuilder.GetModel(entity, clientProcedureEntities);
        }

        public async Task<IEnumerable<InvoiceAccessModel>> GetInvoicesAsync()
        {
            var entities = _context.Invoices
                                    .Include(x => x.Client)
                                    .AsNoTrackingWithIdentityResolution()
                                    .OrderByDescending(x => x.DateCreated);

            var accessModels = entities.Select(x => _invoiceDataAccessBuilder.GetModel(x, new List<ClientProcedure>()));
            return await accessModels.ToListAsync();
        }

        public async Task<InvoiceAccessModel> GetInvoiceByIdAsync(string id)
        {
            var entity = await _context.Invoices
                                    .Include(x => x.Client)
                                    .AsNoTracking()
                                    .Include(x => x.InvoiceDetails).ThenInclude(x => x.InvoiceDetailsTeeth)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == new Guid(id));

            var clientProcedureIds = entity.InvoiceDetails.Select(y => y.ClientProcedureId);
            var clientProcedureEntities = await GetClientProcedureEntities(clientProcedureIds);
            return _invoiceDataAccessBuilder.GetModel(entity, clientProcedureEntities);
        }

        public async Task<bool> IsValidInvoiceIdAsync(string id)
        {
            var entity = await _context.Invoices.FirstOrDefaultAsync(x => x.Id == new Guid(id));
            return entity != null;
        }

        public async Task<InvoiceAccessModel> UpdateInvoiceStatusIdAsync(InvoiceStatusAccessRequest accessRequest)
        {
            var entity = await _context.Invoices
                                    .FirstOrDefaultAsync(x => x.Id == new Guid(accessRequest.InvoiceId));
            entity.Status = accessRequest.Status;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _invoiceDataAccessBuilder.GetModel(entity, new List<ClientProcedure>());
        }

        private async Task<IEnumerable<ClientProcedure>> GetClientProcedureEntities(IEnumerable<Guid> clientProcedureIds)
        {
            return await _context.ClientProcedures
                                    .Include(x => x.Procedure)
                                    .Where(x => clientProcedureIds.Contains(x.Id)).ToListAsync();
        }
    }
}

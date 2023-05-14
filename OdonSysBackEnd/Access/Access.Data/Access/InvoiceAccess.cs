using Access.Contract.Invoices;
using Access.Sql;
using Access.Sql.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Access.Data.Access
{
    internal class InvoiceAccess : IInvoiceAccess
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public InvoiceAccess(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<InvoiceAccessModel> CreateInvoiceAsync(InvoiceAccessRequest accessRequest)
        {
            var entity = _mapper.Map<Invoice>(accessRequest);
            _context.Invoices.Add(entity);
            await _context.SaveChangesAsync();
            var clientProcedureIds = entity.InvoiceDetails.Select(x => x.ClientProcedureId);
            var clientProcedureEntities = await GetClientProcedureEntities(clientProcedureIds);
            return GetModel(entity, clientProcedureEntities);
        }

        public async Task<IEnumerable<InvoiceAccessModel>> GetInvoicesAsync()
        {
            var entities = await _context.Invoices
                                    .Include(x => x.InvoiceDetails)
                                    .AsNoTracking()
                                    .ToListAsync();
            var clientProcedureIds = entities.SelectMany(x => x.InvoiceDetails.Select(y => y.ClientProcedureId));
            var clientProcedureEntities = await GetClientProcedureEntities(clientProcedureIds);
            return entities.Select(x => GetModel(x, clientProcedureEntities));
        }

        private async Task<IEnumerable<ClientProcedure>> GetClientProcedureEntities(IEnumerable<Guid> clientProcedureIds)
        {
            return await _context.ClientProcedures
                                    .Include(x => x.Procedure)
                                    .Where(x => clientProcedureIds.Contains(x.Id)).ToListAsync();
        }

        private static InvoiceAccessModel GetModel(Invoice entity, IEnumerable<ClientProcedure> clientProcedureEntities)
        {
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
                entity.InvoiceDetails.Select(x => new InvoiceDetailAccessModel(
                    x.Id,
                    x.InvoiceId,
                    clientProcedureEntities.First(y => y.Id == x.ClientProcedureId).Procedure.Name,
                    x.ProcedurePrice,
                    x.FinalPrice
                ))
            );
        }

        public async Task<bool> IsValidInvoiceIdAsync(string id)
        {
            var entity = await _context.Invoices.FirstOrDefaultAsync(x => x.Id == new Guid(id));
            return entity != null;
        }
    }
}

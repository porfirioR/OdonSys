using Access.Contract.Invoices;
using Access.Sql;
using Access.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using Utilities.Enums;

namespace Access.Data.Access;

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
                                .FirstAsync(x => x.Id == new Guid(id));

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

    public async Task<InvoiceAccessModel> UpdateInvoiceAsync(UpdateInvoiceAccessRequest accessRequest)
    {
        var entity = await _context.Invoices
                .Include(x => x.InvoiceDetails).ThenInclude(x => x.InvoiceDetailsTeeth)
                .Include(x => x.Payments)
                .FirstAsync(x => x.Id == accessRequest.Id);

        foreach (var invoiceDetailAccessRequest in accessRequest.InvoiceDetails)
        {
            var entityInvoiceDetail = entity.InvoiceDetails.First(x => x.Id == invoiceDetailAccessRequest.Id);
            entityInvoiceDetail.FinalPrice = invoiceDetailAccessRequest.FinalPrice;
            // new inoice detail teeth
            var allTeeth = entityInvoiceDetail.InvoiceDetailsTeeth.Select(x => x.ToothId.ToString());
            var newTeeth = invoiceDetailAccessRequest.ToothIds.Where(x => !allTeeth.Contains(x)).Select(x => new InvoiceDetailTooth() { ToothId = new Guid(x), InvoiceDetailId = invoiceDetailAccessRequest.Id });
            var removeInvoiceTeeth = entityInvoiceDetail.InvoiceDetailsTeeth.Where(x => !invoiceDetailAccessRequest.ToothIds.Contains(x.ToothId.ToString()));

            var persistInvoiceDetailTeeth = entityInvoiceDetail.InvoiceDetailsTeeth.Where(x => invoiceDetailAccessRequest.ToothIds.Contains(x.ToothId.ToString()));
            entityInvoiceDetail.InvoiceDetailsTeeth = persistInvoiceDetailTeeth.Concat(newTeeth).ToList();
        }

        entity.Iva10 = accessRequest.Iva10;
        entity.TotalIva = accessRequest.TotalIva;
        entity.SubTotal = accessRequest.SubTotal;
        entity.Total = accessRequest.Total;
        var invoicePaymentsAmount = entity.Payments.Sum(x => x.Amount);
        if (invoicePaymentsAmount == entity.Total)
        {
            entity.Status = InvoiceStatus.Completado;
        }
        _context.Entry(entity).State = EntityState.Modified;

        await _context.SaveChangesAsync();
        return _invoiceDataAccessBuilder.GetModel(entity, new List<ClientProcedure>());
    }

    public async Task<IEnumerable<InvoiceAccessModel>> GetInvoicesByClientIdAsync(string clientId)
    {
        var entities = await _context.Invoices
                                .Include(x => x.Client)
                                .AsNoTracking()
                                .Include(x => x.InvoiceDetails).ThenInclude(x => x.InvoiceDetailsTeeth)
                                .AsNoTracking()
                                .Where(x => x.ClientId == new Guid(clientId)).ToListAsync();

        var accessModelList = new List<InvoiceAccessModel>();
        foreach (var entity in entities)
        {
            var clientProcedureIds = entity.InvoiceDetails.Select(y => y.ClientProcedureId);
            var clientProcedureEntities = await GetClientProcedureEntities(clientProcedureIds);
            accessModelList.Add(_invoiceDataAccessBuilder.GetModel(entity, clientProcedureEntities));
        }
        return accessModelList;
    }
}

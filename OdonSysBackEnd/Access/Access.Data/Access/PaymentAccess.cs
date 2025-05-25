using Access.Contract.Orthodontics;
using Access.Contract.Payments;
using Access.Sql;
using Microsoft.EntityFrameworkCore;

namespace Access.Data.Access
{
    internal sealed class PaymentAccess : IPaymentAccess
    {
        private readonly DataContext _context;
        private readonly IPaymentDataAccessBuilder _paymentDataAccessBuilder;

        public PaymentAccess(DataContext context, IPaymentDataAccessBuilder paymentDataAccessBuilder)
        {
            _context = context;
            _paymentDataAccessBuilder = paymentDataAccessBuilder;
        }

        public async Task<PaymentAccessModel> RegisterPayment(PaymentAccessRequest accessRequest)
        {
            var entity = _paymentDataAccessBuilder.MapPaymentAccessRequestToPayment(accessRequest);
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return _paymentDataAccessBuilder.MapPaymentToPaymentAccessModel(entity);
        }

        public async Task<IEnumerable<PaymentAccessModel>> GetPaymentsByInvoiceIdAsync(string invoiceId)
        {
            var entities = await _context.Payments
                                .AsNoTracking()
                                .Where(x => x.InvoiceId == new Guid(invoiceId))
                                .ToListAsync();

            return entities.Select(_paymentDataAccessBuilder.MapPaymentToPaymentAccessModel);
        }

        public async Task<IEnumerable<PaymentAmountAccessModel>> GetPaymentsAmountByInvoiceIdAsync(IEnumerable<Guid> invoiceIds)
        {
            var groupByList = await _context.Payments
                                .AsNoTracking()
                                .Where(x => invoiceIds.Contains(x.InvoiceId))
                                .GroupBy(x => x.InvoiceId)
                                .Select(x => new { InvoiceId = x.Key, PaymentAmount = x.Sum(y => y.Amount) })
                                .ToListAsync();
            var sumPayments = groupByList.Select(x => new PaymentAmountAccessModel(x.InvoiceId, x.PaymentAmount));
            return sumPayments;
        }
    }
}

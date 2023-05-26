using Access.Contract.Payments;
using Access.Sql;
using Access.Sql.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Access.Data.Access
{
    internal class PaymentAccess : IPaymentAccess
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PaymentAccess(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PaymentAccessModel> RegisterPayment(PaymentAccessRequest accessRequest)
        {
            var entity = _mapper.Map<Payment>(accessRequest);
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return new PaymentAccessModel(
                entity.InvoiceId.ToString(),
                entity.UserId.ToString(),
                entity.DateCreated,
                entity.Amount
            );
        }

        public async Task<IEnumerable<PaymentAccessModel>> GetPaymentsByInvoiceIdAsync(string invoiceId)
        {
            var entities = await _context.Payments
                                .AsNoTracking()
                                .Where(x => x.InvoiceId == new Guid(invoiceId))
                                .ToListAsync();

            return entities.Select(entity => new PaymentAccessModel(entity.InvoiceId.ToString(), entity.UserId.ToString(), entity.DateCreated, entity.Amount));
        }

        public async Task<int> GetPaymentsAmountByInvoiceIdAsync(Guid invoiceId)
        {
            var sumPayments = await _context.Payments
                                .AsNoTracking()
                                .Where(x => x.InvoiceId == invoiceId)
                                .Select(x => x.Amount)
                                .SumAsync();

            return sumPayments;
        }
    }
}

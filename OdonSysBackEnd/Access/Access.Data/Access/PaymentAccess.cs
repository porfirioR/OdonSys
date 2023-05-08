using Access.Contract.Payments;
using Access.Sql;
using Access.Sql.Entities;
using AutoMapper;

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
            _context.Payments.Add(entity);
            await _context.SaveChangesAsync();
            return new PaymentAccessModel(entity.HeaderBillId.ToString(), entity.UserId.ToString(), entity.Amount);
        }
    }
}

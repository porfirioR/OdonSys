﻿using Access.Contract.Payments;
using Contract.Pyment.Payments;

namespace Manager.Payment
{
    internal class PaymentManager : IPaymentManager
    {
        private readonly IPaymentAccess _paymentAccess;

        public PaymentManager(IPaymentAccess paymentAccess)
        {
            _paymentAccess = paymentAccess;
        }

        public async Task<IEnumerable<PaymentModel>> GetPaymentsByBillHeaderIdAsync(string headerBillId)
        {
            var accessModel = await _paymentAccess.GetPaymentsByBillHeaderId(headerBillId);
            return accessModel.Select(x => new PaymentModel(x.HeaderBillId, x.UserId, x.DateCreated, x.Amount));
        }

        public async Task<PaymentModel> RegisterPaymentAsync(PaymentRequest request)
        {
            var accessRequest = new PaymentAccessRequest(request.HeaderBillId, request.UserId, request.Amount);
            var accessModel = await _paymentAccess.RegisterPayment(accessRequest);
            return new PaymentModel(accessModel.HeaderBillId, accessModel.UserId, accessModel.DateCreated, accessModel.Amount);
        }
    }
}

﻿namespace Contract.Payment.Payments;

public interface IPaymentManager
{
    Task<IEnumerable<PaymentModel>> GetPaymentsByInvoiceIdAsync(string invoiceId);
    Task<RegisterPaymentModel> RegisterPaymentAsync(PaymentRequest accessRequest);
}

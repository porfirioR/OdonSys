using Contract.Payment.Invoices;
using Contract.Payment.Payments;
using Microsoft.Extensions.DependencyInjection;

namespace Manager.Payment;

public class ServiceInjection
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IInvoiceManager, InvoiceManager>();
        services.AddTransient<IPaymentManager, PaymentManager>();
    }
}

using Contract.Pyment.Invoices;
using Contract.Pyment.Payments;
using Microsoft.Extensions.DependencyInjection;

namespace Manager.Payment
{
    public class ServiceInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IInvoiceManager, InvoiceManager>();
            services.AddTransient<IPaymentManager, PaymentManager>();
        }
    }
}

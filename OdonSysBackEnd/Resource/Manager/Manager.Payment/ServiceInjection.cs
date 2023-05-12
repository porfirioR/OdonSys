using Contract.Pyment.Bills;
using Contract.Pyment.Payments;
using Microsoft.Extensions.DependencyInjection;

namespace Manager.Payment
{
    public class ServiceInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IBillManager, BillManager>();
            services.AddTransient<IPaymentManager, PaymentManager>();
        }
    }
}

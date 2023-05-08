using Contract.Pyments.Bills;
using Microsoft.Extensions.DependencyInjection;

namespace Manager.Payment
{
    public class ServiceInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IBillManager, BillManager>();
        }
    }
}

using Access.Contract.Auth;
using Access.Contract.ClientProcedure;
using Access.Contract.Clients;
using Access.Contract.Files;
using Access.Contract.Invoices;
using Access.Contract.Payments;
using Access.Contract.Procedure;
using Access.Contract.Roles;
using Access.Contract.Teeth;
using Access.Contract.Users;
using Access.Data.Access;
using Contract.Workspace.Files;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Access.Data
{
    public class ServiceInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAuthAccess, AuthAccess>();
            services.AddTransient<IClientAccess, ClientAccess>();
            services.AddTransient<IClientProcedureAccess, ClientProcedureAccess>();
            services.AddTransient<IFileAccess, Access.FileAccess>();
            services.AddTransient<IInvoiceAccess, InvoiceAccess>();
            services.AddTransient<IPaymentAccess, PaymentAccess>();
            services.AddTransient<IProcedureAccess, ProcedureAccess>();
            services.AddTransient<IRoleAccess, RoleAccess>();
            //services.AddTransient<IToothAccess, ToothAccess>();
            services.AddTransient<IUserDataAccess, UserAccess>();
            services.AddHttpContextAccessor();
        }
    }
}

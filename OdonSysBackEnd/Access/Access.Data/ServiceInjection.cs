using Access.Contract.Auth;
using Access.Contract.ClientProcedures;
using Access.Contract.Clients;
using Access.Contract.Files;
using Access.Contract.Invoices;
using Access.Contract.Payments;
using Access.Contract.Procedures;
using Access.Contract.Roles;
using Access.Contract.Users;
using Access.Data.Access;
using Access.Data.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Access.Data
{
    public class ServiceInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAuthAccess, AuthenticationAccess>();
            services.AddTransient<IClientAccess, ClientAccess>();
            services.AddTransient<IClientProcedureAccess, ClientProcedureAccess>();
            services.AddTransient<IFileAccess, Access.FileAccess>();
            services.AddTransient<IInvoiceAccess, InvoiceAccess>();
            services.AddTransient<IPaymentAccess, PaymentAccess>();
            services.AddTransient<IProcedureAccess, ProcedureAccess>();
            services.AddTransient<IRoleAccess, RoleAccess>();
            //services.AddTransient<IToothAccess, ToothAccess>();
            services.AddTransient<IUserDataAccess, UserAccess>();

            services.AddTransient<IClientDataAccessBuilder, ClientDataAccessBuilder>();
            services.AddTransient<IClientProcedureDataAccessBuilder, ClientProcedureDataAccessBuilder>();
            services.AddTransient<IInvoiceDataAccessBuilder, InvoiceDataAccessBuilder>();
            services.AddTransient<IPaymentDataAccessBuilder, PaymentDataAccessBuilder>();
            services.AddTransient<IProcedureDataAccessBuilder, ProcedureDataAccessBuilder>();
            services.AddTransient<IRoleDataAccessBuilder, RoleDataAccessBuilder>();
            services.AddTransient<IUserDataAccessBuilder, UserDataAccessBuilder>();

            services.AddHttpContextAccessor();
        }
    }
}

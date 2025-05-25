using Access.Contract.Authentication;
using Access.Contract.Azure;
using Access.Contract.ClientProcedures;
using Access.Contract.Clients;
using Access.Contract.Files;
using Access.Contract.Invoices;
using Access.Contract.Orthodontics;
using Access.Contract.Payments;
using Access.Contract.Procedures;
using Access.Contract.Roles;
using Access.Contract.Teeth;
using Access.Contract.Users;
using Access.Data.Access;
using Access.Data.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Access.Data;

public class ServiceInjection
{
    public static void ConfigureServices(IServiceCollection services)
    {
        // Access
        services.AddTransient<IAuthenticationAccess, AuthenticationAccess>();
        services.AddTransient<IClientAccess, ClientAccess>();
        services.AddTransient<IClientProcedureAccess, ClientProcedureAccess>();
        services.AddTransient<IFileAccess, Access.FileAccess>();
        services.AddTransient<IInvoiceAccess, InvoiceAccess>();
        services.AddTransient<IOrthodonticAccess, OrthodonticAccess>();
        services.AddTransient<IPaymentAccess, PaymentAccess>();
        services.AddTransient<IProcedureAccess, ProcedureAccess>();
        services.AddTransient<IRoleAccess, RoleAccess>();
        services.AddTransient<IToothAccess, ToothAccess>();
        services.AddTransient<IUserDataAccess, UserAccess>();
        services.AddTransient<IUserDataAccess, UserAccess>();

        // Builders
        services.AddTransient<IClientDataAccessBuilder, ClientDataAccessBuilder>();
        services.AddTransient<IClientProcedureDataAccessBuilder, ClientProcedureDataAccessBuilder>();
        services.AddTransient<IInvoiceDataAccessBuilder, InvoiceDataAccessBuilder>();
        services.AddTransient<IPaymentDataAccessBuilder, PaymentDataAccessBuilder>();
        services.AddTransient<IProcedureDataAccessBuilder, ProcedureDataAccessBuilder>();
        services.AddTransient<IRoleDataAccessBuilder, RoleDataAccessBuilder>();
        services.AddTransient<IToothDataAccessBuilder, ToothDataAccessBuilder>();
        services.AddTransient<IUserDataAccessBuilder, UserDataAccessBuilder>();

        // Azure
        services.AddTransient<IGraphService, GraphService>();
        //services.AddTransient<IAzureAdB2CRoleDataAccess, AzureAdB2CRoleDataAccess>();
        services.AddTransient<IAzureAdB2CUserDataAccess, AzureAdB2CUserDataAccess>();

        services.AddHttpContextAccessor();
    }
}

using Access.Contract.Payments;
using Access.Data.Mapper;
using Host.Api.Mapper;
using Manager.Admin.Mapper;
using Manager.Workspace.Procedures;

namespace Host.Api
{
    public partial class Startup
    {
        public void ConfigureMappings(IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(BillAccessProfile),
                typeof(ClientAccessProfile),
                typeof(ClientProcedureAccessProfile),
                typeof(PaymentAccessRequest),
                typeof(ProcedureAccessProfile),
                typeof(RoleAccessProfile),
                typeof(ToothAccessProfile),
                typeof(UserDataAccessProfile),

                typeof(ProcedureManagerProfile),
                typeof(UserManagerProfile),

                typeof(ProcedureHostProfile),
                typeof(UserHostProfile)
            );
        }
    }
}

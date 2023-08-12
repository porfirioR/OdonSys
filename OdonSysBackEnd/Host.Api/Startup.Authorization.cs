using Host.Api.Contract.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Utilities.Enums;

namespace Host.Api
{
    public partial class Startup
    {
        public void ConfigureAuthorization(IServiceCollection services)
        {
            var policiesToAdd = new List<PolicyModel>
            {
                new PolicyModel(Policy.CanAccessRole, new AuthorizationRequirement(PermissionName.AccessRoles)),
                new PolicyModel(Policy.CanManageRole, new AuthorizationRequirement(PermissionName.ManageRoles)),
                new PolicyModel(Policy.CanAssignRoleDoctors, new AuthorizationRequirement(PermissionName.AssignRoleDoctors)),

                new PolicyModel(Policy.CanAccessInvoice, new AuthorizationRequirement(PermissionName.AccessInvoices)),
                new PolicyModel(Policy.CanAccessMyInvoice, new AuthorizationRequirement(PermissionName.AccessMyInvoices)),
                new PolicyModel(Policy.CanCreateInvoice, new AuthorizationRequirement(PermissionName.CreateInvoices)),
                new PolicyModel(Policy.CanAccessInvoiceFiles, new AuthorizationRequirement(
                    new List<PermissionName> {
                        PermissionName.CreateInvoices,
                        PermissionName.AccessMyInvoices,
                        PermissionName.AccessInvoices
                    }
                )),
                new PolicyModel(Policy.CanAccessTeeth, new AuthorizationRequirement(
                    new List<PermissionName> {
                        PermissionName.AccessMyInvoices,
                        PermissionName.AccessInvoices,
                        PermissionName.AccessClients,
                        PermissionName.AccessMyClients
                    }
                )),
                new PolicyModel(Policy.CanChangeInvoiceStatus, new AuthorizationRequirement(PermissionName.ChangeInvoiceStatus)),

                new PolicyModel(Policy.CanAccessClient, new AuthorizationRequirement(
                    new List<PermissionName> {
                        PermissionName.AccessClients,
                        PermissionName.AccessMyInvoices,
                        PermissionName.AccessInvoices,
                        PermissionName.AccessMyClients
                    }
                )),
                new PolicyModel(Policy.CanManageClient, new AuthorizationRequirement(
                    new List<PermissionName> {
                        PermissionName.CreateClients,
                        PermissionName.UpdateClients,
                        PermissionName.CreateInvoices
                    }
                )),
                new PolicyModel(Policy.CanModifyVisibilityClient, new AuthorizationRequirement(
                    new List<PermissionName> {
                        PermissionName.RestoreClients,
                        PermissionName.DeactivateClients
                    }
                )),
                new PolicyModel(Policy.CanDeleteClient, new AuthorizationRequirement(PermissionName.DeleteClients)),
                new PolicyModel(Policy.CanAssignClient, new AuthorizationRequirement(
                    new List<PermissionName> {
                        PermissionName.AssignClients,
                        PermissionName.CreateInvoices
                    }
                )),
                new PolicyModel(Policy.CanAccessMyClients, new AuthorizationRequirement(PermissionName.AccessMyClients)),
                new PolicyModel(Policy.CanApproveDoctor, new AuthorizationRequirement(PermissionName.ApproveDoctors)),
                new PolicyModel(Policy.CanDeleteDoctor, new AuthorizationRequirement(PermissionName.DeleteDoctors)),
                new PolicyModel(Policy.CanAssignDoctorRoles, new AuthorizationRequirement(PermissionName.AssignDoctorRoles)),
                new PolicyModel(Policy.CanModifyVisibilityDoctor, new AuthorizationRequirement(
                    new List<PermissionName> {
                        PermissionName.RestoreDoctors,
                        PermissionName.DeactivateDoctors
                    }
                )),
                new PolicyModel(Policy.CanUpdateDoctor, new AuthorizationRequirement(PermissionName.UpdateDoctors)),
                new PolicyModel(Policy.CanAccessDoctor, new AuthorizationRequirement(
                    new List<PermissionName> {
                        PermissionName.AccessMyData,
                        PermissionName.AccessDoctors,
                        PermissionName.RegisterPayments
                    }
                )),
                //new PolicyModel(Policy.CanManageClient, new AuthRequirement(new List<PermissionName> {
                //        PermissionName.CreateDoctors,
                //        PermissionName.UpdateDoctors,
                //        PermissionName.DeleteDoctors,
                //    })),
                new PolicyModel(Policy.CanCreateClientProcedure, new AuthorizationRequirement(
                    new List<PermissionName> {
                        PermissionName.CreateClientProcedures,
                        PermissionName.CreateInvoices
                    }
                )),
                new PolicyModel(Policy.CanUpdateClientProcedure, new AuthorizationRequirement(PermissionName.UpdateClientProcedures)),

                new PolicyModel(Policy.CanAccessProcedure, new AuthorizationRequirement(
                    new List<PermissionName> {
                        PermissionName.AccessProcedures,
                        PermissionName.CreateInvoices
                    }
                )),
                new PolicyModel(Policy.CanModifyVisibilityProcedure, new AuthorizationRequirement(
                    new List<PermissionName> {
                        PermissionName.RestoreProcedures,
                        PermissionName.DeactivateProcedures
                    }
                )),
                new PolicyModel(Policy.CanManageProcedure, new AuthorizationRequirement(
                    new List<PermissionName> {
                        PermissionName.CreateProcedures,
                        PermissionName.UpdateProcedures,
                        PermissionName.DeleteProcedures,
                    }
                )),
                new PolicyModel(Policy.CanAccessPayment, new AuthorizationRequirement(PermissionName.AccessPayments)),
                new PolicyModel(Policy.CanRegisterPayment, new AuthorizationRequirement(PermissionName.RegisterPayments)),
            };

            //  Add Authorization
            services.AddAuthorization(options =>
            {
                foreach (var policyToAdd in policiesToAdd)
                {
                    options.AddPolicy(policyToAdd.Name, policy => policyToAdd.AuthRequirements.ToList().ForEach(authRequirement => policy.Requirements.Add(authRequirement)));
                }

                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .Build();
            });
        }
    }
}

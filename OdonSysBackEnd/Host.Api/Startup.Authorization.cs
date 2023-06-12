using Host.Api.Models.Auth;
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
                new PolicyModel(Policy.CanAccessRole, new AuthRequirement(PermissionName.AccessRoles)),
                new PolicyModel(Policy.CanManageRole, new AuthRequirement(PermissionName.ManageRoles)),
                new PolicyModel(Policy.CanAssignRoleDoctors, new AuthRequirement(PermissionName.AssignRoleDoctors)),

                new PolicyModel(Policy.CanAccessInvoice, new AuthRequirement(PermissionName.AccessInvoices)),
                new PolicyModel(Policy.CanAccessMyInvoice, new AuthRequirement(PermissionName.AccessMyInvoices)),
                new PolicyModel(Policy.CanCreateInvoice, new AuthRequirement(PermissionName.CreateInvoices)),
                new PolicyModel(Policy.CanAccessInvoiceFiles, new AuthRequirement(
                    new List<PermissionName> {
                        PermissionName.CreateInvoices,
                        PermissionName.AccessMyInvoices,
                        PermissionName.AccessInvoices
                    }
                )),
                new PolicyModel(Policy.CanChangeInvoiceStatus, new AuthRequirement(PermissionName.ChangeInvoiceStatus)),

                new PolicyModel(Policy.CanAccessClient, new AuthRequirement(
                    new List<PermissionName> {
                        PermissionName.AccessClients,
                        PermissionName.AccessMyInvoices,
                        PermissionName.AccessInvoices
                    }
                )),
                new PolicyModel(Policy.CanManageClient, new AuthRequirement(
                    new List<PermissionName> {
                        PermissionName.CreateClients,
                        PermissionName.UpdateClients,
                        PermissionName.CreateInvoices
                    }
                )),
                new PolicyModel(Policy.CanModifyVisibilityClient, new AuthRequirement(
                    new List<PermissionName> {
                        PermissionName.RestoreClients,
                        PermissionName.DeactivateClients
                    }
                )),
                new PolicyModel(Policy.CanDeleteClient, new AuthRequirement(PermissionName.DeleteClients)),
                new PolicyModel(Policy.CanAssignClient, new AuthRequirement(
                    new List<PermissionName> {
                        PermissionName.AssignClients,
                        PermissionName.CreateInvoices
                    }
                )),
                new PolicyModel(Policy.CanAccessMyClients, new AuthRequirement(PermissionName.AccessMyClients)),
                new PolicyModel(Policy.CanApproveDoctor, new AuthRequirement(PermissionName.ApproveDoctors)),
                new PolicyModel(Policy.CanDeleteDoctor, new AuthRequirement(PermissionName.DeleteDoctors)),
                new PolicyModel(Policy.CanAssignDoctorRoles, new AuthRequirement(PermissionName.AssignDoctorRoles)),
                new PolicyModel(Policy.CanModifyVisibilityDoctor, new AuthRequirement(
                    new List<PermissionName> {
                        PermissionName.RestoreDoctors,
                        PermissionName.DeactivateDoctors
                    }
                )),
                new PolicyModel(Policy.CanUpdateDoctor, new AuthRequirement(PermissionName.UpdateDoctors)),
                new PolicyModel(Policy.CanAccessDoctor, new AuthRequirement(
                    new List<PermissionName> {
                        PermissionName.AccessDoctors,
                        PermissionName.RegisterPayments
                    }
                )),
                //new PolicyModel(Policy.CanManageClient, new AuthRequirement(new List<PermissionName> {
                //        PermissionName.CreateDoctors,
                //        PermissionName.UpdateDoctors,
                //        PermissionName.DeleteDoctors,
                //    })),
                new PolicyModel(Policy.CanCreateClientProcedure, new AuthRequirement(
                    new List<PermissionName> {
                        PermissionName.CreateClientProcedures,
                        PermissionName.CreateInvoices
                    }
                )),
                new PolicyModel(Policy.CanUpdateClientProcedure, new AuthRequirement(PermissionName.UpdateClientProcedures)),

                new PolicyModel(Policy.CanAccessProcedure, new AuthRequirement(
                    new List<PermissionName> {
                        PermissionName.AccessProcedures,
                        PermissionName.CreateInvoices
                    }
                )),
                new PolicyModel(Policy.CanModifyVisibilityProcedure, new AuthRequirement(
                    new List<PermissionName> {
                        PermissionName.RestoreProcedures,
                        PermissionName.DeactivateProcedures
                    }
                )),
                new PolicyModel(Policy.CanManageProcedure, new AuthRequirement(
                    new List<PermissionName> {
                        PermissionName.CreateProcedures,
                        PermissionName.UpdateProcedures,
                        PermissionName.DeleteProcedures,
                    }
                )),
                new PolicyModel(Policy.CanAccessPayment, new AuthRequirement(PermissionName.AccessPayments)),
                new PolicyModel(Policy.CanRegisterPayment, new AuthRequirement(PermissionName.RegisterPayments)),
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

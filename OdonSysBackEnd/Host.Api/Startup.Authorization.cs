using Host.Api.Contract.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Utilities.Enums;

namespace Host.Api;

public partial class Startup
{
    public void ConfigureAuthorization(IServiceCollection services)
    {
        var policiesToAdd = new List<PolicyModel>
        {
            new(Policy.CanAccessRole, new AuthorizationRequirement(PermissionName.AccessRoles)),
            new(Policy.CanManageRole, new AuthorizationRequirement(PermissionName.ManageRoles)),
            new(Policy.CanAssignRoleDoctors, new AuthorizationRequirement(PermissionName.AssignRoleDoctors)),

            new(Policy.CanAccessInvoice, new AuthorizationRequirement(
                [
                    PermissionName.AccessMyInvoices,
                    PermissionName.AccessInvoices
                ]
            )),
            new(Policy.CanAccessMyInvoice, new AuthorizationRequirement(PermissionName.AccessMyInvoices)),
            new(Policy.CanCreateInvoice, new AuthorizationRequirement(PermissionName.CreateInvoices)),
            new(Policy.CanUpdateInvoice, new AuthorizationRequirement(PermissionName.UpdateInvoices)),
            new(Policy.CanAccessInvoiceFiles, new AuthorizationRequirement(
                [
                    PermissionName.CreateInvoices,
                    PermissionName.AccessMyInvoices,
                    PermissionName.AccessInvoices
                ]
            )),
            new(Policy.CanAccessTeeth, new AuthorizationRequirement(
                [
                    PermissionName.AccessMyInvoices,
                    PermissionName.AccessInvoices,
                    PermissionName.AccessClients,
                    PermissionName.AccessMyClients,
                    PermissionName.UpdateInvoices
                ]
            )),
            new(Policy.CanChangeInvoiceStatus, new AuthorizationRequirement(PermissionName.ChangeInvoiceStatus)),

            new(Policy.CanAccessClient, new AuthorizationRequirement(
                [
                    PermissionName.AccessClients,
                    PermissionName.AccessMyInvoices,
                    PermissionName.AccessInvoices,
                    PermissionName.AccessMyClients
                ]
            )),
            new(Policy.CanManageClient, new AuthorizationRequirement(
                [
                    PermissionName.CreateClients,
                    PermissionName.UpdateClients,
                    PermissionName.CreateInvoices
                ]
            )),
            new(Policy.CanModifyVisibilityClient, new AuthorizationRequirement(
                [
                    PermissionName.RestoreClients,
                    PermissionName.DeactivateClients
                ]
            )),
            new(Policy.CanDeleteClient, new AuthorizationRequirement(PermissionName.DeleteClients)),
            new(Policy.CanAssignClient, new AuthorizationRequirement(
                [
                    PermissionName.AssignClients,
                    PermissionName.CreateInvoices
                ]
            )),
            new(Policy.CanAccessMyClients, new AuthorizationRequirement(PermissionName.AccessMyClients)),
            new(Policy.CanApproveDoctor, new AuthorizationRequirement(PermissionName.ApproveDoctors)),
            new(Policy.CanDeleteDoctor, new AuthorizationRequirement(PermissionName.DeleteDoctors)),
            new(Policy.CanAssignDoctorRoles, new AuthorizationRequirement(PermissionName.AssignDoctorRoles)),
            new(Policy.CanModifyVisibilityDoctor, new AuthorizationRequirement(
                [
                    PermissionName.RestoreDoctors,
                    PermissionName.DeactivateDoctors
                ]
            )),
            new(Policy.CanUpdateDoctor, new AuthorizationRequirement(PermissionName.UpdateDoctors)),
            new(Policy.CanAccessDoctor, new AuthorizationRequirement(
                [
                    PermissionName.AccessMyData,
                    PermissionName.AccessDoctors,
                    PermissionName.RegisterPayments
                ]
            )),
            //new(Policy.CanManageClient, new AuthRequirement(new List<PermissionName> {
            //        PermissionName.CreateDoctors,
            //        PermissionName.UpdateDoctors,
            //        PermissionName.DeleteDoctors,
            //    })),
            new(Policy.CanCreateClientProcedure, new AuthorizationRequirement(
                [
                    PermissionName.CreateClientProcedures,
                    PermissionName.CreateInvoices
                ]
            )),
            new(Policy.CanUpdateClientProcedure, new AuthorizationRequirement(PermissionName.UpdateClientProcedures)),

            new(Policy.CanAccessProcedure, new AuthorizationRequirement(
                [
                    PermissionName.AccessProcedures,
                    PermissionName.CreateInvoices
                ]
            )),
            new(Policy.CanModifyVisibilityProcedure, new AuthorizationRequirement(
                [
                    PermissionName.RestoreProcedures,
                    PermissionName.DeactivateProcedures
                ]
            )),
            new(Policy.CanManageProcedure, new AuthorizationRequirement(
                [
                    PermissionName.CreateProcedures,
                    PermissionName.UpdateProcedures,
                    PermissionName.DeleteProcedures,
                ]
            )),
            new(Policy.CanAccessPayment, new AuthorizationRequirement(PermissionName.AccessPayments)),
            new(Policy.CanRegisterPayment, new AuthorizationRequirement(PermissionName.RegisterPayments)),
            new(Policy.CanAccessOrthodontic, new AuthorizationRequirement(PermissionName.AccessOrthodontics)),
            new(Policy.CanCreateOrthodontic, new AuthorizationRequirement(PermissionName.CreateOrthodontics)),
            new(Policy.CanUpdateOrthodontic, new AuthorizationRequirement(PermissionName.UpdateOrthodontics)),
            new(Policy.CanDeleteOrthodontic, new AuthorizationRequirement(PermissionName.DeleteOrthodontics)),
            new(Policy.CanAccessAllOrthodontics, new AuthorizationRequirement(PermissionName.AccessAllOrthodontics)),
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

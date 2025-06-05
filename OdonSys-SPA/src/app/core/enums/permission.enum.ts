export enum Permission {
  // Clients
  AccessClients = 'AccessClients',
  CreateClients = 'CreateClients',
  UpdateClients = 'UpdateClients',
  AssignClients = 'AssignClients',
  DeleteClients = 'DeleteClients',
  AccessMyClients = 'AccessMyClients',
  FullFieldUpdateClients = 'FullFieldUpdateClients',
  RestoreClients = 'RestoreClients',
  DeactivateClients = 'DeactivateClients',

  // Doctors
  AccessDoctors = 'AccessDoctors',
  AccessMyData = 'AccessMyData',
  ApproveDoctors = 'ApproveDoctors',
  UpdateDoctors = 'UpdateDoctors',
  DeleteDoctors = 'DeleteDoctors',
  AssignDoctorRoles = 'AssignDoctorRoles',
  DeactivateDoctors = 'DeactivateDoctors',
  RestoreDoctors = 'RestoreDoctors',

  //Invoices
  AccessInvoices = 'AccessInvoices',
  AccessMyInvoices = 'AccessMyInvoices',
  CreateInvoices = 'CreateInvoices',
  UpdateInvoices = 'UpdateInvoices',
  ChangeInvoiceStatus = 'ChangeInvoiceStatus',

  // Payments
  AccessPayments = 'AccessPayments',
  RegisterPayments = 'RegisterPayments',

  // Procedures
  AccessProcedures = 'AccessProcedures',
  CreateProcedures = 'CreateProcedures',
  UpdateProcedures = 'UpdateProcedures',
  DeleteProcedures = 'DeleteProcedures',
  DeactivateProcedures = 'DeactivateProcedures',
  RestoreProcedures = 'RestoreProcedures',

  // Role
  AccessRoles = 'AccessRoles',
  ManageRoles = 'ManageRoles',
  AssignRoleDoctors = 'AssignRoleDoctors',

  AccessClientProcedures = 'AccessClientProcedures',
  CreateClientProcedures = 'CreateClientProcedures',
  UpdateClientProcedures = 'UpdateClientProcedures',

  // Orthodontics
  AccessOrthodontics = 'AccessOrthodontics',
  AccessAllOrthodontics = 'AccessAllOrthodontics',
  CreateOrthodontics = 'CreateOrthodontics',
  UpdateOrthodontics = 'UpdateOrthodontics',
  DeleteOrthodontics = 'DeleteOrthodontics',
}

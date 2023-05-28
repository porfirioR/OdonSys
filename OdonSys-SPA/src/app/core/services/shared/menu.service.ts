import { Injectable } from '@angular/core';
import { MenuItem } from '../../models/view/menu-item';
import { Permission } from '../../enums/permission.enum';

@Injectable({
  providedIn: 'root'
})
export class MenuService {

  constructor() { }

  public static getPrincipalItems = (): MenuItem[] => {
    return [
      new MenuItem('Doctores', 'admin/doctores', Permission.AccessDoctors),
      new MenuItem('Roles', 'admin/roles', Permission.AccessRoles),
      new MenuItem('Procedimientos', 'admin/procedimientos', Permission.AccessProcedures),
      new MenuItem('Pacientes', 'admin/pacientes', Permission.AccessClients),
      new MenuItem('Mis Pacientes', 'trabajo/mis-pacientes', Permission.AccessMyClients),
      new MenuItem('Facturas', 'trabajo/facturas', Permission.AccessInvoices),
      new MenuItem('Mis Facturas', 'trabajo/mis-facturas', Permission.AccessMyInvoices),
      new MenuItem('Pagos', 'trabajo/pagos', Permission.AccessPayments),
    ]
  }
}

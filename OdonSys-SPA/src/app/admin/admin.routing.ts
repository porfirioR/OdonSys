import { Routes } from '@angular/router';
import { AdminProcedureComponent } from './components/admin-procedure/admin-procedure.component';
import { UpsertProcedureComponent } from './components/admin-procedure/upsert-procedure/upsert-procedure.component';
import { DoctorsComponent } from './components/doctors/doctors.component';

export const AdminRoutes: Routes = [
  { 
    path: '',
    children: [
      { path: 'procedimiento', component: AdminProcedureComponent },
      { path: 'procedimiento/crear', component: UpsertProcedureComponent },
      { path: 'procedimiento/actualizar', component: UpsertProcedureComponent },
      { path: 'doctores', component: DoctorsComponent },
    ]
  },
];

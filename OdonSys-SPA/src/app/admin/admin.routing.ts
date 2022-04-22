import { Routes } from '@angular/router';
import { AdminProcedureComponent } from './components/admin-procedure/admin-procedure.component';
import { UpsertProcedureComponent } from './components/admin-procedure/upsert-procedure/upsert-procedure.component';
import { DoctorsComponent } from './components/doctors/doctors.component';

export const AdminRoutes: Routes = [
  { 
    path: '',
    children: [
      { path: 'procedimientos', component: AdminProcedureComponent },
      { path: 'procedimientos/crear', component: UpsertProcedureComponent },
      { path: 'procedimientos/actualizar', component: UpsertProcedureComponent },
      { path: 'doctores', component: DoctorsComponent },
    ]
  },
];

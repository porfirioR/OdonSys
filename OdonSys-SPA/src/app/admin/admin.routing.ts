import { Routes } from '@angular/router';
import { AdminProcedureComponent } from './components/admin-procedure/admin-procedure.component';
import { DoctorsComponent } from './components/doctors/doctors.component';

export const AdminRoutes: Routes = [
  { 
    path: '',
    children: [
      { path: 'procedimiento', component: AdminProcedureComponent },
      { path: 'doctores', component: DoctorsComponent },
    ]
  },
];

import { Routes } from '@angular/router';
import { AdminProcedureComponent } from './components/admin-procedure/admin-procedure.component';

export const AdminRoutes: Routes = [
  { 
    path: '',
    children: [
      { path: 'procedimiento', component: AdminProcedureComponent }
    ]
  },
];

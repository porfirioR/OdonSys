import { Routes } from '@angular/router';
import { AdminComponent } from './admin.component';
import { AdminProcedureComponent } from './components/admin-procedure/admin-procedure.component';

export const AdminRoutes: Routes = [
  { 
    path: '',
    children: [
      { path: '', component: AdminComponent },
      { path: 'procedimiento', component: AdminProcedureComponent }
    ]
  },
];

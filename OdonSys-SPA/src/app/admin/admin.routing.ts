import { Routes } from '@angular/router';
import { UpsertClientComponent } from '../core/components/clients/upsert-client/upsert-client.component';
import { AdminClientsComponent } from './components/admin-clients/admin-clients.component';
import { AdminProcedureComponent } from './components/admin-procedure/admin-procedure.component';
import { UpsertProcedureComponent } from './components/admin-procedure/upsert-procedure/upsert-procedure.component';
import { DoctorsComponent } from './components/doctors/doctors.component';

export const AdminRoutes: Routes = [
  { 
    path: '',
    children: [
      { path: 'procedimientos', component: AdminProcedureComponent, },
      { path: 'procedimientos/crear', component: UpsertProcedureComponent },
      { path: 'procedimientos/actualizar/:id/:active', component: UpsertProcedureComponent },
      { path: 'doctores', component: DoctorsComponent },
      { path: 'pacientes', component: AdminClientsComponent },
      { path: 'pacientes/ver/:id', component: AdminClientsComponent },
      { path: 'pacientes/actualizar/:id', component: UpsertClientComponent },
    ]
  },
];

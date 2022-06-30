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
      { path: 'procedimientos', component: AdminProcedureComponent, title: 'Procedimientos' },
      { path: 'procedimientos/crear', component: UpsertProcedureComponent, title: 'Crear Procedimientos' },
      { path: 'procedimientos/actualizar/:id/:active', component: UpsertProcedureComponent, title: 'Actualizar Procedimientos' },
      { path: 'doctores', component: DoctorsComponent, title: 'Doctores' },
      { path: 'pacientes', component: AdminClientsComponent, title: 'Pacientes' },
      { path: 'pacientes/ver/:id', component: AdminClientsComponent, title: 'Ver pacientes' },
      { path: 'pacientes/actualizar/:id', component: UpsertClientComponent, title: 'Actualizar pacientes' },
    ]
  },
];

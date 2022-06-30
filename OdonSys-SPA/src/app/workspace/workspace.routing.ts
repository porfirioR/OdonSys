import { Routes } from '@angular/router';
import { ClientsComponent } from '../core/components/clients/clients.component';
import { UpsertClientComponent } from '../core/components/clients/upsert-client/upsert-client.component';
import { MyConfigurationComponent } from './components/my-configuration/my-configuration.component';

export const WorkspaceRoutes: Routes = [
  { 
    path: '',
    children: [
      { path: 'configuración/datos', component: MyConfigurationComponent, title: 'Mis datos' },
      { path: 'misPacientes', component: ClientsComponent, title: 'Mis pacientes' },
      { path: 'misPacientes/registrar', component: UpsertClientComponent, title: 'Registrar paciente' },
      { path: 'misPacientes/actualizar/:id', component: UpsertClientComponent, title: 'Actualizar paciente' },
    ]
  },
];

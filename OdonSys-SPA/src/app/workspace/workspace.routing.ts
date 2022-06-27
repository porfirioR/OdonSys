import { Routes } from '@angular/router';
import { ClientsComponent } from '../core/components/clients/clients.component';
import { UpsertClientComponent } from '../core/components/clients/upsert-client/upsert-client.component';
import { MyConfigurationComponent } from './components/my-configuration/my-configuration.component';

export const WorkspaceRoutes: Routes = [
  { 
    path: '',
    children: [
      { path: 'configuración/datos', component: MyConfigurationComponent },
      { path: 'misPacientes', component: ClientsComponent },
      { path: 'misPacientes/registrar', component: UpsertClientComponent },
      { path: 'misPacientes/actualizar/:id', component: UpsertClientComponent },
    ]
  },
];

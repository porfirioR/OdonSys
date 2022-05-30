import { Routes } from '@angular/router';
import { MyConfigurationComponent } from './components/my-configuration/my-configuration.component';

export const WorkspaceRoutes: Routes = [
  { 
    path: '',
    children: [
      { path: 'configuración/datos', component: MyConfigurationComponent },
    ]
  },
];

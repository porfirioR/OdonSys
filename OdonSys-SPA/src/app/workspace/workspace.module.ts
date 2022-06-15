import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyConfigurationComponent } from './components/my-configuration/my-configuration.component';
import { PatientsComponent } from './components/patients/patients.component';
import { CoreModule } from '../core/core.module';
import { RouterModule } from '@angular/router';
import { WorkspaceRoutes } from './workspace.routing';

@NgModule({
  imports: [
    CoreModule,
    CommonModule,
    RouterModule.forChild(WorkspaceRoutes)
  ],
  declarations: [
    MyConfigurationComponent,
    PatientsComponent
  ]
})
export class WorkspaceModule { }

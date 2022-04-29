import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyConfigurationComponent } from './components/my-configuration/my-configuration.component';
import { PatientsComponent } from './components/patients/patients.component';
import { CoreModule } from '../core/core.module';

@NgModule({
  imports: [
    CommonModule,
    CoreModule
  ],
  declarations: [
    MyConfigurationComponent,
    PatientsComponent
  ]
})
export class WorkspaceModule { }

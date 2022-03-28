import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AdminRoutes } from './admin.routing';
import { AdminProcedureComponent } from './components/admin-procedure/admin-procedure.component';
import { CoreModule } from '../core/core.module';
import { DoctorsComponent } from './components/doctors/doctors.component';
import { UpsertDoctorComponent } from './components/doctors/upsert-doctor/upsert-doctor.component';

@NgModule({
  imports: [
    CommonModule,
    CoreModule,
    RouterModule.forChild(AdminRoutes)
  ],
  declarations: [
    AdminProcedureComponent,
    DoctorsComponent,
    UpsertDoctorComponent
  ],

})
export class AdminModule { }

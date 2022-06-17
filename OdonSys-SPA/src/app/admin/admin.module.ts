import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AdminRoutes } from './admin.routing';

import { AdminProcedureComponent } from './components/admin-procedure/admin-procedure.component';
import { CoreModule } from '../core/core.module';
import { DoctorsComponent } from './components/doctors/doctors.component';
import { UpsertDoctorComponent } from './components/doctors/upsert-doctor/upsert-doctor.component';
import { UpsertProcedureComponent } from './components/admin-procedure/upsert-procedure/upsert-procedure.component';
import { ClientAdminApiService } from './service/client-admin-api.service';
import { ProcedureApiService } from './service/procedure-admin-api.service';
import { UserApiService } from './service/user-api.service';

@NgModule({
  imports: [
    CommonModule,
    CoreModule,
    RouterModule.forChild(AdminRoutes)
  ],
  declarations: [
    AdminProcedureComponent,
    UpsertProcedureComponent,
    DoctorsComponent,
    UpsertDoctorComponent,
    DoctorsComponent
  ],
  providers:[
    ClientAdminApiService,
    ProcedureApiService,
    UserApiService
  ]

})
export class AdminModule { }

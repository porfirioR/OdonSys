import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AdminRoutes } from './admin.routing';

import { AdminProcedureComponent } from './components/admin-procedure/admin-procedure.component';
import { CoreModule } from '../core/core.module';
import { DoctorsComponent } from './components/doctors/doctors.component';
import { UpsertDoctorComponent } from './components/doctors/upsert-doctor/upsert-doctor.component';
import { UpsertProcedureComponent } from './components/admin-procedure/upsert-procedure/upsert-procedure.component';
import { ProcedureApiService } from './service/procedure-admin-api.service';
import { UserApiService } from './service/user-api.service';
import { AdminClientsComponent } from './components/admin-clients/admin-clients.component';
import { BasicServiceModule } from '../basic-service.module';
import { ClientAdminApiService } from './service/client-admin-api.service';
import { RolesComponent } from './components/roles/roles.component';
import { PermissionGuard } from '../core/guards/permission.guard';

@NgModule({
  imports: [
    CommonModule,
    CoreModule,
    RouterModule.forChild(AdminRoutes),
    BasicServiceModule
  ],
  declarations: [
    AdminProcedureComponent,
    UpsertProcedureComponent,
    DoctorsComponent,
    UpsertDoctorComponent,
    AdminClientsComponent,
    RolesComponent
  ],
  providers:[
    ClientAdminApiService,
    ProcedureApiService,
    UserApiService
  ]

})
export class AdminModule { }

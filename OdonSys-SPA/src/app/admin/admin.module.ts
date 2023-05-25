import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AdminRoutes } from './admin.routing';

import { AdminProcedureComponent } from './components/admin-procedure/admin-procedure.component';
import { CoreModule } from '../core/core.module';
import { DoctorsComponent } from './components/doctors/doctors.component';
import { UpsertDoctorComponent } from './components/upsert-doctor/upsert-doctor.component';
import { UpsertProcedureComponent } from './components/upsert-procedure/upsert-procedure.component';
import { ProcedureApiService } from '../core/services/api/procedure-admin-api.service';
import { UserApiService } from './services/user-api.service';
import { AdminClientsComponent } from './components/admin-clients/admin-clients.component';
import { BasicServiceModule } from '../basic-service.module';
import { ClientAdminApiService } from '../core/services/api/client-admin-api.service';
import { RolesComponent } from './components/roles/roles.component';
import { UpsertRoleComponent } from './components/upsert-role/upsert-role.component';
import { UserRoleComponent } from './modals/user-role/user-role.component';

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
    RolesComponent,
    UpsertRoleComponent,
    UserRoleComponent
  ],
  providers:[
    ClientAdminApiService,
    ProcedureApiService,
    UserApiService
  ]

})
export class AdminModule { }

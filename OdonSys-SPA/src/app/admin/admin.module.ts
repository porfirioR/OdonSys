import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AdminRoutes } from './admin.routing';
import { CoreModule } from '../core/core.module';

import { AdminProcedureComponent } from './components/admin-procedure/admin-procedure.component';
import { DoctorsComponent } from './components/doctors/doctors.component';
import { UpsertProcedureComponent } from './components/upsert-procedure/upsert-procedure.component';
import { AdminClientsComponent } from './components/admin-clients/admin-clients.component';
import { RolesComponent } from './components/roles/roles.component';
import { UpsertRoleComponent } from './components/upsert-role/upsert-role.component';
import { ProcedureApiService } from '../core/services/api/procedure-admin-api.service';
import { UserApiService } from '../core/services/api/user-api.service';
import { ClientAdminApiService } from '../core/services/api/client-admin-api.service';
import { UserRoleComponent } from './modals/user-role/user-role.component';

@NgModule({
  imports: [
    CommonModule,
    CoreModule,
    RouterModule.forChild(AdminRoutes),
  ],
  declarations: [
    AdminProcedureComponent,
    UpsertProcedureComponent,
    DoctorsComponent,
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

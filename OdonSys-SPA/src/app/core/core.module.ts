import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { AgGridModule } from 'ag-grid-angular';

import { HeaderComponent } from './components/header/header.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { PrincipalPageComponent } from './components/principal-page/principal-page.component';
import { ProgressComponent } from './components/progress/progress.component';
import { TextNumberComponent } from './components/inputs/text-number-input/text-number-input.component';
import { CheckInputComponent } from './components/inputs/check-input/check-input.component';
import { GridActionsComponent } from './components/grid-actions/grid-actions.component';
import { SelectInputComponent } from './components/inputs/select-input/select-input.component';
import { AuthenticateComponent } from './components/authenticate/authenticate.component';
import { RegisterUserComponent } from './components/register-user/register-user.component';
import { ClientsComponent } from './components/my-clients/my-clients.component';
import { UpsertClientComponent } from './components/upsert-client/upsert-client.component';
import { AnimationComponent } from './components/animation/animation.component';
import { GridBadgeComponent } from './components/grid-badge/grid-badge.component';
import { UploadFileComponent } from './components/upload-file/upload-file.component';
import { ClientDetailComponent } from './components/client-detail/client-detail.component';

import { AuthGuard } from './guards/auth.guard';
import { PermissionGuard } from './guards/permission.guard';

import * as fromSaving from './store/saving/saving.reducer';
import * as fromRoles from './store/roles/roles.reducer';
import * as fromProcedure from './store/procedures/procedure.reducer';
import * as fromClient from './store/clients/client.reducer';

import { RolesEffects } from './store/roles/roles.effects';
import { ProcedureEffects } from './store/procedures/procedure.effects';
import { ClientEffects } from './store/clients/client.effects';
import { environment } from '../../environments/environment';
import { NgxDropzoneModule } from 'ngx-dropzone';
import * as fromDoctor from './store/doctors/doctor.reducer';
import { DoctorEffects } from './store/doctors/doctor.effects';
import { NgxMaskDirective, NgxMaskPipe, provideEnvironmentNgxMask } from 'ngx-mask';

@NgModule({
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    RouterModule,
    AgGridModule,
    NgbModule,
    StoreModule.forFeature(fromSaving.savingFeatureKey, fromSaving.reducer),
    StoreModule.forFeature(fromRoles.roleFeatureKey, fromRoles.reducer),
    !environment.production ? StoreDevtoolsModule.instrument() : [],
    // StoreModule.forFeature(fromUserInfo.userInfoFeatureKey, fromUserInfo.reducer),
    StoreModule.forFeature(fromProcedure.proceduresFeatureKey, fromProcedure.reducer),
    StoreModule.forFeature(fromClient.clientsFeatureKey, fromClient.reducer),
    EffectsModule.forFeature([RolesEffects, ProcedureEffects, ClientEffects, DoctorEffects]),
    NgxDropzoneModule,
    StoreModule.forFeature(fromDoctor.doctorsFeatureKey, fromDoctor.reducer),
    NgxMaskDirective,
    NgxMaskPipe
  ],
  declarations: [
    HeaderComponent,
    PrincipalPageComponent,
    NotFoundComponent,
    ProgressComponent,
    TextNumberComponent,
    CheckInputComponent,
    SelectInputComponent,
    GridActionsComponent,
    AuthenticateComponent,
    RegisterUserComponent,
    ClientsComponent,
    UpsertClientComponent,
    AnimationComponent,
    GridBadgeComponent,
    UploadFileComponent,
    ClientDetailComponent
  ],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    AgGridModule,
    NgbModule,
    NgxMaskDirective,
    NgxMaskPipe,
    HeaderComponent,
    PrincipalPageComponent,
    NotFoundComponent,
    ProgressComponent,
    TextNumberComponent,
    CheckInputComponent,
    SelectInputComponent,
    GridActionsComponent,
    AuthenticateComponent,
    ClientsComponent,
    UpsertClientComponent,
    AnimationComponent,
    UploadFileComponent
  ],
  providers:[
    AuthGuard,
    PermissionGuard,
    provideEnvironmentNgxMask()
  ]
})
export class CoreModule { }

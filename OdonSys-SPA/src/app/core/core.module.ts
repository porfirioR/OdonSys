import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AgGridModule } from 'ag-grid-angular';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AuthGuard } from './guards/auth.guard';

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
import { ClientsComponent } from './components/clients/clients.component';
import { UpsertClientComponent } from './components/clients/upsert-client/upsert-client.component';
import { AnimationComponent } from './components/animation/animation.component';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { PermissionGuard } from './guards/permission.guard';
import { environment } from '../../environments/environment';
import * as fromSaving from './store/saving/saving.reducer';
import * as fromRoles from './store/roles/roles.reducer';
import { RolesEffects } from './store/roles/roles.effects';

@NgModule({
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    RouterModule,
    AgGridModule.withComponents([]),
    NgbModule,
    StoreModule.forFeature(fromSaving.savingFeatureKey, fromSaving.reducer),
    StoreModule.forFeature(fromRoles.roleFeatureKey, fromRoles.rolesReducer),
    !environment.production ? StoreDevtoolsModule.instrument() : [],
    // StoreModule.forFeature(fromUserInfo.userInfoFeatureKey, fromUserInfo.reducer),
    EffectsModule.forFeature([RolesEffects]),
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
    AnimationComponent
  ],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    AgGridModule,
    NgbModule,
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
  ],
  providers:[
    AuthGuard,
    PermissionGuard
  ]
})
export class CoreModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AgGridModule } from 'ag-grid-angular';

// Components
import { HeaderComponent } from './components/header/header.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { PrincipalPageComponent } from './components/principal-page/principal-page.component';
import { ProgressComponent } from './components/progress/progress.component';
import { TextNumberComponent } from './components/inputs/text-number-input/text-number-input.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CheckInputComponent } from './components/inputs/check-input/check-input.component';
import { GridActionsComponent } from './components/grid-actions/grid-actions.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SelectInputComponent } from './components/inputs/select-input/select-input.component';
import { AuthenticateComponent } from './components/authenticate/authenticate.component';
import { RegisterUserComponent } from './components/register-user/register-user.component';
import { AuthGuard } from './guards/auth.guard';

@NgModule({
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    RouterModule,
    AgGridModule.withComponents([]),
    NgbModule,
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
    RegisterUserComponent
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
    AuthenticateComponent
  ],
  providers:[
    AuthGuard
  ]
})
export class CoreModule { }

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
    GridActionsComponent
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
    GridActionsComponent,
  ]
})
export class CoreModule { }

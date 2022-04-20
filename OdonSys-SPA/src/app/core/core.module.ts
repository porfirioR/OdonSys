import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgGridModule } from 'ag-grid-angular';
import { RouterModule } from '@angular/router';

// Components
import { HeaderComponent } from '../layout/header/header.component';
import { NotFoundComponent } from '../layout/not-found/not-found.component';
import { PrincipalPageComponent } from '../layout/principal-page/principal-page.component';
import { ProgressComponent } from './components/progress/progress.component';
import { TextNumberComponent } from './components/inputs/text-number-input/text-number-input.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    AgGridModule.withComponents([]),
    FormsModule,
    ReactiveFormsModule
  ],
  declarations: [
    HeaderComponent,
    PrincipalPageComponent,
    NotFoundComponent,
    ProgressComponent,
    TextNumberComponent
  ],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    AgGridModule,
    HeaderComponent,
    PrincipalPageComponent,
    NotFoundComponent,
    ProgressComponent,
    TextNumberComponent
  ]
})
export class CoreModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgGridModule } from 'ag-grid-angular';
import { RouterModule } from '@angular/router';

// Components
import { HeaderComponent } from '../layout/header/header.component';
import { NotFoundComponent } from '../layout/not-found/not-found.component';
import { PrincipalPageComponent } from '../layout/principal-page/principal-page.component';
import { ProgressComponent } from './components/progress/progress.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    AgGridModule.withComponents([]),
  ],
  declarations: [
    HeaderComponent,
    PrincipalPageComponent,
    NotFoundComponent,
    ProgressComponent
  ],
  exports: [
    AgGridModule,
    HeaderComponent,
    PrincipalPageComponent,
    NotFoundComponent,
    ProgressComponent
  ]
})
export class CoreModule { }

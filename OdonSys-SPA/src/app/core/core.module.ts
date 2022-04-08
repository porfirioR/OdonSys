import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AgGridModule } from 'ag-grid-angular';

// Components
import { HeaderComponent } from '../layout/header/header.component';
import { NotFoundComponent } from '../layout/not-found/not-found.component';
import { PrincipalPageComponent } from '../layout/principal-page/principal-page.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    AgGridModule.withComponents([]),
  ],
  declarations: [
    HeaderComponent,
    PrincipalPageComponent,
    NotFoundComponent
  ],
  exports: [
    AgGridModule,
    HeaderComponent,
    PrincipalPageComponent,
    NotFoundComponent
  ]
})
export class CoreModule { }

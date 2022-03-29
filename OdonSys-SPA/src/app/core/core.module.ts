import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreRoutes } from './core.routing';

// Components
import { HeaderComponent } from '../layout/header/header.component';
import { RouterModule } from '@angular/router';
import { NotFoundComponent } from '../layout/not-found/not-found.component';
import { PrincipalPageComponent } from '../layout/principal-page/principal-page.component';
import { AgGridModule } from 'ag-grid-angular';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(CoreRoutes),
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

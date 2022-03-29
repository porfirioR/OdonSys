import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreRoutes } from './core.routing';

// Components
import { HeaderComponent } from './components/header/header.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { PrincipalPageComponent } from './components/principal-page/principal-page.component';
import { RouterModule } from '@angular/router';
import { AgGridModule } from 'ag-grid-angular/public-api';

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

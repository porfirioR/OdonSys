import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CoreRoutes } from './core.routing';
import { AgGridModule } from 'ag-grid-angular';

// Components
import { HeaderComponent } from './components/header/header.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { PrincipalPageComponent } from './components/principal-page/principal-page.component';

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

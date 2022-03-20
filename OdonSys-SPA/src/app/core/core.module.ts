import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CoreRoutes } from './core.routing';
import { AgGridModule } from 'ag-grid-angular';

// Components
import { PrincipalPageComponent } from './components/principal-page/principal-page.component';
import { HeaderComponent } from './components/header/header.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(CoreRoutes),
    AgGridModule.withComponents([]),
  ],
  declarations: [
    PrincipalPageComponent,
    HeaderComponent,
  ],
  exports: [
    AgGridModule
  ]
})
export class CoreModule { }

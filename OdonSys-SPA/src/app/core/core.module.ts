import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreRoutes } from './core.routing';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { PrincipalPageComponent } from './components/principal-page/principal-page.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { HeaderComponent } from './components/header/header.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(CoreRoutes),
    SharedModule
  ],
  declarations: [
    PrincipalPageComponent,
    SidebarComponent,
    HeaderComponent,
    
  ]
})
export class CoreModule { }
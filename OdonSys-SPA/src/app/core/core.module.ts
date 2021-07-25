import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreRoutes } from './core.routing';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(CoreRoutes),
    SharedModule
  ],
  declarations: []
})
export class CoreModule { }

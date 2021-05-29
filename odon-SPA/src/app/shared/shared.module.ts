import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedComponent } from './shared.component';
import { RouterModule } from '@angular/router';
import { SharedRoutes } from './shared.routing';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(SharedRoutes)
  ],
  declarations: [SharedComponent]
})
export class SharedModule { }

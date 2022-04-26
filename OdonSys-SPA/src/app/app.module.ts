import { ErrorHandler, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutes } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { CustomErrorHandler } from './core/helpers/custom-error-handler';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    CoreModule,
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(AppRoutes),
  ],
  providers: [
    {provide: ErrorHandler, useClass: CustomErrorHandler}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { ErrorHandler, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { registerLocaleData } from '@angular/common';
import localEs from '@angular/common/locales/es';

import { CoreModule } from './core/core.module';
import { EffectsModule } from '@ngrx/effects';
import { NgbModule, NgbNavModule } from '@ng-bootstrap/ng-bootstrap';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';

import { AppRoutes } from './app.routing';
import { AppComponent } from './app.component';
import { CustomErrorHandler } from './core/helpers/custom-error-handler';
import { JwtInterceptor } from './core/interceptors/jwt.interceptor';
import { environment } from '../environments/environment';
import { reducers, metaReducers } from './store';

registerLocaleData(localEs, 'es')

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    CoreModule,
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(AppRoutes),
    NgbModule,
    NgbNavModule,
    StoreDevtoolsModule.instrument({ maxAge: 25, logOnly: !environment.production }),
    StoreModule.forRoot(reducers, { metaReducers }),
    !environment.production ? StoreDevtoolsModule.instrument() : [],
    EffectsModule.forRoot([])
  ],
  providers: [
    { provide: ErrorHandler, useClass: CustomErrorHandler },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

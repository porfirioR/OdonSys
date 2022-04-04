import { Injectable, Injector } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AppInjector {
  private static myInjector: Injector;

  static setInjector(injector: Injector) {
    AppInjector.myInjector = injector;
  }

  static getInjector(): Injector {
    return AppInjector.myInjector;
  }

}

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppInjector } from '../../helpers/app-injector';
import { AlertService } from '../../services/shared/alert.service';

@Component({
  selector: 'app-basic',
  template: ''
})
export class BasicComponent implements OnInit {

  protected readonly router: Router;
  protected readonly alertService: AlertService;

  constructor() {
    const injector = AppInjector.getInjector();
    this.router = injector.get(Router);
    this.alertService = injector.get(AlertService);
  }

  ngOnInit() {
  }

}

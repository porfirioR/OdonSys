import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { DoctorApiService } from 'src/app/core/services/api/doctor-api.service';
import { AlertService } from 'src/app/core/services/shared/alert.service';
import { UpdateUserRequest } from '../../../core/models/users/update-user-request';

@Component({
  selector: 'app-my-configuration',
  templateUrl: './my-configuration.component.html',
  styleUrls: ['./my-configuration.component.scss'],
})
export class MyConfigurationComponent implements OnInit {
  public load: boolean = false;
  public saving: boolean = false;
  public formGroup: FormGroup = new FormGroup({});
  public id!: string;
  constructor(
    private readonly router: Router,
    private readonly alertService: AlertService,
    private readonly doctorApiService: DoctorApiService
  ) {}

  ngOnInit() {}

  public save = () => {
    this.saving = true;
    const request = this.formGroup.getRawValue() as UpdateUserRequest;
    this.doctorApiService
      .updateConfiguration(this.id, request)
      .pipe(
        tap(() => {
          this.alertService.showSuccess('Datos guardados.');
          this.close();
        }),
        catchError((e) => {
          this.saving = false;
          return throwError(e);
        })
      )
      .subscribe();
  };

  public close = () => {
    this.router.navigate(['']);
  };
}

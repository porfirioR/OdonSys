import { Component, OnInit } from '@angular/core';
import { UntypedFormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Country } from '../../../core/enums/country.enum';
import { DoctorApiService } from '../../../core/services/api/doctor-api.service';
import { AlertService } from '../../../core/services/shared/alert.service';
import { UpdateUserRequest } from '../../../core/models/users/update-user-request';

@Component({
  selector: 'app-my-configuration',
  templateUrl: './my-configuration.component.html',
  styleUrls: ['./my-configuration.component.scss'],
})
export class MyConfigurationComponent implements OnInit {
  public load: boolean = false;
  public saving: boolean = false;
  public formGroup: UntypedFormGroup = new UntypedFormGroup({});
  public id!: string;
  public countries: Map<string, string> = new Map<string, string>();

  constructor(
    private readonly router: Router,
    private readonly alertService: AlertService,
    private readonly doctorApiService: DoctorApiService
  ) {
    Object.keys(Country).map((key) => this.countries.set(key as string, Country[key as keyof typeof Country]));
  }

  ngOnInit() {
    this.loadConfiguration();
  }

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

  private loadConfiguration = () => {
    
  }
}

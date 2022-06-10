import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Country } from '../../../core/enums/country.enum';
import { DoctorApiService } from '../../../core/services/api/doctor-api.service';
import { AlertService } from '../../../core/services/shared/alert.service';
import { UpdateUserRequest } from '../../../core/models/users/update-user-request';
import { CustomValidators } from '../../../core/helpers/custom-validators';

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
    this.doctorApiService.updateConfiguration(this.id, request).subscribe({
      next: () => {
        this.alertService.showSuccess('Datos guardados.');
        this.close();
      }, error: (e) => {
        this.saving = false;
        throw new Error(e);
      }
    });
  };

  public close = () => {
    this.router.navigate(['']);
  };

  private loadConfiguration = () => {
    this.formGroup = new FormGroup({
      name: new FormControl('', [Validators.required, Validators.maxLength(25)]),
      middleName: new FormControl('', [Validators.maxLength(25)]),
      lastName: new FormControl('', [Validators.required, Validators.maxLength(25)]),
      middleLastName: new FormControl('', [Validators.maxLength(25)]),
      document: new FormControl('', [Validators.required, Validators.maxLength(15), Validators.min(0)]),
      phone: new FormControl('', [Validators.required, Validators.maxLength(15), CustomValidators.checkPhoneValue()]),
      email: new FormControl('', [Validators.required, Validators.maxLength(20), Validators.email]),
      country: new FormControl('', [Validators.required])
    });
  }
}

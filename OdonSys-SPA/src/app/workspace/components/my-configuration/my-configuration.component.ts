import { Component, NgZone, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DoctorApiService } from '../../../core/services/api/doctor-api.service';
import { AlertService } from '../../../core/services/shared/alert.service';
import { UpdateUserRequest } from '../../../core/models/users/update-user-request';
import { CustomValidators } from '../../../core/helpers/custom-validators';
import { UserInfoService } from '../../../core/services/shared/user-info.service';
import { DoctorApiModel } from '../../../core/models/api/doctor/doctor-api-model';
import { EnumHandler } from '../../../core/helpers/enum-handler';

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
    private readonly doctorApiService: DoctorApiService,
    private readonly userInfoService: UserInfoService,
    private readonly zone: NgZone,
  ) {
    this.countries = EnumHandler.getCountries();
  }

  ngOnInit() {
    this.loadConfiguration();
  }

  public save = () => {
    this.saving = true;
    const request = this.getDoctorRequest();
    this.doctorApiService.update(this.id, request).subscribe({
      next: () => {
        this.alertService.showSuccess('Datos guardados.');
        this.close();
      }, error: (e) => {
        this.saving = false;
        throw e;
      }
    });
  };

  public close = () => {
    this.router.navigate(['']);
  }

  private loadConfiguration = () => {
    const user = this.userInfoService.getUserData()
    if (!user || !user.id) {
      this.zone.run(() => this.router.createUrlTree(['/login']))
    }
    this.doctorApiService.getById(user.id).subscribe({
      next: (user: DoctorApiModel) => {
        this.formGroup = new FormGroup({
          id: new FormControl({ value: user.id, disabled: true }),
          name: new FormControl(user.name, [Validators.required, Validators.maxLength(25)]),
          middleName: new FormControl(user.middleName, [Validators.maxLength(25)]),
          lastName: new FormControl(user.lastName, [Validators.required, Validators.maxLength(25)]),
          middleLastName: new FormControl(user.middleLastName, [Validators.maxLength(25)]),
          document: new FormControl(user.document, [Validators.required, Validators.maxLength(15), Validators.min(0)]),
          phone: new FormControl(user.phone, [Validators.required, Validators.maxLength(15), CustomValidators.checkPhoneValue()]),
          email: new FormControl({value: user.email, disabled: true }),
          country: new FormControl(user.country, [Validators.required]),
          active: new FormControl(user.active, [Validators.required]),
        });
        this.load = true;
      }
    });
  }

  private getDoctorRequest = (): UpdateUserRequest => {
    return new UpdateUserRequest(
      this.formGroup.controls.id.value,
      this.formGroup.controls.name.value,
      this.formGroup.controls.middleName.value,
      this.formGroup.controls.lastName.value,
      this.formGroup.controls.middleLastName.value,
      this.formGroup.controls.document.value,
      this.formGroup.controls.country.value,
      this.formGroup.controls.phone.value,
      this.formGroup.controls.active.value,
    );
  }
}

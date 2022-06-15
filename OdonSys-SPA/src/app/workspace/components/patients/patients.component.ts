import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CustomValidators } from 'src/app/core/helpers/custom-validators';
import { AlertService } from '../../../core/services/shared/alert.service';

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.scss']
})
export class PatientsComponent implements OnInit {
  public formGroup: FormGroup = new FormGroup({});
  public load: boolean = false;

  constructor(
    private readonly alertService: AlertService,

  ) { }

  ngOnInit() {
    this.loadConfiguration();
  }

  private loadConfiguration = () => {
    // const user = this.userInfoService.getUserData();
    // if (!user || !user.id) { this.router.navigate(['/login']); }
    // this.doctorApiService.getById(user.id).subscribe({
    //   next: (user: DoctorApiModel) => {
    //     this.formGroup = new FormGroup({
    //       id: new FormControl({ value: user.id, disabled: true }),
    //       name: new FormControl(user.name, [Validators.required, Validators.maxLength(25)]),
    //       middleName: new FormControl(user.middleName, [Validators.maxLength(25)]),
    //       lastName: new FormControl(user.lastName, [Validators.required, Validators.maxLength(25)]),
    //       middleLastName: new FormControl(user.middleLastName, [Validators.maxLength(25)]),
    //       document: new FormControl(user.document, [Validators.required, Validators.maxLength(15), Validators.min(0)]),
    //       phone: new FormControl(user.phone, [Validators.required, Validators.maxLength(15), CustomValidators.checkPhoneValue()]),
    //       email: new FormControl({value: user.email, disabled: true }),
    //       country: new FormControl(user.country, [Validators.required]),
    //       active: new FormControl(user.active, [Validators.required]),
    //     });
    //     this.load = true;
    //   }
    // });
  }

}

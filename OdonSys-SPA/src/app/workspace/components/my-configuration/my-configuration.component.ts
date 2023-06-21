import { Component, NgZone, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { combineLatest, debounceTime } from 'rxjs';
import { AlertService } from '../../../core/services/shared/alert.service';
import { UserInfoService } from '../../../core/services/shared/user-info.service';
import { RoleApiService } from '../../../core/services/api/role-api.service';
import { CustomValidators } from '../../../core/helpers/custom-validators';
import { EnumHandler } from '../../../core/helpers/enum-handler';
import { MethodHandler } from '../../../core/helpers/method-handler';
import { UpdateUserRequest } from '../../../core/models/users/api/update-user-request';
import { SelectModel } from '../../../core/models/view/select-model';
import { Country } from '../../../core/enums/country.enum';
import { Permission } from '../../../core/enums/permission.enum';
import { SubGroupPermissions } from '../../../core/forms/sub-group-permissions.form';
import { selectDoctor } from '../../../core/store/doctors/doctor.selectors';
import  * as fromDoctorsActions from '../../../core/store/doctors/doctor.actions';

@Component({
  selector: 'app-my-configuration',
  templateUrl: './my-configuration.component.html',
  styleUrls: ['./my-configuration.component.scss'],
})
export class MyConfigurationComponent implements OnInit {
  protected load: boolean = false
  protected saving: boolean = false
  protected canEdit = false
  public formGroup = new FormGroup({
    id: new FormControl({ value: '', disabled: true }),
    name: new FormControl('', [Validators.required, Validators.maxLength(25)]),
    middleName: new FormControl('', [Validators.maxLength(25)]),
    surname: new FormControl('', [Validators.required, Validators.maxLength(25)]),
    secondSurname: new FormControl('', [Validators.maxLength(25)]),
    document: new FormControl('', [Validators.required, Validators.maxLength(15), Validators.min(0)]),
    phone: new FormControl('', [Validators.required, Validators.maxLength(15), CustomValidators.checkPhoneValue()]),
    email: new FormControl({value: '', disabled: true }),
    country: new FormControl({ value: Country.Paraguay, disabled: true }, [Validators.required]),
    ruc: new FormControl({ value: 0, disabled: true }, [Validators.required]),
    active: new FormControl(true, [Validators.required]),
    subGroupPermissions: new FormArray<FormGroup<SubGroupPermissions>>([])
  })
  protected id!: string
  protected countries: SelectModel[] = []
  private canAccessData = false

  constructor(
    private readonly router: Router,
    private readonly alertService: AlertService,
    private readonly userInfoService: UserInfoService,
    private readonly zone: NgZone,
    private readonly roleApiService: RoleApiService,
    private readonly store: Store,
  ) {
    this.countries = EnumHandler.getCountries()
    this.canEdit = userInfoService.havePermission(Permission.UpdateDoctors)
    this.canAccessData = userInfoService.havePermission(Permission.AccessDoctors)
  }

  ngOnInit() {
    this.loadConfiguration()
  }

  public save = () => {
    if (this.formGroup.invalid) { return }
    this.saving = true
    const request = this.getDoctorRequest()
    this.store.dispatch(fromDoctorsActions.updateDoctor({ user: request }))
  }

  public close = () => {
    this.zone.run(() => this.router.navigate(['']))
  }

  private loadConfiguration = () => {
    const user = this.userInfoService.getUserData()
    if (!user || !user.id) {
      this.zone.run(() => this.router.createUrlTree(['/login']))
    }
    if (!this.canAccessData) {
      this.alertService.showInfo('No tiene permisos para acceder a estos datos.')
      this.close()
      this.load = true
      return
    }
    this.store.dispatch(fromDoctorsActions.loadDoctor({ doctorId: user.id })) 
    const user$ = this.store.select(selectDoctor(user.id)).pipe(debounceTime(500))
    combineLatest([
      user$,
      this.roleApiService.getPermissions()
    ]).subscribe({
      next: ([user, allPermissions]) => {
        const rolePermissions = this.userInfoService.getPermissions()
        const permissions = allPermissions.filter(x => rolePermissions.includes(x.code))
        MethodHandler.setSubGroupPermissions(permissions, rolePermissions, this.formGroup.controls.subGroupPermissions)
        this.formGroup.controls.id.setValue(user!.id)
        this.formGroup.controls.name.setValue(user!.name)
        this.formGroup.controls.middleName.setValue(user!.middleName)
        this.formGroup.controls.surname.setValue(user!.surname)
        this.formGroup.controls.secondSurname.setValue(user!.secondSurname)
        this.formGroup.controls.document.setValue(user!.document)
        this.formGroup.controls.phone.setValue(user!.phone)
        this.formGroup.controls.email.setValue(user!.email)
        this.formGroup.controls.country.setValue(user!.country)
        this.formGroup.controls.active.setValue(user!.active)
        this.formGroup.controls.ruc.setValue(MethodHandler.calculateCheckDigit(user!.document, user!.country))
        this.load = true
      }
    })
  }

  private getDoctorRequest = (): UpdateUserRequest => {
    return new UpdateUserRequest(
      this.formGroup.controls.id.value!,
      this.formGroup.value.name!,
      this.formGroup.value.middleName!,
      this.formGroup.value.surname!,
      this.formGroup.value.secondSurname!,
      this.formGroup.value.document!,
      this.formGroup.controls.country.value!,
      this.formGroup.value.phone!,
      this.formGroup.value.active!
    )
  }
}

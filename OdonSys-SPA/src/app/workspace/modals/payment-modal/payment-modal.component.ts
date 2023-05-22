import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormControl, FormGroup, ValidationErrors } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { combineLatest, take } from 'rxjs';
import { UserRoleApiRequest } from '../../../core/models/api/roles/user-role-api-request';
import { CheckFormGroup } from '../../../core/forms/check-form-group';
import { RoleApiService } from '../../../core/services/api/role-api.service';
import { UserInfoService } from '../../../core/services/shared/user-info.service';
import { AlertService } from '../../../core/services/shared/alert.service';
import { DoctorApiService } from '../../../core/services/api/doctor-api.service';

@Component({
  selector: 'app-payment-modal',
  templateUrl: './payment-modal.component.html',
  styleUrls: ['./payment-modal.component.scss']
})
export class PaymentModalComponent implements OnInit {
  @Input() userId!: string
  @Input() name!: string

  protected saving = false
  public formGroup = new FormGroup( {
    roles: new FormArray<FormGroup<CheckFormGroup>>([])
  })

  constructor(
    public activeModal: NgbActiveModal,
    private readonly rolesApiService: RoleApiService,
    private userInfoService: UserInfoService,
    private alertService: AlertService,
    private readonly doctorApiService: DoctorApiService,
  ) { }

  ngOnInit() {
    // combineLatest([this.rolesApiService.getAll(), this.doctorApiService.getById(this.userId)]).pipe(take(1)).subscribe({
    //   next: ([roles, user]) => {
    //     const userRoles = user.roles
    //     roles.forEach(x => {
    //       const formGroup = new FormGroup<CheckFormGroup>({
    //         name: new FormControl(x.name),
    //         code: new FormControl(x.code),
    //         value: new FormControl(userRoles.includes(x.code))
    //       })
    //       this.formGroup.controls.roles.push(formGroup)
    //     })
    //     this.formGroup.controls.roles.addValidators(this.minimumOneSelectedValidator)
    //   },
    //   error: (e) => {
    //     throw e;
    //   }
    // })
  }

  protected save = () => {
    this.saving = true
    const roles = this.formGroup.value.roles!.filter(x => x.value).map(x => x.code!)
    const userRoles = new UserRoleApiRequest(this.userId, roles)
    // this.userApiService.setUserRoles(userRoles).pipe(take(1)).subscribe({
    //   next: (response) => {
    //     if(this.userInfoService.getUserData().id === this.userId) {
    //       this.userInfoService.setRoles(response)
    //     }
    //     this.alertService.showSuccess('Lista de Roles actualizada correctamente.')
    //     this.saving = false
    //     this.activeModal.close(response)
    //   }, error: (e) => {
    //     this.saving = false
    //     throw e
    //   }
    // })
  }

  private minimumOneSelectedValidator = (abstractControl: AbstractControl): ValidationErrors | null => {
    const roles = abstractControl as FormArray<FormGroup<CheckFormGroup>>
    const selectedRoles = roles.controls.map(x => x.controls.value.value)
    return selectedRoles.some(x => x) ? null : { noneSelected : true }
  }
}

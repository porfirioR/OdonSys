import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Store, select } from '@ngrx/store';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, combineLatest, map } from 'rxjs';
import { RoleApiService } from '../../../../core/services/api/role-api.service';
import * as fromRolesActions from '../../../../core/store/roles/roles.actions';
import { selectRoles } from '../../../../core/store/roles/roles.selectors';
import { savingSelector } from '../../../../core/store/saving/saving.selector';
import { PermissionModel } from '../../../../core/models/view/permission-model';
import { CreateRoleApiRequest } from '../../../../core/models/api/roles/create-role-api-request';
import { UpdateRoleApiRequest } from '../../../../core/models/api/roles/update-role-api-request';
import { SubGroupPermissions } from '../../../../core/forms/sub-group-permissions.form';
import { MethodHandler } from '../../../../core/helpers/method-handler';

@Component({
  selector: 'app-upsert-role',
  templateUrl: './upsert-role.component.html',
  styleUrls: ['./upsert-role.component.scss']
})
export class UpsertRoleComponent implements OnInit {
  public formGroup = new FormGroup( {
    name: new FormControl('', [Validators.required, Validators.maxLength(30)]),
    code: new FormControl('', [Validators.required, Validators.maxLength(30)]),
    subGroupPermissions: new FormArray<FormGroup<SubGroupPermissions>>([])
  })
  public saveData: boolean = false
  protected saving$: Observable<boolean> = this.store.select(savingSelector)
  protected title = 'Crear'
  private code = ''

  constructor(
    private readonly roleApiService: RoleApiService,
    private store: Store,
    private activatedRoute: ActivatedRoute,
    private readonly router: Router
  ) { }

  ngOnInit() {
    this.code = this.activatedRoute.snapshot.params['code']
    const isUpdateUrl = this.activatedRoute.snapshot.url[1].path === 'actualizar'
    const role$ = this.store.pipe(
      select(selectRoles),
      map(x => this.code ? x.find(y => y.code === this.code) ?? undefined : undefined)
    )
    combineLatest([this.roleApiService.getPermissions(), role$])
    .subscribe({
      next: ([permissions, role]) => {
        if (isUpdateUrl && !role) {
          this.router.navigate(['admin/roles/crear'])
        }
        if (role) {
          this.title = 'Modificar'
          this.formGroup.controls.code.setValue(role.code)
          this.formGroup.controls.name.setValue(role.name)
          this.formGroup.controls.code.disable()
        }
        this.preparePermissions(permissions, role?.rolePermissions ?? [])
      }
    })
  }

  protected save = () => {
    this.saveData = true
    this.code ? this.update() : this.create()
  }

  protected close = () => {
    this.router.navigate(['admin/roles'])
  }

  private create = () => {
    const request =  new CreateRoleApiRequest(
      this.formGroup.value.name!,
      this.formGroup.value.code!,
      this.getSelectedPermissions()
    )
    this.store.dispatch(fromRolesActions.createRole({ createRole: request }))
  }

  private update = (): void => {
    const request =  new UpdateRoleApiRequest(
      this.formGroup.value.name!,
      this.formGroup.controls.code.value!,
      true,
      this.getSelectedPermissions(),
      []
    )
    this.store.dispatch(fromRolesActions.updateRole({ updateRole: request }))
  }

  private preparePermissions = (allPermissions: PermissionModel[], rolePermissions: string[]) => {
    MethodHandler.setSubGroupPermissions(allPermissions, rolePermissions, this.formGroup.controls.subGroupPermissions)
    this.formGroup.controls.subGroupPermissions!.addValidators(this.minimumOneSelectedValidator)
  }

  private minimumOneSelectedValidator = (abstractControl: AbstractControl): ValidationErrors | null => {
    const groupPermissions = abstractControl as FormArray<FormGroup<SubGroupPermissions>>
    const permissionFormGroup =  groupPermissions.controls.map(x => x.controls).map(x => x.permissions.controls)
    const atLeastOneIsSelected = permissionFormGroup.some(permissions => permissions.some(permission => permission.value.value!))
    return atLeastOneIsSelected ? null : { noneSelected : true }
  }

  private getSelectedPermissions = (): string[] => {
    const selectedPermissions = this.formGroup.controls.subGroupPermissions!.controls
            .map(x => x.controls.permissions.controls)
            .map(x => x.filter(y => y.controls.value!.value!).map(x => x.controls.code.value!))
            .reduce((accumulator, value) => accumulator.concat(value), [])
    return selectedPermissions
  }
}

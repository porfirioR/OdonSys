import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Location } from '@angular/common';
import { Store, select } from '@ngrx/store';
import { ActivatedRoute } from '@angular/router';
import { Observable, combineLatest, map } from 'rxjs';
import { RoleApiService } from '../../../../core/services/api/role-api.service';
import * as fromRolesActions from '../../../../core/store/roles/roles.actions';
import { selectRoles } from '../../../../core/store/roles/roles.selectors';
import { savingSelector } from '../../../../core/store/saving/saving.selector';
import { PermissionModel } from '../../../../core/models/view/permission-model';
import { CreateRoleApiRequest } from '../../../../core/models/api/roles/create-role-api-request';
import { UpdateRoleApiRequest } from '../../../../core/models/api/roles/update-role-api-request';
import { SubGroupPermissions } from '../../../../core/forms/sub-group-permissions.form';
import { PermissionFormGroup } from '../../../../core/forms/permission-form-group.form';
@Component({
  selector: 'app-upsert-role',
  templateUrl: './upsert-role.component.html',
  styleUrls: ['./upsert-role.component.scss']
})
export class UpsertRoleComponent implements OnInit {
  public load: boolean = false;
  public saving$: Observable<boolean> = this.store.select(savingSelector)
  private code = '';
  public title = 'Crear';
  public formGroup = new FormGroup( {
    name : new FormControl('', [Validators.required, Validators.maxLength(30)]),
    code : new FormControl('', [Validators.required, Validators.maxLength(30)]),
    subGroupPermissions: new FormArray<FormGroup<SubGroupPermissions>>([])
  })

  constructor(
    private readonly location: Location,
    private readonly roleApiService: RoleApiService,
    private store: Store,
    private activateRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    this.code = this.activateRoute.snapshot.params['code']
    const role$ = this.store.pipe(
      select(selectRoles),
      map(x => this.code ? x.find(y => y.code === this.code) ?? undefined : undefined)
    )
    combineLatest([this.roleApiService.getPermissions(), role$])
    .subscribe({
      next: ([permissions, role]) => {
        if (role) {
          this.title = 'Modificar'
          this.formGroup.controls.code.setValue(role.code)
          this.formGroup.controls.name.setValue(role.name)
        }
        this.preparePermissions(permissions, role?.rolePermissions ?? [])
        this.load = true
      }, error: (e) => {
        this.load = true
        throw e
      }
    })
  }

  protected save = () => {
    this.code ? this.update() : this.create()
  }

  protected close = () => {
    this.location.back();
  }

  
  private create = () => {
    const request =  new CreateRoleApiRequest(
      this.formGroup.value.name!,
      this.formGroup.value.code!,
      this.getSelectedPermissions()
    )
    this.store.dispatch(fromRolesActions.createRole({createRole: request}))
  }
  
  private update = (): void => {
    const request =  new UpdateRoleApiRequest(
      this.formGroup.value.name!,
      this.formGroup.value.code!,
      true,
      this.getSelectedPermissions(),
      []
    )
    this.store.dispatch(fromRolesActions.updateRole({ updateRole: request }))
  }

  private preparePermissions = (allPermissions: PermissionModel[], rolePermissions: string[]) => {
    const subGroup = [...new Set(allPermissions.map(x => x.subGroup))].sort((a, b) => a.localeCompare(b))
    subGroup.forEach(x => {
      const permissionsFormGroups = allPermissions.map(x => 
        new FormGroup<PermissionFormGroup>({
          name: new FormControl(x.name),
          code: new FormControl(x.code),
          group: new FormControl(x.group),
          subGroup: new FormControl(x.subGroup),
          value: new FormControl(rolePermissions.includes(x.code))
        })
      )
      const permissions = permissionsFormGroups.filter(formGroup => formGroup.value.subGroup! === x)
      const subGroupPermissions = new FormGroup({
        subGroup: new FormControl(x, { nonNullable: true}),
        permissions: new FormArray(permissions)
      })
      this.formGroup.controls.subGroupPermissions!.push(subGroupPermissions)
    })
    this.formGroup.controls.subGroupPermissions!.addValidators(this.minimumOneSelectedValidator)
  }

  private minimumOneSelectedValidator = (abstractControl: AbstractControl): ValidationErrors | null => {
    const groupPermissions = abstractControl as FormArray<FormGroup<SubGroupPermissions>>
    const permissionFormGroup =  groupPermissions.controls.map(x => x.controls).map(x => x.permissions.controls);
    const atLeastOneIsSelected = permissionFormGroup.some(permissions => permissions.some(permission => permission.value.value!))
    return atLeastOneIsSelected ? null : { noneSelected : true };
  }

  private getSelectedPermissions = (): string[] => {
    const selectedPermissions = this.formGroup.controls.subGroupPermissions!.controls
                .map(x => x.controls.permissions.controls)
                .map(x => x.filter(y => y.controls.value).map(x => x.controls.code.value!))
                .reduce((accumulator, value) => accumulator.concat(value), [])
    return selectedPermissions
  }
}

import { Component, OnInit } from '@angular/core';
import { FormArray, FormGroup } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { take } from 'rxjs';
import { CheckFormGroup } from '../../../core/forms/check-form-group';
import { RoleModel } from '../../../core/models/view/role-model';
import { RoleApiService } from '../../../core/services/api/role-api.service';
import { UserInfoService } from '../../../core/services/shared/user-info.service';

@Component({
  selector: 'app-user-role',
  templateUrl: './user-role.component.html',
  styleUrls: ['./user-role.component.scss']
})
export class UserRoleComponent implements OnInit {
  protected roles: RoleModel[] = []
  protected userForm = new FormGroup({
    roles: new FormArray<FormGroup<CheckFormGroup>>([])
  })
  constructor(
    public activeModal: NgbActiveModal,
    private readonly rolesApiService: RoleApiService,
    private userInfoService: UserInfoService
  ) { }

  ngOnInit() {
    this.rolesApiService.getAll().pipe(take(1)).subscribe({
      next: (roles) => {
        this.roles = roles.map(x => new RoleModel(x.name, x.code, x.userCreated, x.userUpdated, x.dateCreated, x.dateModified, x.rolePermissions, x.userRoles))
        const myRoles = this.userInfoService.getUserData().roles
      }, error: (e) => {
        
        throw e;
      }
    })
  }

  protected save = () => {

  }

}

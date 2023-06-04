import { HttpErrorResponse } from '@angular/common/http';
import { createAction, props } from '@ngrx/store';
import { CreateRoleApiRequest } from '../../models/api/roles/create-role-api-request';
import { RoleModel } from '../../models/view/role-model';
import { UpdateRoleApiRequest } from '../../models/api/roles/update-role-api-request';

export const loadRoles = createAction(
  '[Roles Component] Load Roles'
)

export const allRolesLoaded = createAction(
  '[Role Effect] Load Roles',
  props<{ roles: RoleModel[] }>()
)

export const rolesFailure = createAction(
  '[Roles] Get Roles Failure',
  props<{ error: HttpErrorResponse }>()
)

export const createRole = createAction(
  '[Create Role Component] Create Role',
  props<{ createRole: CreateRoleApiRequest }>()
)

export const createRoleSuccess = createAction(
  '[Role Effects] Create Role Success',
  props<{ role: RoleModel }>()
)

export const updateRole = createAction(
  '[Update Role Component] Update Role',
  props<{ updateRole: UpdateRoleApiRequest }>()
)

export const updateRoleSuccess = createAction(
  '[Role Effects] Update Role Success',
  props<{ role: RoleModel }>()
)

export const clearRoles = createAction(
  '[Role/API] Clear Roles'
)
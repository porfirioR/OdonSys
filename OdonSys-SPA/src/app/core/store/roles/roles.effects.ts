import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import * as roleActions from './roles.actions';
import { catchError, map, of, switchMap, tap, withLatestFrom } from 'rxjs';
import { RoleApiService } from '../../services/api/role-api.service';
import { RoleModel } from '../../models/view/role-model';
import { Store } from '@ngrx/store';
import { selectRoles } from './roles.selectors';

@Injectable()
export class RolesEffects {

  constructor(
    private actions$: Actions,
    private readonly roleApiService: RoleApiService,
    private readonly store: Store
  ) {}

  getRoles$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(roleActions.loadRoles),
      withLatestFrom(this.store.select(selectRoles)),
      switchMap(([action, roles]) => roles.length > 0 ?
        of(roleActions.allRolesLoaded({ roles:roles })) :
        this.roleApiService.getAll().pipe(
          map(response => roleActions.allRolesLoaded({
            roles: response.map(x =>
              new RoleModel(
                x.name,
                x.code,
                x.userCreated,
                x.userUpdated,
                x.dateCreated,
                x.dateModified,
                x.rolePermissions,
                x.userRoles
                )
              )
          })),
          catchError(error => of(roleActions.rolesFailure({ error })))
        )
      )
    )
  })

  createRole$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(roleActions.createRole),
      switchMap((action) =>
        this.roleApiService.create(action.createRole).pipe(
          map(role => roleActions.createRoleSuccess({ role: new RoleModel(role.name, role.code, role.userCreated, role.userUpdated, role.dateCreated, role.dateModified, role.rolePermissions, role.userRoles) })),
          catchError(error => of(roleActions.rolesFailure({ error })))
        )
      )
    )
  })

  updateRole$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(roleActions.updateRole),
      switchMap((action) =>
        this.roleApiService.update(action.updateRole).pipe(
          map(role => roleActions.createRoleSuccess({ role: new RoleModel(role.name, role.code, role.userCreated, role.userUpdated, role.dateCreated, role.dateModified, role.rolePermissions, role.userRoles) })),
          catchError(error => of(roleActions.rolesFailure({ error })))
        )
      )
    )
  })

  errorHandler$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(roleActions.rolesFailure),
      tap((x) => {throw x.error})
    )
  })
}

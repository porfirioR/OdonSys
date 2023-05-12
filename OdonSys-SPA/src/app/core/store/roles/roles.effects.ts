import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import * as roleActions from './roles.actions';
import { catchError, map, of, switchMap, tap, withLatestFrom } from 'rxjs';
import { RoleApiService } from '../../services/api/role-api.service';
import { AlertService } from '../../services/shared/alert.service';
import { RoleModel } from '../../models/view/role-model';
import { selectRoles } from './roles.selectors';

@Injectable()
export class RolesEffects {

  constructor(
    private actions$: Actions,
    private readonly roleApiService: RoleApiService,
    private readonly store: Store,
    private readonly router: Router,
    private readonly alertService: AlertService
  ) {}

  getRoles$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(roleActions.loadRoles),
      withLatestFrom(this.store.select(selectRoles)),
      switchMap(([action, roles]) => roles.length > 0 ?
        of(roleActions.allRolesLoaded({ roles: roles })) :
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

  create$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(roleActions.createRole),
      switchMap((action) =>
        this.roleApiService.create(action.createRole).pipe(
          map(role => {
            this.router.navigate(['/admin/roles'])
            this.alertService.showSuccess('Rol creado con éxito.')
            return roleActions.createRoleSuccess({ role: new RoleModel(role.name, role.code, role.userCreated, role.userUpdated, role.dateCreated, role.dateModified, role.rolePermissions, role.userRoles) })
          }),
          catchError(error => of(roleActions.rolesFailure({ error })))
        )
      )
    )
  })

  update$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(roleActions.updateRole),
      switchMap((action) =>
        this.roleApiService.update(action.updateRole).pipe(
          map(role => {
            this.router.navigate(['/admin/roles'])
            this.alertService.showSuccess('Rol actualizado con éxito.')
            return roleActions.updateRoleSuccess({ role: new RoleModel(role.name, role.code, role.userCreated, role.userUpdated, role.dateCreated, role.dateModified, role.rolePermissions, role.userRoles) })
          }),
          catchError(error => of(roleActions.rolesFailure({ error })))
        )
      )
    )
  })

  errorHandler$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(roleActions.rolesFailure),
      tap((x) => { throw x.error })
    )
  })
}

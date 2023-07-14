import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import * as roleActions from './roles.actions';
import { catchError, map, of, switchMap, tap, withLatestFrom } from 'rxjs';
import { RoleApiService } from '../../services/api/role-api.service';
import { AlertService } from '../../services/shared/alert.service';
import { UserInfoService } from '../../services/shared/user-info.service';
import { SubscriptionService } from '../../services/shared/subscription.service';
import { RoleModel } from '../../models/view/role-model';
import { RoleApiModel } from '../../models/api/roles/role-api-model';
import { selectRoles } from './roles.selectors';

@Injectable()
export class RolesEffects {

  constructor(
    private actions$: Actions,
    private readonly roleApiService: RoleApiService,
    private readonly store: Store,
    private readonly router: Router,
    private readonly alertService: AlertService,
    private userInfoService: UserInfoService,
    private readonly subscriptionService: SubscriptionService
  ) {}

  protected getRoles$ = createEffect(() => {
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

  protected create$ = createEffect(() => {
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

  protected update$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(roleActions.updateRole),
      switchMap((action) =>
        this.roleApiService.update(action.updateRole).pipe(
          switchMap(role => {
            const roles = this.userInfoService.getUserData().roles
            if (roles.includes(role.code)) {
              const permissions$ = this.roleApiService.getMyPermissions()
              return permissions$.pipe(map(permissions => {
                this.userInfoService.setUserPermissions(permissions)
                return this.roleSuccess(role)
              }))
            } else {
              return of(this.roleSuccess(role))
            }
          }),
          catchError(error => of(roleActions.rolesFailure({ error })))
        )
      )
    )
  })

  private errorHandler$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(roleActions.rolesFailure),
      tap((x) => {
        this.subscriptionService.emitErrorInSave()
        throw x.error
      })
    )
  })

  private roleSuccess = (role: RoleApiModel ) => {
    this.router.navigate(['/admin/roles'])
    this.alertService.showSuccess('Rol actualizado con éxito.')
    return roleActions.updateRoleSuccess({ role: new RoleModel(role.name, role.code, role.userCreated, role.userUpdated, role.dateCreated, role.dateModified, role.rolePermissions, role.userRoles) })
  }
}

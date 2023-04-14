import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import * as roleActions from './roles.actions';
import { catchError, map, of, switchMap } from 'rxjs';
import { UserDataApiService } from '../../services/api/user-data-api.service';
import { RoleApiService } from '../../services/api/role-api.service';
import { RoleModel } from '../../models/view/role-model';

@Injectable()
export class UserInfoEffects {

  constructor(
    private actions$: Actions,
    private readonly roleApiService: RoleApiService
  ) {}

  getRoles$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(roleActions.loadRoles),
      switchMap(() =>
        this.roleApiService.getAll().pipe(
          map(response => roleActions.allRolesLoaded({ roles: response.map(x => new RoleModel(x.name, x.code, x.rolePermission, x.userRoles)) })),
          catchError(error => of(roleActions.rolesFailure({ error })))
        )
      ),
    );
  });
}

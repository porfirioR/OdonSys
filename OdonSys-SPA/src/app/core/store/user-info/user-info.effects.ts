import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import * as userInfoActions from './user-info.actions';
import { catchError, map, of, switchMap } from 'rxjs';
import { UserDataApiService } from '../../services/api/user-data-api.service';

@Injectable()
export class UserInfoEffects {

  constructor(
    private actions$: Actions,
    private readonly userDataApiService: UserDataApiService
  ) {}

  getUserInfo$ = createEffect(() => {
    return this.actions$.pipe(
        ofType(userInfoActions.getUserInfo),
        switchMap(() =>
        this.userDataApiService.getUserData().pipe(
          map(user => userInfoActions.userInfoSuccess({ user })),
          catchError(error => of(userInfoActions.userInfoFailure({ error })))
        )
      ),
    );
  });
}

import { HttpErrorResponse } from '@angular/common/http';
import { createAction, props } from '@ngrx/store';
import { UserDataApiModel } from '../../models/api/user-data-api-model';

export const getUserInfo = createAction(
  '[UserInfo] Get UserInfo'
);

export const userInfoSuccess = createAction(
  '[UserInfo] Get UserInfo Success',
  props<{ user: UserDataApiModel }>()
);

export const userInfoFailure = createAction(
  '[UserInfo] Get UserInfo Failure',
  props<{ error: HttpErrorResponse  }>()
);

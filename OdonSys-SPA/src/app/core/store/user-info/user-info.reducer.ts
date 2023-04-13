import { createReducer, on } from '@ngrx/store';
import * as userInfoActions from './user-info.actions';


export const userInfoFeatureKey = 'userInfo';

export interface UserInfoState {

}

export const initialState: UserInfoState = {

};

export const reducer = createReducer(
  initialState,
  on(userInfoActions.userInfoSuccess,
    (state: UserInfoState, { user }) => ({
      ...state,
      user
    })
  )
);

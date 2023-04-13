import {
  ActionReducer,
  ActionReducerMap,
  createFeatureSelector,
  createSelector,
  MetaReducer
} from '@ngrx/store'
import * as fromUserInfo from './user-info/user-info.reducer'

export const coreFeatureKey = 'core'

export interface CoreState {
  [fromUserInfo.userInfoFeatureKey]: fromUserInfo.UserInfoState,
}

export const reducers: ActionReducerMap<CoreState> = {
  [fromUserInfo.userInfoFeatureKey]: fromUserInfo.reducer,
}

export const selectCoreFeature = createFeatureSelector<CoreState>(coreFeatureKey)

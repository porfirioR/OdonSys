import { createSelector } from '@ngrx/store';
import * as fromCore from '../index'

export const selectUserInfo = createSelector(
  fromCore.selectCoreFeature,
  (state) => state.userInfo
)

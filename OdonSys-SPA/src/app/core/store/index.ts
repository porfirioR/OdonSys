import {
  ActionReducer,
  ActionReducerMap,
  createFeatureSelector,
  createSelector,
  MetaReducer
} from '@ngrx/store'
import * as fromRoles from './roles/roles.reducer'
import * as fromSaving from './saving/saving.reducer'

export const coreFeatureKey = 'core'

export interface CoreState {
  [fromRoles.roleFeatureKey]: fromRoles.RoleState,
  [fromSaving.savingFeatureKey]: fromSaving.SavingState,
}

export const reducers: ActionReducerMap<CoreState> = {
  [fromRoles.roleFeatureKey]: fromRoles.rolesReducer,
  [fromSaving.savingFeatureKey]: fromSaving.reducer
}

export const selectCoreFeature = createFeatureSelector<CoreState>(coreFeatureKey)

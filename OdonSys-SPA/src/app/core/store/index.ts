import {
  ActionReducer,
  ActionReducerMap,
  createFeatureSelector,
  createSelector,
  MetaReducer
} from '@ngrx/store'
import * as fromRoles from './roles/roles.reducer'

export const coreFeatureKey = 'core'

export interface CoreState {
  [fromRoles.roleFeatureKey]: fromRoles.RoleState,
}

export const reducers: ActionReducerMap<CoreState> = {
  [fromRoles.roleFeatureKey]: fromRoles.rolesReducer,
}

export const selectCoreFeature = createFeatureSelector<CoreState>(coreFeatureKey)

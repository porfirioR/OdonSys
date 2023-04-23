import {
  ActionReducer,
  ActionReducerMap,
  createFeatureSelector,
  createSelector,
  MetaReducer
} from '@ngrx/store'
import * as fromRoles from './roles/roles.reducer'
import * as fromSaving from './saving/saving.reducer';
import * as fromProcedure from './procedure/procedure.reducer'

export const coreFeatureKey = 'core'

export interface CoreState {
  [fromRoles.roleFeatureKey]: fromRoles.RoleState,
  [fromSaving.savingFeatureKey]: fromSaving.SavingState,
  [fromProcedure.proceduresFeatureKey]: fromProcedure.ProcedureState;

}

export const reducers: ActionReducerMap<CoreState> = {
  [fromRoles.roleFeatureKey]: fromRoles.rolesReducer,
  [fromSaving.savingFeatureKey]: fromSaving.reducer,
  [fromProcedure.proceduresFeatureKey]: fromProcedure.reducer,
}

export const selectCoreFeature = createFeatureSelector<CoreState>(coreFeatureKey)

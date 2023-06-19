import {
  ActionReducer,
  ActionReducerMap,
  createFeatureSelector,
  createSelector,
  MetaReducer
} from '@ngrx/store'
import * as fromRoles from './roles/roles.reducer'
import * as fromSaving from './saving/saving.reducer';
import * as fromProcedure from './procedures/procedure.reducer';
import * as fromClient from './clients/client.reducer';
import * as fromDoctor from './doctors/doctor.reducer'

export const coreFeatureKey = 'core'

export interface CoreState {
  [fromRoles.roleFeatureKey]: fromRoles.RoleState,
  [fromSaving.savingFeatureKey]: fromSaving.SavingState,
  [fromProcedure.proceduresFeatureKey]: fromProcedure.ProcedureState,
  [fromClient.clientsFeatureKey]: fromClient.ClientState,
  [fromDoctor.doctorsFeatureKey]: fromDoctor.DoctorState;


}

export const reducers: ActionReducerMap<CoreState> = {
  [fromRoles.roleFeatureKey]: fromRoles.reducer,
  [fromSaving.savingFeatureKey]: fromSaving.reducer,
  [fromProcedure.proceduresFeatureKey]: fromProcedure.reducer,
  [fromClient.clientsFeatureKey]: fromClient.reducer,
  [fromDoctor.doctorsFeatureKey]: fromDoctor.reducer,
}

export const selectCoreFeature = createFeatureSelector<CoreState>(coreFeatureKey)

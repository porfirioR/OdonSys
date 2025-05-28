import {
  ActionReducerMap,
  createFeatureSelector} from '@ngrx/store'
import * as fromRoles from './roles/roles.reducer'
import * as fromSaving from './saving/saving.reducer';
import * as fromProcedure from './procedures/procedure.reducer';
import * as fromClient from './clients/client.reducer';
import * as fromDoctor from './doctors/doctor.reducer';
import * as fromTooth from './teeth/tooth.reducer'

export const coreFeatureKey = 'core'

export interface CoreState {
  [fromRoles.roleFeatureKey]: fromRoles.RoleState,
  [fromSaving.savingFeatureKey]: fromSaving.SavingState,
  [fromProcedure.proceduresFeatureKey]: fromProcedure.ProcedureState,
  [fromClient.clientsFeatureKey]: fromClient.ClientState,
  [fromDoctor.doctorsFeatureKey]: fromDoctor.DoctorState;
  [fromTooth.teethFeatureKey]: fromTooth.ToothState;
}

export const reducers: ActionReducerMap<CoreState> = {
  [fromRoles.roleFeatureKey]: fromRoles.reducer,
  [fromSaving.savingFeatureKey]: fromSaving.reducer,
  [fromProcedure.proceduresFeatureKey]: fromProcedure.reducer,
  [fromClient.clientsFeatureKey]: fromClient.reducer,
  [fromDoctor.doctorsFeatureKey]: fromDoctor.reducer,
  [fromTooth.teethFeatureKey]: fromTooth.reducer,
}

export const selectCoreFeature = createFeatureSelector<CoreState>(coreFeatureKey)

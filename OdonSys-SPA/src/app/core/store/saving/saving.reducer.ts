import { createReducer, on } from '@ngrx/store';
import * as fromRolesActions from '../roles/roles.actions';
import * as fromSavingActions from './saving.actions'

export const savingFeatureKey = 'saving';

export interface SavingState {
  saving: boolean
}

export const initialState: SavingState = {
  saving: false
}

export const reducer = createReducer(
  initialState,
  on(
    fromRolesActions.updateRole,
    fromRolesActions.createRole,
    (state) => ({
      ...state,
      saving: true
    })
  ),
  on(
    fromRolesActions.createRoleSuccess,
    fromRolesActions.updateRoleSuccess,
    (state) => ({
      ...state,
      saving: false
    })
  ),
  on(
    fromSavingActions.savingFailed,
    fromRolesActions.rolesFailure,
    (state) => ({
      ...state,
      saving: false
    })
  )
)
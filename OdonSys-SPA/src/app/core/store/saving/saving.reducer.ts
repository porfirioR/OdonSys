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
    (state) => ({
      ...state,
      saving: true
    })
  ),
  on(
    fromRolesActions.updateRoleSuccess,
    (state) => ({
      ...state,
      saving: false
    })
  ),
  on(fromSavingActions.savingFailed,
    (state) => ({
      ...state,
      saving: false
    })
  )
)

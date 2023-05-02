import { createReducer, on } from '@ngrx/store'
import * as fromRolesActions from '../roles/roles.actions'
import * as fromProceduresActions from '../procedures/procedure.actions'
import * as fromClientsActions from '../clients/client.actions'
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
    fromProceduresActions.changeProcedureVisibility,
    fromClientsActions.addClient,
    fromClientsActions.updateClient,
    fromClientsActions.changeClientVisibility,
    (state) => ({
      ...state,
      saving: true
    })
  ),
  on(
    fromRolesActions.createRoleSuccess,
    fromRolesActions.updateRoleSuccess,
    fromClientsActions.addClientSuccess,
    fromClientsActions.updateClientSuccess,
    (state) => ({
      ...state,
      saving: false
    })
  ),
  on(
    fromSavingActions.savingFailed,
    fromRolesActions.rolesFailure,
    fromProceduresActions.procedureFailure,
    fromClientsActions.clientFailure,
    (state) => ({
      ...state,
      saving: false
    })
  )
)

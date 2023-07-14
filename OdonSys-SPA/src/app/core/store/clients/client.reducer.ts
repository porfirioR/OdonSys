import { createReducer, on } from '@ngrx/store';
import { EntityState, EntityAdapter, createEntityAdapter } from '@ngrx/entity';
import * as ClientActions from './client.actions';
import { ClientModel } from '../../models/view/client-model';

export const clientsFeatureKey = 'clients';

export interface ClientState extends EntityState<ClientModel> { }

export const adapter: EntityAdapter<ClientModel> = createEntityAdapter<ClientModel>()

export const initialState: ClientState = adapter.getInitialState({ })

export const reducer = createReducer(
  initialState,
  on(ClientActions.allClientsLoaded,
    (state, action) => adapter.setAll(action.clients, state)
  ),
  on(ClientActions.addClientSuccess,
    (state, action) => adapter.addOne(action.client, state)
  ),
  on(
    ClientActions.updateClientSuccess,
    ClientActions.changeClientVisibilitySuccess,
    (state, action) => adapter.upsertOne(action.client, state)
  ),
  on(ClientActions.deleteClient,
    (state, action) => adapter.removeOne(action.id, state)
  ),
  on(ClientActions.clearClients,
    state => adapter.removeAll(state)
  )
)

export const {
  selectIds,
  selectEntities,
  selectAll,
  selectTotal,
} = adapter.getSelectors()

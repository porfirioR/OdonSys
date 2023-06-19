import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as fromClient from './client.reducer';
export const selectClientsFeature = createFeatureSelector<fromClient.ClientState>(fromClient.clientsFeatureKey);

export const selectClients = createSelector(
  selectClientsFeature,
  fromClient.selectAll
)

export const selectActiveClients = createSelector(
  selectClientsFeature,
  selectClients,
  (state, clients) => clients.filter(x => x.active)
)

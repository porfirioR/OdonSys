import { HttpErrorResponse } from '@angular/common/http';
import { createAction, props } from '@ngrx/store';
import { ClientModel } from '../../models/view/client-model';
import { CreateClientRequest } from '../../models/api/clients/create-client-request';
import { UpdateClientRequest } from '../../models/api/clients/update-client-request';
import { PatchRequest } from '../../models/api/patch-request';

export const loadClients = createAction(
  '[Client Component] Load Clients'
)

export const allClientsLoaded = createAction(
  '[Client Effect] Load Clients',
  props<{ clients: ClientModel[] }>()
)

export const addClient = createAction(
  '[Client Component] Add Client',
  props<{ client: CreateClientRequest }>()
)

export const addClientSuccess = createAction(
  '[Client Effects] Add Client Success',
  props<{ client: ClientModel }>()
)

export const updateClientSuccess = createAction(
  '[Client Effects] Update Client Success',
  props<{ client: ClientModel }>()
)

export const updateClient = createAction(
  '[Client Component] Update Client',
  props<{ client: UpdateClientRequest }>()
)

export const deleteClient = createAction(
  '[Client ] Delete Client',
  props<{ id: string }>()
)

export const changeClientVisibility = createAction(
  '[Client Component] Change Visibility Client',
  props<{
    id: string,
    model: PatchRequest
  }>()
)

export const changeClientVisibilitySuccess = createAction(
  '[Client Effect] Change Visibility Client Success',
  props<{ client: ClientModel }>()
)

export const clearClients = createAction(
  '[Client] Clear Clients'
)

export const clientFailure = createAction(
  '[Client] Get Clients Failure',
  props<{ error: HttpErrorResponse }>()
)
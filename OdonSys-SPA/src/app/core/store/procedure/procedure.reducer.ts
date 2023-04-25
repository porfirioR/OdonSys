import { createReducer, on } from '@ngrx/store';
import { EntityState, EntityAdapter, createEntityAdapter } from '@ngrx/entity';
import * as ProcedureActions from './procedure.actions';
import { ProcedureModel } from '../../models/procedure/procedure-model';

export const proceduresFeatureKey = 'procedures';

export interface ProcedureState extends EntityState<ProcedureModel> { }

export const adapter: EntityAdapter<ProcedureModel> = createEntityAdapter<ProcedureModel>();

export const initialState: ProcedureState = adapter.getInitialState({ });

export const reducer = createReducer(
  initialState,
  on(ProcedureActions.allProceduresLoaded,
    (state, action) => adapter.setAll(action.procedures, state)
  ),
  on(ProcedureActions.addProcedureSuccess,
    (state, action) => adapter.addOne(action.procedure, state)
  ),
  on(
    ProcedureActions.updateProcedureSuccess,
    ProcedureActions.changeProcedureVisibilitySuccess,
    (state, action) => adapter.upsertOne(action.procedure, state)
  ),
  on(ProcedureActions.deleteProcedure,
    (state, action) => adapter.removeOne(action.id, state)
  ),
  on(ProcedureActions.clearProcedures,
    state => adapter.removeAll(state)
  ),
);

export const {
  selectIds,
  selectEntities,
  selectAll,
  selectTotal,
} = adapter.getSelectors();

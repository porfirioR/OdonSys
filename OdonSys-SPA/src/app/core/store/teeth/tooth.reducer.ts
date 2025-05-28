import { createReducer, on } from '@ngrx/store';
import { EntityState, EntityAdapter, createEntityAdapter } from '@ngrx/entity';
import * as toothActions  from './tooth.actions';
import { ToothModel } from '../../models/tooth/tooth-model';

export const teethFeatureKey = 'teeth';

export interface ToothState extends EntityState<ToothModel> { }

export const adapter: EntityAdapter<ToothModel> = createEntityAdapter<ToothModel>();

export const initialState: ToothState = adapter.getInitialState({ });

export const reducer = createReducer(
  initialState,
  on(toothActions.effectAllTeethLoaded,
    (state, action) => adapter.setAll(action.teeth, state)
  ),
  on(toothActions.clearTeeth,
    state => adapter.removeAll(state)
  ),
);

export const {
  selectIds,
  selectEntities,
  selectAll,
  selectTotal,
} = adapter.getSelectors();

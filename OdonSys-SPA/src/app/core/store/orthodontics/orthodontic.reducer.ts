import { createFeature, createReducer, on } from '@ngrx/store';
import { EntityState, EntityAdapter, createEntityAdapter } from '@ngrx/entity';
import { OrthodonticActions } from './orthodontic.actions';
import { OrthodonticModel } from '../../models/view/orthodontic-model';

export const orthodonticsFeatureKey = 'orthodontics';

export interface OrthodonticState extends EntityState<OrthodonticModel> { }

export const adapter: EntityAdapter<OrthodonticModel> = createEntityAdapter<OrthodonticModel>();

export const initialState: OrthodonticState = adapter.getInitialState({ });

export const reducer = createReducer(
  initialState,
  on(OrthodonticActions.addOrthodontics,
    (state, action) => adapter.setAll(action.orthodontics, state)
  ),
  on(OrthodonticActions.addOrthodonticSuccess,
    (state, action) => adapter.addOne(action.orthodontic, state)
  ),
  on(OrthodonticActions.addOrthodontics,
    (state, action) => adapter.addMany(action.orthodontics, state)
  ),
  on(OrthodonticActions.updateOrthodonticSuccess,
    (state, action) => adapter.upsertOne(action.orthodontic, state)
  ),
  on(OrthodonticActions.deleteOrthodonticSuccess,
    (state, action) => adapter.removeOne(action.id, state)
  ),
  on(OrthodonticActions.clearOrthodontics,
    state => adapter.removeAll(state)
  ),
)

export const {
  selectIds,
  selectEntities,
  selectAll,
  selectTotal,
} = adapter.getSelectors()

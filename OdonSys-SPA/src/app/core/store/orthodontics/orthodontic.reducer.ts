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
  on(OrthodonticActions.addOrthodontic,
    (state, action) => adapter.addOne(action.orthodontic, state)
  ),
  on(OrthodonticActions.upsertOrthodontic,
    (state, action) => adapter.upsertOne(action.orthodontic, state)
  ),
  on(OrthodonticActions.addOrthodontics,
    (state, action) => adapter.addMany(action.orthodontics, state)
  ),
  on(OrthodonticActions.upsertOrthodontics,
    (state, action) => adapter.upsertMany(action.orthodontics, state)
  ),
  on(OrthodonticActions.updateOrthodontic,
    (state, action) => adapter.updateOne(action.orthodontic, state)
  ),
  on(OrthodonticActions.updateOrthodontics,
    (state, action) => adapter.updateMany(action.orthodontics, state)
  ),
  on(OrthodonticActions.deleteOrthodontic,
    (state, action) => adapter.removeOne(action.id, state)
  ),
  on(OrthodonticActions.deleteOrthodontics,
    (state, action) => adapter.removeMany(action.ids, state)
  ),
  on(OrthodonticActions.loadOrthodontics,
    (state, action) => adapter.setAll(action.orthodontics, state)
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

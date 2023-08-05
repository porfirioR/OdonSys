import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as fromTooth from './tooth.reducer';

export const selectTeethFeature = createFeatureSelector<fromTooth.ToothState>(fromTooth.teethFeatureKey);

export const selectTeeth = createSelector(
  selectTeethFeature,
  fromTooth.selectAll
)

export const selectDoctor = (toothId: string) => createSelector(
  selectTeethFeature,
  state => state.entities[toothId.toUpperCase()]!
)

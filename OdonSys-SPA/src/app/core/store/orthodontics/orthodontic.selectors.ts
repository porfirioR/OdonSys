import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as fromOrthodontic from './orthodontic.reducer';
export const selectOrthodonticsFeature = createFeatureSelector<fromOrthodontic.OrthodonticState>(fromOrthodontic.orthodonticsFeatureKey);

export const selectOrthodontics = createSelector(
  selectOrthodonticsFeature,
  fromOrthodontic.selectAll
)

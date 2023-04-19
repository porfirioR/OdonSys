import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as fromSaving from '../saving/saving.reducer'

export const savingFeatureSelector = createFeatureSelector<fromSaving.SavingState>(fromSaving.savingFeatureKey);

export const savingSelector = createSelector(
    savingFeatureSelector,
    state => state.saving
)
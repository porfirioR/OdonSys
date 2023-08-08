import { HttpErrorResponse } from '@angular/common/http';
import { createActionGroup, emptyProps, props } from '@ngrx/store';
import { ToothApiModel } from '../../models/tooth/tooth-api-model';

const toothActions = createActionGroup({
  source: 'Tooth',
  events: {
    'Component Load Teeth': emptyProps(),
    'Effect All Teeth Loaded': props<{ teeth: ToothApiModel[] }>(),
    'Failure To Load Teeth': props<{ error: HttpErrorResponse }>(),
    'Clear Teeth': emptyProps(),
  }
});

export const {
  componentLoadTeeth,
  effectAllTeethLoaded,
  failureToLoadTeeth,
  clearTeeth
} = toothActions
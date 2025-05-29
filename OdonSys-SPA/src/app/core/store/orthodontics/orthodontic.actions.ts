import { HttpErrorResponse } from '@angular/common/http';
import { createActionGroup, emptyProps, props } from '@ngrx/store';
import { OrthodonticModel } from '../../models/view/orthodontic-model';
import { OrthodonticRequest } from '../../models/api/orthodontics/orthodontic-request';

export const OrthodonticActions = createActionGroup({
  source: 'Orthodontic',
  events: {
    'Load Orthodontics': emptyProps(),
    'Add Orthodontic': props<
      {
        orthodontic: OrthodonticRequest,
        redirectUrl: string
      }
    >(),
    'Add Orthodontics': props<{ orthodontics: OrthodonticModel[] }>(),
    'Update Orthodontic': props<
      {
        id: string,
        orthodontic: OrthodonticRequest,
        redirectUrl: string
      }
    >(),
    'Delete Orthodontic': props<{ id: string }>(),
    'Add Orthodontic Success': props<{ orthodontic: OrthodonticModel }>(),
    'Update Orthodontic Success': props<{ orthodontic: OrthodonticModel }>(),
    'Delete Orthodontic Success': props<{ id: string }>(),
    'Clear Orthodontics': emptyProps(),
    'Failure': props<{ error: HttpErrorResponse }>(),
  }
});

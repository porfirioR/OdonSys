import { Injectable } from '@angular/core';
import Swal, { SweetAlertResult } from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class AlertService {

constructor() { }

  public showQuestionModal = async (title: string, text: string): Promise<SweetAlertResult<unknown>> => {
    const result = await Swal.fire({
      title,
      text,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes',
      cancelButtonText: 'No'
    });
    return result;
  }

  
  public showSuccess = (text: string = ''): void => {
    // this.zone.run(() => {
    //   this.snackBar.open(
    //     text,
    //     'Dismiss',
    //     {
    //       duration: 5000,
    //       horizontalPosition: 'end',
    //       verticalPosition: 'bottom',
    //       panelClass: ['success-snackbar']
    //     });
    // });
  }
}

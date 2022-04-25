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
      confirmButtonText: 'Si',
      cancelButtonText: 'No'
    });
    return result;
  }


  public showSuccess = (title: string = 'Operación exitosa'): void => {
    Swal.mixin({
      title,
      toast: true,
      position: 'bottom-end',
      showConfirmButton: false,
      timer: 3000,
      timerProgressBar: true,
      icon: 'success',
      didOpen: (toast) => {
        toast.addEventListener('mouseenter', Swal.stopTimer)
        toast.addEventListener('mouseleave', Swal.resumeTimer)
      }
    }).fire();
  }

  public showError = (text: string = ''): void => {
    Swal.fire({
      title: 'Operación fallida',
      text,
      icon: 'error',
    });
  }
}

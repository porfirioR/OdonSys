import { Injectable } from '@angular/core';
import Swal, { SweetAlertIcon, SweetAlertResult } from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class AlertService {

  constructor() { }

  public showQuestionModal = async (title: string, text: string = '', icon: SweetAlertIcon = 'warning'): Promise<SweetAlertResult<any>> => {
    const result = await Swal.fire({
      title,
      text,
      icon: icon,
      showCancelButton: true,
      confirmButtonText: 'Si',
      cancelButtonText: 'No',
      reverseButtons: true,
      focusCancel: true,
      customClass: {
        cancelButton: 'btn btn-outline-primary p-t-12 p-b-12 p-l-16 p-r-16 m-r-12',
        confirmButton: 'btn btn-outline-primary p-t-12 p-b-12 p-l-18 p-r-18'
      },
      buttonsStyling: false
    })
    return result
  }


  public showSuccess = (title: string = 'Operación éxitosa'): void => {
    Swal.mixin({
      title,
      toast: true,
      position: 'bottom-end',
      showConfirmButton: false,
      timer: 3000,
      timerProgressBar: true,
      icon: 'success',
      didOpen: (toast: any) => {
        toast.addEventListener('mouseenter', Swal.stopTimer)
        toast.addEventListener('mouseleave', Swal.resumeTimer)
      }
    }).fire()
  }

  public showError = (text: string = ''): void => {
    Swal.fire({
      title: 'Operación fallida',
      text,
      icon: 'error',
      confirmButtonText: 'De acuerdo',
      customClass: {
        confirmButton: 'btn btn-outline-primary',
      },
      buttonsStyling: false,
    })
  }

  public showInfo = (text: string = ''): void => {
    Swal.fire({
      title: 'Información',
      text,
      icon: 'info',
      confirmButtonText: 'De acuerdo',
      customClass: {
        confirmButton: 'btn btn-outline-primary p-t-12 p-b-12 p-l-18 p-r-18',
      },
      buttonsStyling: false
    })
  }
}

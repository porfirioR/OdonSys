import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SubscriptionService {
  private onCheckMenuSubject = new Subject<boolean>()
  public onCheckUpdateMenu: Observable<boolean> = this.onCheckMenuSubject

  private onErrorInSaveSubject = new Subject<boolean>()
  public onErrorInSave: Observable<boolean> = this.onErrorInSaveSubject

  constructor() { }

  public emitCheckMenu = (model: boolean) => {
    this.onCheckMenuSubject.next(model)
  }

  public emitErrorInSave = (model = true) => {
    this.onErrorInSaveSubject.next(model)
  }
}

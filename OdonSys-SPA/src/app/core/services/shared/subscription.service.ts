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

  private onDeleteSubject = new Subject<string>()
  public onDeleteId: Observable<string> = this.onDeleteSubject

  constructor() { }

  public emitCheckMenu = (model: boolean) => {
    this.onCheckMenuSubject.next(model)
  }

  public emitErrorInSave = (model = true) => {
    this.onErrorInSaveSubject.next(model)
  }

  public emitDeleteId = (id: string) => {
    this.onDeleteSubject.next(id)
  }
}

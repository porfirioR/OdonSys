import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SubscriptionService {
  private onCheckMenuSubject = new Subject<boolean>()
  public onCheckUpdateMenu: Observable<boolean> = this.onCheckMenuSubject

  constructor() { }

  public emitCheckMenu = (model: boolean) => {
    this.onCheckMenuSubject.next(model)
  }
}

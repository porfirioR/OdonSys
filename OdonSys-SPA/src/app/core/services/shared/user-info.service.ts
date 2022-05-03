import { Injectable } from '@angular/core';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class UserInfoService {
  private userKey = 'userData';

  constructor(private readonly localStorageService: LocalStorageService) { }

  public getUserId = () => {
    const userData = this.getUserData();
    return userData.id;
  }

  private getUserData = () => {
    const userData = this.localStorageService.getByKey(this.userKey);
    if (!userData) {
      throw new Error('Unable to get UserInfo');
    }
    return userData;
  }
}

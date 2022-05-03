import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LocalStorageService {

  public clearAll = (key: string) => {
    localStorage.removeItem(key);
  };

  public setData = (key: string, value: any) => {
    localStorage.setItem(key, JSON.stringify(value));
  };

  public getByKey = (key: string): any => {
    return JSON.parse(localStorage.getItem(key) as any);
  };

}

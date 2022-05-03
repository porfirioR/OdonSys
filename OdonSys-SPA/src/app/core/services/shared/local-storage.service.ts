import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LocalStorageService {

  clearAll = (key: string) => {
    localStorage.removeItem(key);
  };

  setData = (key: string, value: any) => {
    localStorage.setItem(key, JSON.stringify(value));
  };

  getByKey = (key: string): any => {
    return JSON.parse(localStorage.getItem(key) as any);
  };

}

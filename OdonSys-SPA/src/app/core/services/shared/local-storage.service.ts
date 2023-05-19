import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

  public clearAll = (key: string) => {
    localStorage.removeItem(key)
  }

  public setData = (key: string, value: any, isString = false) => {
    localStorage.setItem(key, isString ? value : JSON.stringify(value))
  }

  public getByKey = (key: string, isObject: boolean = true): any => {
    const value = localStorage.getItem(key) as any
    return isObject ? JSON.parse(value) : value
  }

  public getArrayByKey = (key: string): Array<any> => {
    return JSON.parse(localStorage.getItem(key) || '[]')
  }

}

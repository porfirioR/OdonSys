import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, retry } from 'rxjs';
import { UserInfoService } from '../services/shared/user-info.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private readonly userInfoService: UserInfoService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = this.userInfoService.getToken()
    if (token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      })
    }
    return next.handle(request).pipe(retry(1))
  }
}

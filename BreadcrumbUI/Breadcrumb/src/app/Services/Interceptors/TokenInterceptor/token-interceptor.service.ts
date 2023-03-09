import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import * as CONSTANTS from '../../../CONSTANTS/CONSTANTS';
import { CoreService } from '../../Other Services/CoreService/core.service';

@Injectable({
  providedIn: 'root'
})

export class TokenInterceptorService implements HttpInterceptor{

  constructor(private Core:CoreService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (req.headers.has(CONSTANTS.InterceptorSkipTokkenHeader)) {
      const headers = req.headers.delete(CONSTANTS.InterceptorSkipTokkenHeader);
      return next.handle(req.clone({ headers }));
    }

    let tokenizedReq = req.clone({
      setHeaders: {
        Authorization: 'Bearer ' + this.Core.getToken()
      }
    });
    return next.handle(tokenizedReq);
  }
}

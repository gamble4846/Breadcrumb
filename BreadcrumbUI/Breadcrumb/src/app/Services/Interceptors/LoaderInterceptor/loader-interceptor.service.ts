import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { finalize, Observable } from 'rxjs';
import * as CONSTANTS from '../../../CONSTANTS/CONSTANTS';
import { CoreService } from '../../Other Services/CoreService/core.service';

@Injectable({
  providedIn: 'root'
})
export class LoaderInterceptorService implements HttpInterceptor{

  constructor(private Core:CoreService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (req.headers.has(CONSTANTS.InterceptorSkipLoaderHeader)) {
      const headers = req.headers.delete(CONSTANTS.InterceptorSkipLoaderHeader);
      return next.handle(req.clone({ headers }));
    }

    console.log(req.url);

    this.Core.showLoader();
    return next.handle(req).pipe(
      finalize(() => {
        this.Core.hideLoader();
      })
    );
  }
}

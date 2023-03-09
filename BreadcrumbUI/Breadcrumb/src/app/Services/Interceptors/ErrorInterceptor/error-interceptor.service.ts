import { HttpErrorResponse, HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, tap, throwError } from 'rxjs';
import * as CONSTANTS from '../../../CONSTANTS/CONSTANTS';
import { CoreService } from '../../Other Services/CoreService/core.service';

@Injectable({
  providedIn: 'root'
})
export class ErrorInterceptorService {

  constructor(private Core:CoreService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    if (req.headers.has(CONSTANTS.InterceptorSkipErrorHeader)) {
      const headers = req.headers.delete(CONSTANTS.InterceptorSkipErrorHeader);
      return next.handle(req.clone({ headers }));
    }

    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        console.log(error);
        if(error.status == 0 && error.message.includes("Http failure response for")){
          this.Core.showMessage("error","API Offline")
        }
        else if(error.status == 403){
          this.Core.showMessage("error","You are not Authorized to perform this operation")
        }
        else{
          this.Core.showMessage("error","API Error")
        }

        let errorObj:any = {
          message: "Error Occured",
          error: error
        }

        return throwError(errorObj);
      })
    ).pipe(
      tap((data:any) => {
        try{
          if(data.body.code == 0){
            this.Core.showMessage("error",data.body.message);
            console.log(data, "API ERROR", req.urlWithParams);
          }
        }
        catch(ex){}
      }),
    );
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  constructor(private http: HttpClient) { }

  loadConfigFile(){
    this.http.get("assets/app.config.json").subscribe(
      (data:any) => {
        localStorage.setItem('apiUrl',data.apiUrl);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getApiUrl(){
    return localStorage.getItem("apiUrl") || "";
  }
}

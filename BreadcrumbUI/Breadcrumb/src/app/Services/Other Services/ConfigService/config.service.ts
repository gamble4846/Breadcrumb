import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  constructor(private http: HttpClient) { }

  loadEverything(){
    sessionStorage.setItem('openAPINumber', '0');
    this.loadApiConfigFile();
    this.loadOthersConfigFile();
  }

  loadApiConfigFile(){
    this.http.get("assets/api.config.json").subscribe(
      (data:any) => {
        localStorage.setItem('ApiConfig', JSON.stringify(data));
      },
      (error) => {
        console.log(error);
      }
    );
  }

  loadOthersConfigFile(){
    this.http.get("assets/others.config.json").subscribe(
      (data:any) => {
        localStorage.setItem('OthersConfig', JSON.stringify(data));
      },
      (error) => {
        console.log(error);
      }
    );
  }

  GetApiConfigFile(){
    return JSON.parse(localStorage.getItem('ApiConfig') || "{}");
  }

  GetOthersConfigFile(){
    return JSON.parse(localStorage.getItem('OthersConfig') || "{}");
  }
}

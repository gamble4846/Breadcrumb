import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CoreService {

  constructor() { }

  getToken(){
    return localStorage.getItem("token");
  }

  setToken(token:string){
    return localStorage.setItem(token, "token")
  }

  getMenus(){
    let links = [
      {
        "text": "SHOWS",
        "routerLink": "/",
      },
      {
        "text": "BOOKS",
        "routerLink": "/",
      },
      {
        "text": "FOLDERS & FILES",
        "routerLink": "/",
      },
      {
        "text": "SETTINGS",
        "routerLink": "/",
      },
    ]

    return links;
  }
}

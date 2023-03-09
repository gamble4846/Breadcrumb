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

  IsTokenPresent(){
    if(localStorage.getItem("token")){
      return true;
    }
    else{
      return false;
    }
  }

  getMenus(){
    let links = [
      {
        "text": "HOME",
        "routerLink": "/",
      },
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

  showLoader(){
    let openAPI:any = 0;
    let localOpenAPI = sessionStorage.getItem("openAPINumber");
    try{
      openAPI = parseInt(localOpenAPI || "0");
    }
    catch(ex){
      openAPI = 0;
    }

    openAPI++;
    sessionStorage.setItem("openAPINumber", openAPI);
    document.getElementById("FullPageCrystalLoader")?.classList.add("show");
  }

  hideLoader(){
    let openAPI:any = 0;
    let localOpenAPI = sessionStorage.getItem("openAPINumber");
    try{
      openAPI = parseInt(localOpenAPI || "0");
    }
    catch(ex){
      openAPI = 0;
    }

    openAPI--;
    sessionStorage.setItem("openAPINumber", openAPI + "");
    if(openAPI <= 0){
      sessionStorage.setItem("openAPINumber", "0");
      document.getElementById("FullPageCrystalLoader")?.classList.remove("show");
    }
  }
}

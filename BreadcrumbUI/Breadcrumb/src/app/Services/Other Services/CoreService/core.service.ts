import { Injectable } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd/message';
import { Observable } from 'rxjs';
import { TokenModel } from 'src/app/Models/AdminModels';
import { tbCoversModel } from 'src/app/Models/CoversModels';
import { RandomCovers } from 'src/app/Modules/ShowsModule/OpenerModels';
import { CoversService } from '../../API Services/CoversService/covers.service';
import { TokenService } from '../../API Services/TokenService/token.service';
import { DomSanitizer } from '@angular/platform-browser';
import { ConfigService } from '../ConfigService/config.service';

@Injectable({
  providedIn: 'root'
})
export class CoreService {

  constructor(
    private message: NzMessageService,
    private Covers: CoversService,
    private Token: TokenService,
    public sanitizer: DomSanitizer,
    private Config: ConfigService,
  ) { }

  getToken() {
    return localStorage.getItem("BreadcrumbToken");
  }

  setToken(token: string) {
    return localStorage.setItem("BreadcrumbToken", token)
  }

  IsTokenPresent() {
    if (localStorage.getItem("BreadcrumbToken")) {
      return true;
    }
    else {
      return false;
    }
  }

  getMenus() {
    let links = [
      {
        "text": "HOME",
        "routerLink": "/",
      },
      {
        "text": "SHOWS",
        "routerLink": ["/Shows"],
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
        "text": "BROWSE",
        "routerLink": "/",
      },
      {
        "text": "ADMIN",
        "routerLink": ["/Admin"],
      },
      {
        "text": "SETTINGS",
        "routerLink": ["/Admin/Settings"],
      },
    ]

    return links;
  }

  showLoader() {
    let openAPI: any = 0;
    let localOpenAPI = sessionStorage.getItem("openAPINumber");
    try {
      openAPI = parseInt(localOpenAPI || "0");
    }
    catch (ex) {
      openAPI = 0;
    }

    openAPI++;
    sessionStorage.setItem("openAPINumber", openAPI);
    document.getElementById("FullPageCrystalLoader")?.classList.add("show");
  }

  hideLoader() {
    let openAPI: any = 0;
    let localOpenAPI = sessionStorage.getItem("openAPINumber");
    try {
      openAPI = parseInt(localOpenAPI || "0");
    }
    catch (ex) {
      openAPI = 0;
    }

    openAPI--;
    sessionStorage.setItem("openAPINumber", openAPI + "");
    if (openAPI <= 0) {
      sessionStorage.setItem("openAPINumber", "0");
      document.getElementById("FullPageCrystalLoader")?.classList.remove("show");
    }
  }

  showMessage(type: string, message: string): void {
    this.message.create(type, message);
  }

  copyString(str: string) {
    navigator.clipboard.writeText(str);
  }

  getTokenData() {
    let Response = new Observable((observer: any) => {
      let realTokenDataSTR: string | null = localStorage.getItem("realTokenData");
      let realTokenData: TokenModel;

      if (!realTokenDataSTR) {
        this.Token.GetToken().subscribe((response: any) => {
          if (response.code == 1) {
            realTokenData = response.document;
          }
          else {
            realTokenData = JSON.parse(realTokenDataSTR || "{}");
          }
          observer.next(realTokenData);
          observer.complete();
        })
      }
      else {
        realTokenData = JSON.parse(realTokenDataSTR || "{}");
        observer.next(realTokenData);
        observer.complete();
      }
    });

    return Response;
  }

  GetAspectRatio(width:number, height:number) {
    var ratioValue = this.gcd(width, height);
    var ratioString = width / ratioValue + ":" + height / ratioValue;
    return ratioString;
  }

  gcd(a:number, b:number) : any {
    return (b == 0) ? a : this.gcd(b, a % b);
  }

  AreDimentionsSameAsAspectRatio(Dimensions: string, AspectRatio: string) {
    try{
      var AspectRaioFromDimentions = this.GetAspectRatio(parseInt(Dimensions.split("X")[0]), parseInt(Dimensions.split("X")[1]));
      return AspectRaioFromDimentions == AspectRatio;
    }
    catch{
      return false;
    }
  }

  getRandomCoversByBreadIdsAndOthers(BreadIds: Array<string>, AspectRatio: string) {
    let Response = new Observable((observer: any) => {
      let CoversData: Array<tbCoversModel> = [];
      let RandomCoversURLS: Array<RandomCovers> = [];

      this.Covers.GetCoverByBreadIds(BreadIds).subscribe((response: any) => {
        if (response.code == 1) {
          CoversData = response.document;
          this.getTokenData().subscribe((token: any) => {
            let TokenData: TokenModel = token;
            BreadIds.forEach((breadId: string) => {
              var currentCovers = CoversData.filter((x: tbCoversModel) => x.breadId == breadId && this.AreDimentionsSameAsAspectRatio(x.dimensions, AspectRatio));

              if (!TokenData.showNSFWCovers) {
                currentCovers = currentCovers.filter((x: tbCoversModel) => !x.isNSFW);
              }

              if(TokenData.showOnlyCustomCovers){
                let filteredCovers = currentCovers.filter((x: tbCoversModel) => !x.link.includes("[||REPLACEWITHTMDBIMAGEHOST||]"));

                if(filteredCovers.length > 0){
                  currentCovers = filteredCovers;
                }
              }

              if (currentCovers.length > 0) {
                var randomCover = currentCovers[Math.floor(Math.random() * currentCovers.length)];
                RandomCoversURLS.push({
                  breadId: breadId,
                  link: this.GetFinalLink(randomCover.link)
                });
              }
            });

            RandomCoversURLS.forEach((random: RandomCovers) => {
              if (!random.link) {
                random.link = "https://i.imgur.com/KDUg5q8.png";
              }
            });
          })
        }
        setTimeout(function () {
          console.log(RandomCoversURLS);
          observer.next(RandomCoversURLS);
          observer.complete();
        }, 500);
      })
    });
    return Response;
  }

  getSafeURL(url: string) {
    return this.sanitizer.bypassSecurityTrustResourceUrl(url);
  }

  getGoogleDrivePreviewLink(url: string) {
    var linkArr = url.split("/");
    var fileId = linkArr[5];
    var previewLink = `https://drive.google.com/file/d/${fileId}/preview`;
    return previewLink;
  }

  ObjectToString(objData: any) {
    return JSON.stringify(objData);
  }

  OpenLink(link: string, target: string = '_self') {
    window.open(link, target);
  }

  GetFinalLink(thumbailLink: string) {
    let finalThumbnailLink: string = "";

    if (!thumbailLink) {
      finalThumbnailLink = "https://i.imgur.com/5lq108M.png";
      return finalThumbnailLink;
    }

    finalThumbnailLink = thumbailLink;
    if (finalThumbnailLink.includes("[||REPLACEWITHTMDBIMAGEHOST||]")) {
      finalThumbnailLink = finalThumbnailLink.replaceAll("[||REPLACEWITHTMDBIMAGEHOST||]", this.Config.GetOthersConfigFile()["TMDBImageHost"]);
      return finalThumbnailLink;
    }

    return thumbailLink;
  }
}

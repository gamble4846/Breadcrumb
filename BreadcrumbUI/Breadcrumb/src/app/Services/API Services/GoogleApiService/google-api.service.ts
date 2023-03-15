import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConfigService } from '../../Other Services/ConfigService/config.service';

@Injectable({
  providedIn: 'root'
})
export class GoogleApiService {
  private ApiConfigFile:any;

  constructor(private Config:ConfigService, private http: HttpClient) {
    this.ApiConfigFile = this.Config.GetApiConfigFile();
  }

  GetFilesFromFolderId(FolderId:string){
    let apiLink = this.ApiConfigFile['MainAPI']+`api/GetFilesFromFolderId/${FolderId}`;
    return this.http.get(apiLink);
  }
}

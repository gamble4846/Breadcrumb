import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FinalFile } from 'src/app/Models/FoldersAndFileApiModel';
import { ConfigService } from '../../Other Services/ConfigService/config.service';

@Injectable({
  providedIn: 'root'
})
export class FilesService {
  private ApiConfigFile:any;

  constructor(private Config:ConfigService, private http: HttpClient) {
    this.ApiConfigFile = this.Config.GetApiConfigFile();
  }

  AddFinalFiles(finalFile:Array<FinalFile>){
    let apiLink = this.ApiConfigFile['MainAPI']+`api/Files/AddFinalFile`;
    return this.http.post(apiLink, finalFile);
  }

  GetNotAssignedFiles(){
    let apiLink = `https://localhost:44376/api/Files/NotAssignedFiles`;
    return this.http.get(apiLink);
  }

  GetAllFiles(){
    let apiLink = `https://localhost:44376/api/Files/All`;
    return this.http.get(apiLink);
  }
}

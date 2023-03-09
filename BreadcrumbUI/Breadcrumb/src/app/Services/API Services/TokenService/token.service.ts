import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TokenModel } from 'src/app/Models/AdminModels';
import { ConfigService } from '../../Other Services/ConfigService/config.service';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  private ApiConfigFile:any;

  constructor(private Config:ConfigService, private http: HttpClient) {
    this.ApiConfigFile = this.Config.GetApiConfigFile();
  }

  CreateToken(tokenData:TokenModel){
    let apiLink = this.ApiConfigFile['MainAPI']+"api/CreateToken";
    return this.http.post(apiLink,tokenData);
  }

  GetToken(){
    let apiLink = this.ApiConfigFile['MainAPI']+"api/GetToken";
    return this.http.get(apiLink);
  }
}

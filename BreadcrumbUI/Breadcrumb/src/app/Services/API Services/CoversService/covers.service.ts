import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConfigService } from '../../Other Services/ConfigService/config.service';
import { tbCoversModel } from 'src/app/Models/CoversModels';

@Injectable({
  providedIn: 'root'
})
export class CoversService {
  private ApiConfigFile: any;

  constructor(private Config: ConfigService, private http: HttpClient) {
    this.ApiConfigFile = this.Config.GetApiConfigFile();
  }

  GetCoverByBreadId(BreadId: string) {
    const headers = new HttpHeaders()
      .set('X-Skip-Error-Interceptor', 'X-Skip-Error-Interceptor');

    let apiLink = this.ApiConfigFile['MainAPI'] + `api/Cover/${BreadId}`;
    return this.http.get(apiLink, { headers });
  }

  GetCoverByBreadIds(BreadId: Array<string>){
    let apiLink = this.ApiConfigFile['MainAPI'] + `api/Cover/Get`;
    return this.http.post(apiLink,BreadId);
  }

  InsertUpdateDeleteCoversForSingleBread(CoverData: Array<tbCoversModel>){
    let apiLink = this.ApiConfigFile['MainAPI'] + `api/InsertUpdateDeleteCovers/SingleBread/Post`;
    // let apiLink = `https://localhost:44376/api/InsertUpdateDeleteCovers/SingleBread/Post`;
    return this.http.post(apiLink,CoverData);
  }
}

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConfigService } from '../../Other Services/ConfigService/config.service';

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
}

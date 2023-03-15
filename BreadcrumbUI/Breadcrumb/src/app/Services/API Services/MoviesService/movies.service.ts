import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { vMoviesViewModel } from 'src/app/Models/vMoviesModels';
import { ConfigService } from '../../Other Services/ConfigService/config.service';

@Injectable({
  providedIn: 'root'
})
export class MoviesService {
  private ApiConfigFile:any;

  constructor(private Config:ConfigService, private http: HttpClient) {
    this.ApiConfigFile = this.Config.GetApiConfigFile();
  }

  GetMovies(page:number = 1, itemsPerPage:number = 100, orderBy:string = "", FilterQuery:string = ""){
    let apiLink = this.ApiConfigFile['MainAPI']+`api/Movies/Get?page=${page}&itemsPerPage=${itemsPerPage}&orderBy=${orderBy}&FilterQuery=${FilterQuery}`;
    return this.http.get(apiLink);
  }

  GetMovieById(ShowId:string){
    let apiLink = this.ApiConfigFile['MainAPI']+`api/Movies/Get/${ShowId}`;
    return this.http.get(apiLink);
  }

  InsertMovie(ViewModel:vMoviesViewModel){
    let apiLink = this.ApiConfigFile['MainAPI']+`api/Movies/Insert`;
    return this.http.post(apiLink,ViewModel);
  }

  UpdateMovie(ViewModel:vMoviesViewModel, ShowId:string){
    let apiLink = this.ApiConfigFile['MainAPI']+`api/Movies/Update/${ShowId}`;
    return this.http.post(apiLink,ViewModel);
  }

  InsertUpdateFullMovie(IMDBId:string){
    let apiLink = this.ApiConfigFile['MainAPI']+`api/Movies/Full/InsertUpdate/${IMDBId}`;
    return this.http.post(apiLink, {});
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tbEpisodesViewModel } from 'src/app/Models/tbEpisodesModels';
import { tbSeasonsViewModel } from 'src/app/Models/tbSeasonsModels';
import { vTvShowsViewModel } from 'src/app/Models/vTvShowsModels';
import { ConfigService } from '../../Other Services/ConfigService/config.service';
import { tbShowsFile } from 'src/app/Models/tbShowsFile';

@Injectable({
  providedIn: 'root'
})
export class TvShowsService {
  private ApiConfigFile:any;

  constructor(private Config:ConfigService, private http: HttpClient) {
    this.ApiConfigFile = this.Config.GetApiConfigFile();
  }

  GetTvshow(page:number = 1, itemsPerPage:number = 100, orderBy:string = "", FilterQuery:string = ""){
    let apiLink = this.ApiConfigFile['MainAPI']+`api/TvShows/Get?page=${page}&itemsPerPage=${itemsPerPage}&orderBy=${orderBy}&FilterQuery=${FilterQuery}`;
    return this.http.get(apiLink);
  }

  GetTvshowById(ShowId:string){
    let apiLink = this.ApiConfigFile['MainAPI']+`api/TvShows/Get/${ShowId}`;
    return this.http.get(apiLink);
  }

  InsertTvshow(ViewModel:vTvShowsViewModel){
    let apiLink = this.ApiConfigFile['MainAPI']+`api/TvShows/Insert`;
    return this.http.post(apiLink,ViewModel);
  }

  UpdateTvshow(ViewModel:vTvShowsViewModel, ShowId:string){
    let apiLink = this.ApiConfigFile['MainAPI']+`api/TvShows/Update/${ShowId}`;
    return this.http.post(apiLink,ViewModel);
  }

  DeleteTvshow(ShowId:string){
    let apiLink = this.ApiConfigFile['MainAPI']+`api/TvShows/Delete/${ShowId}`;
    return this.http.delete(apiLink);
  }

  GetTvShowSeasons(ShowId:string){
    let apiLink = this.ApiConfigFile['MainAPI']+`api/TvShows/Seasons/Get/${ShowId}`;
    return this.http.get(apiLink);
  }

  InsertTvShowSeason(ViewModel:tbSeasonsViewModel){
    let apiLink = this.ApiConfigFile['MainAPI']+`api/TvShows/Insert`;
    return this.http.post(apiLink,ViewModel);
  }

  InsertUpdateTvShowSeasonMultiple(ViewModelList:Array<tbSeasonsViewModel>){
    let apiLink = this.ApiConfigFile['MainAPI']+`api/TvShows/Seasons/InsertMultiple`;
    return this.http.post(apiLink,ViewModelList);
  }

  UpdateTvShowSeasons(ViewModel:tbSeasonsViewModel, SeasonId:string){
    let apiLink = this.ApiConfigFile['MainAPI']+`api/TvShows/Seasons/Update/${SeasonId}`;
    return this.http.post(apiLink,ViewModel);
  }

  DeleteTvShowSeasons(SeasonId:string){
    let apiLink = this.ApiConfigFile['MainAPI']+`api/TvShows/Seasons/Delete/${SeasonId}`;
    return this.http.delete(apiLink);
  }

  GetTvShowEpisodes(SeasonId:string){
    let apiLink = this.ApiConfigFile['MainAPI']+`api/TvShows/Episodes/Get/${SeasonId}`;
    return this.http.get(apiLink);
  }

  GetTvShowEpisodesWithFiles(SeasonId:string){
    let apiLink = `https://localhost:44376/api/TvShows/EpisodesWithFiles/Get/${SeasonId}`;
    return this.http.get(apiLink);
  }

  InsertTvShowEpisodes(ViewModel:tbEpisodesViewModel){
    let apiLink = this.ApiConfigFile['MainAPI']+`api/TvShows/Episodes/Insert`;
    return this.http.post(apiLink,ViewModel);
  }

  InsertUpdateTvShowEpisodesMultiple(ViewModelList:Array<tbEpisodesViewModel>){
    let apiLink = this.ApiConfigFile['MainAPI']+`api/TvShows/Episodes/InsertMultiple`;
    return this.http.post(apiLink,ViewModelList);
  }

  UpdateTvShowEpisodes(ViewModel:tbEpisodesViewModel, EpisodeId:string){
    let apiLink = this.ApiConfigFile['MainAPI']+`api/TvShows/Episodes/Update/${EpisodeId}`;
    return this.http.post(apiLink,ViewModel);
  }

  DeleteTvShowEpisodes(EpisodeId:string){
    let apiLink = this.ApiConfigFile['MainAPI']+`api/TvShows/Episodes/Delete/${EpisodeId}`;
    return this.http.delete(apiLink);
  }

  InsertUpdateFullTvShow(IMDBId:string){
    let apiLink = this.ApiConfigFile['MainAPI']+`api/TvShows/Full/InsertUpdate/${IMDBId}`;
    return this.http.post(apiLink, {});
  }

  GetAllTvshows(){
    let apiLink = `https://localhost:44376/api/TvShows/GetAll`;
    return this.http.get(apiLink);
  }

  InsertUpdateEpisodesFiles(model:Array<tbShowsFile>){
    let apiLink = `https://localhost:44376/api/TvShows/EpisodesFiles/InsertUpdate`;
    return this.http.post(apiLink, model);
  }
}

import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { tbEpisodesModel } from 'src/app/Models/tbEpisodesModels';
import { tbSeasonsModel } from 'src/app/Models/tbSeasonsModels';
import { vTvShowsModel } from 'src/app/Models/vTvShowsModels';
import { CoversService } from 'src/app/Services/API Services/CoversService/covers.service';
import { TvShowsService } from 'src/app/Services/API Services/TvShowsService/tv-shows.service';
import { ConfigService } from 'src/app/Services/Other Services/ConfigService/config.service';
import { CoreService } from 'src/app/Services/Other Services/CoreService/core.service';
import { RandomCovers } from '../../OpenerModels';

@Component({
  selector: 'app-tv-show',
  templateUrl: './tv-show.component.html',
  styleUrls: ['./tv-show.component.css']
})
export class TvShowComponent {
  
  ParamShowID:string = "";
  TvShowData:vTvShowsModel = {
    breadId: null,
    showId: null,
    primaryName: '',
    otherNames: '',
    description: '',
    type: '',
    isStared: false,
    iMDBID: '',
    releaseYear: '',
    genres: '',
    genreGUIDs: ''
  };
  RandomCoversURLS:Array<RandomCovers> = [];
  CoverLink:string = "";
  TvShowSeasonsData:Array<tbSeasonsModel> = [];
  SelectedSeason: tbSeasonsModel = {
    id: null,
    showId: null,
    number: 0,
    name: '',
    description: ''
  };
  SelectedSeasonID:string = "";
  TvShowEpisodesData:Array<tbEpisodesModel> = [];
  
  constructor(
    private Core:CoreService,
    private TvShow:TvShowsService,
    private Covers:CoversService,
    private Config:ConfigService,
    private router: Router,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this.ParamShowID = this.route.snapshot.paramMap.get('ShowId') || "";

    if(!this.ParamShowID){
      this.router.navigate(['/Shows'])
    }

    this.GetTvShowData();
  }

  GetTvShowData(){
    this.TvShow.GetTvshowById(this.ParamShowID).subscribe((response:any) => {
      if(response.code == 1){
        this.TvShowData = response.document;
        this.GetTvShowsCovers();
        this.UpdateTvShowSeasonsData();
      }
    })
  }

  GetTvShowsCovers(){
    let ids = [];
    ids.push(this.TvShowData.breadId || "");
    this.Core.getRandomCoversByBreadIdsAndOthers(ids,"1920X1080").subscribe((response:any) => {
      this.RandomCoversURLS = response;
      try{
        this.CoverLink = this.RandomCoversURLS[0].link;
      }
      catch(ex){}
      if(!this.CoverLink){
        this.CoverLink = "https://i.imgur.com/5lq108M.png";
      }
    });
  }

  UpdateTvShowSeasonsData(){
    this.TvShow.GetTvShowSeasons(this.TvShowData.showId || "").subscribe((response:any) => {
      if(response.code == 1){
        this.TvShowSeasonsData = response.document;
        this.TvShowSeasonsData.sort((a:tbSeasonsModel,b:tbSeasonsModel) => (a.number > b.number) ? 1 : ((b.number > a.number) ? -1 : 0));
        this.SelectedSeasonID = this.TvShowSeasonsData[0].id || "";
        this.TvShowSeasonUpdated();
      }
    })
  }

  TvShowSeasonUpdated(){
    this.SelectedSeason = this.TvShowSeasonsData.find((x:tbSeasonsModel) => x.id == this.SelectedSeasonID) || this.SelectedSeason;
    this.UpdateTvShowEpisodesData();
  }


  UpdateTvShowEpisodesData(){
    this.TvShow.GetTvShowEpisodes(this.SelectedSeason.id || "").subscribe((response:any) => {
      if(response.code == 1){
        this.TvShowEpisodesData = response.document;
        console.log(this.TvShowEpisodesData);
      }
    })
  }

  GetThumbnailForEpisode(episode:tbEpisodesModel){
    let finalThumbnailLink:string = "";

    if(!episode.thumbnailLink){
      finalThumbnailLink = "https://i.imgur.com/5lq108M.png";
      return finalThumbnailLink;
    }

    finalThumbnailLink = episode.thumbnailLink;
    if(finalThumbnailLink.includes("[||REPLACEWITHTMDBIMAGEHOST||]")){
      finalThumbnailLink = finalThumbnailLink.replaceAll("[||REPLACEWITHTMDBIMAGEHOST||]", this.Config.GetOthersConfigFile()["TMDBImageHost"]);
      return finalThumbnailLink;
    }

    return episode.thumbnailLink;
  }
}

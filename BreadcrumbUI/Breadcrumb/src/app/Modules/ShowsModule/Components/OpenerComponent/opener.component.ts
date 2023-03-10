import { Component } from '@angular/core';
import { tbCoversModel } from 'src/app/Models/CoversModels';
import { vTvShowsModel } from 'src/app/Models/vTvShowsModels';
import { CoversService } from 'src/app/Services/API Services/CoversService/covers.service';
import { TvShowsService } from 'src/app/Services/API Services/TvShowsService/tv-shows.service';
import { CoreService } from 'src/app/Services/Other Services/CoreService/core.service';
import { RandomCovers } from '../../OpenerModels';

@Component({
  selector: 'app-opener',
  templateUrl: './opener.component.html',
  styleUrls: ['./opener.component.css']
})
export class OpenerComponent {
  constructor(
    private Core:CoreService,
    private TvShow:TvShowsService,
    private Covers:CoversService,
  ) { }

  ngOnInit(): void {
    this.UpdateTvShows();
  }

  TvShowCurrentPage:number = 1;
  TvShowPageSize:number = 20;
  TvShowOrderBy:string = "";
  TvShowFilterQuery:string = "";
  TvShowTotalRecords:number = 0;
  TvShowsData:Array<vTvShowsModel> = [];

  UpdateTvShows(){
    this.TvShow.GetTvshow(this.TvShowCurrentPage,this.TvShowPageSize,this.TvShowOrderBy,this.TvShowFilterQuery).subscribe((response:any) => {
      if(response.code == 1){
        this.TvShowsData = response.document.records;
        this.TvShowCurrentPage = response.document.pageNumber;
        this.TvShowPageSize = response.document.pageSize;
        this.TvShowTotalRecords = response.document.totalRecords;

        this.UpdateCoversForAll();
      }
    })
  }


  CoversData:Array<tbCoversModel> = [];
  RandomCoversURLS:Array<RandomCovers> = [];

  UpdateCoversForAll(){
    this.TvShowsData.forEach((tvShow:vTvShowsModel) => {
      this.Covers.GetCoverByBreadId(tvShow.breadId || "").subscribe((response:any) => {
        if(response.code == 1){
          this.CoversData = this.CoversData.concat(response.document);
          this.UpdateRandomCoverURL();
        }
      })
    });
  }

  UpdateRandomCoverURL(){
    this.TvShowsData.forEach((tvShow:vTvShowsModel) => {
      var currentCovers = this.CoversData.filter((x:tbCoversModel) => x.breadId == tvShow.breadId);
      if(currentCovers.length > 0){
        var randomCover = currentCovers[Math.floor(Math.random()*currentCovers.length)];
        console.log(currentCovers,randomCover);
        this.RandomCoversURLS.push({
          breadId: tvShow.breadId || "",
          link: randomCover.link
        });
      }
    });
  }

  GetRandomURLForBread(breadId:string){
    let random:any = this.RandomCoversURLS.find((x:RandomCovers) => x.breadId == breadId) || null;
    if(random != null){
      return random.link;
    }
    else{
      return "";
    }
  }
}

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
  radioValue:string = "TvShows";

  constructor(
    private Core:CoreService,
    private TvShow:TvShowsService,
    private Covers:CoversService,
  ) { }

  ngOnInit(): void {
    this.UpdateAccordingToRadio();
  }

  UpdateAccordingToRadio(){
    switch(this.radioValue){
      case "TvShows":
        this.UpdateTvShows();
        break;
      case "Movies":
        break;
    }
  }

  TvShowCurrentPage:number = 1;
  TvShowPageSize:number = 20;
  TvShowOrderBy:string = "";
  TvShowFilterQuery:string = "";
  TvShowTotalRecords:number = 0;
  TvShowsData:Array<vTvShowsModel> = [];

  PageChangedTvShow(data:any){
    this.TvShowCurrentPage = data;
    this.UpdateTvShows();
  }

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

  RandomCoversURLS:Array<RandomCovers> = [];

  UpdateCoversForAll(){
    var ids = this.TvShowsData.map(({breadId}) => breadId || "");
    this.Core.getRandomCoversByBreadIdsAndOthers(ids,"1000X1500").subscribe((response:any) => {
      this.RandomCoversURLS = response;
    });
  }

  GetRandomURLForBread(breadId:string){
    let random:any = this.RandomCoversURLS.find((x:RandomCovers) => x.breadId == breadId) || null;
    if(random){
      return random.link;
    }
    else{
      return "https://i.imgur.com/KDUg5q8.png";
    }
  }
}

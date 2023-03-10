import { Component } from '@angular/core';
import { vTvShowsModel } from 'src/app/Models/vTvShowsModels';
import { TvShowsService } from 'src/app/Services/API Services/TvShowsService/tv-shows.service';
import { CoreService } from 'src/app/Services/Other Services/CoreService/core.service';

@Component({
  selector: 'app-opener',
  templateUrl: './opener.component.html',
  styleUrls: ['./opener.component.css']
})
export class OpenerComponent {
  constructor(
    private Core:CoreService,
    private TvShow:TvShowsService
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
        console.log(response);
        this.TvShowsData = response.document.records;
        this.TvShowCurrentPage = response.document.pageNumber;
        this.TvShowPageSize = response.document.pageSize;
        this.TvShowTotalRecords = response.document.totalRecords;
      }
    })
  }
}

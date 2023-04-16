import { Component } from '@angular/core';
import { TvShowsService } from 'src/app/Services/API Services/TvShowsService/tv-shows.service';
import { CoreService } from 'src/app/Services/Other Services/CoreService/core.service';

@Component({
  selector: 'app-insert-update-full-tv-show',
  templateUrl: './insert-update-full-tv-show.component.html',
  styleUrls: ['./insert-update-full-tv-show.component.css']
})
export class InsertUpdateFullTvShowComponent {
  ImdbIdInputValue:string = '';
  responseObj:any = {};

  constructor(
    private _TvShows: TvShowsService,
    private _Core: CoreService,
  ) { }

  ngOnInit(): void {

  }

  SaveFullTvShow(){
    this._TvShows.InsertUpdateFullTvShow(this.ImdbIdInputValue).subscribe((response:any) => {
      if(response.code == 1){
        this._Core.showMessage('success', 'TvShow Added/Updated');
        this.responseObj = response;
      }
    })
  }
}

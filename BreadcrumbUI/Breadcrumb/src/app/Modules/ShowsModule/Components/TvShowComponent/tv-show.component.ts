import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { vTvShowsModel } from 'src/app/Models/vTvShowsModels';
import { CoversService } from 'src/app/Services/API Services/CoversService/covers.service';
import { TvShowsService } from 'src/app/Services/API Services/TvShowsService/tv-shows.service';
import { CoreService } from 'src/app/Services/Other Services/CoreService/core.service';

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

  constructor(
    private Core:CoreService,
    private TvShow:TvShowsService,
    private Covers:CoversService,
    private router: Router,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this.ParamShowID = this.route.snapshot.paramMap.get('RegNumber') || "";

    if(!this.ParamShowID){
      this.router.navigate(['/Shows'])
    }

    this.GetTvShowData();
  }

  GetTvShowData(){
    this.TvShow.GetTvshowById(this.ParamShowID).subscribe((response:any) => {
      if(response.code == 1){
        this.TvShowData = response.document;
      }
    })
  }
}

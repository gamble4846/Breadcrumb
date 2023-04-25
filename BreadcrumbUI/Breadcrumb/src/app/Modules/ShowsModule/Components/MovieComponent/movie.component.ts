import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { vMoviesModel } from 'src/app/Models/vMoviesModels';
import { CoversService } from 'src/app/Services/API Services/CoversService/covers.service';
import { MoviesService } from 'src/app/Services/API Services/MoviesService/movies.service';
import { ConfigService } from 'src/app/Services/Other Services/ConfigService/config.service';
import { CoreService } from 'src/app/Services/Other Services/CoreService/core.service';
import { RandomCovers } from '../../OpenerModels';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css']
})
export class MovieComponent {
  ParamShowID:string = "";
  MovieData:vMoviesModel = {
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

  
  constructor(
    private Core:CoreService,
    private Movie:MoviesService,
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

    this.GetMoiveData();
  }

  GetMoiveData(){
    this.Movie.GetMovieById(this.ParamShowID).subscribe((response:any) => {
      if(response.code == 1){
        this.MovieData = response.document;
        this.GetMoviesCovers();
      }
    })
  }

  GetMoviesCovers(){
    let ids = [];
    ids.push(this.MovieData.breadId || "");
    this.Core.getRandomCoversByBreadIdsAndOthers(ids,"16:9").subscribe((response:any) => {
      this.RandomCoversURLS = response;
      try{
        this.CoverLink = this.Core.GetFinalLink(this.RandomCoversURLS[0].link);
      }
      catch(ex){}
      if(!this.CoverLink){
        this.CoverLink = "https://i.imgur.com/5lq108M.png";
      }
    });
  }
}

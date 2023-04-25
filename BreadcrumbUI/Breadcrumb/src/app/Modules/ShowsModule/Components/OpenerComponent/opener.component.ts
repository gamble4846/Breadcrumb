import { Component } from '@angular/core';
import { tbCoversModel } from 'src/app/Models/CoversModels';
import { vMoviesModel } from 'src/app/Models/vMoviesModels';
import { vTvShowsModel } from 'src/app/Models/vTvShowsModels';
import { CoversService } from 'src/app/Services/API Services/CoversService/covers.service';
import { MoviesService } from 'src/app/Services/API Services/MoviesService/movies.service';
import { TvShowsService } from 'src/app/Services/API Services/TvShowsService/tv-shows.service';
import { CoreService } from 'src/app/Services/Other Services/CoreService/core.service';
import { RandomCovers } from '../../OpenerModels';

@Component({
  selector: 'app-opener',
  templateUrl: './opener.component.html',
  styleUrls: ['./opener.component.css']
})
export class OpenerComponent {
  radioValue: string = "TvShows";

  constructor(
    public Core: CoreService,
    private TvShow: TvShowsService,
    private Moive: MoviesService,
    private Covers: CoversService,
  ) { }

  ngOnInit(): void {
    this.UpdateAccordingToRadio();
  }

  UpdateAccordingToRadio() {
    switch (this.radioValue) {
      case "TvShows":
        this.UpdateTvShows();
        break;
      case "Movies":
        this.UpdateMoives();
        break;
    }
  }

  TvShowCurrentPage: number = 1;
  TvShowPageSize: number = 20;
  TvShowOrderBy: string = "";
  TvShowFilterQuery: string = "";
  TvShowTotalRecords: number = 0;
  TvShowsData: Array<vTvShowsModel> = [];

  PageChangedTvShow(data: any) {
    this.TvShowCurrentPage = data;
    this.UpdateTvShows();
  }

  UpdateTvShows() {
    this.TvShow.GetTvshow(this.TvShowCurrentPage, this.TvShowPageSize, this.TvShowOrderBy, this.TvShowFilterQuery).subscribe((response: any) => {
      if (response.code == 1) {
        this.TvShowsData = response.document.records;
        this.TvShowCurrentPage = response.document.pageNumber;
        this.TvShowPageSize = response.document.pageSize;
        this.TvShowTotalRecords = response.document.totalRecords;
        this.UpdateCoversForAll();
      }
    })
  }

  RandomCoversURLS: Array<RandomCovers> = [];

  UpdateCoversForAll() {
    switch (this.radioValue) {
      case "TvShows":
        var ids = this.TvShowsData.map(({ breadId }) => breadId || "");
        this.Core.getRandomCoversByBreadIdsAndOthers(ids, "2:3").subscribe((response: any) => {
          this.RandomCoversURLS = response;
        });
        break;
      case "Movies":
        var ids = this.MoviesData.map(({ breadId }) => breadId || "");
        this.Core.getRandomCoversByBreadIdsAndOthers(ids, "2:3").subscribe((response: any) => {
          this.RandomCoversURLS = response;
        });
        break;
    }
  }

  GetRandomURLForBread(breadId: string) {
    let random: any = this.RandomCoversURLS.find((x: RandomCovers) => x.breadId == breadId) || null;
    if (random) {
      return random.link;
    }
    else {
      return "https://i.imgur.com/KDUg5q8.png";
    }
  }

  MovieCurrentPage: number = 1;
  MoviePageSize: number = 20;
  MovieOrderBy: string = "";
  MovieFilterQuery: string = "";
  MovieTotalRecords: number = 0;
  MoviesData: Array<vMoviesModel> = [];

  PageChangedMovie(data: any) {
    this.MovieCurrentPage = data;
    this.UpdateMoives();
  }

  UpdateMoives() {
    this.Moive.GetMovies(this.MovieCurrentPage, this.MoviePageSize, this.MovieOrderBy, this.MovieFilterQuery).subscribe((response: any) => {
      if (response.code == 1) {
        this.MoviesData = response.document.records;
        this.MovieCurrentPage = response.document.pageNumber;
        this.MoviePageSize = response.document.pageSize;
        this.MovieTotalRecords = response.document.totalRecords;
        this.UpdateCoversForAll();
      }
    })
  }
}

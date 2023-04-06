import { Component, Input } from '@angular/core';
import { CoversService } from 'src/app/Services/API Services/CoversService/covers.service';
import { MoviesService } from 'src/app/Services/API Services/MoviesService/movies.service';
import { TvShowsService } from 'src/app/Services/API Services/TvShowsService/tv-shows.service';
import { CoreService } from 'src/app/Services/Other Services/CoreService/core.service';
import { FilesViewerModel } from '../Models/FilesViewerModel';

@Component({
  selector: 'files-viewer',
  templateUrl: './files-viewer.component.html',
  styleUrls: ['./files-viewer.component.css']
})
export class FilesViewerComponent {

  @Input() FilesViewerData:FilesViewerModel | undefined;

  constructor(
    private Core: CoreService,
    private TvShow: TvShowsService,
    private Moive: MoviesService,
    private Covers: CoversService,
  ) { }

  ngOnInit(): void {
    if(this.FilesViewerData){
      switch(this.FilesViewerData.FileToGetFrom){
        case "Episode":
          break;
        default:
          break;
      }
    }
  }
}

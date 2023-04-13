import { Component, Input } from '@angular/core';
import { CoversService } from 'src/app/Services/API Services/CoversService/covers.service';
import { MoviesService } from 'src/app/Services/API Services/MoviesService/movies.service';
import { TvShowsService } from 'src/app/Services/API Services/TvShowsService/tv-shows.service';
import { CoreService } from 'src/app/Services/Other Services/CoreService/core.service';
import { FilesViewerModel } from '../Models/FilesViewerModel';
import { FilesService } from 'src/app/Services/API Services/FilesService/files.service';
import { FullShowFilesWithChunksModel } from 'src/app/Models/FullShowFilesWithChunksModel';
import { AllFilesModel } from '../Models/AllFilesModel';

@Component({
  selector: 'files-viewer',
  templateUrl: './files-viewer.component.html',
  styleUrls: ['./files-viewer.component.css']
})
export class FilesViewerComponent {

  @Input() FilesViewerData:FilesViewerModel = {
    FileToGetFrom: '',
    PrimaryId: ''
  };

  AllFiles:Array<AllFilesModel> = [];
  isFileInfoOpen:boolean = false;
  
  constructor(
    private Core: CoreService,
    private TvShow: TvShowsService,
    private Moive: MoviesService,
    private Covers: CoversService,
    private _Files: FilesService,
  ) { }

  ngOnInit(): void {
    if(this.FilesViewerData){
      switch(this.FilesViewerData.FileToGetFrom){
        case "Episode":
          console.log(this.FilesViewerData.PrimaryId);
          this.UpdateFilesByEpisodeID(this.FilesViewerData.PrimaryId);
          break;
        default:
          break;
      }
    }
  }


  FilesDataWithChunks:Array<FullShowFilesWithChunksModel> = [];

  UpdateFilesByEpisodeID(EpisodeId:string){
    this._Files.FilesWithChunksFromEpisodeId(EpisodeId || "").subscribe((response:any) => {
      if(response.code == 1){
        this.FilesDataWithChunks = response.document;
      }

      this.FilesDataWithChunks.forEach((file:FullShowFilesWithChunksModel,index:number) => {
        var AllFile:AllFilesModel = {
          showFileId: file.showFileId,
          fileId: file.fileId,
          description: file.description,
          type: file.type,
          name: file.name,
          thumbnailLink: file.thumbnailLink,
          showId: file.showId,
          seasonId: file.seasonId,
          episodeId: file.episodeId,
          quality: file.quality,
          audioLanguages: file.audioLanguages,
          subtitleLanguages: file.subtitleLanguages,
          chunks: file.chunks,
          ALLFileModelID: (new Date()).getTime().toString() + "_" + index,
        };
        this.AllFiles.push(AllFile);
      });

      this.FileSelected(this.AllFiles[0].ALLFileModelID);
    })
  }

  SelectedFile:AllFilesModel = {
    ALLFileModelID: '',
    showFileId: null,
    fileId: null,
    description: null,
    type: null,
    name: null,
    thumbnailLink: null,
    showId: null,
    seasonId: null,
    episodeId: null,
    quality: null,
    audioLanguages: null,
    subtitleLanguages: null,
    chunks: null
  };

  FileSelected(ALLFileModelID:string){
    this.SelectedFile = this.AllFiles.find((x:AllFilesModel) => x.ALLFileModelID == ALLFileModelID) || this.SelectedFile;
  }
}

import { Component } from '@angular/core';
import { FilesApi } from 'src/app/Models/FoldersAndFileApiModel';
import { EpsiodesDataWithFilesModel, tbEpisodesModel } from 'src/app/Models/tbEpisodesModels';
import { tbSeasonsModel } from 'src/app/Models/tbSeasonsModels';
import { vTvShowsModel } from 'src/app/Models/vTvShowsModels';
import { TokenService } from 'src/app/Services/API Services/TokenService/token.service';
import { TvShowsService } from 'src/app/Services/API Services/TvShowsService/tv-shows.service';
import { CoreService } from 'src/app/Services/Other Services/CoreService/core.service';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { FilesWithShowToInsertModel, tbFilesDataModel } from 'src/app/Models/tbFilesDataModel';
import { FilesService } from 'src/app/Services/API Services/FilesService/files.service';

@Component({
  selector: 'app-assign-files-show-seasons',
  templateUrl: './assign-files-show-seasons.component.html',
  styleUrls: ['./assign-files-show-seasons.component.css']
})
export class AssignFilesShowSeasonsComponent {
  constructor(
    private Core: CoreService,
    private TvShows: TvShowsService,
    private Files: FilesService
  ) { }

  ngOnInit(): void {
    this.UpdateTvShows();
  }

  ShowOnlyUnAssignedFiles: boolean = true;

  drop(event: CdkDragDrop<FilesWithShowToInsertModel[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex,
      );
    }
  }

  //#region TvShows
  SelectedTvShowId: string = '';
  TvShowsData: Array<vTvShowsModel> = [];

  UpdateTvShows() {
    this.TvShows.GetAllTvshows().subscribe((response: any) => {
      if (response.code == 1) {
        this.TvShowsData = response.document;
        console.log(this.TvShowsData);
        this.SelectedTvShowId = this.TvShowsData[0].showId ?? "";
        this.TvShowChanged();
      }
    })
  }

  TvShowChanged() {
    this.UpdateTvShowSeasons();
  }
  //#endregion

  //#region Seasons
  SelectedSeasonId: string = '';
  SeasonsData: Array<tbSeasonsModel> = [];

  UpdateTvShowSeasons() {
    this.TvShows.GetTvShowSeasons(this.SelectedTvShowId).subscribe((response: any) => {
      if (response.code == 1) {
        this.SeasonsData = response.document;
        console.log(this.SeasonsData);
        this.SelectedSeasonId = this.SeasonsData[0].id ?? "";
        this.SeasonChanged();
      }
    })
  }

  SeasonChanged() {
    this.UpdateTvshowEpisodes();
  }
  //#endregion

  //#region Episodes
  EpisodesData: Array<tbEpisodesModel> = [];
  EpsiodesDataWithFiles: Array<EpsiodesDataWithFilesModel> = [];

  UpdateTvshowEpisodes() {
    console.log(this.SelectedSeasonId);
    this.TvShows.GetTvShowEpisodes(this.SelectedSeasonId).subscribe((response: any) => {
      if (response.code == 1) {
        this.EpisodesData = response.document;
        console.log(this.EpisodesData);
        this.SetupEpisodesForFileAdding();
        this.UpdateFiles();
      }
    })
  }

  SetupEpisodesForFileAdding() {
    this.EpsiodesDataWithFiles = [];
    this.EpisodesData.forEach((episode: any) => {
      let epsiodesDataWithFiles: EpsiodesDataWithFilesModel = {
        id: episode.id,
        number: episode.number,
        name: episode.name,
        files: []
      };
      this.EpsiodesDataWithFiles.push(epsiodesDataWithFiles);
    });

    console.log(this.EpsiodesDataWithFiles);
  }
  //#endregion

  //#region Files
  FilesData: Array<tbFilesDataModel> = [];
  FilesWithShowToInsert: Array<FilesWithShowToInsertModel> = [];

  UpdateFiles() {
    if (this.ShowOnlyUnAssignedFiles) {
      this.Files.GetNotAssignedFiles().subscribe((response: any) => {
        if (response.code == 1) {
          this.FilesData = response.document;
          this.ConvertFilesForShowInsert();
        }
      })
    }
    else {
      this.Files.GetAllFiles().subscribe((response: any) => {
        if (response.code == 1) {
          this.FilesData = response.document;
          this.ConvertFilesForShowInsert();
        }
      })
    }

  }

  ConvertFilesForShowInsert() {
    this.FilesWithShowToInsert = [];
    this.FilesData.forEach((file: tbFilesDataModel) => {
      this.FilesWithShowToInsert.push({
        id: file.id,
        description: file.description,
        type: file.type,
        name: file.name,
        thumbnailLink: file.thumbnailLink,
        quality: '',
        audioLanguages: '',
        subtitleLanguages: ''
      });
    })
  }

  DeleteFileFromEpisode(FileIndex: number, EpisodeIndex: number) {
    this.FilesWithShowToInsert.unshift(this.EpsiodesDataWithFiles[EpisodeIndex].files[FileIndex]);
    this.EpsiodesDataWithFiles[EpisodeIndex].files.splice(FileIndex, 1);
  }
  //#endregion

  EditFileForEpisodeData = {
    Quality: "",
    AudioLanguages: "",
    SubtitleLanguages: "",
  }
  EditFileForEpisodeModleVisible: boolean = false;
  CurrentlyEditingFileForEpisode_EpsiodeIndex: number | null = null;
  CurrentlyEditingFileForEpisode_FileIndex: number | null = null;

  OpenModalEditFileForEpisode(FileIndex: number | null, EpsiodeIndex: number | null) {
    this.CurrentlyEditingFileForEpisode_EpsiodeIndex = EpsiodeIndex;
    this.CurrentlyEditingFileForEpisode_FileIndex = FileIndex;

    if (this.CurrentlyEditingFileForEpisode_EpsiodeIndex != null && this.CurrentlyEditingFileForEpisode_FileIndex != null) {
      const CURRENTFILE = this.EpsiodesDataWithFiles[this.CurrentlyEditingFileForEpisode_EpsiodeIndex].files[this.CurrentlyEditingFileForEpisode_FileIndex];
      this.EditFileForEpisodeData = {
        Quality: CURRENTFILE.quality,
        AudioLanguages: CURRENTFILE.audioLanguages,
        SubtitleLanguages: CURRENTFILE.subtitleLanguages,
      }
    }
    else {
      this.EditFileForEpisodeData = {
        Quality: "",
        AudioLanguages: "",
        SubtitleLanguages: "",
      }
    }

    this.EditFileForEpisodeModleVisible = true;
  }

  handleEditFileForEpisodeModalCancel() {
    this.EditFileForEpisodeModleVisible = false;
  }

  handleEditFileForEpisodeModalOk() {
    if (this.CurrentlyEditingFileForEpisode_EpsiodeIndex != null && this.CurrentlyEditingFileForEpisode_FileIndex != null) {
      const CURRENTFILE = this.EpsiodesDataWithFiles[this.CurrentlyEditingFileForEpisode_EpsiodeIndex].files[this.CurrentlyEditingFileForEpisode_FileIndex];
      CURRENTFILE.quality = this.EditFileForEpisodeData.Quality;
      CURRENTFILE.audioLanguages = this.EditFileForEpisodeData.AudioLanguages;
      CURRENTFILE.subtitleLanguages = this.EditFileForEpisodeData.SubtitleLanguages;
    }
    else {
      this.EpsiodesDataWithFiles.forEach((EpisodesData: EpsiodesDataWithFilesModel) => {
        EpisodesData.files.forEach((files: FilesWithShowToInsertModel) => {
          files.quality = this.EditFileForEpisodeData.Quality;
          files.audioLanguages = this.EditFileForEpisodeData.AudioLanguages;
          files.subtitleLanguages = this.EditFileForEpisodeData.SubtitleLanguages;
        });
      });

      this.FilesWithShowToInsert.forEach((FWI: FilesWithShowToInsertModel) => {
        FWI.quality = this.EditFileForEpisodeData.Quality;
        FWI.audioLanguages = this.EditFileForEpisodeData.AudioLanguages;
        FWI.subtitleLanguages = this.EditFileForEpisodeData.SubtitleLanguages;
      });
    }
    this.EditFileForEpisodeModleVisible = false;
  }
}

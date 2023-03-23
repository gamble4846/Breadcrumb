import { Component } from '@angular/core';
import { FilesApi, FinalFile, FolderAPI } from 'src/app/Models/FoldersAndFileApiModel';
import { GoogleApiService } from 'src/app/Services/API Services/GoogleApiService/google-api.service';
import { TokenService } from 'src/app/Services/API Services/TokenService/token.service';
import { CoreService } from 'src/app/Services/Other Services/CoreService/core.service';
import { FilesService } from 'src/app/Services/API Services/FilesService/files.service';
import {CdkDragDrop, moveItemInArray, transferArrayItem} from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-add-file-google-drive',
  templateUrl: './add-file-google-drive.component.html',
  styleUrls: ['./add-file-google-drive.component.css']
})
export class AddFileGoogleDriveComponent {

  InputFolderId:string = "19T8z9XGbaEqTxbwuUblbsJmGo3veWht-";
  FolderData:FolderAPI = {
    name: '',
    id: '',
    files: [],
    folders: []
  };

  FinalFiles:Array<FinalFile> = [];
  FileModalTitle:string = "";

  constructor(
    private Core:CoreService,
    private Token:TokenService,
    private GoogleApi:GoogleApiService,
    private FilesService:FilesService,
  ) { }

  ngOnInit(): void {
    this.FilesService.GetNotAssignedFiles().subscribe((response:any) => {
      console.log(response);
    })
  }

  GetFilesFromGoogleDrive(){
    this.GoogleApi.GetFilesFromFolderId(this.InputFolderId).subscribe((response:any) => {
      if(response.code == 1){
        this.FolderData = response.document;
        this.SetupFinalFiles();
        console.log(this.FolderData);
      }
    })
  }

  SetupFinalFiles(){
    this.FolderData.files.forEach((file:FilesApi) => {
      var finalFile:FinalFile = {
        FileChunks: [file],
        name: file.name,
        thumbnailLink: file.thumbnailLink,
        type: file.type
      }
      this.FinalFiles.push(finalFile);
    });

    console.log(this.FinalFiles[0].FileChunks[0]);
  }

  drop(event: CdkDragDrop<FilesApi[]>) {
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

  EditAddFileModleVisible:boolean = false;
  FinalFileEditIndex:number | null = -1;
  EditFileModelData:FinalFile = {
    FileChunks: [],
    name: '',
    thumbnailLink: '',
    type: ''
  };

  handleEditFileModleCancel(){
    this.EditAddFileModleVisible = false;
  }

  handleEditFileModleOk(){
    if(this.FinalFileEditIndex != null){
      this.FinalFiles[this.FinalFileEditIndex] = structuredClone(this.EditFileModelData);
      this.EditAddFileModleVisible = false;
    }
    else{
      this.FinalFiles.unshift(structuredClone(this.EditFileModelData));
      this.EditAddFileModleVisible = false;
    }
  }

  ShowEditFileModle(data:FinalFile | null = null, index:number | null = null){
    console.log(data != null, index != null);
    if(data != null && index != null){
      this.FinalFileEditIndex = index;
      this.EditFileModelData = structuredClone(data);
      this.EditAddFileModleVisible = true;
      this.FileModalTitle = "Edit File";
    }
    else{
      this.EditFileModelData = {
        FileChunks: [],
        name: '',
        thumbnailLink: '',
        type: ''
      };
      this.FinalFileEditIndex = null;
      this.FileModalTitle = "Add File";
      this.EditAddFileModleVisible = true;
    }
  }

  DeleteFile(index:number){
    this.FinalFiles.splice(index,1);
    this.EditAddFileModleVisible = false;
  }

  EditAddFileChunkModleVisible:boolean = false;
  EditAddFileChunkModalTitle:string = "";
  EditAddFileChunkModelData:FilesApi = {
    name: '',
    thumbnailLink: '',
    type: '',
    size: '',
    email: '',
    id: ''
  };

  EditAddFileChunkIndex_File:number = 0;
  EditAddFileChunkIndex_FileChunk:number | null = null;

  handleEditAddFileChunkModleCancel(){
    this.EditAddFileChunkModleVisible = false;
  }

  handleEditAddFileChunkModleOk(){
    if(this.EditAddFileChunkIndex_File != null &&  this.EditAddFileChunkIndex_FileChunk != null){
      this.FinalFiles[this.EditAddFileChunkIndex_File].FileChunks[ this.EditAddFileChunkIndex_FileChunk] = structuredClone(this.EditAddFileChunkModelData);
      this.EditAddFileChunkModleVisible = false;
    }
    else{
      this.FinalFiles[this.EditAddFileChunkIndex_File].FileChunks.unshift(structuredClone(this.EditAddFileChunkModelData));
      this.EditAddFileChunkModleVisible = false;
    }
  }

  ShowEditAddFileChunkModle(editAddFileChunkIndex_File:number,editAddFileChunkIndex_FileChunk:number | null = null){
    this.EditAddFileChunkIndex_File = editAddFileChunkIndex_File;
    this.EditAddFileChunkIndex_FileChunk = editAddFileChunkIndex_FileChunk;
    if(this.EditAddFileChunkIndex_File != null &&  this.EditAddFileChunkIndex_FileChunk != null){
      this.EditAddFileChunkModelData = structuredClone(this.FinalFiles[this.EditAddFileChunkIndex_File].FileChunks[ this.EditAddFileChunkIndex_FileChunk]);
      this.EditAddFileChunkModleVisible = true;
      this.EditAddFileChunkModalTitle = "Edit File Chunk";
    }
    else{
      this.EditAddFileChunkModelData = {
        name: '',
        thumbnailLink: '',
        type: '',
        size: '',
        email: '',
        id: ''
      };
      this.EditAddFileChunkModleVisible = true;
      this.EditAddFileChunkModalTitle = "Add File Chunk";
    }
  }

  SaveFiles(){
    let finalFileFiltered = this.FinalFiles.filter((x:FinalFile) => x.FileChunks.length > 0);
    console.log(finalFileFiltered);
    this.FilesService.AddFinalFiles(finalFileFiltered).subscribe((response:any) => {
      if(response.code == 1){
        this.Core.showMessage("success", "Files Saved");
        this.InputFolderId = "";
        this.FolderData = {
          name: '',
          id: '',
          files: [...[]],
          folders: [...[]]
        };

        this.FinalFiles = [...[]];
        this.FileModalTitle = "";
      }
    })
  }
}

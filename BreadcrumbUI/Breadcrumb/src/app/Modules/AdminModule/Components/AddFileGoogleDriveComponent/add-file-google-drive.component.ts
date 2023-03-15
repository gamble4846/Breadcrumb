import { Component } from '@angular/core';
import { FilesApi, FinalFile, FolderAPI } from 'src/app/Models/FoldersAndFileApiModel';
import { GoogleApiService } from 'src/app/Services/API Services/GoogleApiService/google-api.service';
import { TokenService } from 'src/app/Services/API Services/TokenService/token.service';
import { CoreService } from 'src/app/Services/Other Services/CoreService/core.service';
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

  constructor(
    private Core:CoreService,
    private Token:TokenService,
    private GoogleApi:GoogleApiService,
  ) { }

  ngOnInit(): void {
    
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

  EditFileModleVisible:boolean = false;
  FinalFileEditIndex:number = -1;
  EditFileModelData:FinalFile = {
    FileChunks: [],
    name: '',
    thumbnailLink: '',
    type: ''
  };

  handleEditFileModleCancel(){
    this.EditFileModleVisible = false;
  }

  handleEditFileModleOk(){
    this.FinalFiles[this.FinalFileEditIndex] = this.EditFileModelData;
    this.EditFileModleVisible = false;
  }

  ShowEditFileModle(data:FinalFile, index:number){
    this.FinalFileEditIndex = index;
    this.EditFileModelData = structuredClone(data);
    this.EditFileModleVisible = true;
  }

  DeleteFile(index:number){
    this.FinalFiles.splice(index,1);
  }

  SaveFiles(){
    let finalDileFiltered = this.FinalFiles.filter((x:FinalFile) => x.FileChunks.length > 0);

    console.log(finalDileFiltered);
  }
}

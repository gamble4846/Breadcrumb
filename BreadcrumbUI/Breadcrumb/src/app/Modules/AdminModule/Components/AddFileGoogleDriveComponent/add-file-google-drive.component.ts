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
  FileModalTitle:string = "";

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
      this.FinalFiles[this.FinalFileEditIndex] = this.EditFileModelData;
      this.EditAddFileModleVisible = false;
    }
    else{
      this.FinalFiles.unshift(this.EditFileModelData);
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
      this.FinalFileEditIndex = null;
      this.FileModalTitle = "Add File";
      this.EditAddFileModleVisible = true;
    }
  }

  DeleteFile(index:number){
    this.FinalFiles.splice(index,1);
    this.EditAddFileModleVisible = false;
  }

  SaveFiles(){
    let finalDileFiltered = this.FinalFiles.filter((x:FinalFile) => x.FileChunks.length > 0);

    console.log(finalDileFiltered);
  }
}

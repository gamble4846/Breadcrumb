import { Component } from '@angular/core';
import { FilesApi, FinalFile } from 'src/app/Models/FoldersAndFileApiModel';
import { FilesService } from 'src/app/Services/API Services/FilesService/files.service';
import { CoreService } from 'src/app/Services/Other Services/CoreService/core.service';

@Component({
  selector: 'app-single-chunk-files-jexcel',
  templateUrl: './single-chunk-files-jexcel.component.html',
  styleUrls: ['./single-chunk-files-jexcel.component.css']
})
export class SingleChunkFilesJExcelComponent {

  Columns: Array<any> = [];
  Data: Array<any> = [];
  FinalFiles:Array<FinalFile> = [];

  constructor(
    private _Core: CoreService,
    private _File: FilesService
  ) { }

  ngOnInit(): void {
    this.Columns = [
      {
        type: 'text',
        title: 'Name',
        width: 150
      },
      {
        type: 'text',
        title: 'Thumbnail Link',
        width: 150
      },
      {
        type: 'text',
        title: 'Type',
        width: 150
      },
      {
        type: 'text',
        title: 'Size',
        width: 150
      },
      {
        type: 'text',
        title: 'Email',
        width: 150
      },
      {
        type: 'text',
        title: 'Link',
        width: 150
      },
      {
        type: 'text',
        title: 'Password',
        width: 150
      },
      {
        type: 'text',
        title: 'OtherData',
        width: 150
      }
    ]

    this.Data = [];
  }

  OnSave(data:Array<any>){
    this.FinalFiles = [];
    let toRemove:Array<number> = [];
    let NewData:Array<any> = [];

    for (let index = 0; index < data.length; index++) {
      const currentData = data[index];
      let isEmpty:boolean = true;
      currentData.forEach((currentInnerData:any) => {
        if(currentInnerData){
          isEmpty = false;
        }
      });

      if(isEmpty){
        toRemove.push(index);
      }
    }

    for (let index = 0; index < data.length; index++) {
      const currentData = data[index];
      if(!toRemove.includes(index)){
        NewData.push(currentData);
      }
    }
    
    NewData.forEach((currData:any) => {
      let chunkData:Array<FilesApi> = [{
        name: currData[0],
        thumbnailLink: currData[1],
        type: currData[2],
        size: currData[3],
        email: currData[4],
        id: currData[5],
        password: currData[6],
        otherData: currData[7]
      }];

      let FileData: FinalFile = {
        FileChunks: chunkData,
        name: currData[0],
        thumbnailLink: currData[1],
        type: currData[2]
      };

      this.FinalFiles.push(FileData);
    });

    console.log(this.FinalFiles);
    this._File.AddFinalFiles(this.FinalFiles).subscribe((response:any) => {
      if(response.code == 1){
        this._Core.showMessage("success", "Files Saved");
      }
    })
  }

}

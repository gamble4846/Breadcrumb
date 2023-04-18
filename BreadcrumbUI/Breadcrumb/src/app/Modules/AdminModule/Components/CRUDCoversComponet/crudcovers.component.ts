import { Component } from '@angular/core';
import { NzModalService } from 'ng-zorro-antd/modal';
import { CoverBreadModel } from 'src/app/Models/CoverBreadModel';
import { tbCoversModel } from 'src/app/Models/CoversModels';
import { vTvShowsModel } from 'src/app/Models/vTvShowsModels';
import { CoversService } from 'src/app/Services/API Services/CoversService/covers.service';
import { TvShowsService } from 'src/app/Services/API Services/TvShowsService/tv-shows.service';
import { CoreService } from 'src/app/Services/Other Services/CoreService/core.service';

@Component({
  selector: 'app-crudcovers',
  templateUrl: './crudcovers.component.html',
  styleUrls: ['./crudcovers.component.css']
})
export class CRUDCoversComponent {
  BreadTypes: Array<string> = ['Show'];
  DimensionsTypes: Array<string> = ['1920X1080','1000X1500'];
  SelectedBreadType: string = "";

  CoverBreads: Array<CoverBreadModel> = [];
  SelectedBreadId: string = "";

  CoversData:Array<tbCoversModel> = [];

  constructor(
    private _TvShows: TvShowsService,
    private _Core: CoreService,
    private _Covers: CoversService,
    private modal: NzModalService,
  ) { }

  ngOnInit(): void {
    this.SelectedBreadType = this.BreadTypes[0];
    this.BreadTypeChanged();
  }

  BreadTypeChanged() {
    switch (this.SelectedBreadType) {
      case "Show":
        this.UpdateCoverBreadsForShows();
        break;
      default:
        break;
    }
  }

  UpdateCoverBreadsForShows() {
    this._TvShows.GetAllTvshows().subscribe((response: any) => {
      let TvShowsData: Array<vTvShowsModel> = [];
      if (response.code == 1) {
        TvShowsData = response.document;
      }
      TvShowsData.forEach((tvshowData:vTvShowsModel) => {
        let coverBread:CoverBreadModel = {
          breadId: tvshowData.breadId ?? "",
          displayBread: tvshowData.primaryName + ` [${tvshowData.otherNames.toString()}]`,
          otherData: tvshowData
        }
        this.CoverBreads.push(coverBread);
      });
      this.SelectedBreadId = this.CoverBreads[0].breadId;
      this.BreadChanged();
    })
  }

  BreadChanged(){
    this.CoversData = [];
    this._Covers.GetCoverByBreadId(this.SelectedBreadId).subscribe((response:any) => {
      if(response.code == 1){
        this.CoversData = response.document;
      }
      console.log(this.CoversData);
    })
  }

  DeleteCoverConfirm(indexCover:number){
    this.modal.confirm({
      nzTitle: '<i>Are you sure you want to delete this Cover?</i>',
      nzOnOk: () => this.DeleteCover(indexCover),
    });
  }

  DeleteCover(indexCover:number){
    this.CoversData.splice(indexCover, 1);
    this.CoversData = [...this.CoversData];
  }

  InsetUpdateCoverModelIsVisible:boolean = false;
  InsetUpdateCoverModelTitle:string = "";
  CurrentlyEditingCoverIndex:number | null = null;
  CurrentlyEditingCoverModel:tbCoversModel = {
    id: null,
    breadId: null,
    link: '',
    dimensions: '',
    isNSFW: false
  };

  ShowInsetUpdateCoverModel(index:number | null = null){
    if(index != null){
      this.CurrentlyEditingCoverIndex = index;
      this.CurrentlyEditingCoverModel = structuredClone(this.CoversData[index]); 
      this.InsetUpdateCoverModelTitle = "Edit Cover Data";
    }
    else{
      this.CurrentlyEditingCoverIndex = null;
      this.InsetUpdateCoverModelTitle = "Add Cover Data";
      this.CurrentlyEditingCoverModel = {
        id: null,
        breadId: null,
        link: '',
        dimensions: '',
        isNSFW: false
      };
    }

    this.InsetUpdateCoverModelIsVisible = true;
  }

  handleInsetUpdateCoverModelCancel(){
    this.InsetUpdateCoverModelIsVisible = false;
  }

  handleInsetUpdateCoverModelOk(){
    if(this.CurrentlyEditingCoverIndex == null){
      this.CurrentlyEditingCoverModel.breadId = this.SelectedBreadId;
      this.CoversData.push(structuredClone(this.CurrentlyEditingCoverModel));
    }
    else{
      this.CoversData[this.CurrentlyEditingCoverIndex] = structuredClone(this.CurrentlyEditingCoverModel); 
    }

    this.CoversData = [...this.CoversData];
    this.InsetUpdateCoverModelIsVisible = false;
  }

  SaveCovers(){
    console.log(this.CoversData);
    this._Covers.InsertUpdateDeleteCoversForSingleBread(this.CoversData).subscribe((response:any) => {
      if(response.code == 1){
        this._Core.showMessage("success", "Data Saved");
        this.BreadChanged();
      }
    })
  }

  DropDownImage:string = "";

  UpdateDropDownImage(newLink:string){
    this.DropDownImage = newLink;
  }
}

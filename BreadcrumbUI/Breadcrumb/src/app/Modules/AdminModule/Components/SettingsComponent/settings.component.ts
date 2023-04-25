import { Component } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { GoogleAPI, Server, TokenModel } from 'src/app/Models/AdminModels';
import { TokenService } from 'src/app/Services/API Services/TokenService/token.service';
import { CoreService } from 'src/app/Services/Other Services/CoreService/core.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent {
  tokenData:TokenModel = {
    servers: [],
    theMovieDBAPIKey: '',
    showNSFWCovers: false,
    googleAPIs: [],
    showOnlyCustomCovers: false
  };

  tokenValue:string = "";

  constructor(
    private Core:CoreService,
    private Token:TokenService
  ) { }

  ngOnInit(): void {
    this.SetDefaultTokenData();
    this.GetTokenFromLocal();
  }

  addNewServer(){
    if(!this.tokenData.servers){
      this.tokenData.servers = [];
    }

    let newServer:Server = {
      databaseType: 'SQLServer',
      connectionString: '',
      isSelected: false
    }

    this.tokenData.servers.push(newServer);
  }

  addNewGoogleAPi(){
    if(!this.tokenData.googleAPIs){
      this.tokenData.googleAPIs = [];
    }

    let newGoogleAPi:GoogleAPI = {
      apiKey: '',
      isPrimary: false
    }
    
    this.tokenData.googleAPIs.push(newGoogleAPi);
  }

  SwitchGoogleAPIChanged(GoogleApiIndex:number){
    if(this.tokenData.googleAPIs[GoogleApiIndex].isPrimary){
      this.tokenData.googleAPIs.forEach((api:GoogleAPI) => {
        api.isPrimary = false;
      });
      this.tokenData.googleAPIs[GoogleApiIndex].isPrimary = true;
    }
  }

  DeleteGoogleApi(GoogleApiIndex:number){
    this.tokenData.googleAPIs.splice(GoogleApiIndex, 1);
  }

  SwitchServerChanged(ServerIndex:number){
    if(this.tokenData.servers[ServerIndex].isSelected){
      this.tokenData.servers.forEach((server:Server) => {
        server.isSelected = false;
      });
      this.tokenData.servers[ServerIndex].isSelected = true;
    }
  }

  SaveToken(){
    this.Token.CreateToken(this.tokenData).subscribe((response:any) => {
      if(response.code == 1){
        this.Core.setToken(response.document);
        this.GetTokenFromLocal();
      }
    })
  }

  SetDefaultTokenData(){
    if(this.Core.IsTokenPresent()){
      this.Token.GetToken().subscribe((response:any) => {
        if(response.code == 1){
          this.tokenData = response.document;
          console.log(this.tokenData);
        }
      })
    }
  }

  GetTokenFromLocal(){
    this.tokenValue = this.Core.getToken() || "";
  }

  DeleteServer(ServerIndex:number){
    this.tokenData.servers.splice(ServerIndex, 1);
  }

  CopyToken(){
    this.Core.copyString(this.tokenValue);
    this.Core.showMessage("success","Token Copied");
  }

  TokenFromStringSave(){
    this.Core.setToken(this.tokenValue);
    this.SetDefaultTokenData();
  }
}

import { Component } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { Server, TokenModel } from 'src/app/Models/AdminModels';
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
    theMovieDBAPIKey: ''
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
    let newServer:Server = {
      databaseType: 'SQLServer',
      connectionString: '',
      isSelected: false
    }
    this.tokenData.servers.push(newServer);
  }

  SwitchChanged(ServerIndex:number){
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

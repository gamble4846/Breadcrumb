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

  constructor(
    private Core:CoreService,
    private Token:TokenService
  ) { }

  ngOnInit(): void {

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
        console.log(response);
      }
    })
  }
}

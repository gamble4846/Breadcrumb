import { Component } from '@angular/core';
import { CoreService } from 'src/app/Services/Other Services/CoreService/core.service';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent {

  links:any = [];
  linksPart1:any = [];
  linksPart2:any = [];

  constructor(private Core:CoreService) { }

  ngOnInit(): void {
    this.links = this.Core.getMenus();

    for (var i=0; i<this.links.length; i++){
      if ((i+2)%2==0) {
        this.linksPart1.push(this.links[i]);
      }
      else {
        this.linksPart2.push(this.links[i]);
      }
    }
  }

  SocialClicked(social:string){
    let url = "";
    switch(social){
      case 'instagram':
        url = "https://www.instagram.com/gamble4846/";
        break;
      case 'github':
        url = "https://github.com/gamble4846";
        break;
      case 'linkedin':
        url = "https://www.linkedin.com/in/rohanpatel4846/";
        break;
      case 'reddit':
        url = "https://www.reddit.com/user/gamble4846";
        break;
      default:
        break;
    }
    window.open(url, '_blank')!.focus();
  }
}

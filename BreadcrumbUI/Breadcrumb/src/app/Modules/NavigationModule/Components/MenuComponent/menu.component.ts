import { Component } from '@angular/core';
import { CoreService } from 'src/app/Services/Other Services/CoreService/core.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent {
  links:any = [];
  
  constructor(private Core:CoreService) { }

  ngOnInit(): void {
    this.links = this.Core.getMenus();
  }

  ToggleMenu() {
    const burgerMenu = document.getElementById("burger");
    const navbarMenu = document.getElementById("menu");

    burgerMenu?.classList.toggle("is-active");
    navbarMenu?.classList.toggle("is-active");

    if (navbarMenu?.classList.contains("is-active")) {
      navbarMenu.style.maxHeight = navbarMenu.scrollHeight + "px";
    } else {
      navbarMenu?.removeAttribute("style");
    }
  }
}

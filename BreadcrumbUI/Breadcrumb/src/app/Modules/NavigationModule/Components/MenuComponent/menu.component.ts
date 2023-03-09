import { Component } from '@angular/core';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent {
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

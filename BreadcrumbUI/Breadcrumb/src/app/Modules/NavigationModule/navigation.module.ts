import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NavigationRoutingModule } from './navigation-routing.module';
import { MenuComponent } from './Components/MenuComponent/menu.component';
import { NzIconModule } from 'ng-zorro-antd/icon';


@NgModule({
  declarations: [
    MenuComponent
  ],
  imports: [
    CommonModule,
    NavigationRoutingModule,
    NzIconModule
  ],
  exports: [
    MenuComponent
  ]
})
export class NavigationModule { }

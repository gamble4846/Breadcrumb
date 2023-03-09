import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NavigationRoutingModule } from './navigation-routing.module';
import { MenuComponent } from './Components/MenuComponent/menu.component';
import { FooterComponent } from './Components/FooterComponent/footer.component';

import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzCollapseModule } from 'ng-zorro-antd/collapse';
import { NzIconModule } from 'ng-zorro-antd/icon';


@NgModule({
  declarations: [
    MenuComponent,
    FooterComponent
  ],
  imports: [
    CommonModule,
    NavigationRoutingModule,
    NzIconModule,
    NzGridModule,
    NzCollapseModule
  ],
  exports: [
    MenuComponent,
    FooterComponent
  ]
})
export class NavigationModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ShowsRoutingModule } from './shows-routing.module';
import { OpenerComponent } from './Components/OpenerComponent/opener.component';
import { OtherComponentsModule } from '../../Components/other-components.module';


import { NzGridModule } from 'ng-zorro-antd/grid';


@NgModule({
  declarations: [
    OpenerComponent
  ],
  imports: [
    CommonModule,
    ShowsRoutingModule,
    OtherComponentsModule,
    NzGridModule
  ]
})
export class ShowsModule { }

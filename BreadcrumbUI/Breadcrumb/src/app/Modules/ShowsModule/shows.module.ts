import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ShowsRoutingModule } from './shows-routing.module';
import { OpenerComponent } from './Components/OpenerComponent/opener.component';
import { OtherComponentsModule } from '../../Components/other-components.module';
import { TvShowComponent } from './Components/TvShowComponent/tv-show.component';

import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { NzRadioModule } from 'ng-zorro-antd/radio';
import { FormsModule } from '@angular/forms';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzSelectModule } from 'ng-zorro-antd/select';


@NgModule({
  declarations: [
    OpenerComponent,
    TvShowComponent
  ],
  imports: [
    CommonModule,
    ShowsRoutingModule,
    OtherComponentsModule,
    NzGridModule,
    NzTabsModule,
    NzRadioModule,
    FormsModule,
    NzPaginationModule,
    NzSelectModule
  ]
})
export class ShowsModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoaderComponent } from './Loader/loader.component';
import { BreadCardComponent } from './BreadCardComponent/bread-card.component';

import { NzSpinModule } from 'ng-zorro-antd/spin';
import { EpisodeCardComponent } from './EpisodeCardComponent/episode-card.component';



@NgModule({
  declarations: [
    LoaderComponent,
    BreadCardComponent,
    EpisodeCardComponent
  ],
  imports: [
    CommonModule,
    NzSpinModule
  ],
  exports: [
    LoaderComponent,
    BreadCardComponent,
    EpisodeCardComponent
  ]
})
export class OtherComponentsModule { }

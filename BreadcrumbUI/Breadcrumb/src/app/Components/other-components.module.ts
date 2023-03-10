import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoaderComponent } from './Loader/loader.component';
import { BreadCardComponent } from './BreadCardComponent/bread-card.component';

import { NzSpinModule } from 'ng-zorro-antd/spin';



@NgModule({
  declarations: [
    LoaderComponent,
    BreadCardComponent
  ],
  imports: [
    CommonModule,
    NzSpinModule
  ],
  exports: [
    LoaderComponent,
    BreadCardComponent
  ]
})
export class OtherComponentsModule { }

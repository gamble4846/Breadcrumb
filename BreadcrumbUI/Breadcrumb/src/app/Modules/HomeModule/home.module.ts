import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { OpenerComponent } from './OpenerComponent/opener.component';
import { MyCommonModule } from "../MyCommonModule/my-common.module";


@NgModule({
  declarations: [
    OpenerComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    MyCommonModule
  ]
})
export class HomeModule { }

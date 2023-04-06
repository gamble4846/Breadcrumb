import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MyCommonRoutingModule } from './my-common-routing.module';
import { FilesViewerComponent } from './Components/FilesViewer/files-viewer.component';


@NgModule({
  declarations: [
    FilesViewerComponent
  ],
  imports: [
    CommonModule,
    MyCommonRoutingModule
  ],
  exports: [
    FilesViewerComponent
  ]
})
export class MyCommonModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MyCommonRoutingModule } from './my-common-routing.module';
import { FilesViewerComponent } from './Components/FilesViewer/files-viewer.component';
import { NzCollapseModule } from 'ng-zorro-antd/collapse';
import { CachedSRCDirective } from './Directives/CachedSRC/cached-src.directive';


@NgModule({
  declarations: [
    FilesViewerComponent,
    CachedSRCDirective
  ],
  imports: [
    CommonModule,
    MyCommonRoutingModule,
    NzCollapseModule
  ],
  exports: [
    FilesViewerComponent
  ]
})
export class MyCommonModule { }

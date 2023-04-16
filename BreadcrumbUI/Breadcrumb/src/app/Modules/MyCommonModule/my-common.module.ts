import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MyCommonRoutingModule } from './my-common-routing.module';
import { FilesViewerComponent } from './Components/FilesViewer/files-viewer.component';
import { NzCollapseModule } from 'ng-zorro-antd/collapse';
import { CachedSRCDirective } from './Directives/CachedSRC/cached-src.directive';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzButtonModule } from 'ng-zorro-antd/button';


@NgModule({
  declarations: [
    FilesViewerComponent,
    CachedSRCDirective
  ],
  imports: [
    CommonModule,
    MyCommonRoutingModule,
    NzCollapseModule,
    NzTabsModule,
    NzGridModule,
    NzButtonModule
  ],
  exports: [
    FilesViewerComponent
  ]
})
export class MyCommonModule { }

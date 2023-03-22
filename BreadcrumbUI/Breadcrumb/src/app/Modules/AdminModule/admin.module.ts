import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { SettingsComponent } from './Components/SettingsComponent/settings.component';
import { AdminComponent } from './Components/AdminComponent/admin.component';

import { NzFormModule } from 'ng-zorro-antd/form';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzSwitchModule } from 'ng-zorro-antd/switch';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzCardModule } from 'ng-zorro-antd/card';
import { AddFileGoogleDriveComponent } from './Components/AddFileGoogleDriveComponent/add-file-google-drive.component';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { NzModalModule } from 'ng-zorro-antd/modal';


@NgModule({
  declarations: [
    SettingsComponent,
    AdminComponent,
    AddFileGoogleDriveComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    NzFormModule,
    FormsModule,
    ReactiveFormsModule,
    NzInputModule,
    NzSelectModule,
    NzButtonModule,
    NzGridModule,
    NzSwitchModule,
    NzIconModule,
    NzCardModule,
    DragDropModule,
    NzModalModule
  ]
})
export class AdminModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddFileGoogleDriveComponent } from './Components/AddFileGoogleDriveComponent/add-file-google-drive.component';
import { AdminComponent } from './Components/AdminComponent/admin.component';
import { AssignFilesShowSeasonsComponent } from './Components/AssignFilesShowSeasonsComponent/assign-files-show-seasons.component';
import { SettingsComponent } from './Components/SettingsComponent/settings.component';
import { InsertUpdateFullTvShowComponent } from './Components/InsertUpdateFullTvShowComponent/insert-update-full-tv-show.component';
import { SingleChunkFilesJExcelComponent } from './Components/SingleChunkFilesJExcelComponent/single-chunk-files-jexcel.component';
import { CRUDCoversComponent } from './Components/CRUDCoversComponet/crudcovers.component';

const routes: Routes = [
  { path: '', title: 'Admin - Breadcrumb', component:AdminComponent },
  { path: 'Settings', title: 'Settings - Breadcrumb', component:SettingsComponent },
  { path: 'AddFileGoogleDrive', title: 'AddFileGoogleDrive - Breadcrumb', component:AddFileGoogleDriveComponent },
  { path: 'AssignFilesShowSeasons', title: 'AssignFilesShowSeasons - Breadcrumb', component:AssignFilesShowSeasonsComponent },
  { path: 'InsertUpdateFullTvShow', title: 'InsertUpdateFullTvShow - Breadcrumb', component:InsertUpdateFullTvShowComponent },
  { path: 'SingleChunkFilesJExcel', title: 'SingleChunkFilesJExcel - Breadcrumb', component:SingleChunkFilesJExcelComponent },
  { path: 'CRUDCovers', title: 'CRUDCovers - Breadcrumb', component:CRUDCoversComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddFileGoogleDriveComponent } from './Components/AddFileGoogleDriveComponent/add-file-google-drive.component';
import { AdminComponent } from './Components/AdminComponent/admin.component';
import { AssignFilesShowSeasonsComponent } from './Components/AssignFilesShowSeasonsComponent/assign-files-show-seasons.component';
import { SettingsComponent } from './Components/SettingsComponent/settings.component';
import { InsertUpdateFullTvShowComponent } from './Components/InsertUpdateFullTvShowComponent/insert-update-full-tv-show.component';
import { SingleChunkFilesJExcelComponent } from './Components/SingleChunkFilesJExcelComponent/single-chunk-files-jexcel.component';

const routes: Routes = [
  { path: '', component:AdminComponent },
  { path: 'Settings', component:SettingsComponent },
  { path: 'AddFileGoogleDrive', component:AddFileGoogleDriveComponent },
  { path: 'AssignFilesShowSeasons', component:AssignFilesShowSeasonsComponent },
  { path: 'InsertUpdateFullTvShow', component:InsertUpdateFullTvShowComponent },
  { path: 'SingleChunkFilesJExcel', component:SingleChunkFilesJExcelComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }

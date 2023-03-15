import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './Components/AdminComponent/admin.component';
import { SettingsComponent } from './Components/SettingsComponent/settings.component';

const routes: Routes = [
  { path: '', component:AdminComponent },
  { path: 'Settings', component:SettingsComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }

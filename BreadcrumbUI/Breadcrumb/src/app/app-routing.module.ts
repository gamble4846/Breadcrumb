import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'Admin', loadChildren: () => import('./Modules/AdminModule/admin.module').then(m => m.AdminModule) },
  { path: 'Home', loadChildren: () => import('./Modules/HomeModule/home.module').then(m => m.HomeModule) },
  { path: 'Shows', loadChildren: () => import('./Modules/ShowsModule/shows.module').then(m => m.ShowsModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

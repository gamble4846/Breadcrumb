import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'Home' },
  { path: 'Admin', loadChildren: () => import('./Modules/AdminModule/admin.module').then(m => m.AdminModule) },
  { path: 'Shows', loadChildren: () => import('./Modules/ShowsModule/shows.module').then(m => m.ShowsModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

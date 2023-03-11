import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OpenerComponent } from './Components/OpenerComponent/opener.component';
import { TvShowComponent } from './Components/TvShowComponent/tv-show.component';

const routes: Routes = [
  { path: '', pathMatch: "full", component:OpenerComponent },
  { path: 'TvShow/:RegNumber', component:TvShowComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ShowsRoutingModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MovieComponent } from './Components/MovieComponent/movie.component';
import { OpenerComponent } from './Components/OpenerComponent/opener.component';
import { TvShowComponent } from './Components/TvShowComponent/tv-show.component';

const routes: Routes = [
  { path: '', pathMatch: "full", component:OpenerComponent },
  { path: 'TvShow/:ShowId', component:TvShowComponent },
  { path: 'Movie/:ShowId', component:MovieComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ShowsRoutingModule { }

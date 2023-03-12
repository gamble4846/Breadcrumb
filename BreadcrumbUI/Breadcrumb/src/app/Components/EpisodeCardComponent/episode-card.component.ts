import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-episode-card',
  templateUrl: './episode-card.component.html',
  styleUrls: ['./episode-card.component.css']
})
export class EpisodeCardComponent {
  @Input() Number:number = 0;
  @Input() Name:string = '';
  @Input() Description:string = '';
  @Input() ThumbnailLink:string = '';
}

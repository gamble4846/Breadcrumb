import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-bread-card',
  templateUrl: './bread-card.component.html',
  styleUrls: ['./bread-card.component.css']
})
export class BreadCardComponent {
  @Input() imageSRC = '';
  @Input() name = '';
}

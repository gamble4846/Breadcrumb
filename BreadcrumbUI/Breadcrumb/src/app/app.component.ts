import { Component } from '@angular/core';
import { TempService } from '../app/Services/TempService/temp.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Breadcrumb';

  constructor(
    private TempService:TempService
  ) { }

  ngOnInit(): void {
    this.TempService.get().subscribe((response:any) => {
      console.log(response);
    })
  }
}

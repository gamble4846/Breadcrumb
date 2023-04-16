import { Component } from '@angular/core';
import { CoreService } from 'src/app/Services/Other Services/CoreService/core.service';

@Component({
  selector: 'app-single-chunk-files-jexcel',
  templateUrl: './single-chunk-files-jexcel.component.html',
  styleUrls: ['./single-chunk-files-jexcel.component.css']
})
export class SingleChunkFilesJExcelComponent {
  tableData:any = [
    ['', 'Tesla', 'Nissan', 'Toyota', 'Honda', 'Mazda', 'Ford', 's'],
    ['2017', 10, 11, 12, 13, 15, 16, 16],
    ['2018', 10, 11, 12, 13, 15, 16, 16],
    ['2019', 10, 11, 12, 13, 15, 16, 16],
    ['2020', 10, 11, 12, 13, 15, 16, 16],
    ['2021', 10, 11, 12, 13, 15, 16, 16]
  ];

  colHeaders:Array<string> = ["Name","Thumbnail Link","Type","Size","Email","Link","Password","OtherData"];
  colWidths:Array<string> = ["150px","150px","150px","150px","150px","150px","150px","150px"];

  constructor(
    private Core:CoreService
  ) { }

  ngOnInit(): void {

  }

  saveTable(){
    console.log(this.tableData);
  }
}

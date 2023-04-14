import { Component } from '@angular/core';
import { FilesViewerModel } from '../../MyCommonModule/Components/Models/FilesViewerModel';

@Component({
  selector: 'app-opener',
  templateUrl: './opener.component.html',
  styleUrls: ['./opener.component.css']
})
export class OpenerComponent {
  FilesViewerData:FilesViewerModel = {
    FileToGetFrom: 'Episode',
    PrimaryId: 'd7b1739e-98d6-ed11-b5c9-34e12dcd564e'
  }
}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JExcelJSComponent } from './jexcel-js.component';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { FormsModule } from '@angular/forms';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';



@NgModule({
  declarations: [
    JExcelJSComponent
  ],
  imports: [
    CommonModule,
    NzInputNumberModule,
    FormsModule,
    NzInputModule,
    NzButtonModule,
    NzIconModule
  ],
  exports: [
    JExcelJSComponent
  ]
})
export class JExcelJSModule { }

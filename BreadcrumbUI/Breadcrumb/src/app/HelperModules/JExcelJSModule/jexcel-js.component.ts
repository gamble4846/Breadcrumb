import { Component, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core';

declare var jspreadsheet: any;

@Component({
    selector: 'jexcel-js',
    templateUrl: './jexcel-js.component.html',
    styleUrls: ['./jexcel-js.component.css']
})

export class JExcelJSComponent {
    @ViewChild('spreadsheet', {read: ElementRef})
    spreadsheet: ElementRef<HTMLElement> | undefined;

    @Input() Columns: Array<any> = [];
    @Input() Data: Array<any> = [];
    @Input() ShowAddRows: boolean = false;
    @Input() ShowSave: boolean = true;
    @Input() minRows: number = 10;

    @Output() OnSave = new EventEmitter<any>();

    AddRowsCount:number = 0;
    JSpreadsheet:any;

    constructor(
    ) { }


    ngOnInit(): void {
        
    }

    ngAfterViewInit():void {
        this.JSpreadsheet = jspreadsheet(this.spreadsheet?.nativeElement, {
            data: this.Data,
            columns: this.Columns,
        });

        let dataLength = this.JSpreadsheet.getData().length;

        if(dataLength < this.minRows){
            this.AddRowsCount = this.minRows - dataLength;
            this.AddNewRows();
        }
    }

    AddNewRows(){
        for (let index = 0; index < this.AddRowsCount; index++) {
            this.Data.push([]);
        }
        this.JSpreadsheet.setData(this.Data);
        this.AddRowsCount = 0;
    }

    SaveJExcel(){
        this.OnSave.emit(this.JSpreadsheet.getData());
    }

}

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SingleChunkFilesJExcelComponent } from './single-chunk-files-jexcel.component';

describe('SingleChunkFilesJExcelComponent', () => {
  let component: SingleChunkFilesJExcelComponent;
  let fixture: ComponentFixture<SingleChunkFilesJExcelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SingleChunkFilesJExcelComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SingleChunkFilesJExcelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

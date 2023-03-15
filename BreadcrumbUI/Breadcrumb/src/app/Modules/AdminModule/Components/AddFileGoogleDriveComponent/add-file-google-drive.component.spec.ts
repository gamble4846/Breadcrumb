import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddFileGoogleDriveComponent } from './add-file-google-drive.component';

describe('AddFileGoogleDriveComponent', () => {
  let component: AddFileGoogleDriveComponent;
  let fixture: ComponentFixture<AddFileGoogleDriveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddFileGoogleDriveComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddFileGoogleDriveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

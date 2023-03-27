import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignFilesShowSeasonsComponent } from './assign-files-show-seasons.component';

describe('AssignFilesShowSeasonsComponent', () => {
  let component: AssignFilesShowSeasonsComponent;
  let fixture: ComponentFixture<AssignFilesShowSeasonsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AssignFilesShowSeasonsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AssignFilesShowSeasonsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CRUDCoversComponent } from './crudcovers.component';

describe('CRUDCoversComponent', () => {
  let component: CRUDCoversComponent;
  let fixture: ComponentFixture<CRUDCoversComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CRUDCoversComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CRUDCoversComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

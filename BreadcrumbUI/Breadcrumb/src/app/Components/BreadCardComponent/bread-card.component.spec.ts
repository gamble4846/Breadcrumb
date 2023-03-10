import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BreadCardComponent } from './bread-card.component';

describe('BreadCardComponent', () => {
  let component: BreadCardComponent;
  let fixture: ComponentFixture<BreadCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BreadCardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BreadCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

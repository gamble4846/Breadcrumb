import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InsertUpdateFullTvShowComponent } from './insert-update-full-tv-show.component';

describe('InsertUpdateFullTvShowComponent', () => {
  let component: InsertUpdateFullTvShowComponent;
  let fixture: ComponentFixture<InsertUpdateFullTvShowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InsertUpdateFullTvShowComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InsertUpdateFullTvShowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

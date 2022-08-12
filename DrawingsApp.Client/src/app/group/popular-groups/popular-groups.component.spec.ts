import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PopularGroupsComponent } from './popular-groups.component';

describe('PopularGroupsComponent', () => {
  let component: PopularGroupsComponent;
  let fixture: ComponentFixture<PopularGroupsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PopularGroupsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PopularGroupsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

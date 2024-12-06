import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TopTabComponent } from './top-tab.component';

describe('TopTabComponent', () => {
  let component: TopTabComponent;
  let fixture: ComponentFixture<TopTabComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TopTabComponent]
    });
    fixture = TestBed.createComponent(TopTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

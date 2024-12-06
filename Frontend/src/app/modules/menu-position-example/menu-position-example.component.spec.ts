import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MenuPositionExampleComponent } from './menu-position-example.component';

describe('MenuPositionExampleComponent', () => {
  let component: MenuPositionExampleComponent;
  let fixture: ComponentFixture<MenuPositionExampleComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MenuPositionExampleComponent]
    });
    fixture = TestBed.createComponent(MenuPositionExampleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

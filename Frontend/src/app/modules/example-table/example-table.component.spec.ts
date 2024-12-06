import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExampleTableComponent } from './example-table.component';

describe('ExampleTableComponent', () => {
  let component: ExampleTableComponent;
  let fixture: ComponentFixture<ExampleTableComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ExampleTableComponent]
    });
    fixture = TestBed.createComponent(ExampleTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

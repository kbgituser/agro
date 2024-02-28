import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RequestIndexComponent } from './request-index.component';

describe('RequestIndexComponent', () => {
  let component: RequestIndexComponent;
  let fixture: ComponentFixture<RequestIndexComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RequestIndexComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RequestIndexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

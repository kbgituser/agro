import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CityIndexComponent } from './city-index.component';

describe('CitiesComponent', () => {
  let component: CityIndexComponent;
  let fixture: ComponentFixture<CityIndexComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CityIndexComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CityIndexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

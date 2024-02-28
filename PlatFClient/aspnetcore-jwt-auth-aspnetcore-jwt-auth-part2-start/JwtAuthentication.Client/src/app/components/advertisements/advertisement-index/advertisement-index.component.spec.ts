import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdvertisementIndexComponent } from './advertisement-index.component';

describe('AdvertisementIndexComponent', () => {
  let component: AdvertisementIndexComponent;
  let fixture: ComponentFixture<AdvertisementIndexComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdvertisementIndexComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdvertisementIndexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

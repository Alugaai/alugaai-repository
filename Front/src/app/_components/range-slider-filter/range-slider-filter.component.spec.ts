import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RangeSliderFilterComponent } from './range-slider-filter.component';

describe('RangeSliderFilterComponent', () => {
  let component: RangeSliderFilterComponent;
  let fixture: ComponentFixture<RangeSliderFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RangeSliderFilterComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RangeSliderFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { Component } from '@angular/core';

@Component({
  selector: 'app-range-slider-filter',
  templateUrl: './range-slider-filter.component.html',
  styleUrl: './range-slider-filter.component.scss'
})
export class RangeSliderFilterComponent {
  startValue: number = 25;
  endValue: number = 75;

  onInputChange(event: Event) {
    // Cast the event target to HTMLInputElement
    const target = event.target as HTMLInputElement;

    // Check if the input is the startThumb or endThumb
    if (target.hasAttribute('matSliderStartThumb')) {
      this.startValue = parseFloat(target.value);
    } else if (target.hasAttribute('matSliderEndThumb')) {
      this.endValue = parseFloat(target.value);
    }
  }
}

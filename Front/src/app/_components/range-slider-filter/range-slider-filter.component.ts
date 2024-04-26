import { Component, EventEmitter, Output } from '@angular/core';

export interface IAges {
  initialAge: number;
  finalAge: number;
}

@Component({
  selector: 'app-range-slider-filter',
  templateUrl: './range-slider-filter.component.html',
  styleUrl: './range-slider-filter.component.scss'
})
export class RangeSliderFilterComponent {
  // Declare the endValue property
  age: IAges = { initialAge: 0, finalAge: 100 };

  @Output() ageValue = new EventEmitter<IAges>();

  onInputChange(event: Event) {
    // Cast the event target to HTMLInputElement
    const target = event.target as HTMLInputElement;

    // Check if the input is the startThumb or endThumb
    if (target.hasAttribute('matSliderStartThumb')) {
      this.age.initialAge = parseFloat(target.value);
    } else if (target.hasAttribute('matSliderEndThumb')) {
      this.age.finalAge = parseFloat(target.value);
    }

    this.ageValue.emit({
      initialAge: this.age.initialAge,
      finalAge: this.age.finalAge
    });
  }
}

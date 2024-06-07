import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { ILocationFilterCity } from '../../_models/ILocationFilterCity';

@Component({
  selector: 'app-place-autocomplete',
  templateUrl: './place-autocomplete.component.html',
  styleUrl: './place-autocomplete.component.scss'
})
export class PlaceAutocompleteComponent implements AfterViewInit{

  @ViewChild('inputField') inputField?: ElementRef;

  @Input() placeholder = 'Filtrar por local';

  @Output() initialLocationFilter = new EventEmitter<ILocationFilterCity>();

  autocomplete: google.maps.places.Autocomplete | undefined;

  constructor() { }


  ngAfterViewInit(): void {
    this.autocomplete = new google.maps.places.Autocomplete(this.inputField?.nativeElement);

    this.autocomplete.addListener('place_changed', () => {
      const place = this.autocomplete?.getPlace();

      if (place && place.geometry && place.geometry.location) {
        this.initialLocationFilter.emit({
          lat: place.geometry.location.lat(),
          long: place.geometry.location.lng()
        });
      }
    });
  }

}

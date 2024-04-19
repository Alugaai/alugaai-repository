import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { Component, DoCheck, ElementRef, EventEmitter, Output, ViewChild, inject } from '@angular/core';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatAutocompleteSelectedEvent, MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatChipInputEvent, MatChipsModule } from '@angular/material/chips';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { MatIconModule } from '@angular/material/icon';
import { AsyncPipe } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { LiveAnnouncer } from '@angular/cdk/a11y';
@Component({
  selector: 'app-auto-complete',
  templateUrl: './auto-complete.component.html',
  styleUrl: './auto-complete.component.scss'
})
export class AutoCompleteComponent{
  separatorKeysCodes: number[] = [ENTER, COMMA];
  interesseCtrl = new FormControl('');
  filteredInteresses: Observable<string[]>;
  interesses: string[] = [];
  allInteresses: string[] = ['Filmes', 'Animes', 'Treinar', 'Correr', 'Baralho', 'Harry Potter', 'League of Legends', 'Valorant'];

  @Output() interessesChange = new EventEmitter<string[]>();
  @ViewChild('interesseInput') interesseInput!: ElementRef<HTMLInputElement>;

  announcer = inject(LiveAnnouncer);

  constructor() {
    this.filteredInteresses = this.interesseCtrl.valueChanges.pipe(
      startWith(null),
      map((interesse: string | null) => (interesse ? this._filter(interesse) : this.allInteresses.slice())),
    );
  }

  add(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();

  // Add our interesse
  if (value && this.allInteresses.includes(value)) {
    this.interesses!.push(value);
  }

  // Clear the input value
  event.chipInput!.clear();

  this.interesseCtrl.setValue(null);

  // Emit the change event
  this.interessesChange.emit(this.interesses);
  }

  remove(interesse: string): void {
    const index = this.interesses!.indexOf(interesse);

    if (index >= 0) {
      this.interesses!.splice(index, 1);

      this.announcer.announce(`Removed ${interesse}`);

      // emitindo evento
      this.interessesChange.emit(this.interesses);
    }
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    this.interesses!.push(event.option.viewValue);
    this.interesseInput.nativeElement.value = '';
    this.interesseCtrl.setValue(null);

    // emitindo evento
    this.interessesChange.emit(this.interesses);
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.allInteresses.filter(interesse => interesse.toLowerCase().includes(filterValue));
  }

}


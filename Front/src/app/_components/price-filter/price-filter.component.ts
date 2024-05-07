import { Component, EventEmitter, Output } from "@angular/core";

@Component({
  selector: 'app-price-filter',
  templateUrl: './price-filter.component.html',
  styleUrls: ['./price-filter.component.scss']
})
export class PriceFilterComponent {
  iconName: boolean = true;
  disabled = false;
  max = 10000;
  min = 0;
  showTicks = false;
  step = 1;
  thumbLabel = false;
  value = 0;

  @Output() applyFilter = new EventEmitter<{ min: number; max: number }>();

  apply() {
    this.applyFilter.emit({ min: this.min, max: this.value });
  }
  toggleIcon(): void{
    this.iconName = !this.iconName
  }

  onMenuClosed(): void {
    this.iconName = !this.iconName
  }
  
}





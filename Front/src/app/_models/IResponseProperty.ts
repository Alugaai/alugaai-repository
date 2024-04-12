export interface IResponseProperty {
  id: number;
  price: number;
  position: { lat: number; lng: number };
  options: google.maps.MarkerOptions;
}

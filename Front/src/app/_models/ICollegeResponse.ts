import { IImage } from "./IImage";

export interface ICollegeResponse {
  id: number;
  name: string;
  address: string;
  number: string;
  neighborhood: string;
  district: string;
  state: string;
  position: { lat: number; lng: number };
  images: Array<IImage>;
  options?: google.maps.MarkerOptions;
}

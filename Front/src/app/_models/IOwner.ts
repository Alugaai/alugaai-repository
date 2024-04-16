import { IImage } from "./IImage";

export interface IOwner {

  id: string;
  name: string;
  email: string;
  phoneNumber: string;
  image: IImage;


}

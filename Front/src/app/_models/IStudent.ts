import { IImage } from "./IImage";

export interface IStudent {
  id: string;
  name: string;
  email: string;
  age: number;
  college: string;
  image: IImage;
  hobbies: string[];
  personalitys: string[];

}

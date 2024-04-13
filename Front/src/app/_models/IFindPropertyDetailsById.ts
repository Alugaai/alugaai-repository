import { IImageBuilding } from './IImageBuilding';
import { IOwner } from './IOwner';
import { IStudentsLikes } from './IStudentsLikes';

export interface IFindPropertyDetailsById {
  id: number;
  name: string;
  address: string;
  number: string;
  homeComplement: string;
  neighborhood: string;
  district: string;
  state: string;
  images: Array<IImageBuilding>;
  price: number;
  bedrooms: number;
  bathrooms: number;
  description: string;
  owner: IOwner;
  studentsLikes: Array<IStudentsLikes>;
}

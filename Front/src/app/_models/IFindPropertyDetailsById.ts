import { IImage } from './IImage';
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
  images: Array<IImage>;
  price: number;
  bedrooms: number;
  bathrooms: number;
  description: string;
  owner?: IOwner;
  studentsLikes: Array<IStudentsLikes>;
}

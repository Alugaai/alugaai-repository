import { IBuildingResponse } from './IBuildingResponse';
import { IConnection } from './IConnection';
import { IFindPropertyDetailsById } from './IFindPropertyDetailsById';
import { IImage } from './IImage';
import { INotification } from './INotification';
import { IStudentPropertyLikes } from './IStudentPropertyLikes';

export interface IUserDetails {
  id: string;
  name: string;
  email: string;
  gender?: string;
  birthDate: Date | string;
  imageUser?: IImage;
  notification?: INotification[];
  properties?: IFindPropertyDetailsById[];
  personalitys?: string[];
  hobbies?: string[];
  connections?: IConnection[];
  pendentsConnectionsId?: string[];
  college?: IBuildingResponse;
  propertiesLikes?: IStudentPropertyLikes[];
}

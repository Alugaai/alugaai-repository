import { IImage } from './IImage';

export interface IStudentsWhoInvitationsConnections {
  id: string;
  notificationsIds: number[];
  name: string;
  college?: string;
  imageUser?: IImage;
}

import { IImage } from './IImage';

export interface IStudentsWhoInvitationsConnections {
  id: string;
  notificationsIds: number[];
  username: string;
  college?: string;
  imageUser?: IImage;
}

export interface INotification {
  id: number;
  userWhoSend : string;
  userWhoReceived: string;
  text: string;
  moment: Date | string;
  read: boolean;
}

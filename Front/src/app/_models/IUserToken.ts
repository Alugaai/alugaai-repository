export interface IUserToken {
  email: string;
  token: string;
  refreshToken: string;
  expiration : Date | string;
  role: Array<string>;
}

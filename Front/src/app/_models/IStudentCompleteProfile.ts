import { IUserCompleteProfile } from './IUserCompleteProfile';
export interface IStudentCompleteProfile extends IUserCompleteProfile {
  collegeId: number;
}

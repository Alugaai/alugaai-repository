export interface IFilterStudent {
  name?: string;
  initialAge?: number;
  finalAge?: number;
  ownCollege?: boolean;
  interests?: string[];
  pageNumber: number;
  pageSize: number;
}

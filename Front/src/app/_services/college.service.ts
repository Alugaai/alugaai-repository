import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { map } from 'rxjs';
import { ICollegeResponse } from '../_models/ICollegeResponse';

@Injectable({
  providedIn: 'root'
})
export class CollegeService {

  constructor(private http: HttpClient) {}

  baseUrl: string = environment.apiUrl;
  colleges: ICollegeResponse[] = [];

  getColleges() {
    return this.http.get(`${this.baseUrl}colleges`).pipe(
      map((response) => {
        return this.colleges = response as ICollegeResponse[];
      })
    );
  }

  getCollegeById(id: number) {
    return this.http.get(`${this.baseUrl}colleges/${id}`).pipe(
      map((response) => {
        console.log(response);
        return response as ICollegeResponse;
      })
    );
  }

}

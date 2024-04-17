import { Injectable } from '@angular/core';
import { IFilterStudent } from '../_models/IFilterStudent';
import { IStudent } from '../_models/IStudent';
import { PaginatedResult } from '../_models/IPagination';
import { environment } from '../../environments/environment.development';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  baseUrl: string = environment.apiUrl;
  paginatedResult: PaginatedResult<Array<IStudent>> = new PaginatedResult();

  constructor(private http: HttpClient) { }



  filterStudent(filterStudent: IFilterStudent) {
    let params = new HttpParams();
    if (filterStudent.name) params = params.append('name', filterStudent.name);
    if (filterStudent.initialAge) params = params.append('initialAge', filterStudent.initialAge);
    if (filterStudent.finalAge) params = params.append('finalAge', filterStudent.finalAge);
    if (filterStudent.ownCollege) params = params.append('ownCollege', filterStudent.ownCollege);
    if (filterStudent.interests) params = params.append('interests', filterStudent.interests.join(','));
    if (filterStudent.pageNumber) params = params.append('pageNumber', filterStudent.pageNumber);
    if (filterStudent.pageSize) params = params.append('pageSize', filterStudent.pageSize);

    console.log('Params', params);

    return this.http.get<any>(this.baseUrl + 'students', {
      observe: 'response',
      params,
    }).pipe(
      map((response) => {
        console.log(response);
        if (response.body) {
          this.paginatedResult.result = response.body;
        }
        const pagination = response.headers.get('Pagination');
        if (pagination) {
          this.paginatedResult.pagination = JSON.parse(pagination);
        }
        console.log('Pagination',this.paginatedResult);
        return this.paginatedResult;
      })
    );
  }



}

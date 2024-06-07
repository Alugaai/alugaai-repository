import { Injectable } from '@angular/core';
import { PaginatedResult } from '../_models/IPagination';
import { environment } from '../../environments/environment.development';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs';
import { IFilterProperty } from '../_models/IFilterProperty';
import { IResponseProperty } from '../_models/IResponseProperty';
import { IFindPropertyDetailsById } from '../_models/IFindPropertyDetailsById';

@Injectable({
  providedIn: 'root',
})
export class PropertyService {
  constructor(private http: HttpClient) {}

  baseUrl: string = environment.apiUrl;

  paginatedResult: PaginatedResult<Array<IResponseProperty>> =
    new PaginatedResult();

  filterProperty(filterProperty?: IFilterProperty) {
    let params = new HttpParams();
    if (filterProperty != null) {
      if (filterProperty.minPrice)
        params = params.append('minPrice', filterProperty.minPrice);
      if (filterProperty.maxPrice)
        params = params.append('maxPrice', filterProperty.maxPrice);
      if (filterProperty.pageNumber)
        params = params.append('pageNumber', filterProperty.pageNumber);
      if (filterProperty.pageSize)
        params = params.append('pageSize', filterProperty.pageSize);
    }

    return this.http
      .get<any>(this.baseUrl + 'properties', {
        observe: 'response',
        params,
      })
      .pipe(
        map((response) => {
          if (response.body) {
            this.paginatedResult.result = response.body;
          }
          const pagination = response.headers.get('Pagination');
          if (pagination) {
            this.paginatedResult.pagination = JSON.parse(pagination);
          }
          return this.paginatedResult;
        })
      );
  }

  findPropertyDetailsById(id: number) {
    return this.http.get(`${this.baseUrl}properties/findPropertyDetailsById/${id}`).pipe(
      map((response) => {
        return response as IFindPropertyDetailsById;
      })
    );
  }


}

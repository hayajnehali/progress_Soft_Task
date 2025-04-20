import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable, InjectionToken } from '@angular/core';
import { catchError, Observable, of, throwError } from 'rxjs';
import { PaginatedResult } from '../model/paginated.result';
import { BusinessCard, BusinessCardFilter } from '../model/BusinessCard';

@Injectable({
  providedIn: 'root'
}) 
export class BusinessCardService { 
  protected baseUrl:string="https://localhost:44315/api/"
  
  constructor(protected http: HttpClient) {

    this.baseUrl=this.baseUrl+"BusinessCard";
  }

 

  getAll(filterCriteria: BusinessCardFilter): Observable<BusinessCardFilter> {
    let params = new HttpParams();
    
    // Assuming filterCriteria is an object with key-value pairs
    Object.keys(filterCriteria).forEach(key => {
      const value = filterCriteria[key as keyof BusinessCardFilter];
      
      if (value !== undefined && value !== null) {
        params = params.append(key, String(value));  
      }
    });

    return this.http.get<BusinessCardFilter>(this.baseUrl+"/getAll", { params })  .pipe(
      catchError(err => {
        console.error('Error occurred:', err);
        return throwError(err);
      })
    );
  }



  getById(id: number): Observable<BusinessCard> {
    return this.http.get<BusinessCard>(`${this.baseUrl}/getById/${id}`).pipe(
      catchError(this.handleError<BusinessCard>(`getById id=${id}`))
    );
  }

  create(item: BusinessCard): Observable<BusinessCard> {
    return this.http.post<BusinessCard>(this.baseUrl+"/create", item)
  }
  // create(item: T): Observable<T> {
  //   return this.http.post<T>(this.baseUrl+"/create", item).pipe(
  //     catchError(this.handleError<T>('create'))
  //   );
  // }

  update(item: BusinessCard): Observable<BusinessCard> {
    return this.http.put<BusinessCard>(`${this.baseUrl}/update`, item)
  } 

  delete(id: number): Observable<PaginatedResult<BusinessCard>> {
    return this.http.delete<PaginatedResult<BusinessCard>>(`${this.baseUrl}/delete/${id}`).pipe(
      catchError(this.handleError<PaginatedResult<BusinessCard>>('delete'))
    );
  }

  protected handleError<BusinessCard>(operation = 'operation', result?: BusinessCard) {
    return (error: any): Observable<BusinessCard> => {
      console.error(`${operation} failed: ${error.message}`);
      return of(result as BusinessCard);
    };
  }
}


export const BASE_URL = new InjectionToken<string>('BaseUrl');
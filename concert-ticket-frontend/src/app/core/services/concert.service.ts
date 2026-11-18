
import { Injectable, inject } from '@angular/core';

import { HttpClient,HttpParams} from '@angular/common/http';

import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';

import { CreateConcertRequest } from '../models/create-concert-request.model';
import { ConcertResponse } from '../models/concert-response.model';

import { PagedConcertResponse } from '../models/paged-concert-response.model';
import { UpdateConcertRequest } from '../models/update-concert-request.model';
@Injectable({
  providedIn: 'root'
})
export class ConcertService {

  // Inject HttpClient to communicate with backend
  private http = inject(HttpClient);

  // Backend POST URL
  private apiUrl = `${environment.apiUrl}/concerts`;

  // POST: Create a new concert
  createConcert(
  request: CreateConcertRequest
): Observable<ConcertResponse> {

  return this.http.post<ConcertResponse>(
    this.apiUrl,
    request
  );
  }

   // GET: Get all concerts with search and pagination
  getConcerts(
    search: string = '',
    pageNumber: number = 1,
    pageSize: number = 10
  ): Observable<PagedConcertResponse> {

    let params = new HttpParams()
      .set('pageNumber', pageNumber)
      .set('pageSize', pageSize);

    if (search.trim()) {
      params = params.set('search', search.trim());
    }

    return this.http.get<PagedConcertResponse>(
      this.apiUrl,
      { params }
    );
  }

  // GET: Get one concert by ID
  getConcertById(id: number): Observable<ConcertResponse> {

    return this.http.get<ConcertResponse>(
      `${this.apiUrl}/${id}`
    );
  }
  
  // PUT: api/concerts/{id}
updateConcert(
  id: number,
  request: UpdateConcertRequest
): Observable<ConcertResponse> {

  return this.http.put<ConcertResponse>(
    `${this.apiUrl}/${id}`,
    request
  );
}

// DELETE: api/concerts/{id}
deleteConcert(id: number): Observable<void> {

  return this.http.delete<void>(
    `${this.apiUrl}/${id}`
  );
}
}

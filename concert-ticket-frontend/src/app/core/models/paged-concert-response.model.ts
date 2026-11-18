
import { ConcertResponse } from './concert-response.model';

export interface PagedConcertResponse {
  concerts: ConcertResponse[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}

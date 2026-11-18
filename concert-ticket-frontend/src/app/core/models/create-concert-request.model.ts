
export interface CreateConcertRequest {
  concertName: string;
  artistName: string;
  description?: string;
  genre: string;
  language: string;
  imageUrl?: string;
  status: string;
}

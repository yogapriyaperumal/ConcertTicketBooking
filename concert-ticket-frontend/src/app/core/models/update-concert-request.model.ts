
export interface UpdateConcertRequest {
  concertName: string;
  artistName: string;
  description: string | null;
  genre: string;
  language: string;
  imageUrl: string | null;
  status: string;
}


export interface ConcertResponse {
  concertId: number;
  concertName: string;
  artistName: string;
  description: string | null;
  genre: string;
  language: string;
  imageUrl: string | null;
  status: string;
  createdAtUtc: string;
  updatedAtUtc: string | null;
}

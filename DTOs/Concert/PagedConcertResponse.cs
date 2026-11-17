namespace ConcertTicketBooking.DTOs.Concert
{
    public class PagedConcertResponse
    {
        public List<ConcertResponse> Concerts { get; set; } = new();

        public int TotalCount { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }
    }
}
using ConcertTicketBooking.Models.Enums;

namespace ConcertTicketBooking.DTOs.Concert
{
    public class ConcertResponse
    {
        public int ConcertId { get; set; }

        public string ConcertName { get; set; } = string.Empty;

        public string ArtistName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string Genre { get; set; } = string.Empty;

        public string Language { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public ConcertStatus Status { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }
    }
}
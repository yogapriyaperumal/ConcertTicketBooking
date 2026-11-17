using ConcertTicketBooking.Models.Enums;

namespace ConcertTicketBooking.Models
{
    public class Concert
    {
        // Primary key
        public int ConcertId { get; set; }

        // Basic concert information
        public string ConcertName { get; set; } = string.Empty;

        public string ArtistName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string Genre { get; set; } = string.Empty;

        public string Language { get; set; } = string.Empty;

        // Poster image URL
        public string? ImageUrl { get; set; }

        // Default status for a newly created concert
        public ConcertStatus Status { get; set; }
            = ConcertStatus.Draft;

        // Audit information
        public DateTime CreatedAtUtc { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }
    }
}
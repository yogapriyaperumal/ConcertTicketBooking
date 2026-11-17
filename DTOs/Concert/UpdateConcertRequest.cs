using System.ComponentModel.DataAnnotations;
using ConcertTicketBooking.Models.Enums;

namespace ConcertTicketBooking.DTOs.Concert
{
    public class UpdateConcertRequest
    {
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string ConcertName { get; set; } = string.Empty;

        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string ArtistName { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        [StringLength(80)]
        public string Genre { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Language { get; set; } = string.Empty;

        [StringLength(500)]
        public string? ImageUrl { get; set; }

        public ConcertStatus Status { get; set; }
    }
}
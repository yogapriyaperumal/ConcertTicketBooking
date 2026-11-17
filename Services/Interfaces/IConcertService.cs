using ConcertTicketBooking.DTOs.Concert;

namespace ConcertTicketBooking.Services.Interfaces
{
    public interface IConcertService
    {
        Task<ConcertResponse> CreateAsync(CreateConcertRequest request);

        Task<ConcertResponse?> GetByIdAsync(int id);

        Task<PagedConcertResponse> GetAllAsync(
            string? search,
            int pageNumber,
            int pageSize);

        Task<ConcertResponse?> UpdateAsync(
            int id,
            UpdateConcertRequest request);

        Task<bool> DeleteAsync(int id);
    }
}
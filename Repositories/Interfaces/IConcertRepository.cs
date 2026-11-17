using ConcertTicketBooking.Models;

namespace ConcertTicketBooking.Repositories.Interfaces
{
    public interface IConcertRepository
    {
        Task<Concert?> GetByIdAsync(int id);

        Task<(List<Concert> Concerts, int TotalCount)> GetPagedAsync(
            string? search,
            int pageNumber,
            int pageSize);

        Task AddAsync(Concert concert);

        void Update(Concert concert);

        void Delete(Concert concert);

        Task SaveChangesAsync();
    }
}
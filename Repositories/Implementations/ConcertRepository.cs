using ConcertTicketBooking.Data;
using ConcertTicketBooking.Models;
using ConcertTicketBooking.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConcertTicketBooking.Repositories.Implementations
{
    public class ConcertRepository : IConcertRepository
    {
        private readonly ApplicationDbContext _context;

        // Constructor injection
        public ConcertRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get one concert by ID
        public async Task<Concert?> GetByIdAsync(int id)
        {
            return await _context.Concerts
                .FirstOrDefaultAsync(c => c.ConcertId == id);
        }

        // Search and paginate concerts
        public async Task<(List<Concert> Concerts, int TotalCount)>
            GetPagedAsync(string? search, int pageNumber, int pageSize)
        {
            IQueryable<Concert> query = _context.Concerts.AsNoTracking();

            // Apply search only when a search term is provided
            if (!string.IsNullOrWhiteSpace(search))
            {
                string searchTerm = search.Trim();

                query = query.Where(c =>
                    c.ConcertName.Contains(searchTerm) ||
                    c.ArtistName.Contains(searchTerm) ||
                    c.Genre.Contains(searchTerm));
            }

            // Count matching records before pagination
            int totalCount = await query.CountAsync();

            // Retrieve only the requested page
            List<Concert> concerts = await query
                .OrderByDescending(c => c.CreatedAtUtc)
                .ThenByDescending(c => c.ConcertId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (concerts, totalCount);
        }

        // Insert a new concert
        public async Task AddAsync(Concert concert)
        {
            await _context.Concerts.AddAsync(concert);
        }

        // Mark entity as modified
        public void Update(Concert concert)
        {
            _context.Concerts.Update(concert);
        }

        // Mark entity for deletion
        public void Delete(Concert concert)
        {
            _context.Concerts.Remove(concert);
        }

        // Save pending changes to SQL Server
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}